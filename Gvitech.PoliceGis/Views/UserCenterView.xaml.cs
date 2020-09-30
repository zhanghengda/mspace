using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;

namespace MMC.MSpace.Views
{
    /// <summary>
    /// UserCenterView.xaml 的交互逻辑
    /// </summary>
    public partial class UserCenterView
    {
        bool rone = false;
        public UserCenterView()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            ChangeStyle();
        }
        
        private void ChangeStyle()
        {
           
            var fireColor = ColorTranslator.FromHtml("#FF50ABFF");
            var offColor = ColorTranslator.FromHtml("#FF6A7076");
            if (rone)
            {
          
                UsuralReport.IsEnabled = true;
                RoneReport.IsEnabled = false;
                ReportCenter.Text = "区域分析报告";
                UsuralReport.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(fireColor.A, fireColor.R, fireColor.G, fireColor.B));
                RoneReport.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(offColor.A, offColor.R, offColor.G, offColor.B));
                rone = false;
            }
            else
            {
                UsuralReport.IsEnabled = false;
                RoneReport.IsEnabled = true;
                ReportCenter.Text = "巡检报告";
                UsuralReport.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(offColor.A, offColor.R, offColor.G, offColor.B));
                RoneReport.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(fireColor.A, fireColor.R, fireColor.G, fireColor.B));
                rone = true;
            }

        }

        private void UsuralReport_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = e.Source as System.Windows.Controls.TabItem;
            if (item?.Header.ToString() == "巡检报告")
            {
                ChangeStyle();
            }
        }

        private void RoneReport_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = e.Source as System.Windows.Controls.TabItem;
            if(item?.Header.ToString() == "区域分析报告" )
            {
                ChangeStyle();
            }
           
        }
    }
}
