using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Utils;
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

namespace Mmc.Mspace.UavModule.UavTracing
{
    /// <summary>
    /// UavTracingView.xaml 的交互逻辑
    /// </summary>
    public partial class UavTracingView 
    {
        public UavTracingView()
        {
            InitializeComponent();
        }

        private void Load()
        {





        }

      

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            var _videoMonitorView = new UavVideoView() { Owner = Application.Current.MainWindow };
            _videoMonitorView.DataContext = new { WindowTitle = Helpers.ResourceHelper.FindKey("Realtimevideo"), ContentWidth = 390, ContentHeight = 300 };
            _videoMonitorView.Play();
            _videoMonitorView.Show();
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try { base.DragMove(); } catch (Exception) { }
        }

        private void Button_CloseCmd(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                // 内层ListBox拦截鼠标滚轮事件
                e.Handled = true;

                // 激发一个鼠标滚轮事件，冒泡给外层ListBox接收到
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
    }
}
