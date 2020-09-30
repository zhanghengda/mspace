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
    /// outputView.xaml 的交互逻辑
    /// </summary>
    public partial class AnalysisView : Window
    {
        public Action analysisBtnOnClick;
        public Action showHistoryBtnONClick;
        public Action resultOutputBtnOnClick;
        
        public AnalysisView()
        {
            InitializeComponent();
        }

        private void analysisBtn_Click(object sender, RoutedEventArgs e)
        {

            if (analysisBtnOnClick != null)
            {
                analysisBtnOnClick();
            }
        }
        private void showHistoryBtn_Click(object sender, RoutedEventArgs e)
        {

            if (showHistoryBtnONClick != null)
            {
                showHistoryBtnONClick();
            }
        }


        private void resultOutputBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultOutputBtnOnClick != null)
            {
                resultOutputBtnOnClick();
            }
        }
    }
}
