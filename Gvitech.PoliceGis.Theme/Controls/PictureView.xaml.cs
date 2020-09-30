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

namespace Mmc.Mspace.Theme.Controls
{
    /// <summary>
    /// PictureView.xaml 的交互逻辑
    /// </summary>
    public partial class PictureView : UserControl
    {
        private bool mouseDown;
        private Point mouseXY;
        private double min = 0.1, max = 5.0;//最小/最大放大倍数
        private static TransformGroup defaultTransformGroup;

        public ImageSource ImagePath
        {
            get { return (ImageSource)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImagePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(ImageSource), typeof(PictureView), new PropertyMetadata(PathDataChanged));


        private static void PathDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (defaultTransformGroup == null) return;
            var transform = defaultTransformGroup.Children[1] as TranslateTransform;
            transform.X = 0;
            transform.Y = 0;
        }
        public PictureView()
        {
            InitializeComponent();
            this.Loaded += PictureView_Loaded;
        }

        private void PictureView_Loaded(object sender, RoutedEventArgs e)
        {
            mouseXY = new Point(0, 0);
            defaultTransformGroup = IMG.FindResource("TfGroup") as TransformGroup;
            var transform = defaultTransformGroup.Children[1] as TranslateTransform;

            transform.X = mouseXY.X;
            transform.Y = mouseXY.Y;
        }

        private void ContentControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            img.CaptureMouse();
            mouseDown = true;
            mouseXY = e.GetPosition(img);
        }
        private void ContentControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            img.ReleaseMouseCapture();
            mouseDown = false;
        }
        private void ContentControl_MouseMove(object sender, MouseEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            if (mouseDown)
            {
                Domousemove(img, e);
            }
        }

        private void ContentControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var img = sender as ContentControl;
            if (img == null)
            {
                return;
            }
            var point = e.GetPosition(img);
            var group = IMG.FindResource("TfGroup") as TransformGroup;
            var delta = e.Delta * 0.001;
            DowheelZoom(group, point, delta);
        }
        private void DowheelZoom(TransformGroup group, Point point, double delta)
        {
            var pointToContent = group.Inverse.Transform(point);
            var transform = group.Children[0] as ScaleTransform;
            if (transform.ScaleX + delta < min) return;
            if (transform.ScaleX + delta > max) return;
            transform.ScaleX += delta;
            transform.ScaleY += delta;
            var transform1 = group.Children[1] as TranslateTransform;
            transform1.X = -1 * ((pointToContent.X * transform.ScaleX) - point.X);
            transform1.Y = -1 * ((pointToContent.Y * transform.ScaleY) - point.Y);
        }
        private void Domousemove(ContentControl img, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }
            var group = IMG.FindResource("TfGroup") as TransformGroup;
            var transform = group.Children[1] as TranslateTransform;
            var position = e.GetPosition(img);
            transform.X -= mouseXY.X - position.X;
            transform.Y -= mouseXY.Y - position.Y;
            mouseXY = position;
        }

    }
}
