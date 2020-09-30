using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.ViewModels;
using Mmc.Mspace.Services.NetRouteAnalysisService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.ToolModule
{
    public class LocatingViewModel : CheckedToolItemModel
    {
        private LocatingView locatingView;

        private double _index;

        public double Index
        {
            get { return this._index; }
            set
            {
                _index = value;
                NotifyPropertyChanged("Index");
            }
        }

        private bool _isAddPoi;

        public bool IsAddPoi
        {
            get { return this._isAddPoi; }
            set
            {
                _isAddPoi = value;
                NotifyPropertyChanged("IsAddPoi");
            }
        }
        
            private bool _visibleStation;
        public bool VisibleStation
        {
            get { return this._visibleStation; }
            set
            {
                _visibleStation = value;
                NotifyPropertyChanged("VisibleStation");
                if (label != null)
                {
                    if (VisibleStation == true)
                    {
                        label.SetVisibleMask(gviViewportMode.gviViewportSinglePerspective, 0, true);
                    }
                    else
                    {
                        label.SetVisibleMask(gviViewportMode.gviViewportSinglePerspective, 0, false);
                    }
                }
            }
        }
        private bool _openStation;
        public bool OpenStation
        {
            get { return this._openStation; }
            set
            {
                _openStation = value;
                NotifyPropertyChanged("OpenStation");
            }
        }

        private string _longitude;

        public string Longitude
        {
            get { return this._longitude; }
            set
            {
                _longitude = value;
                NotifyPropertyChanged("Longitude");
            }
        }

        private string _latitude;

        public string Latitude
        {
            get { return this._latitude; }
            set
            {
                _latitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }

        private string _lonDegree;

        public string LonDegree
        {
            get { return this._lonDegree; }
            set
            {
                _lonDegree = value;
                NotifyPropertyChanged("LonDegree");
            }
        }

        private string _lonMinute;

        public string LonMinute
        {
            get { return this._lonMinute; }
            set
            {
                _lonMinute = value;
                NotifyPropertyChanged("LonMinute");
            }
        }

        private string _lonSecond;

        public string LonSecond
        {
            get { return this._lonSecond; }
            set
            {
                _lonSecond = value;
                NotifyPropertyChanged("LonSecond");
            }
        }

        private string _latDegree;

        public string LatDegree
        {
            get { return this._latDegree; }
            set
            {
                _latDegree = value;
                NotifyPropertyChanged("LatDegree");
            }
        }

        private string _latMinute;

        public string LatMinute
        {
            get { return this._latMinute; }
            set
            {
                _latMinute = value;
                NotifyPropertyChanged("LatMinute");
            }
        }

        private string _latSecond;

        public string LatSecond
        {
            get { return this._latSecond; }
            set
            {
                _latSecond = value;
                NotifyPropertyChanged("LatSecond");
            }
        }


        private string _altitude;

        public string Altitude
        {
            get { return this._altitude; }
            set
            {
                _altitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }

        [XmlIgnore] public ICommand CloseCmd { get; set; }
        [XmlIgnore] public ICommand OkCmd { get; set; }
        [XmlIgnore] public ICommand LabelVisiblelCmd { get; set; }
        
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;

            this.CloseCmd = new Mmc.Wpf.Commands.RelayCommand(() => { base.IsChecked = false; });
            this.OkCmd = new Mmc.Wpf.Commands.RelayCommand(() => { GotoPotision(); });
            //this.LabelVisiblelCmd = new Mmc.Wpf.Commands.RelayCommand(() => { LabelVisibleOrNot(); });
        }

        public override void OnChecked()
        {
            base.OnChecked();
            ShowView();
        }

        public override FrameworkElement CreatedView()
        {
            return new LocatingView
            {
                Owner = Application.Current.MainWindow
            };
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            locatingView = (LocatingView)base.View;
            locatingView.Hide();
        }

        public override void Reset()
        {
            base.Reset();
            base.IsChecked = false;
        }

        public void ShowView()
        {
            locatingView = (LocatingView)base.View;

            locatingView.DataContext = this;

            locatingView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            locatingView.Show();
        }

        private void LabelVisibleOrNot()
        {
            if (label != null)
            {
                if (VisibleStation == true)
                {
                    label.SetVisibleMask(gviViewportMode.gviViewportSinglePerspective, 0, true);
                }
                else
                {
                    label.SetVisibleMask(gviViewportMode.gviViewportSinglePerspective, 0, false);
                }
            }
           
        }

        public void GotoPotision()
        {
            var selectIndex = this.Index;

            double x = 0, y = 0, z = 0;

            if (!double.TryParse(this.Altitude, out z))
            {
                z = 0;
            }

            if (selectIndex == 0)
            {
                if (!double.TryParse(this.Longitude, out x) || !double.TryParse(this.Latitude, out y))
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalDoubleNumber"));
                    return;
                }
            }
            else
            {
                if (!int.TryParse(this.LonDegree, out int lonD))
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalIntNumber"));
                    return;
                }

                if (!int.TryParse(this.LatDegree, out int latD))
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalIntNumber"));
                    return;
                }

                if (!int.TryParse(this.LonMinute, out int lonM))
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalIntNumber"));
                    return;
                }

                if (!int.TryParse(this.LatMinute, out int latM))
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalIntNumber"));
                    return;
                }

                if (!double.TryParse(this.LonSecond, out double lonS))
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalDoubleNumber"));
                    return;
                }

                if (!double.TryParse(this.LatSecond, out double latS))
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalDoubleNumber"));
                    return;
                }

                if (lonD > 180 || lonD < -180 || latD > 90 || latD < -90)
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalIntNumber"));
                    return;
                }

                if (lonM > 60 || lonM < -60 || latM > 60 || latM < -60)
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalIntNumber"));
                    return;
                }

                if (lonS > 60 || lonS < -60 || latS > 60 || latS < -60)
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalIntNumber"));
                    return;
                }

                x = lonD + (lonM + lonS / 60) / 60;
                y = latD + (latM + latS / 60) / 60;

            }

            if (x > 180 || x < -180 || y > 90 || y < -90)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalDoubleNumber"));
                return;
            }

            var poi = GviMap.GeoFactory.CreatePoint(x, y, z, crs: GviMap.SpatialCrs);

            GviMap.Camera.GetCamera2(out IPoint Position, out IEulerAngle Angle);
            if (label == null)
            {
                try
                {
                    if (selectIndex == 0)
                    {
                        CreateLabel(poi);
                    }
                    else
                    {
                        CreateLabel(poi, LonDegree,LonMinute,LonSecond,LatDegree,LatMinute,LatSecond);
                    }
                   
                }
                catch
                {
                    Messages.ShowMessage("定位失败");
                }
            }
            else {
               
                if (selectIndex == 0)
                {
                    ChangeLabel(poi);
                }
                else
                {
                    ChangeLabel(poi, LonDegree, LonMinute, LonSecond, LatDegree, LatMinute, LatSecond);
                }
            }
            LabelVisibleOrNot();
            // GviMap.Camera.LookAt2(poi, 1000, Angle);

            if (_openStation)
            {
                base.IsChecked = false;
            }
          
            //if (IsAddPoi)
            //{
            //    var poiMarker = new PoiMarkerViewModel()
            //    {
            //        Longitude = x,
            //        Latitude = y,
            //        Altitude = z
            //    };
            //    poiMarker.ReAssignData(null, false, true);


          //  base.IsChecked = false;
        }
        private ILabel label = null;
        private TextAttribute textAttribute = null;
        Gvitech.CityMaker.RenderControl.TextSymbol symbol = null;
        private void CreateLabel(IPoint _poi)
        {
            label = GviMap.MapControl.ObjectManager.CreateLabel(GviMap.ProjectTree.RootID);
            label.Text = "经度：" + _poi.X+ "°" + "\r\n" + "纬度：" + _poi.Y + "°"+ "\r\n" + "高度：" + _poi.Z + "米";
            label.Position = _poi;
            symbol = new Gvitech.CityMaker.RenderControl.TextSymbol();
            textAttribute = new TextAttribute();
            textAttribute.TextColor = ColorConvert.UintToColor(0xffffff00);
            textAttribute.TextSize = 20;
            textAttribute.Underline = false;
            textAttribute.Font = "楷体";
            symbol.TextAttribute = textAttribute;
            symbol.VerticalOffset = 10;
            symbol.DrawLine = true;
            symbol.MarginColor = ColorConvert.UintToColor(0x8800ffff);
            label.TextSymbol = symbol;
           GviMap.Camera.FlyToObject(label.Guid, gviActionCode.gviActionFlyTo);
        }
        private void CreateLabel(IPoint _poi,string _lonDegree, string _lonMinute, string _lonSecond, string _latDegree, string _latMinute, string _latSecond)
        {
            label = GviMap.MapControl.ObjectManager.CreateLabel(GviMap.ProjectTree.RootID);
            label.Text = "经度：" + _lonDegree+"°"+ _lonMinute+"′"+ _lonSecond+"″" + "\r\n" + "纬度：" + _latDegree + "°" + _latMinute + "′" + _latSecond + "″" + "\r\n" + "高度：" + _poi.Z + "米";
            label.Position = _poi;
            symbol = new Gvitech.CityMaker.RenderControl.TextSymbol();
            textAttribute = new TextAttribute();
            textAttribute.TextColor = ColorConvert.UintToColor(0xffffff00);
            textAttribute.TextSize = 20;
            textAttribute.Underline = false;
            textAttribute.Font = "楷体";
            symbol.TextAttribute = textAttribute;
            symbol.VerticalOffset = 10;
            symbol.DrawLine = true;
            symbol.MarginColor = ColorConvert.UintToColor(0x8800ffff);
            label.TextSymbol = symbol;
            GviMap.Camera.FlyToObject(label.Guid, gviActionCode.gviActionFlyTo);
        }

        private void ChangeLabel(IPoint _poi)
        {
            label.Text = "经度：" + _poi.X+ "°" + "\r\n"+ "纬度：" + _poi.Y+ "°" + "\r\n"+ "高度：" + _poi.Z + "米";
            label.SetPosition(_poi);
            GviMap.Camera.FlyToObject(label.Guid, gviActionCode.gviActionFlyTo);
        }
        private void ChangeLabel(IPoint _poi, string _lonDegree, string _lonMinute, string _lonSecond, string _latDegree, string _latMinute, string _latSecond)
        {
            label.Text = "经度：" + _lonDegree + "°" + _lonMinute + "′" + _lonSecond + "″" + "\r\n" + "纬度：" + _latDegree + "°" + _latMinute + "′" + _latSecond + "″" + "\r\n" + "高度：" + _poi.Z + "米";
            label.SetPosition(_poi);
            GviMap.Camera.FlyToObject(label.Guid, gviActionCode.gviActionFlyTo);
        }

        private void UpdatedPoi(MarkerNew markerInfo)
        {
            //MarkerHelper.Instance.UpdateMarkerList(markerInfo);
            //var newPoi = new Dictionary<string, MarkerModel>();
            //newPoi.Add(markerInfo.id.ToString(), markerInfo);
            //var view = this.leftWindow as IWebView;
            //string jsonStr = JsonUtil.SerializeToString(newPoi);
            //view.InvokeScript("addMarker", jsonStr);
        }
    }
}