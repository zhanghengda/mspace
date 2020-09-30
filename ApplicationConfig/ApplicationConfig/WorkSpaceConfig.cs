using ApplicationConfig.FieldFilter;
using ApplicationConfig.FieldMap;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ApplicationConfig
{
	[XmlInclude(typeof(FilterField)), XmlInclude(typeof(TableFieldsFilter)),
        XmlInclude(typeof(FieldsFilters)), XmlInclude(typeof(MapField)),
        XmlInclude(typeof(TableFieldsMap)), XmlInclude(typeof(FieldsMaps)),
        XmlInclude(typeof(LibraryConfig)), XmlInclude(typeof(TerrainConfig)),
        XmlInclude(typeof(TileLayerConfig)),XmlInclude(typeof(ImageLayerConfig))
        ]
	public class WorkSpaceConfig : IWorkSpaceConfig
	{
		public FieldsMaps FieldsMaps { get; set; }
		public FieldsFilters FieldsFilters { get; set; }
		public TerrainConfig SysTerrain { get; set; }
		public List<LibraryConfig> SysPoiLibs { get; set; }
		public List<LibraryConfig> SysActualityLibs { get; set; }
		public List<LibraryConfig> SysShps { get; set; }
        public List<TileLayerConfig> Sys3DTileLayers { get; set; }
        public List<ImageLayerConfig> SysImageLayers { get; set; }
    }
}
