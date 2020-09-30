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

namespace Mmc.Mspace.IntelligentAnalysisModule
{
    /// <summary>
    /// ImportImgView.xaml 的交互逻辑
    /// </summary>
    public partial class ImportImgView : Window
    {
        public Action OnBtnClick;

        public Action importDataBtnOnClick;
        public Action showHistoryBtnOnClick;
        public ImportImgView()
        {
            InitializeComponent();
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (OnBtnClick != null)
            {
                OnBtnClick();
            }
        }

        private void importDataBtn_Click(object sender, RoutedEventArgs e)
        {
            if (importDataBtnOnClick != null)
            {
                importDataBtnOnClick();
            }
        }

        private void showHistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (showHistoryBtnOnClick != null)
            {
                showHistoryBtnOnClick();
            }
        }
    }
}
