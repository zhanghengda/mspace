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
    public interface IFeatureLayerProp : IUserInfo
    {
        string GeoRender { get; set; }
        string TxtRender { get; set; }
        double MaxVisibleDistance { get; set; }
        float MinVisiblePixels { get; set; }
        bool ForceCullMode { get; set; }
        string GeometryFiledName { get; set; }
        string LayerName { get; set; }
        string FcGuid { get; set; }
        string FcName { get; set; }
        string DataSetGuid { get; set; }
        string DataSourceGuid { get; set; }

    }
    public class FeatureLayerProp : IFeatureLayerProp
    {
        public int Id { get; set; }
        public string GeoRender { get; set; }
        public string TxtRender { get; set; }
        public double MaxVisibleDistance { get; set; }
        public float MinVisiblePixels { get; set; }
        public bool ForceCullMode { get; set; }
        public string GeometryFiledName { get; set; }
        public string LayerName { get; set; }
        public string UserName { get; set; }
        
        public string FcGuid { get; set; }
        public string FcName { get; set; }
        public string DataSetGuid { get; set; }
        public string DataSourceGuid { get; set; }
        public int IsAdministrator { get; set; }
    }
}
