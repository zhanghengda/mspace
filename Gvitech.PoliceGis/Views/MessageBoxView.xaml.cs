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

namespace MMC.MSpace.Views
{
    /// <summary>
    /// MessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxView : Window
    {
        public string BtnConfirmContent
        {
            get { return this.btnConfirm.Content.ToString();}
            set { this.btnConfirm.Content = value; }
        }

        public string BtnCancelContent
        {
            get { return this.btnCancel.Content.ToString(); }
            set { this.btnCancel.Content = value; }
        }

        public string Title
        {
            get { return this.tbTitle.Text; }
            set { this.tbTitle.Text = value; }
        }

        public string Message
        {
            get { return this.tbMessage.Text; }
            set { this.tbMessage.Text = value; }
        }

        public Action ConfirmAction;
        public Action CancelAction;

        public MessageBoxView()
        {
            InitializeComponent();
        }

        private void BtnCancle_Click(object sender, RoutedEventArgs e)
        {
            CancelAction?.Invoke();
            this.Close();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            ConfirmAction?.Invoke();
            this.Close();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
