using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Utils;
using Mmc.Mspace.UavModule.UavTracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LibVLCSharp.WPF;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Services;

namespace Mmc.Mspace.UavModule
{
    /// <summary>
    /// StatisticsWebView.xaml 的交互逻辑
    /// </summary>
    public partial class UavVideoVLCView
    {
        private string OriginUrl;
        private string AIUrl;

        private SingleVideoViewModel _singleVideoViewModel = new SingleVideoViewModel();

        public UavVideoVLCView()
        {
            InitializeComponent();

            VideoView.DataContext = _singleVideoViewModel;

            Loaded += UavVideoVLCView_Loaded;
        }

        private void UavVideoVLCView_Loaded(object sender, RoutedEventArgs e)
        {
            var permission = CacheData.UserInfo.mspace_config.configs.FirstOrDefault(t => t.config_key == "AIButton");
            if (permission != null)
            {
                AIBtnPanel.Visibility = Visibility.Visible;
            }
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

        public void SetUrl(string url)
        {
            OriginUrl = url;
            _singleVideoViewModel.SetStreamUrl(url);
            _singleVideoViewModel.Play();

            try
            {
                var json = JsonUtil.DeserializeFromFile<dynamic>(
                    AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);

                string getAIUrl = $"{json.aiUrl}stream/?streamUrl={OriginUrl}";
                HttpService httpService = new HttpService();
                var res = httpService.HttpRequest(getAIUrl);
                var resDynamic = JsonUtil.DeserializeFromString<dynamic>(res);

                AIUrl = resDynamic?.outputStreamUrl;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SystemLog.Log(ex);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _singleVideoViewModel.Stop();
            #region 关闭AI流

            var json = JsonUtil.DeserializeFromFile<dynamic>(
                AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string closeStreamUrl = $"{json.aiUrl}stream-off";
            HttpService httpService = new HttpService();
            var res = httpService.HttpRequest(closeStreamUrl);

            #endregion
            
            ((UavWebVideoViewModel)this.DataContext).IsChecked = false;
        }

        private void CheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            SystemLog.Log($"UavVideoVLCView AIUrl is [{AIUrl}]");
            if (string.IsNullOrWhiteSpace(AIUrl))
            {
                MessageBox.Show("AI视频流为空!");
                CheckBox.IsChecked = false;
                return;
            }

            _singleVideoViewModel.SetStreamUrl(AIUrl);
            _singleVideoViewModel.Play();

        }

        private void CheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            SystemLog.Log($"UavVideoVLCView OriginUrl is [{OriginUrl}]");
            if (string.IsNullOrWhiteSpace(OriginUrl))
            {
                MessageBox.Show("视频流为空!");
                return;
            }

            _singleVideoViewModel.SetStreamUrl(OriginUrl);
            _singleVideoViewModel.Play();
        }
    }
}