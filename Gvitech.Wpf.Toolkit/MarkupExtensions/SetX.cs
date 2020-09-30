using System;
using System.Windows.Data;
using System.Windows.Markup;
using Mmc.Wpf.Toolkit.Utils;

namespace Mmc.Wpf.Toolkit.MarkupExtensions
{
	// Token: 0x02000009 RID: 9
	[MarkupExtensionReturnType(typeof(double))]
	public class SetX : MarkupExtension
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002321 File Offset: 0x00000521
		public SetX(object value)
		{
			this.Value = value;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002333 File Offset: 0x00000533
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000233B File Offset: 0x0000053B
		[ConstructorArgument("value")]
		public object Value { get; set; }

		// Token: 0x0600001E RID: 30 RVA: 0x00002344 File Offset: 0x00000544
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			double size = (double)(this.Value as Binding).Source;
			return ScreenHelper.GetX(size);
		}
	}
}
