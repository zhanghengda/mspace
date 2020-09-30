using FireControlModule.InsideBuild;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace FireControlModule
{
    public class InsideBuildWebViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.Command = new InsideBuildWebCmd();
            base.ViewType = (ViewType)1;
            (base.Command as InsideBuildWebCmd).CommandCompleted += delegate (object s, EventArgs e)
            {
                this.Layers = ServiceManager.GetService<IDataBaseService>(null).GetLayerItemModels(base.Content);
                this.OtherLayers = ServiceManager.GetService<IDataBaseService>(null).GetOtherLayerItemModels(base.Content);
            };
            this.CloseCmd = new RelayCommand(() =>
            {
                if (this._view != null)
                    this._view.Hide();
            });
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
                bool flag2 = this._view != null;
                if (flag2)
                {
                    this._view.Visibility = Visibility.Collapsed;
                }
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
                        bool flag5 = featureLayer != null && displayLayer.Name == "JZDK" && featureLayer.Guid.ToString() == fcGuid;
                        //bool flag5 = featureLayer != null && displayLayer.AliasName == "建筑" && featureLayer.Guid.ToString() == text;
                        if (flag5)
                        {
                            displayLayer.HighLightFeature(featureId.ToString(), GviMap.MapControl.FeatureManager, 4294901760u);
                            this._oldSelectFc = new KeyValuePair<string, IDisplayLayer>(featureId.ToString(), displayLayer);
                            IFieldInfoCollection fields = this._oldSelectFc.Value.Fc.GetFields();
                            int position = fields.IndexOf("Name");
                            // int position = fields.IndexOf("地址");
                            IRowBuffer row = this._oldSelectFc.Value.Fc.GetRow(featureId);
                            object obj = row.GetValue(position) ?? string.Empty;
                            this.Address = obj.ToString();
                            bool flag6 = this._view == null;
                            //
                            //string buildUrl = "http://58.60.185.51:5759/xfyhpc-ps-web/system.do?toBuildInfoMoreNoLogin&buildid=";
                            // string url = buildUrl + Address;
                            //"file:///G:/code/Police3D/%E9%A1%B9%E7%9B%AE%E6%95%B0%E6%8D%AE/ZHNS_JG_12_8_1/ZHNS_JG_12_8_1.html";
                            string url = @System.AppDomain.CurrentDomain.BaseDirectory + "项目数据/ZHNS_JG_12_8_1/ZHNS_JG_12_8_1.html";
                            Uri uri = new Uri(url);

                            if (flag6)
                            {
                                this._view = new InsideBuildView
                                {
                                    DataContext = this,
                                    Tag = fcGuid,
                                    Owner = ServiceManager.GetService<IShellService>(null).ShellWindow,
                                };
                                this._view.webCtrl.Navigate(uri);

                                this._view.Show();
                            }
                            else
                            {
                                this._view.Left = 0;
                                this._view.Top = 0;
                                this._view.webCtrl.Navigate(uri);
                                this._view.Visibility = Visibility.Visible;
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

        private InsideBuildView _view;

        private int _x;

        private int _y;
    }
}