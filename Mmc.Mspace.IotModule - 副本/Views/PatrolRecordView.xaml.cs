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
    /// PatrolRecordView.xaml 的交互逻辑
    /// </summary>
    public partial class PatrolRecordView 
    {
       public Action<string> SelectedDateTimeChange;
        public Action<string> DisplayDateTimeChange;
        public PatrolRecordView()
        {
            InitializeComponent();
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e != null)
            {
                var date = e.Source.ToString();

                //Console.Write(date);
                
                SelectedDateTimeChange(date);
            }
        }

        private void MyCalendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            if (e != null)
            {
                //var year = e.AddedDate.Value.Year;
                //var month = e.AddedDate.Value.Month;
                var month = e.AddedDate.Value.ToString("yyyy-MM");
                //var date = year.ToString("yyyy")+month.ToString("MM");
                //Console.Write(date);

                //SelectedDateTime(date);
                DisplayDateTimeChange(month);
            }
        }
    }
}
