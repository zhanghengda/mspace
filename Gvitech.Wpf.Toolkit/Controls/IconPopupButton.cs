using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Mmc.Wpf.Toolkit.Controls
{
	// Token: 0x02000017 RID: 23
	public class IconPopupButton : IconToggleButton
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002B14 File Offset: 0x00000D14
		static IconPopupButton()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(IconPopupButton), new FrameworkPropertyMetadata(typeof(IconPopupButton)));
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002B74 File Offset: 0x00000D74
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002B96 File Offset: 0x00000D96
		public PlacementMode Placement
		{
			get
			{
				return (PlacementMode)base.GetValue(IconPopupButton.PlacementProperty);
			}
			set
			{
				base.SetValue(IconPopupButton.PlacementProperty, value);
			}
		}

		// Token: 0x04000016 RID: 22
		public static readonly DependencyProperty PlacementProperty = DependencyProperty.Register("Placement", typeof(PlacementMode), typeof(IconPopupButton), new PropertyMetadata(PlacementMode.Right));
	}
}
