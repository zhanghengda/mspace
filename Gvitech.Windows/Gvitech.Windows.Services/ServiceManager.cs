using System;
using System.Collections.Generic;

namespace Mmc.Windows.Services
{
	public class ServiceManager
	{
		private static readonly Dictionary<Type, ProvideService> serviceProviders = new Dictionary<Type, ProvideService>();

		public static T GetService<T>(object context = null)
		{
			Type typeFromHandle = typeof(T);
			bool flag = !typeFromHandle.IsInterface;
			if (flag)
			{
				throw new ArgumentException("获取的服务必须是接口类型");
			}
			Dictionary<Type, ProvideService> obj = ServiceManager.serviceProviders;
			T result;
			lock (obj)
			{
				bool flag3 = !ServiceManager.serviceProviders.ContainsKey(typeFromHandle);
				if (flag3)
				{
					throw new ArgumentException(string.Format("服务{0}未注册", typeFromHandle.FullName));
				}
				result = (T)((object)ServiceManager.serviceProviders[typeFromHandle](context));
			}
			return result;
		}

		public static bool HasService<T>()
		{
			Dictionary<Type, ProvideService> obj = ServiceManager.serviceProviders;
			bool result;
			lock (obj)
			{
				result = ServiceManager.serviceProviders.ContainsKey(typeof(T));
			}
			return result;
		}

		public static void RegisterService<T>(ProvideService provideService)
		{
			Type typeFromHandle = typeof(T);
			bool flag = !typeFromHandle.IsInterface;
			if (flag)
			{
				throw new ArgumentException("注册的服务必须是接口类型");
			}
			Dictionary<Type, ProvideService> obj = ServiceManager.serviceProviders;
			lock (obj)
			{
				bool flag3 = provideService != null || !ServiceManager.serviceProviders.ContainsKey(typeFromHandle);
				if (flag3)
				{
					ServiceManager.serviceProviders[typeFromHandle] = provideService;
				}
				else
				{
					ServiceManager.serviceProviders.Remove(typeFromHandle);
				}
			}
		}
	}
}
