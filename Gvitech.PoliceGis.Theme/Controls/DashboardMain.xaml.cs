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

namespace Mmc.Mspace.Theme.Controls
{
    /// <summary>
    /// DashboardMain.xaml 的交互逻辑
    /// </summary>
    public partial class DashboardMain : UserControl
    {
        /// <summary>
        /// 动画时间(毫秒)
        /// </summary>
        public static int AnimationInterval = 1000;

        public DashboardMain()
        {
            InitializeComponent();
        }

        public double Direction
        {
            get { return (double)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register("Direction", typeof(double), typeof(DashboardMain), new PropertyMetadata(0.0d,OnDirectionChanged));

        private static void OnDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dashboard = d as DashboardMain;
            var direction = (double) e.NewValue;

            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = (double)e.OldValue,
                To = (double)e.NewValue,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };

            RotateTransform rotate = new RotateTransform(direction);
            dashboard.Cursor.RenderTransform = rotate;

            Storyboard.SetTarget(doubleAnimation, dashboard.Cursor);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Angle"));
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();
        }



        public double Roll
        {
            get { return (double)GetValue(RollProperty); }
            set { SetValue(RollProperty, value); }
        }

        public static readonly DependencyProperty RollProperty =
            DependencyProperty.Register("Roll", typeof(double), typeof(DashboardMain), new PropertyMetadata(0.0d,OnRollChanged));

        private static void OnRollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dashboard = d as DashboardMain;
            var direction = (double)e.NewValue;

            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = (double)e.OldValue,
                To = (double)e.NewValue,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };

            RotateTransform rotate = new RotateTransform(direction);
            dashboard.RollCursor.RenderTransform = rotate;

            Storyboard.SetTarget(doubleAnimation, dashboard.RollCursor);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.Angle"));
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();
        }



        public double Tilt
        {
            get { return (double)GetValue(TiltProperty); }
            set { SetValue(TiltProperty, value); }
        }


        public static readonly DependencyProperty TiltProperty =
            DependencyProperty.Register("Tilt", typeof(double), typeof(DashboardMain), new PropertyMetadata(0.0d, OnTiltChanged));

        private static void OnTiltChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dashboard = d as DashboardMain;
            var oldValue = (double) e.OldValue / 90 * 30;
            var direction = (double) e.NewValue / 90 * 30;

            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation1 = new DoubleAnimation
            {
                From = oldValue,
                To = direction,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };
            DoubleAnimation doubleAnimation2 = new DoubleAnimation
            {
                From = oldValue * 0.7,
                To = direction * 0.7,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };
            DoubleAnimation doubleAnimation3 = new DoubleAnimation
            {
                From = oldValue * 0.5,
                To = direction * 0.5,
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationInterval))
            };

            TranslateTransform translate1 = new TranslateTransform(0, direction);
            TranslateTransform translate2 = new TranslateTransform(0, direction * 0.7);
            TranslateTransform translate3 = new TranslateTransform(0, direction * 0.5);
            dashboard.TiltCursor1.RenderTransform = translate1;
            dashboard.TiltCursor2.RenderTransform = translate2;
            dashboard.TiltCursor3.RenderTransform = translate3;

            Storyboard.SetTarget(doubleAnimation1, dashboard.TiltCursor1);
            Storyboard.SetTargetProperty(doubleAnimation1,new PropertyPath("RenderTransform.Y"));
            storyboard.Children.Add(doubleAnimation1);
            Storyboard.SetTarget(doubleAnimation2, dashboard.TiltCursor2);
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("RenderTransform.Y"));
            storyboard.Children.Add(doubleAnimation2);
            Storyboard.SetTarget(doubleAnimation3, dashboard.TiltCursor3);
            Storyboard.SetTargetProperty(doubleAnimation3, new PropertyPath("RenderTransform.Y"));
            storyboard.Children.Add(doubleAnimation3);

            storyboard.Begin();
        }
    }
}
