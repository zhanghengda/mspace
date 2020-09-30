using System;
using System.Collections.Generic;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using Mmc.Mspace.Common;

namespace Mmc.DataSourceAccess
{
	
	public interface IDisplayLayer : IShowLayer
	{
		
		// (get) Token: 0x06000055 RID: 85
		IFeatureClass Fc { get; }

		
		// (get) Token: 0x06000056 RID: 86
		List<IFeatureLayer> FLyers { get; }

		
		// (get) Token: 0x06000057 RID: 87
		string Name { get; }

		
		// (get) Token: 0x06000058 RID: 88
		string Guid { get; }

		
		bool AddFeatureLayer(IFeatureLayer fly);

		
		bool AddFeatureLayers(List<IFeatureLayer> flys);
	}
}
