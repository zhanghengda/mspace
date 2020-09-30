using System;

namespace Mmc.Windows.Attributes
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	public class AliasAttribute : Attribute
	{
		public string AliasName
		{
			get;
			set;
		}

		public AliasAttribute(string alias)
		{
			bool flag = string.IsNullOrEmpty(alias);
			if (flag)
			{
				throw new ArgumentNullException("alias");
			}
			this.AliasName = alias;
		}
	}
}
