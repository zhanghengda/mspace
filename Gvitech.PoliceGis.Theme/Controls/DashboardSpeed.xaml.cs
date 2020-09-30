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
    /// DashboardSpeed.xaml 的交互逻辑
    /// </summary>
    public partial class DashboardSpeed : UserControl
    {
        /// <summary>
        /// 动画时间(毫秒)
        /// </summary>
        public static int AnimationInterval = 1000;

        public DashboardSpeed()
        {
            InitializeComponent();
        }


        public double AirSpeed
        {
            get { return (double)GetValue(AirSpeedProperty); }
            set { SetValue(AirSpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AirSpeed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AirSpeedProperty =
            DependencyProperty.Register("AirSpeed", typeof(double), typeof(DashboardSpeed), new PropertyMetadata(0.0d, OnAirSpeedChanged));

        private static void OnAirSpeedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dashboard = d as DashboardSpeed;
            var oldSpeed = (double)e.OldValue;
            var speed = (double)e.NewValue;

            dashboard.runAirSpeed.Text = speed.ToString("N1");

            if (oldSpeed > 30) oldSpeed = 30;
            if (speed > 30) speed = 30;

            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = oldSpeed * 6,
                To = speed * 6,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };

            DoubleAnimation doubleAnimationBg = new DoubleAnimation
            {
                From = oldSpeed * 6 - 90,
                To = speed * 6 - 90,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };

            RotateTransform rotate = new RotateTransform(speed * 6);
            dashboard.CursorAir.RenderTransform = rotate;

            Storyboard.SetTarget(doubleAnimation, dashboard.CursorAir);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Angle"));
            storyboard.Children.Add(doubleAnimation);

            Storyboard.SetTarget(doubleAnimationBg, dashboard.ArcAir);
            Storyboard.SetTargetProperty(doubleAnimationBg, new PropertyPath(Arc.EndAngleProperty.Name));
            storyboard.Children.Add(doubleAnimationBg);

            storyboard.Begin();
        }



        public double GroundSpeed
        {
            get { return (double)GetValue(GroundSpeedProperty); }
            set { SetValue(GroundSpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GroundSpeed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroundSpeedProperty =
            DependencyProperty.Register("GroundSpeed", typeof(double), typeof(DashboardSpeed), new PropertyMetadata(0.0d, OnGroundSpeedChanged));

        private static void OnGroundSpeedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dashboard = d as DashboardSpeed;
            var oldSpeed = (double)e.OldValue;
            var speed = (double)e.NewValue;

            dashboard.runGroundSpeed.Text = speed.ToString("N1");

            if (oldSpeed > 30) oldSpeed = 30;
            if (speed > 30) speed = 30;

            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = -oldSpeed * 6,
                To = -speed * 6,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };

            DoubleAnimation doubleAnimationBg = new DoubleAnimation
            {
                From = oldSpeed * 6 - 90,
                To = speed * 6 - 90,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };

            RotateTransform rotate = new RotateTransform(-speed * 6);
            dashboard.CursorGround.RenderTransform = rotate;

            Storyboard.SetTarget(doubleAnimation, dashboard.CursorGround);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Angle"));
            storyboard.Children.Add(doubleAnimation);

            Storyboard.SetTarget(doubleAnimationBg, dashboard.ArcGround);
            Storyboard.SetTargetProperty(doubleAnimationBg, new PropertyPath(Arc.EndAngleProperty.Name));
            storyboard.Children.Add(doubleAnimationBg);

            storyboard.Begin();
        }
    }
}
