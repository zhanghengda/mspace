using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using Mmc.Windows.Attributes;

namespace Mmc.Wpf.Toolkit.MarkupExtensions
{
	// Token: 0x02000006 RID: 6
	[MarkupExtensionReturnType(typeof(object[]))]
	public class EnumNamesExtension : MarkupExtension
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021C6 File Offset: 0x000003C6
		public EnumNamesExtension()
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D0 File Offset: 0x000003D0
		public EnumNamesExtension(Type enumType)
		{
			this.EnumType = enumType;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021E2 File Offset: 0x000003E2
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021EA File Offset: 0x000003EA
		[ConstructorArgument("enumType")]
		public Type EnumType { get; set; }

		/// <summary>
		///     获取枚举每一项的别名的集合
		/// </summary>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		// Token: 0x06000011 RID: 17 RVA: 0x000021F4 File Offset: 0x000003F4
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			bool flag = this.EnumType == null;
			if (flag)
			{
				throw new ArgumentException("The enum type is not set");
			}
			string[] names = Enum.GetNames(this.EnumType);
			bool flag2 = names == null;
			object result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				AliasAttribute aliasAtt = null;
				IEnumerable<EnumProvider> enumerable = names.Select(delegate(string item)
				{
					aliasAtt = (Attribute.GetCustomAttribute(this.EnumType.GetField(item), typeof(AliasAttribute), false) as AliasAttribute);
					return new EnumProvider
					{
						Value = item,
						Alias = ((aliasAtt != null) ? aliasAtt.AliasName : item)
					};
				});
				result = ((enumerable != null) ? enumerable.ToList<EnumProvider>() : new List<EnumProvider>());
			}
			return result;
		}
	}
}
