using ApplicationConfig;
using LiteDB;
using Mmc.Mspace.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerPropConfig
{
    public interface ITileLayerProp : IUserInfo
    {
        /// <summary>
        /// 灰度化显示是否开启，默认不开启。取值为：0/1 或 false/true 或 任何可以转化为bool类型的值。
        /// </summary>
        bool GrayShowEnable { get; set; }
        /// <summary>
        ///  灰度化影响因子。默认值是1.5f，取值范围是[0.0f, 255.0f]
        /// </summary>
        float GrayShowScalar { get; set; }
        /// <summary>
        /// 透明度。取值范围是[0.0, 1.0(默认)]
        /// </summary>
        bool Transparency { get; set; }

        string TileLayerGuid { get; set; }

    }

    public class TileLayerProp : ITileLayerProp
    {
        public bool GrayShowEnable { get; set; }
        public float GrayShowScalar { get; set; }
        public bool Transparency { get; set; }
        public string UserName { get; set; }
        public int Id { get; set; }
        public string TileLayerGuid { get; set; }

        public int IsAdministrator { get; set; }
    }
}
