using FireControlModule.FireIot;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule
{
   public class TempAndHumViewModel: CheckedToolItemModel
    {
        private List<IRenderable> _renderableList;
        private Dictionary<string, ITableLabel> _tableLabelList;
        private System.Timers.Timer timer = new System.Timers.Timer(3000);//实例化Timer类
        private readonly object SynObject = new object();
        private Dictionary<string, DeviceInfoModel> _tempInfoList;
        private Dictionary<string, DeviceInfoModel> _humInfoList;
        private CreatePoiManager _poiManager;
        private DeviceDataManager _deviceManager;
        private Dictionary<string, IRenderPOI> _rPoiDic;
        private static bool ShowState = false;

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            this._renderableList = new List<IRenderable>();
            this._tableLabelList = new Dictionary<string, ITableLabel>();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(theout);
            //this.Command = new RelayCommand(GetSource);
            _tempInfoList = new Dictionary<string, DeviceInfoModel>();
            _humInfoList = new Dictionary<string, DeviceInfoModel>();
            _poiManager = new CreatePoiManager();
            _deviceManager = new DeviceDataManager();
            _rPoiDic = new Dictionary<string, IRenderPOI>();
        }

        public override void OnChecked()
        {
            base.OnChecked();
            ShowData();
        }


        public override void OnUnchecked()
        {
            base.OnUnchecked();
            HiddenData();
        }
        //private void GetSource()
        //{
        //    if (ShowState)
        //        HiddenData();
        //    else
        //        ShowData();
        //}
        private void ShowData()
        {
            IsSelected = true;
            ShowState = true;
            Task.Run(() => {
                try
                {
                    try
                    {
                        _deviceManager.GetDeviceInfoList("temp", ref _tempInfoList);
                        _deviceManager.GetDeviceInfoList("humidity", ref _humInfoList);
                    }
                    catch { }

                    foreach (var item in _tempInfoList.ToArray())
                    {
                        var rPoi = _poiManager.CreatePoi(item.Value, "温湿度");
                        _rPoiDic.Add(item.Key, rPoi);
                        if (Convert.ToDouble(item.Value.datainfo) > 29.5)
                            _poiManager.ChangePoiImage(rPoi, "温湿度Warning");
                        _renderableList.Add(rPoi);
                        var point = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        point.X = item.Value.longitude;
                        point.Y = item.Value.latitude;
                        point.SpatialCRS = GviMap.SpatialCrs;
                        var table = createTableLable(point, item.Value.device_name);
                        table.SetRecord(0, 1, item.Value.datainfo + " ℃");
                        table.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        _tableLabelList.Add(item.Key, table);
                        _renderableList.Add(table);
                    }
                    foreach (var item in _humInfoList.ToArray())
                    {
                        if (this._tableLabelList.ContainsKey(item.Key))
                            this._tableLabelList[item.Key].SetRecord(1, 1, item.Value.datainfo + " %rh");
                    }
                    timer.Start();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.StackTrace);
                }
            });
            
        }

        private void HiddenData()
        {
            IsSelected = false;
            ShowState = false;
            timer.Stop();
            _tableLabelList?.Clear();
            GviMap.ObjectManager.ReleaseRenderObject(_renderableList?.ToArray());
            _renderableList?.Clear();
            _tempInfoList?.Clear();
            _humInfoList?.Clear();
            _rPoiDic?.Clear();
        }

        //public override void OnChecked()
        //{
        //    base.OnChecked();
        //}

        private void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            lock (SynObject)
            {
                Task.Run(() =>
                {
                    try
                    {
                        _deviceManager.GetDeviceInfoList("temp", ref _tempInfoList);
                        _deviceManager.GetDeviceInfoList("humidity", ref _humInfoList);
                    }
                    catch { }

                    foreach (var item in _tempInfoList.ToArray())
                    {
                        if (this._rPoiDic.ContainsKey(item.Key))
                            if (Convert.ToDouble(item.Value.datainfo) > 29.5)
                                _poiManager.ChangePoiImage(this._rPoiDic[item.Key], "温湿度Warning");
                            else
                                _poiManager.ChangePoiImage(this._rPoiDic[item.Key], "温湿度");
                        if (this._tableLabelList.ContainsKey(item.Key))
                            this._tableLabelList[item.Key].SetRecord(0, 1, item.Value.datainfo + " ℃");
                    }
                    foreach (var item in _humInfoList.ToArray())
                        if (this._tableLabelList.ContainsKey(item.Key))
                            this._tableLabelList[item.Key].SetRecord(1, 1, item.Value.datainfo + " %rh");
                });
            }
        }
        
        private ITableLabel createTableLable(IPoint pt ,string device_name)
        {
            var tableLabel = TableLabelFactory.CreateWindTable(GviMap.ObjectManager,2,2);
            tableLabel.Position = pt;
            tableLabel.VisibleMask = gviViewportMask.gviViewAllNormalView;
            tableLabel.TitleText = device_name;
            // 设定表格中第1行，第1列的显示文字
            tableLabel.SetRecord(0, 0, "温度:");
            // 第1行，第2列
            tableLabel.SetRecord(0, 1, "0" + "  ℃");
            // 第2行，第1列
            tableLabel.SetRecord(1, 0, "湿度:");
            // 第2行，第2列
            tableLabel.SetRecord(1, 1, " %rh");

            return tableLabel;
        }

        //public override void OnUnchecked()
        //{
        //    base.OnUnchecked();
        //}

        public override void Reset()
        {
            base.Reset();
            base.IsChecked = false;
        }

    }
}
