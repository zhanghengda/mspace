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
using GFramework.BlankWindow;

namespace Mmc.Mspace.RegularInspectionModule.Views
{
    /// <summary>
    /// UploadView.xaml 的交互逻辑
    /// </summary>
    public partial class UploadView : BlankWindow
    {
        public UploadView()
        {
            InitializeComponent();
        }

        public void ChangeButtonEnable(bool isEnable)
        {
            this.Dispatcher.Invoke(() =>
            {
                btnClose.IsEnabled = isEnable;
                btnConfirm.IsEnabled = isEnable;
                btnSelectFile.IsEnabled = isEnable;
                btnTopClose.IsEnabled = isEnable;
            });
        }
    }
}
