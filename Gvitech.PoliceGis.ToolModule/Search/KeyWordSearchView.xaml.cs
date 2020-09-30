
using System;
using System.Windows;
using System.Windows.Input;

namespace Mmc.Mspace.ToolModule.Search
{
    public partial class KeyWordSearchView 
    {
        public KeyWordSearchView()
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