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
    public interface IImageLayerProp:IUserInfo
    {
        string RasterSymbol { get; set; }
        string ImgaeGuid { get; set; }

    }

    public class ImageLayerProp : IImageLayerProp
    {
        public string RasterSymbol { get; set; }
        public string ImgaeGuid { get; set; }
        public string UserName { get; set; }
        public int Id { get; set; }
        public int IsAdministrator { get; set; }
    }
}
