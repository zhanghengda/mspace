using System;
using System.Windows;
using System.Windows.Input;

namespace FireControlModule.FireIot
{
    /// <summary>
    /// FloorMapView.xaml 的交互逻辑
    /// </summary>
    public partial class FloorMapView : Window
    {
        public FloorMapView()
        {
            InitializeComponent();
        }

        private void bigImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var d = e.Delta / 1000.0;
            if (scale.ScaleX + d < 1)
            {
                scale.ScaleX = scale.ScaleY = 1;
                return;
            }
            scale.ScaleX += d;
            scale.ScaleY += d;
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                base.DragMove();
            }
            catch (Exception)
            {
            }
        }
    }
}