using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mmc.Wpf.Toolkit.Controls
{
	// Token: 0x02000015 RID: 21
	public class IconDataButton : Button
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002A20 File Offset: 0x00000C20
		static IconDataButton()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(IconDataButton), new FrameworkPropertyMetadata(typeof(IconDataButton)));
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002AA8 File Offset: 0x00000CA8
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002ACA File Offset: 0x00000CCA
		public StreamGeometry IconGeoData
		{
			get
			{
				return (StreamGeometry)base.GetValue(IconDataButton.IconGeoDataProperty);
			}
			set
			{
				base.SetValue(IconDataButton.IconGeoDataProperty, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002ADC File Offset: 0x00000CDC
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002AFE File Offset: 0x00000CFE
		public IconDataButtonTypes IconDataButtonType
		{
			get
			{
				return (IconDataButtonTypes)base.GetValue(IconDataButton.IconDataButtonTypeProperty);
			}
			set
			{
				base.SetValue(IconDataButton.IconDataButtonTypeProperty, value);
			}
		}

		// Token: 0x04000011 RID: 17
		public static readonly DependencyProperty IconGeoDataProperty = DependencyProperty.Register("IconGeoData", typeof(StreamGeometry), typeof(IconDataButton), new PropertyMetadata());

		// Token: 0x04000012 RID: 18
		public static readonly DependencyProperty IconDataButtonTypeProperty = DependencyProperty.Register("IconDataButtonType", typeof(IconDataButtonTypes), typeof(IconDataButton), new PropertyMetadata(IconDataButtonTypes.Circle));
	}
}
