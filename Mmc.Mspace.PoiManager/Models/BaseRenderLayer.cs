using Gvitech.CityMaker.RenderControl;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public class BaseRenderLayer: BindableBase
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged("Name"); }
        }

        private string aliasName;

        public string AliasName
        {
            get { return aliasName; }
            set { aliasName = value; NotifyPropertyChanged("AliasName"); }
        }

        private bool isLocal;

        public bool IsLocal
        {
            get { return isLocal; }
            set { isLocal = value; NotifyPropertyChanged("IsLocal"); }
        }

        private string guid;

        public string Guid
        {
            get { return guid; }
            set { guid = value; NotifyPropertyChanged("Guid"); }
        }
        private RenderLayerType layerType;

        public RenderLayerType LayerType
        {
            get { return layerType; }
            set { layerType = value; NotifyPropertyChanged("LayerType"); }
        }

        private IRenderable renderable;

        public IRenderable Renderable
        {
            get { return renderable; }
            set { renderable = value; NotifyPropertyChanged("Renderable"); }
        }
    }
}
