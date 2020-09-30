using Mmc.Mspace.Services.HttpService;
using QQ2564874169.Miniblink;
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
using System.Windows.Shapes;

namespace Mmc.Mspace.IotModule.Views
{
    /// <summary>
    /// Interaction logic for AbnormalView.xaml
    /// </summary>
    public partial class AbnormalView
    {
        private MiniblinkBrowser Browser = new MiniblinkBrowser();
        public AbnormalView()
        {
            InitializeComponent();
           string url = "http://mspace.mmcuav.cn/ms/index.html#/error?token=" + HttpServiceUtil.Token;

            Browser.LoadUri(url);
            windowsFormsHost.Child = Browser; //把浏览器与前后台进行对接  
            this.Owner = Application.Current.MainWindow;

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
