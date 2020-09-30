using System;

namespace Mmc.Windows.Design
{
	public class Singleton<T> where T : class, new()
	{
		private static Lazy<T> _instance;

		private static readonly object syslock = new object();

		public static T Instance
		{
			get
			{
				bool flag = Singleton<T>._instance == null;
				if (flag)
				{
					object obj = Singleton<T>.syslock;
					lock (obj)
					{
						bool flag3 = Singleton<T>._instance == null;
						if (flag3)
						{
							Singleton<T>._instance = new Lazy<T>(true);
						}
					}
				}
				return Singleton<T>._instance.Value;
			}
		}
	}
}
