using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Expression.Shapes;

namespace Mmc.Mspace.Theme.Controls
{
    /// <summary>
    /// DashboardHeight.xaml 的交互逻辑
    /// </summary>
    public partial class DashboardHeight : UserControl
    {
        /// <summary>
        /// 动画时间(毫秒)
        /// </summary>
        public static int AnimationInterval = 1000;

        private static int MaxUavHeight = 30;
        private static int MaxAltitude = 30;

        public DashboardHeight()
        {
            InitializeComponent();

            MaxUavHeight = 30;
            MaxAltitude = 30;
            Altitude = 0;
            Height = 0;
        }



        public double UavHeight
        {
            get { return (double)GetValue(UavHeightProperty); }
            set { SetValue(UavHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UavHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UavHeightProperty =
            DependencyProperty.Register("UavHeight", typeof(double), typeof(DashboardHeight), new PropertyMetadata(0.0d, OnUavHeightChanged));

        private static void OnUavHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dashboard = d as DashboardHeight;
            var oldHeight = (double)e.OldValue;
            var height = (double)e.NewValue;

            dashboard.runHeight.Text = height.ToString("N1");
            if (height < 0) height = 0;

            if (MaxUavHeight < height)
            {
                MaxUavHeight = (int)((height * 1.2 + 10) / 10) * 10;
                while (MaxUavHeight % 6 != 0)
                {
                    MaxUavHeight++;
                }
                double scale = MaxUavHeight / 6;

                dashboard.txtUp1.Text = scale.ToString("N0");
                dashboard.txtUp2.Text = (2 * scale).ToString("N0");
                dashboard.txtUp3.Text = (3 * scale).ToString("N0");
                dashboard.txtUp4.Text = (4 * scale).ToString("N0");
                dashboard.txtUp5.Text = (5 * scale).ToString("N0");
                dashboard.txtUp6.Text = (6 * scale).ToString("N0");
            }

            var angleScale = (double)180 / MaxUavHeight;

            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = oldHeight * angleScale,
                To = height * angleScale,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };

            DoubleAnimation doubleAnimationBg = new DoubleAnimation
            {
                From = oldHeight * angleScale - 90,
                To = height * angleScale - 90,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };

            RotateTransform rotate = new RotateTransform(height * angleScale);
            dashboard.CursorHeight.RenderTransform = rotate;

            Storyboard.SetTarget(doubleAnimation, dashboard.CursorHeight);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Angle"));
            storyboard.Children.Add(doubleAnimation);

            Storyboard.SetTarget(doubleAnimationBg, dashboard.ArcHeight);
            Storyboard.SetTargetProperty(doubleAnimationBg, new PropertyPath(Arc.EndAngleProperty.Name));
            storyboard.Children.Add(doubleAnimationBg);

            storyboard.Begin();
        }



        public double Altitude
        {
            get { return (double)GetValue(AltitudeProperty); }
            set { SetValue(AltitudeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Alititude.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AltitudeProperty =
            DependencyProperty.Register("Altitude", typeof(double), typeof(DashboardHeight), new PropertyMetadata(0.0d, OnAltitudeChanged));

        private static void OnAltitudeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dashboard = d as DashboardHeight;
            var oldAltitude = (double)e.OldValue;
            var altitude = (double)e.NewValue;

            dashboard.runAltitude.Text = altitude.ToString("N1");
            if (altitude < 0) altitude = 0;

            if (MaxAltitude < altitude)
            {
                MaxAltitude = (int)((altitude * 1.2 + 10) / 10) * 10;
                while (MaxAltitude % 6 != 0)
                {
                    MaxAltitude++;
                }
                double scale = MaxAltitude / 6;

                dashboard.txtDown1.Text = scale.ToString("N0");
                dashboard.txtDown2.Text = (2 * scale).ToString("N0");
                dashboard.txtDown3.Text = (3 * scale).ToString("N0");
                dashboard.txtDown4.Text = (4 * scale).ToString("N0");
                dashboard.txtDown5.Text = (5 * scale).ToString("N0");
                dashboard.txtDown6.Text = (6 * scale).ToString("N0");
            }

            double angleScale = (double)180 / MaxAltitude;

            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = -oldAltitude * angleScale,
                To = -altitude * angleScale,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };

            DoubleAnimation doubleAnimationBg = new DoubleAnimation
            {
                From = oldAltitude * angleScale - 90,
                To = altitude * angleScale - 90,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };

            RotateTransform rotate = new RotateTransform(-altitude * angleScale);
            dashboard.CursorAltitude.RenderTransform = rotate;

            Storyboard.SetTarget(doubleAnimation, dashboard.CursorAltitude);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Angle"));
            storyboard.Children.Add(doubleAnimation);

            Storyboard.SetTarget(doubleAnimationBg, dashboard.ArcAltitude);
            Storyboard.SetTargetProperty(doubleAnimationBg, new PropertyPath(Arc.EndAngleProperty.Name));
            storyboard.Children.Add(doubleAnimationBg);

            storyboard.Begin();
        }
    }
}
