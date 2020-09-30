using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;
using UserControl = System.Windows.Controls.UserControl;

namespace Mmc.Mspace.NavigationModule.Navigation
{
	public partial class NavigationView : UserControl
	{
        public NavigationView()
		{
			this.InitializeComponent();
		}

        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.ToString() == Keys.Z.ToString())
            {
                var model = this.DataContext as NavigationViewModel;
                model.OnAddAnimationPoint();
            }
        }

        private void Results_LostMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
        }

        private void Results_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var model = this.DataContext as NavigationViewModel;
            
        }

        private void Results_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var model = this.DataContext as NavigationViewModel;

            if (e.MouseDevice.Captured != null)
            {
                model.GotMouseCapture = true;
            }
        }
    }
}
