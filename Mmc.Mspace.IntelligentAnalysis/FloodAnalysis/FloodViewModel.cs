using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using Gvitech.Windows.Utils;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.MathUtil;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.IntelligentAnalysisModule
{
    public class FloodViewModel : CheckedToolItemModel
    {
        System.Timers.Timer t = new System.Timers.Timer();
        private FloodView flood;
        // public IRenderModelPoint rmp = null;
        //  public IModelPoint mp = null;
        private double _waterHeght = 0;
        private string _WaterPolygonKey = "_WaterPolygonKey";
        private double _Slider_value;
        private string _lable_text;
        private bool _Loop_play;

        private double part_time;//每次触发事件间隔的时间1000/每秒触发的次数

        private int start_time = 0;   //控制进度条的值，全局
        private bool listening = true;
        IPolygon polygon = null;
        IPoint point = null;
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            if (drawCustomer == null)
            {
                drawCustomer = new DrawCustomerUC("WaterPolygonDrawn", DrawCustomerType.MenuCommand);
            }
            this.DisposeCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                base.OnUnchecked();
                //取消注册事件
                UnRegisterDrawPolygonEvent();
                GviMap.HighlightHelper.VisibleMask = (byte)gviViewportMask.gviViewNone;
                //polygon不可见
                if (GviMap.TempRObjectPool.ContainsKey(_WaterPolygonKey))
                {
                    var rpolygon1 = GviMap.TempRObjectPool[_WaterPolygonKey] as IRenderPolygon;
                    rpolygon1.VisibleMask = gviViewportMask.gviViewNone;
                }
                 base.IsChecked = false;
                t.Close();
                if (flood != null)
                {
                    flood.Close();
                }
                //  model.Dispose();
                _rmp.VisibleMask = gviViewportMask.gviViewNone;
            });
            this.StartAnylyseCmd = new Mmc.Wpf.Commands.RelayCommand<bool>((parament) =>
            {
                if (FloodIsChecked == true)
                { 
                    if (string.IsNullOrEmpty(_curHeight)||!Regex.Match(_curHeight.ToString(),CommonRegex.NumberRegex).Success)
                    {
                        FloodIsChecked = false;
                        Messages.ShowMessage("每次升高只允许输入数字！");
                        return;
                    }
                    if (string.IsNullOrEmpty(_Playtimes) || !Regex.Match(_Playtimes.ToString(), CommonRegex.NumberRegex).Success)
                    {
                        FloodIsChecked = false;
                        Messages.ShowMessage("播放频率只允许输入数字！");
                        return;
                    }
                    t.Interval = part_time;//设置执行间隔单位ms毫秒
                                           //  t.AutoReset = false;
                    if (listening)
                    {
                        t.Elapsed += T_Elapsed;//循环执行函数
                    }
                    t.Start();
                }  
                if (FloodIsChecked == false)
                {
                    t.Close();
                }
                listening = false;

            });
            this.ReStartAnylyseCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                start_time = 0;
                if (Convert.ToInt32(_Playtimes) != 0)
                {
                    t.Interval = part_time; //设置执行间隔单位ms毫秒
              //      t.AutoReset = false;
                 //   t.Elapsed += T_Elapsed;
                    t.Start();//开始计时，开始循环
                }
                // t.Elapsed += new EventHandler(t_Tick);//t_Tick是要执行的函数
                FloodIsChecked = true;
            });


        }

        private bool _floodIsChecked;

        public bool FloodIsChecked
        {
            get { return _floodIsChecked; }
            set { _floodIsChecked = value; NotifyPropertyChanged("FloodIsChecked"); }
        }


        private  void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)

        {
            lock (this)
            {
                if (_Loop_play == false)//模拟一次
                {

                    if (start_time < 100)//进度条不满继续执行
                    {
                        start_time = start_time + 1;
                        Slider_value = Convert.ToDouble(start_time);
                    }
                    // start_time = start_time + 1;
                    if (start_time >= 100)
                    {
                        ServiceManager.GetService<IShellService>(null).ShellWindow.Dispatcher.Invoke(() =>
                        {
                            flood.buttonMouseOver.IsChecked = false;
                           // flood.buttonMouseOver.Content = "开始模拟";
                        });
                        t.Stop();
                    }
                }
                else if (_Loop_play == true)
                {
                    if (start_time < 100)//进度条不满继续执行
                    {
                        start_time = start_time + 1;
                        Slider_value = Convert.ToDouble(start_time);
                    }
                    if (start_time >= 100)
                    {
                        start_time = 0;
                      //  flood.buttonMouseOver.IsChecked = false;
                    }
                }

            }


        }
        [XmlIgnore]
        public ICommand StartAnylyseCmd { get; set; }//开始模拟
        [XmlIgnore]
        public ICommand ReStartAnylyseCmd { get; set; }//重新模拟
        [XmlIgnore]
        public ICommand DisposeCmd { get; set; }//关闭
        private IRenderModelPoint _rmp;
        private IRenderModelPoint _rmpside;
        // private IModelPoint model;
        //  public double first_click_point_height=0;
        public override void OnChecked()
        {
            base.OnChecked();
            RegisterDrawPolygonEvent();
          //  _waterHeght = first_click_point_height;
            _curHeight = "0.5";
            _Playtimes = "5";
            part_time = 200;
            listening = true;
            _Slider_value = 0;
            _Loop_play = true;
            _lable_text = "";
          
        }

        public override void OnUnchecked()//关闭事件
        {
            base.OnUnchecked();
            //取消注册事件
            UnRegisterDrawPolygonEvent();
            //HighlightHelper不可见
            GviMap.HighlightHelper.VisibleMask = (byte)gviViewportMask.gviViewNone;
            //polygon不可见
            if (GviMap.TempRObjectPool.ContainsKey(_WaterPolygonKey))
            {
                var rpolygon1 = GviMap.TempRObjectPool[_WaterPolygonKey] as IRenderPolygon;
                rpolygon1.VisibleMask = gviViewportMask.gviViewNone;
            }
            t.Close();
            if (flood != null)
            {
                flood.buttonMouseOver.IsChecked = false;
                flood.Close();
            }
            //  model.Dispose();
            if (_rmp != null)
            {
                _rmp.VisibleMask = gviViewportMask.gviViewNone;
            }
        }

        private DrawCustomerUC drawCustomer;
        private string _curHeight;
        public string CurHeight
        {
            get { return this._curHeight; }
            set { _curHeight = value; NotifyPropertyChanged("CurHeight"); }
        }
        public string _Playtimes;
        public string Playtimes
        {
            get { return this._Playtimes; }
            set
            {
                _Playtimes = value; NotifyPropertyChanged("Playtimes");

                if(string.IsNullOrEmpty(_Playtimes))
                    return;
                if (!Regex.Match(_Playtimes.ToString(), CommonRegex.NumberRegex).Success)
                {
                    Messages.ShowMessage("播放频率只允许输入数字！");
                    return;
                }
                part_time = 1000 / Convert.ToDouble(_Playtimes) ;
                
            }
        }
    /*   private IGeometryFactory gfactory = null;
        private IPolygon pol = null;
        private IPolygon creater() {  
        gfactory = new GeometryFactory();
        pol = gfactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZM) as IPolygon;
           
            IRing interiorRing1 = gfactory.CreateGeometry(gviGeometryType.gviGeometryRing, gviVertexAttribute.gviVertexAttributeZM) as IRing;
            IRing interiorRing2 = gfactory.CreateGeometry(gviGeometryType.gviGeometryRing, gviVertexAttribute.gviVertexAttributeZM) as IRing;
            point.SetCoords(25, 25, 0, 0, 1);
            interiorRing1.AppendPoint(point);
            point.SetCoords(75, 25, 0, 0, 2);
            interiorRing1.AppendPoint(point);
            point.SetCoords(75, 75, 0, 0, 3);
            interiorRing1.AppendPoint(point);
            point.SetCoords(25, 75, 0, 0, 4);
            interiorRing1.AppendPoint(point);
            point.SetCoords(25, 25, 0, 0, 5);
            interiorRing1.AppendPoint(point);  //闭合

            point.SetCoords(5, 6, 0, 8, 6);
            interiorRing2.AppendPoint(point);
            point.SetCoords(2, 3, 0, 5, 7);
            interiorRing2.AppendPoint(point);
            point.SetCoords(1, 1, 0, 1, 8);
            interiorRing2.AppendPoint(point);
            point.SetCoords(5, 6, 0, 8, 9);
            interiorRing2.AppendPoint(point);

            pol.AddInteriorRing(interiorRing1);
            pol.AddInteriorRing(interiorRing2);
            
            return pol;
        }*/
        public string lable_text
        {
            get { return this._lable_text; }
            set
            {
               
                _lable_text = value; NotifyPropertyChanged("lable_text");

                
        
            }
        }
        public bool Loop_play
        {
            get { return this._Loop_play; }
            set
            {

                _Loop_play = value; NotifyPropertyChanged("Loop_play");

            }

        }
        public double Slider_value
        {
            get { return this._Slider_value; }
            set
            {
              
                var jsonPath = AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigPath.WGS84_UTM_Path;
                // CurHeight = Convert.ToString(_myValue);
                
                _Slider_value = value; NotifyPropertyChanged("Slider_value");
                //  lable_text = Convert.ToString(_Slider_value);//去除slider对高度的显示控制，转由数值直接控制显示
                // _waterHeght = _Slider_value;
                double water_now_height = _Slider_value * Convert.ToDouble(_curHeight) + _waterHeght;
                if (_rmp != null)
                {
                    var model = _rmp.GetFdeGeometry() as IModelPoint;
                    
                    var pt = model.Position;
                    pt.Set(point.X, point.Y, water_now_height);
                    model.Position = pt;
                    _rmp.SetFdeGeometry(model);
                    _rmp.DepthTestMode = gviDepthTestMode.gviDepthTestEnable;
                    if (start_time != 0)
                    {                     
                     water_now_height = start_time * Convert.ToDouble(_curHeight) + _waterHeght;
                
                    }
                    if (start_time == 0)
                    {
                     water_now_height = _Slider_value * Convert.ToDouble(_curHeight) + _waterHeght;
                    }
                    lable_text = Convert.ToString(Math.Round(water_now_height,2));
                }
               
            }
        }
     
        private void RegisterDrawPolygonEvent()
        {
            RCDrawManager.Instance.PolygonDraw.Register(GviMap.AxMapControl, drawCustomer, RCMouseOperType.PickPoint);
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= PolygonDraw_OnDrawFinished;
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished += PolygonDraw_OnDrawFinished;
        }

        private void UnRegisterDrawPolygonEvent()
        {
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= PolygonDraw_OnDrawFinished;
            RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
        }


       
        private void PolygonDraw_OnDrawFinished(object sender, object result)
        {
            try
            {
                List<IPolygon> polside = new List<IPolygon>();
                var rPolygon = result as IRenderPolygon;
                polygon =rPolygon.GetFdeGeometry() as IPolygon;

                polygon.SpatialCRS = GviMap.SpatialCrs;
                //设置统一高度
                var min = polygon.ExteriorRing.GetPoint(0).Z;
                if (polygon.ExteriorRing.PointCount < 3)
                {
                    base.IsChecked = false;
                    return;
                    //弹窗多边形绘制点必须大于两个
                }
                else if (polygon.ExteriorRing.PointCount >= 3) { 

                for (int i = 0; i < polygon.ExteriorRing.PointCount; i++)//point 数目大于2
                {
                    var pt = polygon.ExteriorRing.GetPoint(i);
                    if (pt.Z < min)
                    { min = pt.Z;

                        }
                }
                _waterHeght = min;

                for (int i = 0; i < polygon.ExteriorRing.PointCount; i++)
                {
                    var pt = polygon.ExteriorRing.GetPoint(i);

                    pt.Z = _waterHeght;
                    polygon.ExteriorRing.UpdatePoint(i, pt);
                }
                    point = polygon.Centroid;//获取正确的中心点坐标
                    lable_text = Convert.ToString(Math.Round(_waterHeght, 2));//绘制水面当前高度保留两位小数
                    //lable_text = Convert.ToString(_waterHeght);
                    flood = new FloodView();
                    flood.DataContext = this;                  
                    flood.Show();
                    //var jsonPath = AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigPath.WGS84_UTM_Path;
                 
                    if (polygon != null)
                    {
                        IPolygon dynamicside = polygon;
                  
                        polside = CreateWaterSide(polygon);
                     
                    }
                  var xyzPolygon = polygon.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPolygon;               
                var wkt = Wgs84UtmUtil.GetWkt(point.X);
                if (xyzPolygon.ProjectEx(wkt))
                {
                        polside = CreateWaterSide(xyzPolygon);
                        polside.Add(xyzPolygon);//侧面集合添加top面对象。
                       
                        //   var model = CreateModelByPolygon(xyzPolygon);
                        var model = CreateModelByPolygonList(polside, xyzPolygon.Centroid);//生成整个model
                    var modelName = "testwater_model";
                    GviMap.ObjectManager.AddModel(modelName, model);
                    var mp = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;

                    mp.ModelName = modelName;

                    mp.Position = point.ToVector3();//xyzPolygon.Centroid.ToVector3();//

                    mp.SpatialCRS = GviMap.SpatialCrs;
                    /*
                        
                     */
                    if (_rmp == null)
                        _rmp = GviMap.ObjectManager.CreateRenderModelPoint(mp, null);
                    var model2 = _rmp.GetFdeGeometry() as IModelPoint;

                    double water_now_height = _Slider_value * Convert.ToDouble(_curHeight) + _waterHeght;
                    var pt = model2.Position;
                    pt.Set(point.X, point.Y, water_now_height);
                    model2.Position = pt;
                    _rmp.SetFdeGeometry(model2);
                    _rmp.VisibleMask = gviViewportMask.gviViewAllNormalView;
                    _rmp.MinVisiblePixels = 1;
                   
                    }
                 }
            }
            catch { }



            ////设置highlight
            //GviMap.HighlightHelper.VisibleMask = (byte)gviViewportMask.gviViewAllNormalView;
            //GviMap.HighlightHelper.SetRegion(polygon);
            //GviMap.HighlightHelper.MinZ = 0;
            //GviMap.HighlightHelper.MaxZ = 1;
            //GviMap.HighlightHelper.Color = Color.FromArgb(70, 0, 0, 255);
           
        }
        /// <summary>
        /// 通过top面构造侧面list的函数
        /// </summary>
        /// <param name="polygon_top"></param>
        /// <returns></returns>
        private List<IPolygon> CreateWaterSide(IPolygon polygon_top)
        {
            List <IPolygon>  polygon_contian = new List<IPolygon>();
            int side_num = polygon_top.ExteriorRing.PointCount;
            for (int i = 0; i < side_num; i++)
            {
                IPolygon side = null ;//RemovePolygonPoint( polygon_top);
                if (i < (side_num - 1))
                {
                    side = Creatpolygon(polygon_top.ExteriorRing.GetPoint(i), polygon_top.ExteriorRing.GetPoint(i + 1));
                }
                else if(i==(side_num - 1))
                {
                    side = Creatpolygon(polygon_top.ExteriorRing.GetPoint(i), polygon_top.ExteriorRing.GetPoint(0));
                }
               
                polygon_contian.Add(side);
            }

            return polygon_contian;
        }
     
        /// <summary>
        /// 一个矩形侧面的构造。（待完善）
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        private IPolygon Creatpolygon(IPoint point1,IPoint point2)
        {
            IGeometryFactory gfactory = new GeometryFactory();
            IPolygon polygon = (IPolygon)gfactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                                gviVertexAttribute.gviVertexAttributeZ);
            polygon.SpatialCRS = GviMap.SpatialCrs;
            polygon.ExteriorRing.AppendPoint(point1);
            polygon.ExteriorRing.AppendPoint(point2);
            IPoint point3 = point2;
            point3.Z = -50;
            IPoint point4 = point1;
            point4.Z = -50;
            polygon.ExteriorRing.AppendPoint(point3);
            polygon.ExteriorRing.AppendPoint(point4);
            IPoint point5 = point1;
            polygon.ExteriorRing.AppendPoint(point5);

            return polygon;
        }
       /// <summary>
       /// 输入多边形创建模型
       /// </summary>
       /// <param name="polygon"></param>
       /// <returns></returns>
        private IModel CreateModelByPolygon(IPolygon polygon)
        {
           
                var centpt = polygon.Centroid;
                var model = GviMap.ResourceFactory.CreateModel();
                IPoint pt1 = null;
                IPoint pt0 = null;
                var dg = new DrawGroup();

                for (int i = 0; i < polygon.ExteriorRing.PointCount; i++)
                {
                    if (i == polygon.ExteriorRing.PointCount - 1)
                    {
                        pt0 = polygon.ExteriorRing.GetPoint(i);
                        pt1 = polygon.ExteriorRing.GetPoint(0);

                        // first_click_point_height = pt1.Z;
                    }
                    else
                    {
                        pt0 = polygon.ExteriorRing.GetPoint(i);
                        pt1 = polygon.ExteriorRing.GetPoint(i + 1);
                    }

                    var x0 = pt0.X - centpt.X;
                    var y0 = pt0.Y - centpt.Y;
                    var z0 = pt0.Z - centpt.Z;


                    var x1 = pt1.X - centpt.X;
                    var y1 = pt1.Y - centpt.Y;
                    var z1 = pt1.Z - centpt.Z;



                    var dp = new DrawPrimitive();
                    dp.PrimitiveType = gviPrimitiveType.gviPrimitiveWater;

                    //构造顶点数组
                    dp.VertexArray = new FloatArray();

                    dp.VertexArray.Append(0);
                    dp.VertexArray.Append(0);
                    dp.VertexArray.Append(0);

                    dp.VertexArray.Append((float)x0);
                    dp.VertexArray.Append((float)y0);
                    dp.VertexArray.Append((float)z0);

                    dp.VertexArray.Append((float)x1);
                    dp.VertexArray.Append((float)y1);
                    dp.VertexArray.Append((float)z1);

                    //构造索引数组
                    dp.IndexArray = new UInt16Array();
                    dp.IndexArray.Append(0);
                    dp.IndexArray.Append(1);
                    dp.IndexArray.Append(2);


                    //材质
                    dp.Material = new DrawMaterial();
                    dp.Material.CullMode = gviCullFaceMode.gviCullNone;
                    dp.Material.EnableBlend = true;
                    dp.Material.DiffuseColor = Color.FromArgb(200, 53, 72, 108);
                    dg.AddPrimitive(dp);
                }
                
                model.AddGroup(dg);
                return model;
           
        }/// <summary>
         /// 输入一个多边形集合和他的中心点创建模型
         /// </summary>
         /// <param name="polygonlist"></param>
         /// <param name="Center"></param>
         /// <returns></returns>
        private IModel CreateModelByPolygonList(List<IPolygon> polygonlist,IPoint Center)
        {

            var centpt = Center;//polygon.Centroid;
            var model = GviMap.ResourceFactory.CreateModel();
          
            for (int k = 0; k < polygonlist.Count; k++)
            {
                IPoint pt1 = null;
                IPoint pt0 = null;
                IPoint pt2 = polygonlist[k].Centroid;
                var dg = new DrawGroup();

                for (int i = 0; i < polygonlist[k].ExteriorRing.PointCount; i++)
                {
                    if (i == polygonlist[k].ExteriorRing.PointCount - 1)
                    {
                        pt0 = polygonlist[k].ExteriorRing.GetPoint(i);
                        pt1 = polygonlist[k].ExteriorRing.GetPoint(0);

                        // first_click_point_height = pt1.Z;
                    }
                    else
                    {
                        pt0 = polygonlist[k].ExteriorRing.GetPoint(i);
                        pt1 = polygonlist[k].ExteriorRing.GetPoint(i + 1);
                       
                    }

                    var x0 = pt0.X - centpt.X;
                    var y0 = pt0.Y - centpt.Y;
                    var z0 = pt0.Z - centpt.Z;


                    var x1 = pt1.X - centpt.X;
                    var y1 = pt1.Y - centpt.Y;
                    var z1 = pt1.Z - centpt.Z;


                    var x2 = pt2.X - centpt.X;
                    var y2 = pt2.Y - centpt.Y;
                    var z2 = pt2.Z - centpt.Z;


                    var dp = new DrawPrimitive();
                    dp.PrimitiveType = gviPrimitiveType.gviPrimitiveWater;

                    //构造顶点数组
                    dp.VertexArray = new FloatArray();

                    dp.VertexArray.Append((float)x2);
                    dp.VertexArray.Append((float)y2);
                    dp.VertexArray.Append((float)z2);

                    dp.VertexArray.Append((float)x0);
                    dp.VertexArray.Append((float)y0);
                    dp.VertexArray.Append((float)z0);

                    dp.VertexArray.Append((float)x1);
                    dp.VertexArray.Append((float)y1);
                    dp.VertexArray.Append((float)z1);

                    //构造索引数组
                    dp.IndexArray = new UInt16Array();
                    dp.IndexArray.Append(0);
                    dp.IndexArray.Append(1);
                    dp.IndexArray.Append(2);


                    //材质
                    dp.Material = new DrawMaterial();
                    dp.Material.CullMode = gviCullFaceMode.gviCullNone;
                    dp.Material.EnableBlend = true;
                    dp.Material.DiffuseColor = Color.FromArgb(255, 53, 72, 108);
                    dg.AddPrimitive(dp);
                    
                }
                model.AddGroup(dg);
            }
          
            return model;

        }


        private IGeometry Buffer(IPolygon polygon, double dis)
        {
            var poly = polygon.Clone2(gviVertexAttribute.gviVertexAttributeNone);
            var topo = poly as ITopologicalOperator2D;
            return topo.Buffer2D(dis, gviBufferStyle.gviBufferCapbutt);
        }

        private bool isLoginFailed = true;
        /// <summary>  
        /// <para>获取或设置一个表示是否登录失败的值；true表示登录失败，否则为false。</para>  
        /// <para>与LoginWindow的IsEnable属性绑定。当为False时，关闭Login Window。</para>  
        /// 当该属性更改时通知客户端。   
        /// </summary>  
        public bool IsLoginFailed
        {
            get
            {
                return this.isLoginFailed;
            }

            set
            {
                if (this.isLoginFailed != value)
                {
                    this.isLoginFailed = value;
                    this.NotifyPropertyChanged("IsLoginFailed");
                }
            }
        }



    }
}
