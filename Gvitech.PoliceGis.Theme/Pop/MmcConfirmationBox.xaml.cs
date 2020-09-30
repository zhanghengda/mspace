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

namespace Mmc.Mspace.Theme.Pop
{
    /// <summary>
    /// MmcConfirmationBox.xaml 的交互逻辑
    /// </summary>
    public partial class MmcConfirmationBox : Window
    {
        public MmcConfirmationBox()
        {
            InitializeComponent();
            this.Loaded += MmcConfirmationBox_Loaded;
            this.Closing += MmcConfirmationBox_Closing; ;
        }

        private void MmcConfirmationBox_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void MmcConfirmationBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbMessage.Text = Msg;
            this.tbTitle.Text = Title;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsOk = true;
            this.Close();
        }

        private void BtnCancle_Click(object sender, RoutedEventArgs e)
        {
            IsOk = false;
            this.Close();
        }

        public bool IsOk = false;
        public string Msg { get; set; }
        public string Title { get; set; }

        
    }
}
