using Gvitech.CityMaker.RenderControl;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public class AreaPoiSelectedModel : BindableBase
    {



        private IRenderPolygon _areaSelectedPolygon;
        public IRenderPolygon AreaSelectedPolygon
        {
            get { return _areaSelectedPolygon; }
            set { _areaSelectedPolygon = value; NotifyPropertyChanged("AreaSelectedPolygon"); }
        }

        private string _wktPoly;
        public string WktPoly
        {
            get { return _wktPoly; }
            set { _wktPoly = value; NotifyPropertyChanged("WktPoly"); }
        }
    }
}
