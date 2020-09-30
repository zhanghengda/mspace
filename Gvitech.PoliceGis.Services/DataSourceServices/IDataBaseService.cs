using Gvitech.CityMaker.Controls;
using Mmc.DataSourceAccess;
using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.DataSourceServices
{
    public interface IDataBaseService
    {
        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <param name="renderControl"></param>
        void Init(AxRenderControl renderControl);

        /// <summary>
        /// 获取3D模型图层
        /// </summary>
        /// <returns></returns>
        List<IDisplayLayer> GetActualityLayers();

        /// <summary>
        /// 获取shp矢量图层
        /// </summary>
        /// <returns></returns>
        List<IDisplayLayer> GetShpLayers();

        /// <summary>
        /// 获取倾斜摄影等瓦片图层
        /// </summary>
        /// <returns></returns>
        List<ITileLayer> GetTileLayers();

        /// <summary>
        /// 获取影像图层
        /// </summary>
        /// <returns></returns>
        List<IImageLayer> GetImageLayers();

        List<IDisplayLayer> GetShowLayers();

        List<LayerItemModel> GetLayerItemModels(string groupName);

        List<LayerItemModel> GetOtherLayerItemModels(string groupName);

        List<LayerItemModel> GetAllLayerItemModels();

        //List<CameraTour> GetCameraTours();

        //List<LocationScene> GetLocationScenes();

        IDisplayLayer GetDisPlayLayerByFCAliasName(string fcName);

        IDisplayLayer GetDisPlayLayerByFCGuid(string guid);

        void ShowPOILayers(bool visible = true);

        Action<string> OnLoadingDataSourceProcess { get; set; }


    }
}