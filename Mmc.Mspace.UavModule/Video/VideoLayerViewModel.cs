using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.UavModule.UavTracing;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.UavModule.Video
{
    public class VideoLayerViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
          
            base.ViewType = (ViewType)1;
            this._videoViewModel = new UavVideoViewModel();


        }

        public List<LayerItemModel> Layers { get; set; }

        public List<LayerItemModel> OtherLayers { get; set; }

        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        public string Address
        {
            get
            {
                return this._address;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._address, value, "Address");
            }
        }

        protected virtual bool RenderControl_RcMouseHover(uint flags, int x, int y)
        {
            bool flag = this._x == x && this._y == y;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                this.UnHighLightBuilding();
                this._x = x;
                this._y = y;
                bool flag3 = this._layers.Count == 0;
                if (flag3)
                {
                    this._layers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
                }
                IPoint point;
                IPickResult pickResult = GviMap.MapControl.Camera.ScreenToWorld(x, y, out point);
                bool flag4 = pickResult is IFeatureLayerPickResult;
                if (flag4)
                {
                    IFeatureLayerPickResult featureLayerPickResult = pickResult as IFeatureLayerPickResult;
                    int featureId = featureLayerPickResult.FeatureId;
                    string fcGuid = featureLayerPickResult.FeatureLayer.Guid.ToString();
                    foreach (IDisplayLayer displayLayer in this._layers)
                    {
                        IFeatureLayer featureLayer = displayLayer.FLyers.FirstOrDefault<IFeatureLayer>();
                        bool flag5 = featureLayer != null && displayLayer.Name == "视频监控点" && featureLayer.Guid.ToString() == fcGuid;
                        //bool flag5 = featureLayer != null && displayLayer.AliasName == "建筑" && featureLayer.Guid.ToString() == text;
                        if (flag5)
                        {
                            displayLayer.HighLightFeature(featureId.ToString(), GviMap.MapControl.FeatureManager, 4294901760u);
                            this._oldSelectFc = new KeyValuePair<string, IDisplayLayer>(featureId.ToString(), displayLayer);
                            IFieldInfoCollection fields = this._oldSelectFc.Value.Fc.GetFields();
                            int position = fields.IndexOf("video_url");
                            // int position = fields.IndexOf("地址");
                            IRowBuffer row = this._oldSelectFc.Value.Fc.GetRow(featureId);
                            object obj = row.GetValue(position) ?? string.Empty;
                            this.Address = obj.ToString();
                            bool flag6 = this._videoViewModel == null;

                            string url = obj.ToString();
                            Uri uri = new Uri(url);

                            if (this._videoViewModel!=null)
                            {
                                this._videoViewModel.Width = 500;
                                this._videoViewModel.Url = url;
                                this._videoViewModel.OnChecked();
                            }
                           
                            return false;
                        }
                    }
                }
                result = false;
            }
            return result;
        }

        private void UnHighLightBuilding()
        {
            bool flag = !string.IsNullOrEmpty(this._oldSelectFc.Key);
            if (flag)
            {
                this._oldSelectFc.Value.UnHighLightFeature(this._oldSelectFc.Key, GviMap.MapControl.FeatureManager);
            }
        }

        public override void OnChecked()
        {
            base.OnChecked();
            this.UnRegexEvent();
            GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
            GviMap.AxMapControl.RcMouseHover += new _IRenderControlEvents_RcMouseHoverEventHandler(RenderControl_RcMouseHover);
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            this.UnHighLightBuilding();
            this.UnRegexEvent();
        }

        private void UnRegexEvent()
        {
            GviMap.AxMapControl.RcMouseHover -= new _IRenderControlEvents_RcMouseHoverEventHandler(RenderControl_RcMouseHover);
            GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
        }

        public override void Reset()
        {
            base.Reset();
            base.IsChecked = false;
        }

        private List<IDisplayLayer> _layers = new List<IDisplayLayer>();

        private KeyValuePair<string, IDisplayLayer> _oldSelectFc;

        private string _filterText;

        private string _address;

        private UavVideoViewModel _videoViewModel;

        private int _x;

        private int _y;

    }
}
