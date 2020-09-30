using System;

namespace Mmc.Windows.Services
{
	public interface IServiceProvider
	{
		T GetService<T>(object context = null);

		bool HasService<T>();

		void Initialize();

		void RegisterService<T>(ProvideService provideService);
	}
}
