using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Mspace.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Models.TileLayer
{
    public class TileModifyGeo : IUserInfo
    {
        public string Geom { get; set; }
        public string Style { get; set; }
        public string ConStr { get; set; }
        public string HashCode { get; set; }
        public string UserName { get; set; }
        public int Id { get; set; }
        public int IsAdministrator { get; set; }
        public string LabelText { get; set; }
        public string PolygonStr { get; set; }
      
    }
}
