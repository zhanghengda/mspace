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
using Mmc.Mspace.PoiManagerModule.ViewModels;

namespace Mmc.Mspace.PoiManagerModule.Views
{
    /// <summary>
    /// AccountView.xaml 的交互逻辑
    /// </summary>
    public partial class AccountView 
    {
        AccountViewModel accountViewModel;
        public AccountView()
        {
            InitializeComponent();
        }


        private void Datepicker_SelectedDateEvent(string obj)
        {
            accountViewModel = this.DataContext as AccountViewModel;
            if (!string.IsNullOrEmpty(obj))
            {
                accountViewModel.AccountProblemTime = obj;
            }
            else
            {
                accountViewModel.AccountProblemTime = string.Empty;
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            accountViewModel = this.DataContext as AccountViewModel;
            cb1.IsChecked = false;
            string time = e.OriginalSource.ToString();
            if (!string.IsNullOrEmpty(time))
            {
                tb1.Text = time.Substring(0, time.IndexOf(":") - 1);
                accountViewModel.AccountProblemTime = tb1.Text;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = string.Empty;
            cd1.SelectedDate = null;
        }
    }
}
