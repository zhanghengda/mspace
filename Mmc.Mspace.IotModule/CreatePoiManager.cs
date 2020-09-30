using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule
{
    public class CreatePoiManager
    {

        public IRenderPOI CreatePoi(DeviceInfoModel DeviceInfo, string imgName)
        {
            try
            {
                var poi = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
                poi.SetPostion(DeviceInfo.longitude, DeviceInfo.latitude);
                poi.Size = 32;
                poi.ShowName = false;
                poi.Name = DeviceInfo.device_name;
                poi.ImageName = string.Format("项目数据\\shp\\IMG_POI\\{0}.png", imgName);
                poi.SpatialCRS = GviMap.SpatialCrs;
                var rPoi = GviMap.ObjectManager.CreateRenderPOI(poi);
                rPoi.DepthTestMode = gviDepthTestMode.gviDepthTestAlways;
                return rPoi;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public void ChangePoiImage(IRenderPOI rPoi, string imgName)
        {
            try
            {
                var poi = rPoi.GetFdeGeometry() as IPOI;
                poi.ImageName = string.Format("项目数据\\shp\\IMG_POI\\{0}.png", imgName);
                rPoi.SetFdeGeometry(poi);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void ChangePoiPosition(IRenderPOI rPoi,DeviceInfoModel deviceInfoModel)
        {
            try
            {
                var poi = rPoi.GetFdeGeometry() as IPOI;
                poi.X = deviceInfoModel.longitude;
                poi.Y = deviceInfoModel.latitude;
                rPoi.SetFdeGeometry(poi);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
