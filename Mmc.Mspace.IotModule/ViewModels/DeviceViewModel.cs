using FireControlModule.FireIot;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.IotModule.Models;
using Mmc.Mspace.IotModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Mmc.Mspace.IotModule.ViewModels
{
    public class DeviceViewModel : CheckedToolItemModel
    {

        private DeviceView deviceView = new DeviceView();
        private CameraDetailView cameraDetailView = new CameraDetailView();

        public Dictionary<string, Guid> eventList = new Dictionary<string, Guid>();
        public Dictionary<string, string> eventStatusguid = new Dictionary<string, string>();
        public Dictionary<string, Guid> eventTableList = new Dictionary<string, Guid>();
        public Dictionary<string, Guid> eventTableList1 = new Dictionary<string, Guid>();
        public CameraModel firstmodel = null;
        private CameraModel _cameraModel;
        private List<IRObject> _gviTmpObjList;
        private ISimplePointSymbol _beginPointSymbol;
        private List<CameraModel> _menuItems = new List<CameraModel>();


        private List<CameraModel> _DeviceTypes;

        public List<CameraModel> DeviceTypes
        {
            get { return _DeviceTypes; }
            set { _DeviceTypes = value; NotifyPropertyChanged("DeviceTypes"); }
        }

        public CameraModel CameraModel
        {
            get
            {
                return this._cameraModel;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<CameraModel>(ref this._cameraModel, value, "CameraModel");
            }
        }
        public List<CameraModel> MenuItems
        {
            get
            {
                return this._menuItems;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<CameraModel>>(ref this._menuItems, value, "MenuItems");
            }
        }
        public ICommand CloseCmd
        {
            get;
            set;
        }
        public ICommand CloseDetailCmd
        {
            get;
            set;
        }
        public ICommand SelectAllCmd
        {
            get;
            set;
        }
        public ICommand UnSelectAllCmd
        {
            get;
            set;
        }
        public ICommand ConfirmCmd
        {
            get;
            set;
        }
        public ICommand SelectTagCommand
        {
            get;
            set;
        }
        public ICommand CheckedCommand
        {
            get;
            set;
        }
        

        private List<CameraModel> _cameraItems;
        public List<CameraModel> CameraItems
        {
            get
            {
                return _cameraItems;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<CameraModel>>(ref this._cameraItems, value, "CameraItems");
            }
        }
        public void GetStreetSign()
        {
            string request = Singleton<HttpServiceHelper>.Instance.GetRequest(GridPatrolInterface.GetDhcamera);
            this.CameraItems = JsonUtil.DeserializeFromString<List<CameraModel>>(request);

            this.DeviceTypes = new List<CameraModel>
            {
                 new CameraModel
                {
                    name = "摄像头类型",
                    IsSelected = false
                },
                new CameraModel
                {
                    name = "安装方式",
                    IsSelected = false
                },
                new CameraModel
                {
                    name = "区域",
                    IsSelected = false
                },
                new CameraModel
                {
                    name = "图层类型",
                    IsSelected = false
                },
            };
            this.MenuItems = new List<CameraModel>
            {
              
            };
        }

        public DeviceViewModel()
        {
            this.SelectAllCmd = new RelayCommand(new Action(this.OnSelectAll));
            this.UnSelectAllCmd = new RelayCommand(new Action(this.OnUnSelectAll));
            this.SelectTagCommand = new RelayCommand<CameraModel>(this.OnSelectTagCommand);
            this.CheckedCommand = new RelayCommand<CameraModel>(this.OnCheckedCommand);


            
            this.CloseDetailCmd = new RelayCommand(()=>
            {
                this.cameraDetailView.Hide();
            });
            Messenger.Messengers.Register<bool>("showCamera", (r)=>
            {
                this.ShowView();
            });
            Messenger.Messengers.Register<bool>("undeviceview", (r) =>
            {
                base.IsChecked = false;
            });
        }
        private void OnCheckedCommand(CameraModel obj)
        {
            if (obj == null) return;
            List<CameraModel> list = new List<CameraModel>();
            var selectes = this.MenuItems.Where(t => t.IsSelected).ToList();

            if (obj.name == "摄像头类型")
            {
                for (int i = 0; i < selectes.Count; i++)
                {
                    var newlist = this.CameraItems.Where(t => t.category_id == selectes[i].showname ).ToList();
                    list.AddRange(newlist); 
                }
            }
            else if (obj.name == "安装方式")
            {
                for (int i = 0; i < selectes.Count; i++)
                {
                    var newlist = this.CameraItems.Where(t => t.installation_method == selectes[i].showname).ToList();
                    list.AddRange(newlist);
                }
            }
            else if (obj.name == "区域")
            {
                for (int i = 0; i < selectes.Count; i++)
                {
                    var newlist = this.CameraItems.Where(t => t.high_low == selectes[i].showname).ToList();
                    list.AddRange(newlist);
                }
            }
            else if (obj.name == "图层类型")
            {
                for (int i = 0; i < selectes.Count; i++)
                {
                    var newlist = this.CameraItems.Where(t => t.type == selectes[i].showname).ToList();
                    list.AddRange(newlist);
                }
            }
            this.OnConfirmCmd(list);
        }
        private void OnSelectTagCommand(CameraModel obj)
        {
            this.cameraDetailView.Hide();
            this.ClearRender();
            this._gviTmpObjList = new List<IRObject>();
            this._beginPointSymbol = null;
            this.firstmodel = null;
            if (obj == null) return;

            this.DeviceTypes.ForEach(t => t.IsSelected = false);
            if(obj.name=="摄像头类型")
            {
                this.MenuItems = new List<CameraModel>()
                {
                     new CameraModel
                {
                    showname = "高空瞭望全景摄像机",
                    name = "摄像头类型",
                    IsSelected = false
                },
                new CameraModel
                {
                    showname = "低空瞭望全景摄像机",
                    name = "摄像头类型",
                    IsSelected = false
                },
                };
            }
            else if(obj.name == "安装方式")
            {
                this.MenuItems = new List<CameraModel>()
                {
                new CameraModel
                {
                    showname = "定制支架",
                    name = "安装方式",

                    IsSelected = false
                },
                new CameraModel
                {
                    showname = "普通支架",
                    name = "安装方式",

                    IsSelected = false
                }
                };
            }
            else if(obj.name == "区域")
            {
                this.MenuItems = new List<CameraModel>()
                {
                 new CameraModel
                {
                    showname = "高点",
                    name = "区域",

                    IsSelected = false

                },
                new CameraModel
                {
                    showname = "地面",
                    name = "区域",

                    IsSelected = false
                },
                };
            }
            else if (obj.name == "图层类型")
            {
                this.MenuItems = new List<CameraModel>()
                {
                     new CameraModel
                {
                    showname = "已安装",
                    name = "图层类型",

                    IsSelected = false
                },
                new CameraModel
                {
                    showname = "预安装",
                    name = "图层类型",

                    IsSelected = false
                },
              
                };
            }
            obj.IsSelected = true;
        }
        private void MapSelectEventManager(bool ison)
        {
            if (ison)
            {
                rootId = GviMap.ObjectManager.GetProjectTree().RootID;
                GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
                GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelectGrid;
                GviMap.AxMapControl.RcMouseClickSelect += AxMapControl_RcMouseClickSelectGrid;
            }
            else
            {
                GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
                GviMap.AxMapControl.RcMouseClickSelect -=this.AxMapControl_RcMouseClickSelectGrid;
            }
        }

        IPoint point = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
        private System.Guid rootId = new System.Guid();

        private List<Thread> listThread = new List<Thread>();
        private IRenderPolygon PolygonRender { get; set; }
        Thread thread = null;
        private void AxMapControl_RcMouseClickSelectGrid(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            bool flag = IntersectPoint == null;
            if (!flag)
            {
                bool flag2 = PickResult == null;
                if (!flag2)
                {
                    bool flag3 = PickResult is IRenderPOIPickResult;
                    if (flag3)
                    {
                        IRenderPOI renderPOI = PickResult as IRenderPOI;
                        IRenderPOIPickResult renderPOIPickResult = PickResult as IRenderPOIPickResult;
                        Guid guid = renderPOIPickResult.RenderPOI.Guid;
                        bool flag4 = this.eventList.ContainsValue(guid);
                        bool flag5 = flag4;
                        if (flag5)
                        {
                            KeyValuePair<string, Guid> keys = this.eventList.FirstOrDefault((KeyValuePair<string, Guid> q) => q.Value == guid);
                            
                            this.CameraModel = this.CameraItems.Find((CameraModel t) => t.newGuid == keys.Key); 
                            if (this.CameraModel!=null)
                            {
                            
                                bool flag8 = this.cameraDetailView == null;
                                if (flag8)
                                {
                                    this.cameraDetailView = new CameraDetailView();
                                }
                                this.cameraDetailView.DataContext = this;
                                this.cameraDetailView.Left = Application.Current.MainWindow.Width * 0.3;
                                this.cameraDetailView.Top = Application.Current.MainWindow.Height * 0.2;
                                this.cameraDetailView.Owner = Application.Current.MainWindow;
                                this.cameraDetailView.Show();
                            }
                        }
                    }
                }
            }
        }
        private ISurfaceSymbol CreateDefaultSurfaceSymbol()
        {
            return new SurfaceSymbol
            {
                Color = Color.Transparent,
                BoundarySymbol = new CurveSymbol
                {
                    Color = Color.FromArgb(200, Color.Red),
                    Width = 10f
                }
            };
        }
        public IPolygon UpdateZ(IPolygon geo, double Z)
        {
            bool flag = geo == null;
            IPolygon result;
            if (flag)
            {
                result = geo;
            }
            else
            {
                geo = (geo.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPolygon);
                int num;
                for (int i = 0; i < geo.ExteriorRing.PointCount; i = num + 1)
                {
                    IPoint point = geo.ExteriorRing.GetPoint(i);
                    point.Z = Z;
                    geo.ExteriorRing.UpdatePoint(i, point);
                    num = i;
                }
                result = geo;
            }
            return result;
        }
        public IPolygon PointBuffer(IPoint pnt, double distance)
        {
            ICRSFactory icrsfactory = new CRSFactory();
            ISpatialCRS spatialCRS = icrsfactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;
            ISpatialCRS spatialCRS2 = icrsfactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;
            IPoint point = pnt.Clone() as IPoint;
            point.SpatialCRS = spatialCRS;
            IGemetryExtension.ProjectEx(point, WKTString.PROJ_CGCS2000_WKT);
            ITopologicalOperator2D topologicalOperator2D = point as ITopologicalOperator2D;
            bool flag = topologicalOperator2D == null;
            IPolygon result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IGeometry geometry = topologicalOperator2D.Buffer2D(distance, gviBufferStyle.gviBufferCapround);
                geometry.SpatialCRS = spatialCRS2;
                result = (geometry as IPolygon);
            }
            return result;
        }

        public void Rotate(IViewshed tv,CameraModel cameraModel)
        {
            IEulerAngle angle = new EulerAngle();
            try
            {
                //| blind_spot       | string | 盲区角度| 
                //| view_angle       | string | 视野角度| 
                //| initial_angle       | string | 摄像头初始角度 | 
              
                double _angle = Convert.ToDouble(cameraModel.initial_angle);
                double jiao=(360 - Convert.ToDouble(cameraModel.blind_spot)) * 0.5 - Convert.ToDouble(cameraModel.initial_angle);
                angle.Heading = 30;
                angle.Roll = 0;
                angle.Tilt = -30;
                for (int i = 0; i < 10000; i++)
                {
                    if (listThread.Count == 0) return;
                    Thread.Sleep(2000);
                    if (tv != null)
                    {
                        var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
                        shell.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            if (string.IsNullOrEmpty(cameraModel.initial_angle))
                                cameraModel.initial_angle = "0";
                            if (string.IsNullOrEmpty(cameraModel.blind_spot))
                                cameraModel.blind_spot = "0";

                            if (cameraModel.IsForward)
                            {// Convert.ToDouble(cameraModel.view_angle)
                                if ((angle.Heading +30) >= (360 - Convert.ToDouble(cameraModel.blind_spot)-jiao))
                                {
                                    cameraModel.IsForward = false;
                                    return;
                                }
                                angle.Heading = angle.Heading + 30;// Convert.ToDouble(cameraModel.view_angle);
                            }
                            else
                            {//Convert.ToDouble(cameraModel.view_angle)
                                if ((angle.Heading - 30) <= -jiao )
                                {
                                    cameraModel.IsForward = true;
                                    return;
                                }
                                angle.Heading = angle.Heading - 30;//Convert.ToDouble(cameraModel.view_angle);
                            }
                            try
                            {
                                if(tv.Position!=null)
                                tv.Angle = angle;

                            }
                            catch (Exception)
                            {

                            }
                        }));

                    }
                }

            }
            catch
            {

            }
            finally
            {
                thread = null;
                //thread.Abort();
            }

        }
        public override void Initialize()
        {
            base.ViewType = ViewType.CheckedIcon;
            base.Initialize();
            this.CloseCmd = new RelayCommand(()=>
            {
                base.IsChecked = false;
            });
        }

        public override void OnChecked()
        {
            base.OnUnchecked();
            base.IsSelected = true;
            this.MapSelectEventManager(true);
            Messenger.Messengers.Notify("showMoretool", true);
            Messenger.Messengers.Notify("unAdministrative", false);
            Messenger.Messengers.Notify("unpatrol", false);

            Messenger.Messengers.Notify("HideSummery", false);
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            base.IsSelected = false;
            Messenger.Messengers.Notify("ShowSummery", true);
            Messenger.Messengers.Notify("showMoretool", false);
            this.MapSelectEventManager(false);
            this.deviceView.Hide();
            this.cameraDetailView.Hide();

            this.ClearRender();

        }
        public void OnSelectAll()
        {
            bool flag = this.MenuItems.Count > 0;
            if (flag)
            {
                for (int i = 0; i < this.MenuItems.Count; i++)
                {
                    this.MenuItems[i].IsSelected = true;
                }

            }
            this.OnConfirmCmd(this.CameraItems);

        }
        public void OnUnSelectAll()
        {
            bool flag = this.MenuItems.Count > 0;
            if (flag)
            {
                for (int i = 0; i < this.MenuItems.Count; i++)
                {
                    this.MenuItems[i].IsSelected = false;
                }
            }
        }
        private void OnConfirmCmd(List<CameraModel> list)
        {
            if(listThread.Count>0)
            {
                foreach (var item in listThread)
                {
                    item.Abort();
                }
            }
            listThread = new List<Thread>();
            this.cameraDetailView.Hide();
            this.ClearRender();
            this._gviTmpObjList = new List<IRObject>();
            this._beginPointSymbol = null;
            this.firstmodel = null;
            bool flag = list.Count <= 0;
            if (!flag)
            {
                var draws= list.GroupBy(c => c.sn).Select(c => c.First()).ToList().OrderBy(t=>t.latitude).ToList();
                for (int i = 0; i < draws.Count; i++)
                {
                    this.CreateEventPoint(draws[i]);
                }
            }
        }
        private void ClearRender()
        {
            this.ClearRenderObj(this._gviTmpObjList);
            GviMap.PointManager.Clear();
            GviMap.LinePolyManager.Clear();
            GviMap.TracePoiManager.Clear();
            GviMap.TraceLinePolyManager.Clear();
            this.ClearPatrolList();
            this.CleareventTablList();
        }
        private void ClearRenderObj(List<IRObject> list)
        {
            bool flag = list != null;
            if (flag)
            {
                foreach (IRObject current in list)
                {
                    bool flag2 = current == null;
                    if (!flag2)
                    {
                        GviMap.ObjectManager.DeleteObject(current.Guid);
                    }
                }
                list.Clear();
            }
            this._beginPointSymbol = null;
        }
        public void CleareventTablList()
        {
            Dictionary<string, Guid> expr_07 = this.eventTableList;
            bool flag = expr_07 == null || expr_07.Count > 0;
            if (flag)
            {
                foreach (KeyValuePair<string, Guid> current in this.eventTableList)
                {
                    GviMap.ObjectManager.DeleteObject(current.Value);
                }
            }
            Dictionary<string, Guid> expr_67 = this.eventTableList1;
            bool flag2 = expr_67 == null || expr_67.Count > 0;
            if (flag2)
            {
                foreach (KeyValuePair<string, Guid> current2 in this.eventTableList1)
                {
                    GviMap.ObjectManager.DeleteObject(current2.Value);
                }
            }
            this.eventTableList = new Dictionary<string, Guid>();
            this.eventTableList1 = new Dictionary<string, Guid>();
        }
        public void ClearPatrolList()
        {
            Dictionary<string, Guid> expr_07 = this.eventList;
            bool flag = expr_07 == null || expr_07.Count > 0;
            if (flag)
            {
                foreach (KeyValuePair<string, Guid> current in this.eventList)
                {
                    GviMap.ObjectManager.DeleteObject(current.Value);
                }
            }
            this.eventList = new Dictionary<string, Guid>();
        }
        private void ShowView()
        {
            try
            {
                this.GetStreetSign();
                bool flag = this.deviceView == null;
                if (flag)
                {
                    this.deviceView = new DeviceView();
                }
                this.deviceView.DataContext = this;
                this.deviceView.Left = Application.Current.MainWindow.Width * 0.6;
                this.deviceView.Top = Application.Current.MainWindow.Height * 0.2;
                this.deviceView.Owner = Application.Current.MainWindow;
                this.deviceView.Show();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
        }
        private ITableLabel createTableLable(IPoint pt, string event_name, string type)
        {
            ITableLabel tableLabel = TableLabelFactory.CreateWindTable(GviMap.ObjectManager, 2, 5);
            pt.Z = 10.0;
            tableLabel.Position = pt;
            tableLabel.VisibleMask = gviViewportMask.gviViewAllNormalView;
            tableLabel.TitleText = event_name;
            tableLabel.SetColumnWidth(0, 100);
            tableLabel.SetColumnWidth(1, 105);
            tableLabel.SetColumnWidth(2, 105);
            tableLabel.SetColumnWidth(3, 120);
            tableLabel.SetColumnWidth(4, 120);
            tableLabel.SetRecord(0, 0, "材质:");
            tableLabel.SetRecord(0, 1, "名称:");
            tableLabel.SetRecord(0, 2, "用途:");
            tableLabel.SetRecord(0, 3, "安装日期:");
            tableLabel.SetRecord(0, 4, "维护日期:");
            return tableLabel;
        }
        private void CreateEventPoint(CameraModel eventInfo)
        {
            string text = eventInfo.sn;
            string geom = eventInfo.geom;
            string add_time = eventInfo.add_time;
            string sn = eventInfo.sn;
            bool flag = geom != "" && geom != null && geom != "POINT Z(0 0 0)";
            if (flag)
            {
                string text2 = "摄像头";
                IGeometry geometry = GviMap.GeoFactory.CreateFromWKT(geom);
                geometry.SpatialCRS = GviMap.SpatialCrs;
                IPoint point = geometry as IPoint;
                IPOI iPOI = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
                iPOI.SetPostion(point.X, point.Y, string.IsNullOrEmpty(eventInfo.height) ? 10 : (Convert.ToInt32(Convert.ToDouble(eventInfo.height)+1)));
                bool flag2 = this.firstmodel != null;
                if (flag2)
                {
                    IGeometry geometry2 = GviMap.GeoFactory.CreateFromWKT(this.firstmodel.geom);
                    geometry2.SpatialCRS = GviMap.SpatialCrs;
                    IPoint point2 = geometry2 as IPoint;
                    IPOI this2 = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
                    this2.SetPostion(point2.X, point2.Y, string.IsNullOrEmpty(eventInfo.height)?10: (Convert.ToDouble(eventInfo.height)));
                    this.CreateRenderPolyline(point2, point, string.IsNullOrEmpty(eventInfo.height) ? 10 : Convert.ToInt32(Convert.ToDouble(eventInfo.height)), string.IsNullOrEmpty(this.firstmodel.height) ? 10 : Convert.ToInt32(Convert.ToDouble(this.firstmodel.height)));
                }
                this.firstmodel = eventInfo;
                iPOI.Size = 30;
                iPOI.ShowName = true;
                iPOI.MaxVisibleDistance = 5000.0;
                iPOI.MinVisibleDistance = 100.0;
                iPOI.Name = sn;
                iPOI.SpatialCRS = GviMap.SpatialCrs;
                iPOI.ImageName = string.Format("项目数据\\shp\\IMG_POI\\{0}.png", text2);
                IRenderPOI renderPOI = GviMap.ObjectManager.CreateRenderPOI(iPOI);
                renderPOI.DepthTestMode = gviDepthTestMode.gviDepthTestAdvance;
                text = renderPOI.Guid.ToString();
                bool flag3 = this.eventList.ContainsValue(renderPOI.Guid);
                if (flag3)
                {
                    GviMap.ObjectManager.DeleteObject(renderPOI.Guid);
                    this.eventList.Remove(text);
                }
                this.eventList.Add(text, renderPOI.Guid);
              
                this.CameraItems.Find((CameraModel t) => t.id == eventInfo.id).newGuid = text;
                //IPosition position = new Position();
                //position.X = point.X;
                //position.Y = point.Y;
                //position.Altitude = 1;
                //position.Cartesian = true;
                //position.Heading = 0.0;
                //position.Roll = 0.0;
                //position.Tilt = 0.0;

                double aa = Math.Atan(Convert.ToDouble(eventInfo.height));
                //1、创建圆 参数为位置、半径、线颜色、填充颜色、主节点id
                //ITerrainRegularPolygon terrainRegularPolygon = GviMap.ObjectManager.CreateCircle(position, aa * Convert.ToDouble(eventInfo.height), ColorConvert.UintToColor(4294967040u), ColorConvert.UintToColor(1728053247u), groupID);


                //| blind_spot       | string | 盲区角度| 
                //| view_angle       | string | 视野角度| 
                //| initial_angle       | string | 摄像头初始角度 | 
                if (string.IsNullOrEmpty(eventInfo.initial_angle))
                    eventInfo.initial_angle = "0";
                if (string.IsNullOrEmpty(eventInfo.view_angle))
                    eventInfo.view_angle = "30";
                if (string.IsNullOrEmpty(eventInfo.blind_spot))
                    eventInfo.blind_spot = "0";
               

                double cameraWidth = Math.Atan(Convert.ToDouble(eventInfo.height));//宽度
                point.Z = Convert.ToDouble(eventInfo.height);


                double blind_spot = (360 - Convert.ToDouble(eventInfo.blind_spot));// Convert.ToInt32(eventInfo.initial_angle);

                IPolyline polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeNone) as IPolyline;
                IPolygon polygon = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygon.CreatePolygonWithZ(1);//z偏移防止闪
                IPoint pointOri = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                pointOri.X = point.X;
                pointOri.Y = point.Y;
                pointOri.Z = 1;
                polyline.AppendPoint(pointOri);

                //| blind_spot       | string | 盲区角度| 
                //| view_angle       | string | 视野角度| 
                //| initial_angle       | string | 摄像头初始角度 | 
                double jiao = (blind_spot) * 0.5-Convert.ToDouble(eventInfo.initial_angle);
                for (int i = Convert.ToInt32( -jiao); i < (blind_spot-Convert.ToInt32(jiao)); i++)
                {
                    IEulerAngle eulerAngle2 = new EulerAngle();
                    eulerAngle2.Heading = i;
                    eulerAngle2.Roll = 0;
                    eulerAngle2.Tilt = 0;
                    IPoint newPoint1 = GviMap.Camera.GetAimingPoint2(pointOri, eulerAngle2, aa * Convert.ToDouble(eventInfo.height));
                    polyline.AppendPoint(newPoint1);
                    polygon.ExteriorRing.AppendPoint(newPoint1);
                }
                polyline.AppendPoint(pointOri);
                polyline.SpatialCRS = GviMap.SpatialCrs;

                polygon.ExteriorRing.AppendPoint(pointOri);
                polygon.SpatialCRS = GviMap.SpatialCrs;
                CurveSymbol curveSymbol = new CurveSymbol();
                curveSymbol.Color = ColorConvert.Argb(50, 208, 105, 37);//GviMap.LinePolyManager.CurveSym
                curveSymbol.Width =0.5f;
                var rpolyline = GviMap.ObjectManager.CreateRenderPolyline(polyline, curveSymbol, GviMap.ProjectTree.RootID);
                var renderpolygon = GviMap.ObjectManager.CreateRenderPolygon(polygon, GviMap.LinePolyManager.SurfaceSym,
                 GviMap.ProjectTree.RootID);
                renderpolygon?.SetFdeGeometry(polygon);
                renderpolygon.VisibleMask = gviViewportMask.gviViewAllNormalView;
                renderpolygon.Symbol.BoundarySymbol.Color = System.Drawing.Color.FromArgb(255, 0, 255, 255);
                bool flagrpolyline = this.eventTableList1.ContainsKey(rpolyline.Guid.ToString());
                if (flagrpolyline)
                {
                    GviMap.ObjectManager.DeleteObject(this.eventTableList1[rpolyline.Guid.ToString()]);
                    this.eventTableList1.Remove(rpolyline.Guid.ToString());
                }
                this.eventTableList1.Add(rpolyline.Guid.ToString(), rpolyline.Guid);
                bool flag10 = this.eventTableList1.ContainsKey(renderpolygon.Guid.ToString());
                if (flag10)
                {
                    GviMap.ObjectManager.DeleteObject(this.eventTableList1[renderpolygon.Guid.ToString()]);
                    this.eventTableList1.Remove(renderpolygon.Guid.ToString());
                }
                this.eventTableList1.Add(renderpolygon.Guid.ToString(), renderpolygon.Guid);
                //if (blind_spot == 360)
                //{
                //    ITerrainRegularPolygon terrainRegularPolygon = GviMap.ObjectManager.CreateCircle(position, aa * Convert.ToDouble(eventInfo.height),  ColorConvert.Argb(0, 24, 144, 255), ColorConvert.Argb(70, 208, 105, 37), default(Guid));
                //    terrainRegularPolygon.NumberOfSegments = 50;
                //    bool flag5 = this.eventTableList1.ContainsKey(terrainRegularPolygon.Guid.ToString());
                //    if (flag5)
                //    {
                //        GviMap.ObjectManager.DeleteObject(this.eventTableList1[terrainRegularPolygon.Guid.ToString()]);
                //        this.eventTableList1.Remove(terrainRegularPolygon.Guid.ToString());
                //    }
                //    this.eventTableList1.Add(terrainRegularPolygon.Guid.ToString(), terrainRegularPolygon.Guid);
                //}
                //else
                //{
                //    //、创建圆弧 参数为位置、x方向半径、y方向半径、起始角度、终止角度、线颜色、填充颜色、组成椭圆线段的个数、主节点id
                //    ITerrainArc rArc = GviMap.ObjectManager.CreateArc(position, cameraWidth * Convert.ToDouble(eventInfo.height), cameraWidth * Convert.ToDouble(eventInfo.height), 0, 180, ColorConvert.Argb(0, 24, 144, 255), ColorConvert.Argb(70, 208, 105, 37), 25, default(Guid));

                //    //GviMap.ObjectManager.
                //    if (blind_spot > 180)
                //    {
                //        ITerrainArc rArc2 = GviMap.ObjectManager.CreateArc(position, cameraWidth * Convert.ToDouble(eventInfo.height), cameraWidth * Convert.ToDouble(eventInfo.height), (180 - Convert.ToDouble(eventInfo.blind_spot)), blind_spot, ColorConvert.Argb(0, 24, 144, 255), ColorConvert.Argb(70, 208, 105, 37), 25, default(Guid));
                //        bool flag11 = this.eventTableList1.ContainsKey(eventInfo.id);
                //        if (flag11)
                //        {
                //            GviMap.ObjectManager.DeleteObject(this.eventTableList1[eventInfo.id]);
                //            this.eventTableList1.Remove(eventInfo.id);
                //        }
                //        this.eventTableList1.Add(eventInfo.id, rArc2.Guid);
                //    }
                //    bool flag10 = this.eventTableList1.ContainsKey(eventInfo.sn);
                //    if (flag10)
                //    {
                //        GviMap.ObjectManager.DeleteObject(this.eventTableList1[eventInfo.sn]);
                //        this.eventTableList1.Remove(eventInfo.sn);
                //    }

                //    this.eventTableList1.Add(eventInfo.sn, rArc.Guid);
                //}

                //position.Altitude = 1.1;

                //ITerrainArc rArc3 = GviMap.ObjectManager.CreateArc(position, cameraWidth * Convert.ToDouble(eventInfo.height), cameraWidth * Convert.ToDouble(eventInfo.height), 30,30.21, ColorConvert.Argb(100, 21, 145, 255), ColorConvert.Argb(100, 21, 145, 255), 25, default(Guid));

                //bool flag13 = this.eventTableList1.ContainsKey(eventInfo.geom);
                //if (flag13)
                //{
                //    GviMap.ObjectManager.DeleteObject(this.eventTableList1[eventInfo.geom]);
                //    this.eventTableList1.Remove(eventInfo.geom);
                //}

                //this.eventTableList1.Add(eventInfo.geom, rArc3.Guid);




                bool flag9 = this.eventTableList.ContainsKey(eventInfo.id);
                if (flag9)
                {
                    bool flag12 = this.cameraDetailView == null;
                    if (flag12)
                    {
                        this.cameraDetailView = new CameraDetailView();
                    }
                    this.cameraDetailView.DataContext = this;
                    this.cameraDetailView.Left = Application.Current.MainWindow.Width * 0.3;
                    this.cameraDetailView.Top = Application.Current.MainWindow.Height * 0.2;
                    this.cameraDetailView.Owner = Application.Current.MainWindow;
                    this.cameraDetailView.Show();
                    return;
                }
                IViewshed tv = null;
                tv = GviMap.ObjectManager.CreateViewshed(point, default(Guid));
                tv.DisplayMode = gviTVDisplayMode.gviTVShowEnvelopFaces;
                tv.AttributeMask = gviAttributeMask.gviAttributeCollision;
                //tv.Highlight(ColorConvert.Argb(100, 21, 145, 255));
                tv.FarClip = cameraWidth * Convert.ToDouble(eventInfo.height);
                tv.FieldOfView = 30;// Convert.ToDouble(eventInfo.view_angle);//广角
                this.eventTableList.Add(eventInfo.id, tv.Guid);

                thread = new Thread(new ThreadStart(delegate { Rotate(tv, eventInfo); }));
                thread.IsBackground = true;
                thread.Start();

                listThread.Add(thread);

                //creater(point);
                //ITerrainRegularPolygon terrainRegularPolygon = GviMap.ObjectManager.CreateCircle(position,50.0, ColorConvert.UintToColor(4294967040u), ColorConvert.Argb(100, 24, 144, 255), default(Guid));


                //3、创建圆弧 参数为位置、x方向半径、y方向半径、起始角度、终止角度、线颜色、填充颜色、组成椭圆线段的个数、主节点id


                //创建圆锥 参数为（位置position对象，半径、高度、线框颜色、填充颜色、边数、组节点guid）
                //var rCone = GviMap.ObjectManager.CreateCone(position, 150, string.IsNullOrEmpty(eventInfo.height) ? 10 : Convert.ToDouble(eventInfo.height), ColorConvert.Argb(100, 24, 144, 255), ColorConvert.UintToColor(1728053247u), 100, default(Guid));

                //terrainRegularPolygon.NumberOfSegments = 50;
                //terrainRegularPolygon1.NumberOfSegments = 50;
                //bool flag5 = this.eventTableList1.ContainsKey(terrainRegularPolygon.Guid.ToString());
                //if (flag5)
                //{
                //    GviMap.ObjectManager.DeleteObject(this.eventTableList1[terrainRegularPolygon.Guid.ToString()]);
                //    this.eventTableList1.Remove(terrainRegularPolygon.Guid.ToString());
                //}
                //this.eventTableList1.Add(terrainRegularPolygon.Guid.ToString(), terrainRegularPolygon.Guid);


                //bool flag6 = this.eventTableList1.ContainsKey(rCone.Guid.ToString());
                //if (flag6)
                //{
                //    GviMap.ObjectManager.DeleteObject(this.eventTableList1[rCone.Guid.ToString()]);
                //    this.eventTableList1.Remove(rCone.Guid.ToString());
                //}
                //this.eventTableList1.Add(rCone.Guid.ToString(), rCone.Guid);
                GviMap.Camera.FlyToObject(renderPOI.Guid, gviActionCode.gviActionFlyTo);



                //GviMap.Camera.GetCamera2(out IPoint pointCamera, out IEulerAngle eulerAngle);
                //eulerAngle.Tilt = -90;
                //eulerAngle.Heading = 210;
                //pointCamera.X = point.X;
                //pointCamera.Y = point.Y;
                //pointCamera.Z = 8600;
                //GviMap.Camera.SetCamera2(pointCamera, eulerAngle, 0);

                GviMap.Camera.GetCamera2(out IPoint pointCamera, out IEulerAngle eulerAngle);
                ////GviMap.Camera.FlyToEnvelope(point.Envelope);
                eulerAngle.Tilt = -90;
                eulerAngle.Heading = 210;
                pointCamera.X = 112.602617;
                pointCamera.Y = 23.2301;
                pointCamera.Z = 38600;
                GviMap.Camera.SetCamera2(pointCamera, eulerAngle, 0);
            }
        }
        private IGeometryFactory gfactory = null;
        private IPolygon pol = null;
        private  IPoint point1 = null;
   
        public void CreateRenderPolyline(IPoint p1, IPoint p2,int height2,int height)
        {

            this._beginPointSymbol = new SimplePointSymbol
            {
                Size = 5,
                FillColor = ColorConvert.Argb(100, 235, 245, 255)
            };
            CurveSymbol curveSymbol = new CurveSymbol
            {
                Width = 1f,
                Color = ColorConvert.Argb(100, 24, 144, 255)
            };
            IPolyline polyline = (IPolyline)GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ);

            polyline.StartPoint = p1;
            polyline.EndPoint = p2;
            ICurveSymbol curveSymbol2 = new CurveSymbol();
            curveSymbol.Color = ColorConvert.Argb(100, 24, 144, 255);

            curveSymbol.Width = -5;
            curveSymbol.Pattern = gviDashStyle.gviDashSolid;
            polyline.Generalize(100);

            double length = polyline.Length;
            IPoint beginPoint = GviMap.GeoFactory.CreatePoint(p1.X, p1.Y, height, GviMap.SpatialCrs);
            IPoint endPoint = GviMap.GeoFactory.CreatePoint(p2.X, p2.Y, height2, GviMap.SpatialCrs);

            IRenderPoint renderPoint = GviMap.ObjectManager.CreateRenderPoint(beginPoint, this._beginPointSymbol);
            IRenderPoint renderPoint2 = GviMap.ObjectManager.CreateRenderPoint(endPoint, this._beginPointSymbol);

            bool flag = this.eventTableList1.ContainsKey(renderPoint.Guid.ToString());
            if (flag)
            {
                GviMap.ObjectManager.DeleteObject(this.eventTableList1[renderPoint.Guid.ToString()]);
                this.eventTableList1.Remove(renderPoint.Guid.ToString());
            }
            this.eventTableList1.Add(renderPoint.Guid.ToString(), renderPoint.Guid);
            bool flag2 = this.eventTableList1.ContainsKey(renderPoint2.Guid.ToString());
            if (flag2)
            {
                GviMap.ObjectManager.DeleteObject(this.eventTableList1[renderPoint2.Guid.ToString()]);
                this.eventTableList1.Remove(renderPoint2.Guid.ToString());
            }
            this.eventTableList1.Add(renderPoint2.Guid.ToString(), renderPoint2.Guid);
            IPolyline polyline2 = GviMap.GeoFactory.CreatePolyline(beginPoint, endPoint, GviMap.SpatialCrs);
            polyline2.Generalize(100);//容差值
            IRenderPolyline renderPolyline = GviMap.ObjectManager.CreateRenderPolyline(polyline2, curveSymbol);
            renderPolyline.VisibleMask = gviViewportMask.gviViewAllNormalView;
            renderPolyline.MaxVisibleDistance = GviMap.RoMaxObserveDistance;
            renderPolyline.MinVisiblePixels = (float)GviMap.RoMinObserveDistance;
            this._gviTmpObjList.Add(renderPolyline);
        }
    
    }
}
