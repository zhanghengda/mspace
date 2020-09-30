using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Mmc.Wpf.Toolkit.Controls;

namespace Mmc.Mspace.HumanAssociationModule
{

    // Token: 0x02000003 RID: 3
    public partial class FindPeopleView : Window, IComponentConnector
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
		public FindPeopleView()
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

        // Token: 0x06000005 RID: 5 RVA: 0x000020A8 File Offset: 0x000002A8
        private void SearchTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
        }

        //// Token: 0x06000006 RID: 6 RVA: 0x000020AC File Offset: 0x000002AC
        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //public void InitializeComponent()
        //{
        //	bool contentLoaded = this._contentLoaded;
        //	if (!contentLoaded)
        //	{
        //		this._contentLoaded = true;
        //		Uri resourceLocator = new Uri("/Mmc.Mspace.HumanAssociationModule;component/findpeople/findpeopleview.xaml", UriKind.Relative);
        //		Application.LoadComponent(this, resourceLocator);
        //	}
        //}

        //// Token: 0x06000007 RID: 7 RVA: 0x000020E4 File Offset: 0x000002E4
        //[DebuggerNonUserCode]
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            ((Border)target).MouseDown += this.UIElement_OnPreviewMouseDown;
        //            break;
        //        case 2:
        //            ((SearchTextBox)target).PreviewKeyDown += this.SearchTextBox_PreviewKeyDown;
        //            break;
        //        case 3:
        //            this.lstbox = (ListBox)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}

        //// Token: 0x04000001 RID: 1
        //internal ListBox lstbox;

        //// Token: 0x04000002 RID: 2
        //private bool _contentLoaded;
    }
}
