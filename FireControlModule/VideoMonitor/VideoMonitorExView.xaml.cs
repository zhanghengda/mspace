using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FireControlModule
{
    /// <summary>
    /// VideoMonitorView.xaml 的交互逻辑
    /// </summary>
    public partial class VideoMonitorExView : Window
    {
        public VideoMonitorExView()
        {
            this.InitializeComponent();
           
        }

      

        private string _path;

        private TimeSpan _pausest;

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            bool flag = "oid" == e.Column.Header.ToString().ToLower();
            if (flag)
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try { base.DragMove(); } catch (Exception) { }
        }

        private void Button_CloseCmd(object sender, RoutedEventArgs e)
        {
            base.Hide();
        }
    }
}