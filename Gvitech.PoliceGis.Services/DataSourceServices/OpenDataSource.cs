using ApplicationConfig;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeDataInterop;
using Mmc.DataSourceAccess;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.DataSourceServices
{
    public class OpenDataSource
    {
        public ImageLayer OpenImageLayer(AxRenderControl renderControl, ImageLayerConfig layercnfg)
        {
            ImageLayer resultLayer = null;
            try
            {
                var conType = layercnfg.GetConType();
                if (conType == gviRasterConnectionType.gviRasterConnectionWMTS)
                {
                    //if (layercnfg.ConnInfoString.Contains("tianditu")) layercnfg.ConnInfoString +=  "?tk=09814f3272f70ea01f45ed8650317252";
                    IRasterSourceFactory rasFac = new RasterSourceFactory();
                    var source = rasFac.OpenRasterSource(layercnfg.ConnInfoString,
                        gviRasterConnectionType.gviRasterConnectionWMTS);
                    var names = source?.GetRasterNames();
                    var raster = source?.OpenRaster(names?[0]);

                    var layer = renderControl.ObjectManager.CreateImageryLayer(raster?.ConnStr,
                        renderControl.ProjectTree.RootID);
                    if (layer == null) return null;
                    var symbol = layer.GetRasterSymbol();
                    var alphaEnabled = true;
                    if (bool.TryParse(layercnfg.AlphaEnabled, out alphaEnabled) && symbol != null)
                    {
                        symbol.AlphaEnabled = alphaEnabled;
                        layer.SetRasterSymbol(symbol);
                    }
                    resultLayer = new ImageLayer(layer)
                    {
                        AliasName = !string.IsNullOrEmpty(names[0]) ? names[0] : layercnfg?.AliasName,
                        Name = !string.IsNullOrEmpty(names[0]) ? names[0] : layercnfg?.AliasName,
                        Guid = !string.IsNullOrEmpty(layercnfg.Guid) ? layercnfg.Guid : layer.Guid.ToString(),
                        IsLocal = layercnfg.IsLocal
                    };
                }
                else if (conType == gviRasterConnectionType.gviRasterConnectionFile)
                {
                    var layer = renderControl.ObjectManager.CreateImageryLayer(
                        layercnfg?.ConnInfoString, renderControl.ProjectTree.RootID);
                    if (layer == null) return null;
                    var symbol = layer.GetRasterSymbol();
                    var alphaEnabled = true;
                    if (bool.TryParse(layercnfg.AlphaEnabled, out alphaEnabled) &&
                        symbol != null)
                    {
                        symbol.AlphaEnabled = alphaEnabled;
                        layer.SetRasterSymbol(symbol);
                    }

                    resultLayer = new ImageLayer(layer)
                    {
                        AliasName = layercnfg?.AliasName,
                        Name = layercnfg?.AliasName,
                        Guid = !string.IsNullOrEmpty(layercnfg.Guid) ? layercnfg.Guid : layer.Guid.ToString(),
                        IsLocal = layercnfg.IsLocal
                    };
                }
            }
            catch
            {
                var err = renderControl.GetLastError();
                SystemLog.Log(err.ToString());
            }
            return resultLayer;
        }

        public ITileLayer Open3DTileLayer(AxRenderControl renderControl, TileLayerConfig layercnfg)
        {
            ITileLayer resultLayer = null;
            try
            {
                var layer = renderControl.ObjectManager.Create3DTileLayer(layercnfg.ConnInfoString, layercnfg.Password,
        renderControl.ProjectTree.RootID);
                var tileLayer = new TileLayer(layer)
                {
                    AliasName = layercnfg?.AliasName,
                    Name = layercnfg?.AliasName,
                    Guid = !string.IsNullOrEmpty(layercnfg.Guid) ? layercnfg.Guid : layer.Guid.ToString(),
                    IsLocal = layercnfg.IsLocal
                };
                resultLayer = tileLayer;
            }
            catch
            {
                var err = renderControl.GetLastError();
                SystemLog.Log(err.ToString());
            }
            return resultLayer;
        }

        public DataSourceService<GvitechFeatureDataSet> OpenFeatureDatasource(AxRenderControl renderControl, LibraryConfig layercnfg)
        {
            DataSourceService<GvitechFeatureDataSet> resultLayer = null;
            //bool openSuccess = false;
            try
            {
                DataSourceService<GvitechFeatureDataSet> datasource = null;

                if (layercnfg.ToConnectionInfo().ConnectionType == gviConnectionType.gviConnectionWFS)
                    datasource = new DataSourceService<GvitechFeatureDataSet>(renderControl, layercnfg.ToConnectionInfo(), layercnfg.Guid);
                else
                    datasource = new DataSourceService<GvitechFeatureDataSet>(renderControl, layercnfg.ToConnectionInfo());

                resultLayer = datasource;
            }
            catch
            {
                var err = renderControl.GetLastError();
                SystemLog.Log(err.ToString());
            }
            return resultLayer;
        }
    }
}
