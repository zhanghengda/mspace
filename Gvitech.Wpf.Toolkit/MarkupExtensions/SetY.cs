using System;
using System.Windows.Data;
using System.Windows.Markup;
using Mmc.Wpf.Toolkit.Utils;

namespace Mmc.Wpf.Toolkit.MarkupExtensions
{
	// Token: 0x0200000A RID: 10
	[MarkupExtensionReturnType(typeof(double))]
	public class SetY : MarkupExtension
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002377 File Offset: 0x00000577
		public SetY(object value)
		{
			this.Value = value;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002389 File Offset: 0x00000589
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002391 File Offset: 0x00000591
		[ConstructorArgument("value")]
		public object Value { get; set; }

		// Token: 0x06000022 RID: 34 RVA: 0x0000239C File Offset: 0x0000059C
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			double size = (double)(this.Value as Binding).Source;
			return ScreenHelper.GetY(size);
		}
	}
}
