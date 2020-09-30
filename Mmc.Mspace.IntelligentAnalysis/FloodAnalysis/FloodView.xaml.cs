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
using Mmc.Mspace;
namespace Mmc.Mspace.IntelligentAnalysisModule
{
    /// <summary>
    /// Flood.xaml 的交互逻辑
    /// </summary>
    public partial class FloodView
    {
        public FloodView()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            this.IsEnabledChanged += LoginWindow_IsEnabledChanged;
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void LoginWindow_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false)
            {
                this.Close();
            }
        }

        private void buttonMouseOver_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
