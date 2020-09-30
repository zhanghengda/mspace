using System;
using System.Windows;
using System.Windows.Input;

namespace FireControlModule.FireIot
{
    public partial class PoliceEventExView : Window
    {
        public PoliceEventExView()
        {
            this.InitializeComponent();
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                base.DragMove();
            }
            catch (Exception)
            {
            }
        }
    }
}