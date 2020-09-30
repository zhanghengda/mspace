using Mmc.DataSourceAccess;
using System;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services
{
    public interface IJsScriptInvokerService
    {
        #region 图层

        /// <summary>
        /// 设置图层组可见
        /// </summary>
        /// <param name="layerNames"></param>
        /// <param name="isVisilbe"></param>
        void setLayersVisible(string layerNames, bool isVisilbe);

        /// <summary>
        /// 设置图层可见
        /// </summary>
        /// <param name="layerName"></param>
        /// <param name="isVisilbe"></param>
        void setLayerVisible(string layerName, bool isVisilbe);

        #endregion 图层

        #region 建筑

        /// <summary>
        /// 飞入到建筑
        /// </summary>
        /// <param name="buildCode"></param>
        void flyToBuild(string buildCode);

        /// <summary>
        /// 飞入到建筑
        /// </summary>
        /// <param name="layerName"></param>
        /// <param name="buildCode"></param>
        void flyToBuild(string layerName, string buildCode);

        /// <summary>
        /// 建筑详情
        /// </summary>
        /// <param name="buildCode"></param>
        void showBuildDetail(string buildCode);

        #endregion 建筑

        #region 单位

        /// <summary>
        /// 飞入到单位
        /// </summary>
        /// <param name="unitCode"></param>
        void flyToUnit(string unitCode);

        /// <summary>
        /// 单位详情
        /// </summary>
        /// <param name="unitCode"></param>
        void showUnitDetail(string unitCode);

        /// <summary>
        /// 打开全景
        /// </summary>
        /// <param name="unitCode"></param>
        void openUnitVideo(string unitCode);
        /// <summary>
        /// 保存视角
        /// </summary>
        void saveCameraParam(string unitCode);
        #endregion 单位

        #region 人员

        /// <summary>
        /// 人员详情
        /// </summary>
        /// <param name="peopleCode"></param>
        void showPeopleDetial(string peopleCode);

        #endregion 人员

        #region 三色预警

        /// <summary>
        ///  三色开关
        /// </summary>
        /// <param name="color"></param>
        /// <param name="visible"></param>
        void colorVisible(string color, string visible);

        /// <summary>
        /// 三色图层开关
        /// </summary>
        Action<string, string> ColorVisibleEvent { get; set; }

        #endregion 三色预警

        /// <summary>
        /// 左侧面板折叠
        /// </summary>
        /// <param name="isCollopse"></param>
        void collopsePanel(bool isCollopse);

        /// <summary>
        /// 飞入到区域
        /// </summary>
        /// <param name="regionCode">区域编码</param>
        /// <param name="regionType">区域类型</param>
        void flyToRegion(string regionCode, string regionType);

        void deleteMarkerPoi(string poiId);

        /// <summary>
        /// 设置分屏状态
        /// </summary>
        /// <param name="compareViewState">分屏状态，2表示2屏，以此类推</param>
        void setCompareViewState(int compareViewState);

        IRenderLayer AddImageLayer(string fileName,bool issafe);

        void flyToRederLayer(string LayerGuid);

        void deleteImgRenderLayer(string LayerGuid);

        void SetAllLayerUnvisible();

    }
}