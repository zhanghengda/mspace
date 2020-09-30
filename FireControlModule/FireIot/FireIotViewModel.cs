using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;

namespace FireControlModule.FireIot
{
    public class FireIotViewModel : CheckedToolItemModel
    {
        private IDisplayLayer _dLyr;
        private List<IRenderable> _rObjs;
        private FireIotSevice _fireService;

        //建筑名称，总设备数，安全指数，正常设备数，异常设备数
        private Dictionary<string, Tuple<string, string, string, string, string>> _fireIotAbouts;

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            _rObjs = new List<IRenderable>();
            _fireIotAbouts = new Dictionary<string, Tuple<string, string, string, string, string>>();
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void OnChecked()
        {
            base.OnChecked();
            StaticCamera.SetCamera(x: 496915.17, y: 2496100.37, height: 243, tilt: -45);
            if (_fireService == null)
                _fireService = new FireIotSevice();
            if (_dLyr == null)
            {
                var actaulLyrs = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
                //建筑参与拾取
                actaulLyrs.ForEach(p => { if (p.Fc.Alias == "建筑") _dLyr = p; });
            }

            List<string> buildCodes = new List<string>();
            foreach (var item in _fireService.DicNewBuildInfos.Keys)
                buildCodes.Add(item);
            var filter = new QueryFilter();
            filter.WhereClause = Gvitech.CityMaker.Utils.GviSql.CreateInSql("Name", buildCodes.ToArray(), true);
            filter.AddSubField("geometry");
            filter.AddSubField("Name");
            var cr = _dLyr.Fc.Search(filter, true);
            IRowBuffer row = null;
            while ((row = cr.NextRow()) != null)
            {
                var geo = row.GetValue<IModelPoint>(0);
                var pt = geo.Envelope.Center;
                pt.Z = geo.Envelope.MaxZ;
                var buildCode = row.GetValue<string>(1);
                if (!_fireIotAbouts.ContainsKey(buildCode))
                {
                    var fireInfos = _fireService.GetFireIotByBuildCode(buildCode);
                    if (fireInfos == null || fireInfos.Count == 0)
                        continue;
                    var total = fireInfos.Count;
                    int problemCount = 0;
                    fireInfos.ForEach(p => { if (p.status == "1") problemCount++; });
                    var healthCount = total - problemCount;
                    var healthIndex = Math.Round(healthCount / (total + 0.0) * 100, 2) + "%";
                    _fireIotAbouts.Add(buildCode, new Tuple<string, string, string, string, string>(_fireService.DicNewBuildInfos[buildCode].Item2, total.ToString(), healthIndex, healthCount.ToString(), problemCount.ToString()));
                }

                if (_fireIotAbouts.ContainsKey(buildCode))
                {
                    var tableLabel = TableLabelFactory.CreateTableLabel(GviMap.ObjectManager);
                    tableLabel.Position = pt.ToPoint(GviMap.GeoFactory, GviMap.SpatialCrs);
                    // 设定表头文字
                    tableLabel.TitleText = _fireIotAbouts[buildCode].Item1;
                    // 设定表格中第1行，第1列的显示文字
                    tableLabel.SetRecord(0, 0, "总设备数");
                    // 第1行，第2列
                    tableLabel.SetRecord(0, 1, _fireIotAbouts[buildCode].Item2);
                    // 第2行，第1列
                    tableLabel.SetRecord(1, 0, "安全指数");
                    // 第2行，第2列
                    tableLabel.SetRecord(1, 1, _fireIotAbouts[buildCode].Item3);
                    // 第3行，第1列
                    tableLabel.SetRecord(2, 0, "正常设备数");
                    // 第3行，第2列
                    tableLabel.SetRecord(2, 1, _fireIotAbouts[buildCode].Item4);
                    // 第4行，第1列
                    tableLabel.SetRecord(3, 0, "异常设备数");
                    // 第4行，第2列
                    tableLabel.SetRecord(3, 1, _fireIotAbouts[buildCode].Item5);
                    // 设置为可见
                    tableLabel.VisibleMask = gviViewportMask.gviView0;
                    _rObjs.Add(tableLabel);
                }
            }
            cr.ReleaseComObject();
            filter.ReleaseComObject();
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            GviMap.ObjectManager.ReleaseRenderObject(_rObjs.ToArray());
            _rObjs.Clear();
        }
    }

    public class TableLabelFactory
    {
        public static ITableLabel CreateTableLabel(IObjectManager objManager)
        {
            // 创建一个有3行2列的TableLabel
            var tableLabel = objManager.CreateTableLabel(4, 2);
            // 设置不可见
            tableLabel.VisibleMask = gviViewportMask.gviViewNone;

            ////标牌的位置
            //position.Set(fifthX, fifthY, fifthZ);
            //tableLabel.Position = position;

            // 列宽度
            tableLabel.SetColumnWidth(0, 90);
            tableLabel.SetColumnWidth(1, 50);

            // 表的边框颜色
            tableLabel.BorderColor = ColorConvert.UintToColor(0xffffffff);
            // 表的边框的宽度
            tableLabel.BorderWidth = 2;
            // 表的背景色
            tableLabel.TableBackgroundColor = ColorConvert.UintToColor(4290707456);

            // 标题背景色
            tableLabel.TitleBackgroundColor = ColorConvert.UintToColor(0xff000000);

            // 第一列文本样式
            TextAttribute headerTextAttribute = new TextAttribute();
            headerTextAttribute.TextColor = ColorConvert.UintToColor(0xffffffff);
            headerTextAttribute.OutlineColor = ColorConvert.UintToColor(0xff000000);
            headerTextAttribute.Font = "微软雅黑";
            headerTextAttribute.Bold = true;
            headerTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.SetColumnTextAttribute(0, headerTextAttribute);

            // 第二列文本样式
            TextAttribute contentTextAttribute = new TextAttribute();
            contentTextAttribute.TextColor = ColorConvert.UintToColor(4293256677);
            contentTextAttribute.OutlineColor = ColorConvert.UintToColor(0xff000000);
            contentTextAttribute.Font = "微软雅黑";
            contentTextAttribute.Bold = false;
            contentTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.SetColumnTextAttribute(1, contentTextAttribute);

            // 标题文本样式
            TextAttribute capitalTextAttribute = new TextAttribute();
            capitalTextAttribute.TextColor = ColorConvert.UintToColor(0xffffffff);
            capitalTextAttribute.OutlineColor = ColorConvert.UintToColor(4279834905);
            capitalTextAttribute.Font = "微软雅黑";
            capitalTextAttribute.TextSize = 14;
            capitalTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
            capitalTextAttribute.Bold = true;
            tableLabel.TitleTextAttribute = capitalTextAttribute;

            return tableLabel;
        }

        public static ITableLabel CreateWindTable(IObjectManager objManager, int row, int column)
        {
            // 创建一个有3行2列的TableLabel
            var tableLabel = objManager.CreateTableLabel(row, column);
            // 设置不可见
            tableLabel.VisibleMask = gviViewportMask.gviViewNone;
            ////标牌的位置
            //position.Set(fifthX, fifthY, fifthZ);
            //tableLabel.Position = position;

            // 列宽度

            tableLabel.SetColumnWidth(0, 50);
            tableLabel.SetColumnWidth(1, 80);

            // 表的边框颜色
            tableLabel.BorderColor = ColorConvert.UintToColor(0xff1E2026);

            // 表的边框的宽度
            tableLabel.BorderWidth = 2;
            // 表的背景色
            tableLabel.TableBackgroundColor = ColorConvert.UintToColor(0xff1E2026);

            // 标题背景色
            tableLabel.TitleBackgroundColor = ColorConvert.UintToColor(0xff292C35);

            // 第一列文本样式
            TextAttribute headerTextAttribute = new TextAttribute();
            headerTextAttribute.TextColor = ColorConvert.UintToColor(0xffffffff);
            headerTextAttribute.OutlineColor = ColorConvert.UintToColor(0xff000000);
            headerTextAttribute.Font = "黑体";
            headerTextAttribute.Bold = false;
            headerTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.SetColumnTextAttribute(0, headerTextAttribute);

            // 第二列文本样式
            TextAttribute contentTextAttribute = new TextAttribute();
            contentTextAttribute.TextColor = ColorConvert.UintToColor(0xffffffff);
            contentTextAttribute.OutlineColor = ColorConvert.UintToColor(0xff000000);
            contentTextAttribute.Font = "黑体";
            contentTextAttribute.Bold = false;
            contentTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.SetColumnTextAttribute(1, contentTextAttribute);

            // 标题文本样式
            TextAttribute capitalTextAttribute = new TextAttribute();
            capitalTextAttribute.OutlineColor = ColorConvert.UintToColor(0xff000000);
            capitalTextAttribute.Font = "黑体";
            capitalTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
            capitalTextAttribute.Bold = false;
            tableLabel.TitleTextAttribute = capitalTextAttribute;

            tableLabel.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
            return tableLabel;
        }
    }
}