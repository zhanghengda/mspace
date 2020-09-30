using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Mmc.Wpf.Toolkit.Utils;

namespace Mmc.Wpf.Toolkit.MarkupExtensions
{
	// Token: 0x02000008 RID: 8
	[MarkupExtensionReturnType(typeof(Thickness))]
	public class SetMargin : MarkupExtension
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002297 File Offset: 0x00000497
		public SetMargin(object value)
		{
			this.Value = value;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022A9 File Offset: 0x000004A9
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000022B1 File Offset: 0x000004B1
		[ConstructorArgument("value")]
		public object Value { get; set; }

		// Token: 0x0600001A RID: 26 RVA: 0x000022BC File Offset: 0x000004BC
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			Thickness thickness = (Thickness)(this.Value as Binding).Source;
			Thickness thickness2 = new Thickness(ScreenHelper.GetX(thickness.Left), ScreenHelper.GetY(thickness.Top), ScreenHelper.GetX(thickness.Right), ScreenHelper.GetY(thickness.Bottom));
			return thickness2;
		}
	}
}
