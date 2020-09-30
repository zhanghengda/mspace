using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    class QueryWktGroupVModel : CheckedToolItemModel
    {
        public Action freshMainComBox;
        public Action ClearEnvopleDic;
        public QueryWktGroupView queryWktGroupView = new QueryWktGroupView();
        public ICommand StartDrawCmd { get; set; }
        public ICommand AddDrawCmd { get; set; }
        public ICommand DeleteLastCmd { get; set; }
        public ICommand SaveAllCmd { get; set; }
        public ICommand CloseCmd { get; set; }
       //public Action FreshCollection;
        private DrawCustomerUC poidrawCustomer;       
        List<IRenderPolygon> rPolygonList = new List<IRenderPolygon>();
        List<string> wktList = new List<string>();
        protected bool AreaSelectFlag = false;
        private string _geom;
        public QueryWktGroupVModel(QueryEnvelopeManagementVModel queryEnvelopeManagementVModel)
        {
            queryWktGroupView.DataContext = this;
            this.CloseCmd = new RelayCommand(() =>
            {
                DeleteAllTempObj();
                queryWktGroupView.Hide();
            });
            this.StartDrawCmd = new RelayCommand(() =>
            {
                DeleteAllTempObj();
                wktList?.Clear();
                ChangeListCountNum(wktList);
                RegisterDraw();
            });
            
            this.AddDrawCmd = new RelayCommand(() =>
            {
                RegisterDraw();
            });
            this.DeleteLastCmd = new RelayCommand(() =>
            {
                if (rPolygonList?.Count != 0)
                {
                    var itemObject = rPolygonList[rPolygonList.Count - 1] as IRObject;                    
                    GviMap.ObjectManager.DeleteObject(itemObject.Guid);
                    rPolygonList.Remove(rPolygonList[rPolygonList.Count - 1]);
                    wktList.Remove(wktList[wktList.Count - 1]);
                    queryWktGroupView.listCountNum.Text = Convert.ToString(wktList.Count);
                    freshMainComBox();
                }            
            });
            this.SaveAllCmd = new RelayCommand(() =>
            {
                bool save = SaveData();
                if(save)
                {
                    DeleteAllTempObj();
                    queryWktGroupView.Hide();
                    queryEnvelopeManagementVModel.OnEndPage();
                    freshMainComBox();
                }                
            });

        }
        private string _areaGroupName="";
        public string AreaGroupName
        {
            get { return this._areaGroupName; }
            set { _areaGroupName = value; NotifyPropertyChanged("AreaGroupName"); }
        }

       
        private void Rone_PolygonDraw_OnDrawFinished(object sender, object result)
        {
            var polygon = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                         gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            polygon.SpatialCRS = GviMap.SpatialCrs;
            var render = GviMap.ObjectManager.CreateRenderPolygon(polygon, GviMap.LinePolyManager.SurfaceSym,
                GviMap.ProjectTree.RootID);           
            try
            {
                //if (!AreaSelectFlag)
                //{
                //    return;
                //}
                var rPolygon = result as IRenderPolygon;
                polygon = rPolygon.GetFdeGeometry() as IPolygon;
                polygon.SpatialCRS = GviMap.SpatialCrs;

                if (polygon == null || polygon.ExteriorRing.PointCount < 4)
                {
                    RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= Rone_PolygonDraw_OnDrawFinished;
                    RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
                    return;
                }
                try
                {
                    var rpolygon1 = render;// GviMap.TempRObjectPool[AreaMarkerKey] as IRenderPolygon;
                    rpolygon1?.SetFdeGeometry(polygon);
                    rPolygon.Symbol.BoundarySymbol.Color = Color.FromArgb(255, 0, 255, 255);
                    rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;
                    // GviMap.TempRObjectPool[AreaMarkerKey] = rpolygon1;                    
                    polygon = rpolygon1.GetFdeGeometry() as IPolygon;
                    if (rpolygon1 != null)
                    {
                        rPolygonList.Add(rpolygon1);
                    }                   
                    polygon.SpatialCRS = GviMap.SpatialCrs;
                    var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                    string _poiHost = json.poiUrl;
                    string wkt_poly = polygon.AsWKT();
                    _geom = wkt_poly;
                    wktList.Add(_geom);
                    ChangeListCountNum(wktList);
                    // AreaIsSelected = true;
                    AreaPoiSelectedModel areaPoiSelected = new AreaPoiSelectedModel();
                    areaPoiSelected.AreaSelectedPolygon = rpolygon1;
                    areaPoiSelected.WktPoly = wkt_poly;
                    // WktPoly = wkt_poly;
                    // AreaPoiDic.Clear();
                    // AreaPoiDic.Add(AreaIsSelected, areaPoiSelected);
                    RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= Rone_PolygonDraw_OnDrawFinished;
                    RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
                    //AreaSelectFlag = false;
                }
                catch (Exception e)
                {
                    SystemLog.Log(e);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
       
        private void RegisterDraw()
        {
            try
            {
                if (poidrawCustomer == null)
                {
                    poidrawCustomer = new DrawCustomerUC(Helpers.ResourceHelper.FindKey("AreaMarkerKey"),
                        DrawCustomerType.MenuCommand);
                    //注册绘制多边形事件
                }
                //CreateTempRObj();
                RCDrawManager.Instance.PolygonDraw.Register(GviMap.AxMapControl, poidrawCustomer, RCMouseOperType.PickPoint);
                RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= Rone_PolygonDraw_OnDrawFinished;
                RCDrawManager.Instance.PolygonDraw.OnDrawFinished += Rone_PolygonDraw_OnDrawFinished;

            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
       

        private void ChangeListCountNum(List<string> _wktList)
        {
            if (_wktList != null)
            {
                queryWktGroupView.listCountNum.Text = Convert.ToString(_wktList.Count);
            }
            else
            {
                queryWktGroupView.listCountNum.Text ="0";
            }
        }

        private void DeleteAllTempObj()
        {
            if(rPolygonList?.Count!=0)
            {
                foreach(var item in rPolygonList)
                {
                    GviMap.ObjectManager.DeleteObject(item.Guid);                   
                }
            }
            rPolygonList?.Clear();
        }      
        private bool SaveData()
        {
            if(wktList?.Count!=0)
            {
                if (AreaGroupName != null)
                {
                    string url = MarkInterface.AddQueryWktGroup;
                    var jsonObj = new JObject();
                    var array = new JArray();
                    for (int i =0;i<wktList.Count;i++)
                    {
                        var obj = new JObject { { "id", "content" } } ;
                        obj["id"] = i;
                        obj["content"] = wktList[i];
                        array.Add(obj);
                    }
                    jsonObj["content"] = array;
                    jsonObj["name"] = AreaGroupName;
                    string json = jsonObj.ToString();                   
                    string resStr = HttpServiceHelper.Instance.PostRequestForData(url, json);
                    if (resStr != null && resStr != "")
                    {
                        try
                        {
                            JsonTextReader reader = new JsonTextReader(new StringReader(resStr));
                            JObject jObject = (JObject)JToken.ReadFrom(reader);
                            if (jObject["status"].ToString() == "1")
                            {
                                wktList.Clear();
                                queryWktGroupView.listCountNum.Text = "0";
                                AreaGroupName = "";
                                ClearEnvopleDic();
                                Messages.ShowMessage("新增成功");                                
                                return true;
                            }
                        }
                        catch(Exception e)
                        {
                            SystemLog.WriteLog(e.ToString());
                        }
                    }
                    else
                    {
                        Messages.ShowMessage("新增失败，该名称已存在!");
                    }
                }
                else
                {
                    Messages.ShowMessage("区域组名称不能为空");
                    
                }
            }
            else
            {
                Messages.ShowMessage("尚未绘制区域");
                
            }
            return false;
        }
    }
}
