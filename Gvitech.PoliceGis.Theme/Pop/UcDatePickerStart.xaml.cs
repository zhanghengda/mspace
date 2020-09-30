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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mmc.Mspace.Theme.Pop
{
    /// <summary>
    /// UcDatePickerStart.xaml 的交互逻辑
    /// </summary>
    public partial class UcDatePickerStart : UserControl
    {
        public event Action<string> SelectedDateEvent;
        public UcDatePickerStart()
        {
            InitializeComponent();
        }
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            cb1.IsChecked = false;
            string time = e.OriginalSource.ToString();
            if (!string.IsNullOrEmpty(time))
            {
                tb1.Text = time.Substring(0, time.IndexOf(":") - 1);
                SelectedDateEvent(tb1.Text);
            }
            else
            {
                SelectedDateEvent(string.Empty);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = string.Empty;
            cd1.SelectedDate = null;
        }

    }
}
