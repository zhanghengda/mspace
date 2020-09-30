using System;
using System.Collections.Generic;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using LayerPropConfig;

namespace Mmc.DataSourceAccess
{
	
	public interface IDataSourceService
	{
		bool IsNetLib { get; }

		
		IGeometryRender GeometryRender { get; set; }

		
		ITextRender TextRender { get; set; }

		
		List<IDisplayLayer> DisplayLayers { get; }
		
		IDataSource DataSource { get; }

		
		bool OpenDataSource();

		
		bool CreateFeatureLayers();

        bool CreateFeatureLayers(Dictionary<string, List<FeatureLayerProp>> layerProps);


        IFeatureClass GetFeatureClass(string guid);

		
		List<IFeatureLayer> GetFeatureLayers(string featureClassId);

		
		IFeatureLayer GetFeatureLayer(string guid);

		
		string[] GetFeatueDataSetNames();

        List<string> GetFeatueDataSetGuid();

        string[] GetFeatureClassNames(string dataSetName);

		void FlyToDefault();
	}
}
