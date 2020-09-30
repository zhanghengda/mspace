using Gvitech.CityMaker.RenderControl;
using Mmc.Wpf.Mvvm;
using System.Collections.Generic;
using System.Windows.Media;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public  class RenderLayerModel: BindableBase
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
        private List<RenderLayerModel> rederlayers;

        public List<RenderLayerModel> Rederlayers
        {
            get { return rederlayers; }
            set { rederlayers = value; NotifyPropertyChanged("Rederlayers"); }
        }

        private int _childCount;

        public int ChildCount
        {
            get { return _childCount; }
            set { _childCount = value; NotifyPropertyChanged("ChildCount"); }
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

        private string _menuLevel;

        public string MenuLevel
        {
            get { return _menuLevel; }
            set { _menuLevel = value; NotifyPropertyChanged("MenuLevel"); }
        }
        private string _alphaStation;

        public string AlphaStation
        {
            get { return _alphaStation; }
            set { _alphaStation = value; NotifyPropertyChanged("AlphaStation"); }
        }
        private bool _alphaBtnOn;

        public bool AlphaBtnOn
        {
            get { return _alphaBtnOn; }
            set { _alphaBtnOn = value; NotifyPropertyChanged("AlphaBtnOn"); }
        }
        

        private bool _isSelectedl;

        public bool IsSelected
        {
            get { return _isSelectedl; }
            set { _isSelectedl = value; NotifyPropertyChanged("IsSelected"); }
        }

        private bool _isDisplay;

        public bool IsDisplay
        {
            get { return _isDisplay; }
            set { _isDisplay = value; NotifyPropertyChanged("IsDisplay"); }
        }

        private string _parentName;

        public string ParentName
        {
            get { return _parentName; }
            set { _parentName = value; NotifyPropertyChanged("ParentName"); }
        }
        private ImageSource _dataPath;

        public ImageSource DataPath
        {
            get { return _dataPath; }
            set { _dataPath = value; NotifyPropertyChanged("DataPath"); }
        }

        private bool _hasPathData;

        public bool HasPathData
        {
            get { return _hasPathData; }
            set { _hasPathData = value; NotifyPropertyChanged("HasPathData"); }
        }

        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; NotifyPropertyChanged("IsChecked"); }
        }

        private bool _isSymbolic;

        public bool IsSymbolic
        {
            get { return _isSymbolic; }
            set { _isSymbolic = value; NotifyPropertyChanged("IsSymbolic"); }
        }

        private bool _firstEyeStatus= true;

        public bool FirstEyeStatus
        {
            get { return _firstEyeStatus; }
            set { _firstEyeStatus = value; NotifyPropertyChanged("FirstEyeStatus"); }
        }
        private bool _twoEyeStatus = true;

        public bool TwoEyeStatus
        {
            get { return _twoEyeStatus; }
            set { _twoEyeStatus = value; NotifyPropertyChanged("TwoEyeStatus"); }
        }
        private bool _threeEyeStatus = true;

        public bool ThreeEyeStatus
        {
            get { return _threeEyeStatus; }
            set { _threeEyeStatus = value; NotifyPropertyChanged("ThreeEyeStatus"); }
        }
        private bool _fourEyeStatus = true;

        public bool FourEyeStatus
        {
            get { return _fourEyeStatus; }
            set { _fourEyeStatus = value; NotifyPropertyChanged("FourEyeStatus"); }
        }


        public static RenderLayerModel CreateRenderLayer(BaseRenderLayer renderLayer,string parentName="")
        {
            return new RenderLayerModel()
            {
                Name = renderLayer.Name,
                AliasName = renderLayer.AliasName,
                IsLocal = renderLayer.IsLocal,
                Guid = renderLayer.Guid,
                LayerType = renderLayer.LayerType,
                Renderable = renderLayer.Renderable,
                ParentName= parentName,
                MenuLevel = "3",
                IsDisplay = true,
                IsChecked = false,
                HasPathData = true,
                IsSymbolic=true,
            };
        }
    }
}
