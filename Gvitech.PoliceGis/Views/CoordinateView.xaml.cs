using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Windows.Services;
using Mmc.Wpf.Toolkit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MMC.MSpace.Views
{
    /// <summary>
    /// CoordinateView.xaml 的交互逻辑
    /// </summary>
    public partial class CoordinateView : UserControl
    {
        public CoordinateView()
        {
            InitializeComponent();
            GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            GviMap.AxMapControl.MouseMove += AxMapControl_MouseMove;
            //GviMap.AxMapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectMove;
            //GviMap.AxMapControl.RcMouseClickSelect += AxMapControl_RcMouseClickSelect;
         
        }

        private void AxMapControl_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            try
            {
                if (EventSender == gviMouseSelectMode.gviMouseSelectMove)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (IntersectPoint != null)
                        {
                            GviMap.Camera.GetCamera(out Gvitech.CityMaker.Math.IVector3 Position, out IEulerAngle Angle);
                            this.CoorLongitude.Text = ConvertDigitalToDegreesHelper.ConvertDigitalToDegrees(IntersectPoint.X);
                            this.CoorLatitude.Text = ConvertDigitalToDegreesHelper.ConvertDigitalToDegrees(IntersectPoint.Y);
                            this.CoorAltitude.Text = Math.Round(IntersectPoint.Z, 1).ToString();
                            this.CoorVisualHeight.Text = Math.Round(Position.Z, 1).ToString();
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        private void AxMapControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    //GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                    //var MouseMovePoi = GviMap.Camera.ScreenToWorld(e.X, e.Y, out IPoint mouseposition);
                    //GviMap.Camera.GetCamera(out Gvitech.CityMaker.Math.IVector3 Position, out IEulerAngle Angle);
                    //if (MouseMovePoi != null)o zh
                    //{
                    //    this.CoorLongitude.Text = ConvertDigitalToDegreesHelper.ConvertDigitalToDegrees(mouseposition.X);
                    //    this.CoorLatitude.Text = ConvertDigitalToDegreesHelper.ConvertDigitalToDegrees(mouseposition.Y);
                    //    this.CoorAltitude.Text = Math.Round(mouseposition.Z, 1).ToString();
                    //    this.CoorVisualHeight.Text = Math.Round(Position.Z, 1).ToString();
                    //}


                    GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                    GviMap.Camera.GetCamera(out Gvitech.CityMaker.Math.IVector3 Position, out IEulerAngle Angle);
                    this.CoorLongitude.Text = ConvertDigitalToDegreesHelper.ConvertDigitalToDegrees(Position.X);
                    this.CoorLatitude.Text = ConvertDigitalToDegreesHelper.ConvertDigitalToDegrees(Position.Y);
                    this.CoorAltitude.Text = "0";//Math.Round(Position.Z, 1).ToString();
                    this.CoorVisualHeight.Text = Math.Round(Position.Z, 1).ToString();
                });
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

        }

    }
}
