using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.ViewModels;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.PoiManagerModule.Views
{
    /// <summary>
    /// LabelFilterPopupView.xaml 的交互逻辑
    /// </summary>
    public partial class LabelFilterPopupView : UserControl,INotifyPropertyChanged
    {
        public LabelFilterPopupView()
        {
            InitializeComponent();
            Loaded += LabelFilterPopupView_Loaded;
        }

        private void LabelFilterPopupView_Loaded(object sender, RoutedEventArgs e)
        {
            TagTypes.Add("test1");
            TagTypes.Add("qwe");
            NotifyPropertyChanged("TagTypes");

            Tags.Add(new LabelInfoModel
            {
                LabelName = "test",
            });
            Tags.Add(new LabelInfoModel()
            {
                LabelName = "ad",
                LabelIsSelected = true
            });

            Tags.Add(new LabelInfoModel()
            {
                LabelIsSelected = true,
                LabelName = "aaa"
            });

            NotifyPropertyChanged("TagTypes");
            NotifyPropertyChanged("Tags");
        }

        public ObservableCollection<string> TagTypes = new ObservableCollection<string>();
        public ObservableCollection<LabelInfoModel> Tags = new ObservableCollection<LabelInfoModel>();


        RelayCommand CloseCommand
        {
            get { return new RelayCommand(OnCloseCommand);}
        }

        private void OnCloseCommand()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }
        protected void SetAndNotifyPropertyChanged<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                this.NotifyPropertyChanged(propertyName);
            }
        }
    }
}
