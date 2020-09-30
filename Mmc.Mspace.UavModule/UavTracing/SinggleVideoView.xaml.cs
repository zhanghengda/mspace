using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibVLCSharp.Shared;
using Mmc.Windows.Services;
using MediaPlayer = LibVLCSharp.Shared.MediaPlayer;

namespace Mmc.Mspace.UavModule.Views
{
    /// <summary>
    /// SinggleVideoView.xaml 的交互逻辑
    /// </summary>
    public partial class SinggleVideoView : UserControl
    {
        public Action<object> VideoViewMouseDoubleClick;
        public SinggleVideoView()
        {
            InitializeComponent();
        }

        private void VideoView_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                VideoViewMouseDoubleClick?.Invoke(this);
            }
        }
    }
}
