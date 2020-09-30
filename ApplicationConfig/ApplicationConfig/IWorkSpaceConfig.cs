using System;
using System.Collections.Generic;

namespace ApplicationConfig
{
    public interface IWorkSpaceConfig
    {
        TerrainConfig SysTerrain { get; set; }
        List<LibraryConfig> SysActualityLibs { get; set; }
        List<LibraryConfig> SysPoiLibs { get; set; }
        List<LibraryConfig> SysShps { get; set; }
        List<TileLayerConfig> Sys3DTileLayers { get; set; }

        List<ImageLayerConfig> SysImageLayers { get; set; }
    }
}
