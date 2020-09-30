using Gvitech.CityMaker.FdeDataInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApplicationConfig
{
    public class ImageLayerConfig : DataProviderConfig
    {
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 更新周期，以 天 为单位
        /// </summary>
        public double CycleTime { get; set; }
        /// <summary>
        /// 是否设置透明
        /// </summary>
        [XmlAttribute]
        public string AlphaEnabled { get; set; } = "false";  //A通道默认为true
        [XmlAttribute]
        public string ConType { get; set; }
       
        public gviRasterConnectionType GetConType()
        {
            if (ConType == "WMTS")
                return gviRasterConnectionType.gviRasterConnectionWMTS;
            else if (ConType == "File")
                return gviRasterConnectionType.gviRasterConnectionFile;
            else if (ConType == "WMS")
                return gviRasterConnectionType.gviRasterConnectionWMS;
            return gviRasterConnectionType.gviRasterConnectionUnknown;
        }
    }
}
