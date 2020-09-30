using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmc.Mspace.Common.Models;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Mmc.Wpf.Commands;
using Mmc.Mspace.RoutePlanning.Grid;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using System.Collections.ObjectModel;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Theme.Pop;
using System.Windows.Data;
using Mmc.Windows.Utils;
using System.IO;
using Mmc.Mspace.Const.ConstPath;
using System.Text.RegularExpressions;

namespace Mmc.Mspace.RoutePlanning
{
    public delegate void Callback(IPolyline polyline);
    public class RoutePlanShowPageViewModel : CheckedToolItemModel
    {
        public Callback callback;
        private RoutePlanShowPageView _routePlanShowPageView;
        private string _frontAngle;//正面
        private string _sideAngle;//侧面
        private double _revolveAngle;//旋转角度
        private GridPlugin gridPlugin;
        private string _camera;
        private ObservableCollection<string> _cameras;
        private ObservableCollection<double> _revolveAngles;
        private string _turnoverArea;//周转区
        private string _height;
        private string _groundRixel;
        private List<MappingCamera> _mappingCameraList;
        public Action onInitPolygon;
        public IPolygon _polygon;
        private MappingCamera mappingCamera;
        private bool _heightOption;
        private bool _groundPixelOption;
        private MappingCameraAddViewModel _mappingCameraAddViewModel;
        private string[] _arrMappingCamera = { "MMC OPE", "手动栅格(无相机规格)", "自定义相机网格", "索尼A7R", "MMC 5100" };
       
        [XmlIgnore]
        public ICommand cmdCreatePolygon { get; set; }

        [XmlIgnore]
        public ICommand cmdAddMappingCamera { get; set; }


        [XmlIgnore]
        public ICommand cmdCloseWindow { get; set; }
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            
            this.cmdCreatePolygon = new RelayCommand(() =>
            {
                gridPlugin = new GridPlugin();
                gridPlugin.callBack = new CallBack(GetPolygonCallback);
                gridPlugin.initPolygon();

            });
            cmdCloseWindow = new RelayCommand(()=> {
                releaseWindow();
            });
            cmdAddMappingCamera = new RelayCommand(() =>
            {
                if (_mappingCameraAddViewModel == null)
                {
                    _mappingCameraAddViewModel = new MappingCameraAddViewModel();
                }
                _mappingCameraAddViewModel.mappingCameraList = _mappingCameraList;
                _mappingCameraAddViewModel.getForm(this);
                _mappingCameraAddViewModel?.OnChecked();
            });
        }

     

        private bool isFloat(string val)
        {
            Regex reg = new Regex(@"^(-?\d+)(\.\d+)?$");
            return reg.Match(val).Success;
        }

        /// <summary>
        /// 拿到polygon后的回调函数
        /// </summary>
        /// <param name="polygon"></param>
        public void GetPolygonCallback(IPolygon polygon)
        {
            this._polygon = polygon;
            this.getRoute();
        }


        /// <summary>
        /// 从文件读取Json
        /// </summary>
        public void getJsonFromFile()
        {
            _mappingCameraList = new List<MappingCamera>();
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.MappingCameraConfig);
            for (int i =0;i<json.Count;i++)
            {
                MappingCamera mappingCamera = new MappingCamera();
                mappingCamera.Name = json[i].Name;
                mappingCamera.Focus = json[i].Focus;
                mappingCamera.Width = json[i].Width;
                mappingCamera.Height = json[i].Height;
                _mappingCameraList.Add(mappingCamera);
            }
        }

        public override void OnChecked()
        {
            getJsonFromFile();
            if (this._routePlanShowPageView == null)
            {
                this._routePlanShowPageView = new RoutePlanShowPageView();
                this._routePlanShowPageView.Owner = Application.Current.MainWindow;
                this._routePlanShowPageView.Closed += (sender, e) =>
                {
                    this._routePlanShowPageView = null;
                };
            }
            this._routePlanShowPageView.DataContext = this;
            mappingCamera = new MappingCamera
            {
                Width = 12,
                Height = 8,
                Focus = 1.2
            };
            _revolveAngles = new ObservableCollection<double>();
            for (int i = 0; i <= 180; i++)
            {
                _revolveAngles.Add(i);
            }
            _cameras = new ObservableCollection<string>();
            if (_mappingCameraList?.Count > 0)
            {
                foreach (var element in _mappingCameraList)
                {
                    _cameras.Add(element.Name);
                }
            }
            else
            {
                foreach (var item in _arrMappingCamera)
                {
                    _cameras.Add(item);
                }
            }
            SelectCamera = _cameras[0];
            SelectFrontAngle = "70";
            SelectSideAngle = "70";
            SelectHeight = "100";
            SelectRevoleAngle = 45;
            SelectGroundPixel = "96";
            SelectTurnoverArea = "0";
            this.SelectHeightOption = true;
            if (!_routePlanShowPageView.IsVisible)
            {
                _routePlanShowPageView.WindowStartupLocation = WindowStartupLocation.Manual;
                _routePlanShowPageView.Left = 74;
                _routePlanShowPageView.Top = 0;
                _routePlanShowPageView.Show();
            }
            IsSelected = true;
        }

        /// <summary>
        /// 正面
        /// </summary>
        public string SelectFrontAngle
        {
            get { return _frontAngle; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._frontAngle, value, "SelectFrontAngle");
                if (isFloat(SelectFrontAngle))
                {
                    this.getRoute();
                }
            }
        }

        /// <summary>
        /// 侧面
        /// </summary>
        public string SelectSideAngle
        {
            get { return _sideAngle; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._sideAngle, value, "SelectSideAngle");
                if (isFloat(SelectFrontAngle))
                {
                    this.getRoute();
                }
            }
        }

        /// <summary>
        /// 周转区
        /// </summary>
        public string SelectTurnoverArea
        {
            get { return this._turnoverArea; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._turnoverArea, value, "SelectTurnoverArea");
            }
        }

        public void releaseWindow()
        {
            gridPlugin?.UnRegisterDrawLineEvent();
            IsChecked = false;
            _polygon = null;
            _routePlanShowPageView = null;
            Console.WriteLine("-----------CloseWindow");
        }
        public override void OnUnchecked()
        {
            base.OnUnchecked();
            if (_routePlanShowPageView != null)
            {
                _routePlanShowPageView.Hide();
            }
            if (_mappingCameraAddViewModel != null)
            {
                _mappingCameraAddViewModel.releaseWindow();
            }
            IsSelected = false;
        }

        /// <summary>
        /// 相机列表
        /// </summary>
        public ObservableCollection<string> Cameras
        {
            get { return _cameras; }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<string>>(ref this._cameras, value, "Cameras");
            }
        }

        /// <summary>
        /// 相机
        /// </summary>
        public string SelectCamera
        {
            get { return _camera; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._camera, value, "SelectCamera");
                for (int i = 0; i < _mappingCameraList.Count; i++)
                {
                    if (_mappingCameraList[i].Name == SelectCamera)
                    {
                        mappingCamera = _mappingCameraList[i];
                        getRoute();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 旋转角度列表
        /// </summary>
        public ObservableCollection<double> RevolveAngles
        {
            get { return _revolveAngles; }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<double>>(ref this._revolveAngles, value, "RevolveAngles");
            }
        }

        /// <summary>
        /// 旋转角度
        /// </summary>
        public double SelectRevoleAngle
        {
            get { return _revolveAngle; }
            set
            {
                base.SetAndNotifyPropertyChanged<double>(ref this._revolveAngle, value, "SelectRevoleAngle");
                this.getRoute();
            }
        }

        /// <summary>
        /// 高度
        /// </summary>
        public string SelectHeight
        {
            get { return _height; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._height, value, "SelectHeight");
                this.getRoute();
            }
        }

        /// <summary>
        /// 通过算法计算出航线
        /// </summary>
        private void getRoute()
        {
            try
            {
                if (this._polygon != null)
                {

                    if (_polygon == null || _polygon.ExteriorRing.PointCount < 4)
                    {
                      return;
                    }

                    RouteCalculate routeCalculate = new RouteCalculate();
                    IPolyline route = routeCalculate.getRoute(this._polygon, SelectRevoleAngle, mappingCamera, double.Parse(SelectHeight), double.Parse(SelectGroundPixel), double.Parse(SelectSideAngle));
                    gridPlugin.drawGeometry(double.Parse(this.SelectHeight));
                    callback(route);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// 地面分辨率
        /// </summary>
        public string SelectGroundPixel
        {
            get { return _groundRixel; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._groundRixel, value, "SelectGroundPixel");
                this.getRoute();
            }
        }

        /// <summary>
        /// 高度单选框
        /// </summary>
        public bool SelectHeightOption
        {
            get { return _heightOption; }
            set
            {
                base.SetAndNotifyPropertyChanged<bool>(ref this._heightOption, value, "SelectHeightOption");
                if (this.SelectHeightOption == true)
                {
                    SelectGroundPixelOption = false;
                    _routePlanShowPageView.Id_GD.IsEnabled = true;
                    _routePlanShowPageView.Id_DMFBL.IsEnabled = false;
                    SelectHeight = (double.Parse(SelectGroundPixel) * mappingCamera.Width * mappingCamera.Height / mappingCamera.Focus).ToString();
                }
            }
        }

        /// <summary>
        /// 地面分辨率单选框
        /// </summary>
        public bool SelectGroundPixelOption
        {
            get { return _groundPixelOption; }
            set
            {
                base.SetAndNotifyPropertyChanged<bool>(ref this._groundPixelOption, value, "SelectGroundPixelOption");
                if (this.SelectGroundPixelOption == true)
                {
                    SelectHeightOption = false;
                    _routePlanShowPageView.Id_GD.IsEnabled = false;
                    _routePlanShowPageView.Id_DMFBL.IsEnabled = true;
                    SelectGroundPixel = (double.Parse(SelectHeight) / ( mappingCamera.Width * mappingCamera.Height / mappingCamera.Focus)).ToString();
                }
            }
        }


    }
}
