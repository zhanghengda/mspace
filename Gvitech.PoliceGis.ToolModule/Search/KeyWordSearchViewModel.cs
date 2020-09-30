using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.Controls;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.ToolModule.Search
{
    public class KeyWordSearchViewModel : CheckedToolItemModel
    {
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        public override FrameworkElement CreatedView()
        {
            return new KeyWordSearchView
            {
                Owner = Application.Current.MainWindow
            };
        }

        [XmlIgnore]
        public bool NeedBufferGeoVisible { get; private set; }

        public void SetBufferGeoVisible(bool isBufferGeoVisible)
        {
            NeedBufferGeoVisible = isBufferGeoVisible;
        }
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
            });
        }
        public override void OnChecked()
        {
            base.OnChecked();
            KeyWordSearchView keyWordSearchView = (KeyWordSearchView)base.View;
            var searchControlViewModel = keyWordSearchView.searchControl.DataContext as SearchControlViewModel;
            if (searchControlViewModel != null)
            {
                searchControlViewModel.QueryLayers= ServiceManager.GetService<Mmc.Mspace.Services.DataSourceServices.IDataBaseService>(null).GetOtherLayerItemModels(null);
            }
            
            if (ServiceManager.GetService<IShellService>(null).ContentView.Content != null)
            {
                ToolItemModel toolItemModel = ((FrameworkElement)ServiceManager.GetService<IShellService>(null).ContentView.Content).DataContext as ToolItemModel;
                string content = toolItemModel.Content;
                searchControlViewModel = keyWordSearchView.searchControl.DataContext as SearchControlViewModel;
                if (searchControlViewModel != null)
                {
                    if (content == "警情处理" || NeedBufferGeoVisible)
                        searchControlViewModel.IsBufferGeoVisible = Visibility.Visible;
                    else
                        searchControlViewModel.IsBufferGeoVisible = Visibility.Collapsed;
                }
            }
            keyWordSearchView.Show();
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            KeyWordSearchView keyWordSearchView = (KeyWordSearchView)base.View;
            object dataContext = keyWordSearchView.searchControl.DataContext;
            bool flag = dataContext != null && dataContext is SearchControlViewModel;
            if (flag)
            {
                SearchControlViewModel searchControlViewModel = (SearchControlViewModel)dataContext;
                searchControlViewModel.ClearOldSelectedPOIItem();
            }
            keyWordSearchView.Hide();
        }

        public override void Reset()
        {
            base.Reset();
            base.IsChecked = false;
        }
    }
}