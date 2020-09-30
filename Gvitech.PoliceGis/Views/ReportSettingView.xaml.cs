using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using GFramework.BlankWindow;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Common.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.ViewModels;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Controls;
using Mmc.Windows.Utils;
using Mmc.Wpf.Mvvm;

namespace MMC.MSpace.Views
{
    /// <summary>
    /// ReportSettingView.xaml 的交互逻辑
    /// </summary>
    public partial class ReportSettingView : BlankWindow
    {
        private static List<ImageListModel> reportTemplateList = new List<ImageListModel>
        {
            new ImageListModel
            {
                Name = "默认-鼎湖",
                ImageList = new List<ImageSource>
                {
                    new BitmapImage(new Uri("pack://application:,,,/Mmc.Mspace.Theme;component/Images/ReportTemplate/tongji.png")),
                    new BitmapImage(new Uri("pack://application:,,,/Mmc.Mspace.Theme;component/Images/ReportTemplate/changgui.png")),
                }
            },
            new ImageListModel
            {
                Name = "默认-海宁",
                ImageList = new List<ImageSource>
                {
                    new BitmapImage(new Uri("pack://application:,,,/Mmc.Mspace.Theme;component/Images/ReportTemplate/tongji.png")),
                    new BitmapImage(new Uri("pack://application:,,,/Mmc.Mspace.Theme;component/Images/ReportTemplate/haining.png")),
                }
            },
            new ImageListModel
            {
                Name = "默认-丹河",
                ImageList = new List<ImageSource>
                {
                    new BitmapImage(new Uri("pack://application:,,,/Mmc.Mspace.Theme;component/Images/ReportTemplate/tongji.png")),
                    new BitmapImage(new Uri("pack://application:,,,/Mmc.Mspace.Theme;component/Images/ReportTemplate/danhe.png")),
                }
            },
        };

        public ReportSettingView()
        {
            InitializeComponent();
            Loaded += ReportSettingView_Loaded;
        }

        private void ReportSettingView_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox.SelectionChanged += ComboBox_SelectionChanged;

            this.ComboBox.DisplayMemberPath = "Name";
            this.ComboBox.SelectedValuePath = "ImageList";
            ComboBox.ItemsSource = reportTemplateList;
            ComboBox.SelectedIndex = CacheData.UserInfo.report_template - 1;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ComboBox.SelectedIndex + 1;

            CacheData.UserInfo.report_template = index;
            var dic = new Dictionary<string, int>();
            dic.Add("report_template", index);
            HttpServiceHelper.Instance.PostRequestForStatus("/api/user/report-template", JsonUtil.SerializeToString(dic));
        }

        private void ShowExample_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ComboBox.SelectedItem as ImageListModel;
            if (selectedItem != null)
            {
                ImageSwitchView view = new ImageSwitchView
                {
                    TitleText = selectedItem.Name,
                    Height = 640,
                    Width = 420,
                    ImagePathList = selectedItem.ImageList,
                    SelectIndex = 0
                };
                view.Show();
            }
        }
    }

    public class ImageListModel:BindableBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        } 
        public List<ImageSource> ImageList;
    }
}
