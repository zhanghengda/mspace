using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Utils;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FireControlModule
{
    public class ThreeColorStatics
    {
        private readonly string RedTag = "red";
        private readonly string YellowTag = "yellow";
        private readonly string GreenTag = "green";
        private List<IRenderable> _rObjs = new List<IRenderable>();
        private List<string> _fids = new List<string>();
        private Dictionary<string, List<string>> _threeFids = new Dictionary<string, List<string>>();

        private IDisplayLayer _buildLyr;

        /// <summary>
        /// 三色预警
        /// </summary>
        /// <param name="StaticsInfos">
        /// Dictionary<string, Tuple<string, string,string>  buildCode,type,hight,buildName
        /// </param>
        public void ShowThreeColorStatics(Dictionary<string, Tuple<string, string, string>> StaticsInfos)
        {
            CloseThreeColorStatics();
            var layers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
            layers.ForEach(p =>
            {
                if (p.Fc.Alias == "建筑")
                {
                    bool isFly = false;
                    //先清除高亮
                    p.UnHighLightFeatures(_fids.ToArray(), GviMap.FeatureManager);
                    _fids.Clear();
                    var fc = p.Fc;
                    var filter = new QueryFilter();
                    filter.WhereClause = GviSql.CreateInSql("Name", StaticsInfos.Keys.ToArray<string>(), true);
                    var cursor = fc.Search(filter, false);
                    var indexName = fc.GetFields().IndexOf("Name");
                    var indexGeometry = fc.GetFields().IndexOf("Geometry");
                    IRowBuffer row = null;
                    while ((row = cursor.NextRow()) != null)
                    {
                        var buildCode = row.GetValue<string>(indexName);
                        var geo = row.GetValue<IModelPoint>(indexGeometry);
                        var fid = row.GetFid().ParseTo<string>();
                        _fids.Add(fid);
                        var dic = StaticsInfos[buildCode];
                        var hight = double.Parse(dic.Item2);
                        var type = dic.Item1;
                        var buildName = dic.Item3;

                        //设置poi位置、高度,大小，名称
                        var point = geo.Position;
                        point.Z = point.Z + hight + 2;
                        var poi = (IPOI)GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ ) as IPOI;
                        poi.Position = point;
                        poi.Name = buildName;
                        poi.ShowName = false;
                        poi.Size = 48;
                        string imgName = AppDomain.CurrentDomain.BaseDirectory + "\\data\\poi\\红色预警.png";

                        //第一个飞入
                        if (!isFly)
                        {
                            // GviMap.Camera.FlyToEnvelope(geo.Envelope);
                        }

                        if (type == "0")
                        {
                            p.HighLightFeature(fid, GviMap.FeatureManager, GviColors.OrangeRed);
                            imgName = AppDomain.CurrentDomain.BaseDirectory + "\\data\\poi\\红色预警.png";
                        }
                        else if (type == "1")
                        {
                            p.HighLightFeature(fid, GviMap.FeatureManager, GviColors.Orange);
                            imgName = AppDomain.CurrentDomain.BaseDirectory + "\\data\\poi\\黄色预警.png";
                        }
                        else if (type == "2")
                        {
                            p.HighLightFeature(fid, GviMap.FeatureManager, GviColors.Green);
                            imgName = AppDomain.CurrentDomain.BaseDirectory + "\\data\\poi\\绿色预警.png";
                        }
                        poi.ImageName = imgName;
                        var label = GviMap.ObjectManager.CreateRenderPOI(poi);
                        label.ViewingDistance = 300;
                        label.MaxVisibleDistance = 300;
                        _rObjs.Add(label);
                        /// p.FlyToFeature(fid, GviMap.Camera);
                        row.ReleaseComObject();
                    }
                    cursor.ReleaseComObject();
                    filter.ReleaseComObject();
                }
            });
        }

        public void SetColorVisible(string color, string visible)
        {
            bool isVisible = visible.ParseTo<bool>();
            if (color == RedTag)
            {
                if (isVisible)
                    _buildLyr.HighLightFeatures(_threeFids[RedTag].ToArray(), GviMap.FeatureManager, GviColors.OrangeRed);
                else
                    _buildLyr.UnHighLightFeatures(_threeFids[RedTag].ToArray(), GviMap.FeatureManager);
            }
            else if (color == YellowTag)
            {
                if (isVisible)
                    _buildLyr.HighLightFeatures(_threeFids[YellowTag].ToArray(), GviMap.FeatureManager, GviColors.Orange);
                else
                    _buildLyr.UnHighLightFeatures(_threeFids[YellowTag].ToArray(), GviMap.FeatureManager);
            }
            else if (color == GreenTag)
            {
                if (isVisible)
                    _buildLyr.HighLightFeatures(_threeFids[GreenTag].ToArray(), GviMap.FeatureManager, GviColors.Green);
                else
                    _buildLyr.UnHighLightFeatures(_threeFids[GreenTag].ToArray(), GviMap.FeatureManager);
            }
        }

        /// <summary>
        /// 三色预警
        /// </summary>
        /// <param name="StaticsInfos">
        /// Dictionary<string,string>  buildCode,type
        /// </param>
        public void ShowThreeColorStatics(Dictionary<string, string> StaticsInfos)
        {
            CloseThreeColorStatics();
            _buildLyr = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCAliasName("建筑");
            var layers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
            layers.ForEach(p =>
            {
                if (p.Fc.Alias == "建筑")
                {
                    _buildLyr = p;
                    Random random = new Random();

                    _threeFids.Add(RedTag, new List<string>());
                    _threeFids.Add(YellowTag, new List<string>());
                    _threeFids.Add(GreenTag, new List<string>());
                    //先清除高亮
                    p.UnHighLightFeatures(_fids.ToArray(), GviMap.FeatureManager);
                    _fids.Clear();
                    var fc = p.Fc;
                    var filter = new QueryFilter();
                    filter.AddSubField("oid");
                    //filter.WhereClause = GviSql.CreateInSql("Name", StaticsInfos.Keys.ToArray<string>(), true);
                    var cursor = fc.Search(filter, false);
                    //var indexName = fc.GetFields().IndexOf("Name");
                    IRowBuffer row = null;
                    while ((row = cursor.NextRow()) != null)
                    {
                        //var buildCode = row.GetValue<string>(indexName);
                        // var geo = row.GetValue<IModelPoint>(indexGeometry);
                        var fid = row.GetFid().ParseTo<string>();
                        _fids.Add(fid);
                        var type = random.Next(1, 4).ToString();
                        //if (!StaticsInfos.ContainsKey(fid))
                        //{
                        //    row.ReleaseComObject();
                        //    continue;
                        //}
                        //var type = StaticsInfos[fid].Trim();

                        if (type == "3")
                        {
                            p.HighLightFeature(fid, GviMap.FeatureManager, GviColors.OrangeRed);
                            _threeFids[RedTag].Add(fid);
                        }
                        else if (type == "1")
                        {
                            p.HighLightFeature(fid, GviMap.FeatureManager, GviColors.Orange);
                            _threeFids[YellowTag].Add(fid);
                        }
                        else if (type == "2")
                        {
                            p.HighLightFeature(fid, GviMap.FeatureManager, GviColors.Green);
                            _threeFids[GreenTag].Add(fid);
                        }
                        /// p.FlyToFeature(fid, GviMap.Camera);
                        row.ReleaseComObject();
                    }
                    cursor.ReleaseComObject();
                    filter.ReleaseComObject();
                }
            });
        }

        public void CloseThreeColorStatics()
        {
            GviMap.ObjectManager.ReleaseRenderObject(_rObjs.ToArray());
            _threeFids.Clear();
            var layers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
            layers.ForEach(p =>
            {
                if (p.Fc.Alias == "建筑")
                {
                    //先清除高亮
                    p.UnHighLightFeatures(_fids.ToArray(), GviMap.FeatureManager);
                    _fids.Clear();
                }
            });
        }

        public void TestShowColor1()
        {
            //            4403050080070900009
            //4403050080070800159
            //4403050080070900158
            Dictionary<string, Tuple<string, string, string>> dics = new Dictionary<string, Tuple<string, string, string>>();

            dics.Add("4403050080070900009", new Tuple<string, string, string>("0", "28", "xxx"));
            dics.Add("4403050080070800159", new Tuple<string, string, string>("1", "28", "xxx"));
            dics.Add("4403050080070800158", new Tuple<string, string, string>("2", "28", "xxx"));

            ShowThreeColorStatics(dics);
        }

        public void TestShowColor()
        {
            try
            {
                Dictionary<string, string> dics = new Dictionary<string, string>();
                var fileName = AppDomain.CurrentDomain.BaseDirectory + @"data\json\threeColor.json";
                var json = JsonUtil.DeserializeFromFile<dynamic>(fileName);
                int index = 0;
                foreach (var item in json)
                {
                    //if (index == 999)
                    //    break;
                    string key = item.oid.Value;
                    key = key.Trim();
                    if (!dics.Keys.Contains(key))
                        dics.Add(key, item.type.Value);
                    index++;
                }
                ShowThreeColorStatics(dics);
            }
            catch (Exception ex)
            {
                SystemLog.Log("三色预警失败");
                SystemLog.Log(ex);
            }
        }
    }
}