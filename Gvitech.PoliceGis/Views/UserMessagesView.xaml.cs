using Mmc.Mspace.Common.Cache;
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

namespace MMC.MSpace.Views
{
    /// <summary>
    /// UserMessagesView.xaml 的交互逻辑
    /// </summary>
    public partial class UserMessagesView
    {
        public UserMessagesView()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            string deadLineDay = CacheData.UserInfo.life_day;
            if (Convert.ToInt32(deadLineDay) != -1)
            {
                deadLine.Text = "  有效期还剩" + CacheData.UserInfo.life_day + "天";
            }
            else
            {
                deadLine.Text = "  未受登陆时间限制 ";
            }
        }
    }
}
