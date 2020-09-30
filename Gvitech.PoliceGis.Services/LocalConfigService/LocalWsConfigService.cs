using ApplicationConfig;
using LayerPropConfig;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmc.Mspace.Services.LayerGroupService;
using Mmc.Mspace.Models.TileLayer;

using Mmc.Mspace.Models.DynamicClipData;
using Mmc.Mspace.Models.Navigation;
using Mmc.Mspace.Models.AddressManagementModel;
namespace Mmc.Mspace.Services.LocalConfigService
{
    public class LocalWsConfigService : Singleton<LocalWsConfigService>, ILocalWsConfigService
    {
        private readonly string ShpLibConfigKey = "ShpLibConfigKey";
        private readonly string TileLibConfigKey = "TileLibConfigKey";
        private readonly string ImageLibConfigKey = "ImageLibConfigKey";
        private readonly string ActualConfigKey = "ActualConfigKey";
        private readonly string CrsConfigKey = "CrsConfigKey";
        private readonly string TerrainConfigKey = "TerrainConfigKey";

        private readonly string FeatureLayerPropsConfigKey = "FeatureLayerPropsConfigKey";
        private readonly string ImageLayerPropsConfigKey = "ImageLayerPropsConfigKey";
        private readonly string TileLayerPropsConfigKey = "TileLayerPropsConfigKey";
        private readonly string GroupLayerCfgsKey = "GroupLayerCfgsKey";
        private readonly string TileModifyGeoKey = "TileModifyGeoKey";
        private readonly string ClipDataConfigKey = "ClipDataConfigKey";
        private readonly string CameraTourDatasConfigKey = "CameraTourDatasConfigKey";
        private readonly string AddressManagementDatasConfigKey = "AddressManagementDatasConfigKey";
        private readonly string LastSearchOfLabelsConfigKey = "LastSerachOfLabelsConfigKey";
        private readonly LiteDbHelper _liteDbHelper;

        public string SearchLayerTag { get; private set; }

        public static ILocalWsConfigService GetDefault(object args = null)
        {
            return Instance;
        }

        public DataSourceConfigCols<LibraryConfig> ShpLibs { get; private set; }
        public DataSourceConfigCols<LibraryConfig> ActaulLibs { get; private set; }
        public DataSourceConfigCols<ImageLayerConfig> ImgCfgs { get; private set; }
        public DataSourceConfigCols<TileLayerConfig> TileCfgs { get; private set; }
        public BaseLiteDbKv<TerrainConfig> TerrainConfig { get; private set; }
        public BaseLiteDbKv<CrsConfig> CrsConfig { get; private set; }
        public BaseConfigCols<FeatureLayerProp> FeatureLayerProps { get; private set; }
        public BaseConfigCols<ImageLayerProp> ImageLayerProps { get; private set; }
        public BaseConfigCols<TileLayerProp> TileLayerProps { get; private set; }
        public LocalGroupLayerCfg<LayerGroup> GroupLayerCfgs { get; private set; }
        public BaseConfigCols<TileModifyGeo> TileModifyGeos { get; private set; }
        public BaseConfigCols<CameraTourData> CameraTourDatas { get; private set; }
        public BaseConfigCols<AddressInfoModel> AddressManagementDatas { get; private set; }
        public BaseConfigCols<LastSearchOfLabelService.LastSearchOfLabelService> LastSearchOfLabels { get; private set; }
        public BaseConfigCols<ClipData> ClipDatas { get; private set; }
        public LocalWsConfigService()
        {
            string path = System.Windows.Forms.Application.LocalUserAppDataPath + "\\" + ConfigPath.ConfigDb;
            _liteDbHelper = new LiteDbHelper();
            _liteDbHelper.OpenDb(path);
            CurUserName = CacheData.UserInfo.username;
            ShpLibs = new DataSourceConfigCols<LibraryConfig>(_liteDbHelper, ShpLibConfigKey, CurUserName);
            ActaulLibs = new DataSourceConfigCols<LibraryConfig>(_liteDbHelper, ActualConfigKey, CurUserName);
            ImgCfgs = new DataSourceConfigCols<ImageLayerConfig>(_liteDbHelper, ImageLibConfigKey, CurUserName);
            TileCfgs = new DataSourceConfigCols<TileLayerConfig>(_liteDbHelper, TileLibConfigKey, CurUserName);
            CrsConfig = new BaseLiteDbKv<CrsConfig>(_liteDbHelper, CrsConfigKey, CurUserName);
            TerrainConfig = new BaseLiteDbKv<TerrainConfig>(_liteDbHelper, TerrainConfigKey, CurUserName);
            FeatureLayerProps = new BaseConfigCols<FeatureLayerProp>(_liteDbHelper, FeatureLayerPropsConfigKey, CurUserName);
            ImageLayerProps = new BaseConfigCols<ImageLayerProp>(_liteDbHelper, ImageLayerPropsConfigKey, CurUserName);
            TileLayerProps = new BaseConfigCols<TileLayerProp>(_liteDbHelper, TileLayerPropsConfigKey, CurUserName);
            GroupLayerCfgs = new LocalGroupLayerCfg<LayerGroup>(_liteDbHelper, GroupLayerCfgsKey, CurUserName);
            TileModifyGeos = new BaseConfigCols<TileModifyGeo>(_liteDbHelper, TileModifyGeoKey, CurUserName);
            CameraTourDatas = new BaseConfigCols<CameraTourData>(_liteDbHelper, CameraTourDatasConfigKey, CurUserName);
            AddressManagementDatas = new BaseConfigCols<AddressInfoModel>(_liteDbHelper, AddressManagementDatasConfigKey, CurUserName);
            LastSearchOfLabels = new BaseConfigCols<LastSearchOfLabelService.LastSearchOfLabelService>(_liteDbHelper, LastSearchOfLabelsConfigKey, CurUserName);
            ClipDatas = new BaseConfigCols<ClipData>(_liteDbHelper, ClipDataConfigKey, CurUserName);
            InitWorkSpace();
        }

        private void InitWorkSpace()
        {
            SearchLayerTag = "SearchLayer";
            if (!GroupLayerCfgs.Exist(p => p.GroupName == SearchLayerTag))
            {
                var deFaultTag = new LayerGroup() { GroupName = SearchLayerTag, Alis = "查询图层" };
                GroupLayerCfgs.Add(deFaultTag);
            }
        }

        public string CurUserName { get; private set; }

        
    }




}
