using System;
using System.Windows.Markup;

namespace Mmc.Wpf.Toolkit.Converters
{
	/// <summary>
	///     支持标记扩展的转换器基类。
	/// </summary>
	/// <typeparam name="T">类型参数。</typeparam>
	// Token: 0x0200000D RID: 13
	public class CoreConverter<T> : MarkupExtension
	{
		/// <summary>
		///     获取相应类型的实例对象。
		/// </summary>
		/// <value>实例对象。</value>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000270C File Offset: 0x0000090C
		public static T Instance
		{
			get
			{
				bool flag = CoreConverter<T>._instance == null;
				if (flag)
				{
					object syslock = CoreConverter<T>.Syslock;
					lock (syslock)
					{
						bool flag3 = CoreConverter<T>._instance == null;
						if (flag3)
						{
							CoreConverter<T>._instance = new Lazy<T>(true);
						}
					}
				}
				return CoreConverter<T>._instance.Value;
			}
		}

		/// <summary>
		///     返回默认的转换器对象。
		/// </summary>
		/// <param name="serviceProvider">服务提供者。</param>
		/// <returns>转换器对象。</returns>
		// Token: 0x06000029 RID: 41 RVA: 0x00002784 File Offset: 0x00000984
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return CoreConverter<T>.Instance;
		}

		/// <summary>
		///     实例对象。
		/// </summary>
		// Token: 0x0400000D RID: 13
		private static Lazy<T> _instance;

		/// <summary>
		///     访问锁。
		/// </summary>
		// Token: 0x0400000E RID: 14
		private static readonly object Syslock = new object();
	}
}
