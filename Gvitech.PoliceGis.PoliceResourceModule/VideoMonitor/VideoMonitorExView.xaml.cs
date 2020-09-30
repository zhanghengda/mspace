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

namespace Mmc.Mspace.PoliceResourceModule
{
    /// <summary>
    /// VideoMonitorView.xaml 的交互逻辑
    /// </summary>
    public partial class VideoMonitorExView : Window
    {
        public VideoMonitorExView()
        {
            this.InitializeComponent();
            this.mediaCtrl.MediaEnded += delegate (object s, RoutedEventArgs e)
            {
                this.mediaCtrl.Position = default(TimeSpan);
                this.mediaCtrl.Play();
            };
        }
        public string VideoPath
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
                Uri source = new Uri(this._path);
                this.mediaCtrl.Source = source;
                this.mediaCtrl.Play();
            }
        }
        private string _path;

        // Token: 0x04000005 RID: 5
        private TimeSpan _pausest;

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            bool flag = "oid" == e.Column.Header.ToString().ToLower();
            if (flag)
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            bool flag = this.mediaCtrl.Source == null;
            if (flag)
            {
                this.mediaCtrl.Source = new Uri(this._path);
            }
            this.mediaCtrl.Position = (this._pausest.Equals(this.mediaCtrl.NaturalDuration) ? default(TimeSpan) : this._pausest);
            this.mediaCtrl.Play();
        }

        // Token: 0x06000010 RID: 16 RVA: 0x000025A8 File Offset: 0x000007A8
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            bool canPause = this.mediaCtrl.CanPause;
            if (canPause)
            {
                this.mediaCtrl.Pause();
                this._pausest = this.mediaCtrl.Position;
            }
        }

        // Token: 0x06000011 RID: 17 RVA: 0x000025E4 File Offset: 0x000007E4
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

        // Token: 0x06000012 RID: 18 RVA: 0x00002614 File Offset: 0x00000814
        private void Button_CloseCmd(object sender, RoutedEventArgs e)
        {
            base.Hide();
        }
    }
}
