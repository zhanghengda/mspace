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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mmc.Mspace.Theme.Controls
{
    /// <summary>
    /// DashboardRoll.xaml 的交互逻辑
    /// </summary>
    public partial class DashboardRoll : UserControl
    {
        /// <summary>
        /// 动画时间(毫秒)
        /// </summary>
        public static int AnimationInterval = 300;

        public DashboardRoll()
        {
            InitializeComponent();
        }

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Angle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(DashboardRoll), new PropertyMetadata(0.0d, OnAngleChanged));

        private static void OnAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dashboard = d as DashboardRoll;
            var angle = (double)e.NewValue;

            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = (double)e.OldValue,
                To = (double)e.NewValue,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };

            RotateTransform rotate = new RotateTransform(angle);
            dashboard.CursorGrid.RenderTransform = rotate;

            Storyboard.SetTarget(doubleAnimation, dashboard.CursorGrid);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Angle"));
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();

            dashboard.AngleText.Text = angle.ToString("f2");
        }
    }
}
