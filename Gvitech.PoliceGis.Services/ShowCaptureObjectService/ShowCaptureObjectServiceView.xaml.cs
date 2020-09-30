using Mmc.Framework.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Mmc.Mspace.Services.ShowCaptureObjectService
{
    public partial class ShowCaptureObjectServiceView : UserControl
    {
        public ShowCaptureObjectServiceView()
        {
            this.InitializeComponent();
        }

        private void PopupOpend(object sender, EventArgs e)
        {
            GviMap.AxMapControl.Focus();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            bool flag = e.Column is DataGridTextColumn;
            if (flag)
            {
                DataGridTextColumn dataGridTextColumn = (DataGridTextColumn)e.Column;
                dataGridTextColumn.ElementStyle = (Style)base.Resources["ElementStyle"];
            }
        }
    }
}