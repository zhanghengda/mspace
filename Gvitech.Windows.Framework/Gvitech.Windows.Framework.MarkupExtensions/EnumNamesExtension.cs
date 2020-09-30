using Mmc.Windows.Attributes;
using Mmc.Windows.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;

namespace Mmc.Windows.Framework.MarkupExtensions
{
	[MarkupExtensionReturnType(typeof(object[]))]
	public class EnumNamesExtension : MarkupExtension
	{
		[ConstructorArgument("enumType")]
		public Type EnumType
		{
			get;
			set;
		}

		public EnumNamesExtension()
		{
		}

		public EnumNamesExtension(Type enumType)
		{
			this.EnumType = enumType;
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			if (this.EnumType == null)
			{
				throw new ArgumentException("The enum type is not set");
			}
			string[] items = Enum.GetNames(this.EnumType);
			object result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				AliasAttribute aliasAtt = null;
				IEnumerable<EnumProvider> vr = items.Select(delegate(string item)
				{
					aliasAtt = (Attribute.GetCustomAttribute(this.EnumType.GetField(item), typeof(AliasAttribute), false) as AliasAttribute);
					return new EnumProvider
					{
						Value = item,
						Alias = (aliasAtt != null) ? aliasAtt.AliasName : item
					};
				});
				result = ((vr != null) ? vr.ToList<EnumProvider>() : new List<EnumProvider>());
			}
			return result;
		}
	}
}
