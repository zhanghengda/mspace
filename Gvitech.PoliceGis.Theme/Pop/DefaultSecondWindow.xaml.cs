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
    /// DefaultSecondWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DefaultSecondWindow 
    {
        public DefaultSecondWindow()
        {
            InitializeComponent();
        }

        public void NavigateToPager(object data)
        {
            var arg = data as SecondNotification;
            if (arg == null)
                return;

            this.DataContext = data;
            this.Height = arg.Height;
            this.Width = arg.Width;
            this.Owner = Application.Current.MainWindow;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.Show();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void CloseWindow()
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.CloseWindow();
        }
    }
    public class SecondNotification
    {
        public SecondNotification(string title, object content, double width, double height)
        {
            this.Title = title;
            this.Width = width;
            this.Height = height;
            this.Content = content;
        }
        public double Width { get; set; }
        public double Height { get; set; }

        public string Title { get; set; }
 
        public object Content { get; set; }
    }
}
