using Mmc.Mspace.Common.Models;
using Mmc.Mspace.RegularInspectionModule.Views;
using System.Windows;

namespace Mmc.Mspace.RegularInspectionModule.ViewModels
{
    public class ScreenHintVModel : BaseViewModel
    {
        private ScreenHintView _screenHintView;
        private string _nameTxt;
        public string NameTxt
        {
            get { return _nameTxt; }
            set { _nameTxt = value; OnPropertyChanged("NameTxt"); }
        }

        private Visibility _activeTxtVisible;
        public Visibility ActiveTxtVisible
        {
            get { return _activeTxtVisible; }
            set { _activeTxtVisible = value; OnPropertyChanged("ActiveTxtVisible"); }
        }

        private Visibility _nameVisible;
        public Visibility NameVisible
        {
            get { return _nameVisible; }
            set { _nameVisible = value; OnPropertyChanged("NameVisible"); }
        }

        public ScreenHintVModel()
        {
            _screenHintView = new ScreenHintView();
            _screenHintView.DataContext = this;
            _screenHintView.Owner = Application.Current.MainWindow;
            
        }

        public void ShowViewOfActiveScreen(double width, double top, double left)
        {
            ActiveTxtVisible = Visibility.Visible;
            NameVisible = Visibility.Hidden;
            _screenHintView.Width = width;
            _screenHintView.Top = top;
            _screenHintView.Left = left;
            _screenHintView.WindowStartupLocation = WindowStartupLocation.Manual;
            _screenHintView.Show();
        }

        public void ShowViewOfName(double width,double top,double left,string hint)
        {
            ActiveTxtVisible = Visibility.Hidden;
            NameVisible = Visibility.Visible;
            _screenHintView.Width = width;
            _screenHintView.Top = top;
            _screenHintView.Left = left;
            _screenHintView.WindowStartupLocation = WindowStartupLocation.Manual;
            NameTxt = hint;
            _screenHintView.Show();
        }
      
        public void HideView()
        {
            _screenHintView.Hide();
        }

        public void CloseView()
        {
            _screenHintView.Close();
        }
    }
}
