using System;
using System.Windows.Markup;

namespace Mmc.Windows.Framework.Converters
{
	public class CoreConverter<T> : MarkupExtension
	{
		private static Lazy<T> _instance;

		private static readonly object Syslock = new object();

		public static T Instance
		{
			get
			{
				if (CoreConverter<T>._instance == null)
				{
					lock (CoreConverter<T>.Syslock)
					{
						if (CoreConverter<T>._instance == null)
						{
							CoreConverter<T>._instance = new Lazy<T>(true);
						}
					}
				}
				return CoreConverter<T>._instance.Value;
			}
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return CoreConverter<T>.Instance;
		}
	}
}
