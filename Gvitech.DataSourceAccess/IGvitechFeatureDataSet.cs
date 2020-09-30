using System;
using System.Collections.Generic;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;

namespace Mmc.DataSourceAccess
{
	
	public interface IGvitechFeatureDataSet
	{
		
		// (get) Token: 0x0600005B RID: 91
		IFeatureDataSet FeatureDataSet { get; }

		
		// (get) Token: 0x0600005C RID: 92
		List<IObjectClass> ObjectClasss { get; }

		
		// (get) Token: 0x0600005D RID: 93
		List<IDisplayLayer> Layers { get; }

		
		// (get) Token: 0x0600005E RID: 94
		List<IFeatureClass> FeatureClasss { get; }

		
		// (get) Token: 0x0600005F RID: 95
		List<IFeatureLayer> FeatureLayer { get; }

		
		bool OpenFdsObjectClasss();

		
		bool OpenFdsFeatureClasss();

		
		bool CreateFdsFeatureLayers(IObjectManager om, IGeometryRender geoRender, ITextRender txtRender, string geoField = "Geometry");

       bool CreateFdsFeatureLayers(IObjectManager om, Dictionary<string, Tuple<IGeometryRender, ITextRender, string>> layerProps);

    }
}
