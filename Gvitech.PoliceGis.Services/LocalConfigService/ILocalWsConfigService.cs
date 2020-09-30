using ApplicationConfig;
using LayerPropConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmc.Mspace.Services.LayerGroupService;
using Mmc.Mspace.Models.TileLayer;
using Mmc.Mspace.Models.Navigation;
using Mmc.Mspace.Models.AddressManagementModel;

namespace Mmc.Mspace.Services.LocalConfigService
{
    public interface ILocalWsConfigService
    {
        string CurUserName { get; }
        DataSourceConfigCols<LibraryConfig> ShpLibs { get; }
        DataSourceConfigCols<LibraryConfig> ActaulLibs { get; }
        DataSourceConfigCols<ImageLayerConfig> ImgCfgs { get; }
        DataSourceConfigCols<TileLayerConfig> TileCfgs { get; }
        BaseLiteDbKv<CrsConfig> CrsConfig { get; }
        BaseLiteDbKv<TerrainConfig> TerrainConfig { get; }

        BaseConfigCols<FeatureLayerProp> FeatureLayerProps { get; }
        BaseConfigCols<ImageLayerProp> ImageLayerProps { get; }
        BaseConfigCols<TileLayerProp> TileLayerProps { get; }
        LocalGroupLayerCfg<LayerGroup> GroupLayerCfgs { get; }
        BaseConfigCols<CameraTourData> CameraTourDatas { get;  }
        BaseConfigCols<AddressInfoModel> AddressManagementDatas { get; }
        BaseConfigCols<TileModifyGeo> TileModifyGeos { get; }
        BaseConfigCols<LastSearchOfLabelService.LastSearchOfLabelService> LastSearchOfLabels { get; }
        BaseConfigCols<Models.DynamicClipData.ClipData> ClipDatas { get; }
        string SearchLayerTag { get; }

    }
}
