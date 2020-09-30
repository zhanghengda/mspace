using Gvitech.CityMaker.FdeCore;
using System;

namespace ApplicationConfig
{
	public interface ILibraryConfig: IDataProviderConfig
    {

		IConnectionInfo ToConnectionInfo();
	}
}
