using Mmc.Mspace.PoiManagerModule.ViewModels;
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

namespace Mmc.Mspace.PoiManagerModule.Views
{
    /// <summary>
    /// MarksManagementView.xaml 的交互逻辑
    /// </summary>
    public partial class MarksManagementView : UserControl
    {
        public event Action<string> OnSelectedDateEvent;
        public  MarksManagementVModel model;
        public MarksManagementView()
        {
            InitializeComponent();
        }


        private void Datepicker_StartSelectedDateEvent(string obj)
        {
            model = this.DataContext as MarksManagementVModel;
            if(!string.IsNullOrEmpty(obj))
            {
                model.StartDate = Convert.ToDateTime(obj);
            }
            else
            {
                model.StartDate = null;
            }
            
        }

        private void UcDatePiceker_EndSelectedDateEvent(string obj)
        {
            model = this.DataContext as MarksManagementVModel;
            if (!string.IsNullOrEmpty(obj))
            {
                model.EndDate = Convert.ToDateTime(obj);
            }
            else
            {
                model.EndDate = null;
            }
            
        }
    }
}
