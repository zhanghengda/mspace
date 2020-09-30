using Mmc.Windows.Design;
using Mmc.Windows.Framework.Utils;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Mmc.Windows.Framework.MarkupExtensions
{
	[MarkupExtensionReturnType(typeof(Thickness))]
	public class SetMargin : MarkupExtension
	{
		[ConstructorArgument("value")]
		public object Value
		{
			get;
			set;
		}

		public SetMargin(object value)
		{
			this.Value = value;
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			Thickness y = (Thickness)(this.Value as Binding).Source;
			Thickness newthickness = new Thickness(Singleton<ScreenHelper>.Instance.GetX(y.Left), Singleton<ScreenHelper>.Instance.GetY(y.Top), Singleton<ScreenHelper>.Instance.GetX(y.Right), Singleton<ScreenHelper>.Instance.GetY(y.Bottom));
			return newthickness;
		}
	}
}
