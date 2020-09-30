using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Mspace.Services.LocalConfigService;
using System.Diagnostics;
using System.IO;

namespace MMC.MSpace.Views
{
    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingView 
    {
        bool f = false;
        private string _updaterModulePath;
        public SettingView()
        {
            InitializeComponent();
            this.MatchCoorSys();
            version.Text = CacheData.CurrentVersion;
            _updaterModulePath = AppDomain.CurrentDomain.BaseDirectory+@"\updater.exe";
        }
        private void link_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }
        private void MatchCoorSys()
        {
             this.f = true;
            var crsConfig = ServiceManager.GetService<ILocalWsConfigService>().CrsConfig.Find();
            string strCoorSys = crsConfig.CrsWkt;
            if (strCoorSys.IndexOf("WGS") > -1)
            {
                ChkWGS_Radio.IsChecked = true;
            }
            else if (strCoorSys.IndexOf("CGCS") > -1)
            {
                ChkCGCS_Radio.IsChecked = true;
            }
            else
            {
                ChkCustom_Radio.IsChecked = true;
            }
        }

        private void ChkCustom(object sender, RoutedEventArgs e)
        {
            if (this.f)
            {
                this.f = false;
                return;
            }
            ICoordSysDialog sysDialog = new CoordSysDialog();
            string strWKT = sysDialog.ShowDialog(gviLanguage.gviLanguageChineseSimple);
            if (strWKT != "")
            {
                SetWkt(strWKT);
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Setsuccess"));
            }
        }

        private void ChkCGCS(object sender, RoutedEventArgs e)
        {
            if (this.f)
            {
                this.f = false;
                return;
            }
            SetWkt(WKTString.CGCS2000_WKT);
            Messages.ShowMessage(Helpers.ResourceHelper.FindKey("SetCGCS2000"));
        }

        private void ChkWGS(object sender, RoutedEventArgs e)
        {
            if (this.f)
            {
                this.f = false;
                return;
            }
            SetWkt(WKTString.WGS_84_WKT);
            Messages.ShowMessage(Helpers.ResourceHelper.FindKey("SetWGS84"));
        }

        private void SetWkt(string Wkt)
        {
            var crsConfig = ServiceManager.GetService<ILocalWsConfigService>().CrsConfig.Find();
            crsConfig.CrsWkt = Wkt;
            ServiceManager.GetService<ILocalWsConfigService>().CrsConfig.Update(crsConfig);
        }

        private void BtnSetOragin_Click(object sender, RoutedEventArgs e)
        {
            var dr = Messages.ShowMessageDialog("提示", "是否保存当前视角为原点位置？");
            if (dr)
            {
                IPoint state;
                IEulerAngle eulerAngle;
                GviMap.Camera.GetCamera2(out state, out eulerAngle);
                string path = AppDomain.CurrentDomain.BaseDirectory + ConfigPath.ShellConfig;
                string x = state.X.ToString();
                string y = state.Y.ToString();
                string z = state.Z.ToString();
                string heading = eulerAngle.Heading.ToString();
                string roll = eulerAngle.Roll.ToString();
                string tilt = eulerAngle.Tilt.ToString();
                string content = x + ";" + y + ";" + z + ";" + heading + ";" + tilt + ";" + roll;
                var result = CacheData.SaveConfig("OriginCamera", content);
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Setsuccess"));
                
            }
        }

        private void CkeckUpdater_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(File.Exists(_updaterModulePath))
                {
                    Process process = Process.Start(_updaterModulePath, "/checknow");
                    process.Close();
                }
                
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        private void CloseWindow()
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.CloseWindow();
        }
    }
}
