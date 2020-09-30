using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Models.Human;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Mmc.Mspace.HumanAssociationModule.findpeople;
using Gvitech.CityMaker.Controls;

namespace Mmc.Mspace.HumanAssociationModule
{
    // Token: 0x02000004 RID: 4
    public class FindPeopleViewModel : CheckedToolItemModel
    {
        // Token: 0x06000008 RID: 8 RVA: 0x00002154 File Offset: 0x00000354
        public override void Initialize()
        {
            base.Initialize();
            base.Command = new FindPeopleCmd();
            base.ViewType = (ViewType)1;
            (base.Command as FindPeopleCmd).CommandCompleted += delegate (object s, EventArgs e)
            {
                this.Layers = ServiceManager.GetService<IDataBaseService>(null).GetLayerItemModels(base.Content);
                this.OtherLayers = ServiceManager.GetService<IDataBaseService>(null).GetOtherLayerItemModels(base.Content);
            };
            this.CloseCmd = new RelayCommand(() =>
            {
                bool flag = this._view != null;
                if (flag)
                {
                    this._view.Hide();
                }
            });
        }

        // Token: 0x06000009 RID: 9 RVA: 0x000021AC File Offset: 0x000003AC
        private void FilterPeople(string filterText)
        {
            bool flag = string.IsNullOrEmpty(filterText);
            if (flag)
            {
                this.PopulationInfos = this.ResultPopulationInfos;
            }
            else
            {
                List<PopulationInfo> list = new List<PopulationInfo>();
                foreach (PopulationInfo populationInfo in this.PopulationInfos)
                {
                    bool flag2 = false;
                    PropertyInfo[] properties = typeof(PopulationInfo).GetProperties();
                    int num;
                    for (int i = 0; i < properties.Length; i = num + 1)
                    {
                        PropertyInfo propertyInfo;
                        bool flag3 = (propertyInfo = properties[i]) == null;
                        if (!flag3)
                        {
                            object value = propertyInfo.GetValue(populationInfo);
                            flag2 |= (value != null && value.ToString().Contains(filterText));
                        }
                        num = i;
                    }
                    bool flag4 = flag2;
                    if (flag4)
                    {
                        list.Add(populationInfo);
                    }
                }
                this.PopulationInfos = list;
            }
        }

        // Token: 0x17000001 RID: 1
        // (get) Token: 0x0600000A RID: 10 RVA: 0x000022B0 File Offset: 0x000004B0
        // (set) Token: 0x0600000B RID: 11 RVA: 0x000022B8 File Offset: 0x000004B8
        public List<LayerItemModel> Layers { get; set; }

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x0600000C RID: 12 RVA: 0x000022C1 File Offset: 0x000004C1
        // (set) Token: 0x0600000D RID: 13 RVA: 0x000022C9 File Offset: 0x000004C9
        public List<LayerItemModel> OtherLayers { get; set; }

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x0600000E RID: 14 RVA: 0x000022D4 File Offset: 0x000004D4
        // (set) Token: 0x0600000F RID: 15 RVA: 0x000022EC File Offset: 0x000004EC
        public string FilterText
        {
            get
            {
                return this._filterText;
            }
            set
            {
                this._filterText = value;
                base.SetAndNotifyPropertyChanged<string>(ref this._filterText, value, "FilterText");
                this.FilterPeople(this._filterText);
            }
        }

        // Token: 0x17000004 RID: 4
        // (get) Token: 0x06000010 RID: 16 RVA: 0x00002316 File Offset: 0x00000516
        // (set) Token: 0x06000011 RID: 17 RVA: 0x0000231E File Offset: 0x0000051E
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        // Token: 0x17000005 RID: 5
        // (get) Token: 0x06000012 RID: 18 RVA: 0x00002328 File Offset: 0x00000528
        // (set) Token: 0x06000013 RID: 19 RVA: 0x00002340 File Offset: 0x00000540
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

        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000014 RID: 20 RVA: 0x00002358 File Offset: 0x00000558
        // (set) Token: 0x06000015 RID: 21 RVA: 0x00002370 File Offset: 0x00000570
        public List<PopulationInfo> PopulationInfos
        {
            get
            {
                return this._populationInfo;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<PopulationInfo>>(ref this._populationInfo, value, "PopulationInfos");
            }
        }

        // Token: 0x06000016 RID: 22 RVA: 0x00002388 File Offset: 0x00000588
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
                            string buildUrl = "http://58.60.185.51:5759/xfyhpc-ps-web/system.do?toBuildInfoMoreNoLogin&buildid=";
                            string url = buildUrl + Address;
                            Uri uri = new Uri(url);
                           
                            if (flag6)
                            {

                                this._view = new FindPeopleWebView
                                {
                                    DataContext = this,
                                    Tag = fcGuid,
                                    Owner = ServiceManager.GetService<IShellService>(null).ShellWindow,
                                    BuidId = Address,
                                    
                                    //Left = (double)x,
                                    //Top = (double)y
                                };
                                this._view.webCtrl.Navigate(uri);
                                //this._view.txtCtl.Text = "三合一新村6号A栋 152";
                                //this.PopulationInfos = Singleton<HumanHttpService>.Instance.GetPeopleInfos(this.Address);
                                this._view.Show();
                                //this._view = new FindPeopleView
                                //{
                                //    DataContext = this,
                                //    Tag = text,
                                //    Owner = ServiceManager.GetService<IShellService>(null).ShellWindow,
                                //    Left = (double)x,
                                //    Top = (double)y
                                //};
                                //this.PopulationInfos = Singleton<HumanHttpService>.Instance.GetPeopleInfos(this.Address);
                                //this._view.Show();
                            }
                            else
                            {
                                //this.ResultPopulationInfos = Singleton<HumanHttpService>.Instance.GetPeopleInfos(this.Address);
                                //this.FilterText = string.Empty;
                                //double width = ServiceManager.GetService<IShellService>(null).ShellWindow.Width;
                                //double height = ServiceManager.GetService<IShellService>(null).ShellWindow.Height;
                                //double num = this._view.Width + (double)x;
                                //double num2 = this._view.Height + (double)y;
                                //bool flag7 = num > width;
                                //if (flag7)
                                //{
                                //    this._view.Left = (double)x - this._view.Width;
                                //}
                                //else
                                //{
                                //    this._view.Left = (double)x;
                                //}
                                //bool flag8 = num2 > height;
                                //if (flag8)
                                //{
                                //    bool flag9 = num2 - 100.0 < height;
                                //    if (flag9)
                                //    {
                                //        this._view.Top = (double)y;
                                //    }
                                //    else
                                //    {
                                //        this._view.Top = (double)y - this._view.Height;
                                //    }
                                //}
                                //else
                                //{
                                //    this._view.Top = (double)y;
                                //}
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

        // Token: 0x06000017 RID: 23 RVA: 0x00002734 File Offset: 0x00000934
        private void UnHighLightBuilding()
        {
            bool flag = !string.IsNullOrEmpty(this._oldSelectFc.Key);
            if (flag)
            {
                this._oldSelectFc.Value.UnHighLightFeature(this._oldSelectFc.Key, GviMap.MapControl.FeatureManager);
            }
        }

        // Token: 0x06000018 RID: 24 RVA: 0x0000277F File Offset: 0x0000097F
        public override void OnChecked()
        {
            base.OnChecked();
            this.UnRegexEvent();
            GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
            GviMap.AxMapControl.RcMouseHover += new _IRenderControlEvents_RcMouseHoverEventHandler(RenderControl_RcMouseHover);
        }

        // Token: 0x06000019 RID: 25 RVA: 0x000027B4 File Offset: 0x000009B4
        public override void OnUnchecked()
        {
            base.OnUnchecked();
            this.UnHighLightBuilding();
            this.UnRegexEvent();
        }

        // Token: 0x0600001A RID: 26 RVA: 0x000027CC File Offset: 0x000009CC
        private void UnRegexEvent()
        {
            GviMap.AxMapControl.RcMouseHover -= new _IRenderControlEvents_RcMouseHoverEventHandler(RenderControl_RcMouseHover);
            GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
        }

        // Token: 0x0600001B RID: 27 RVA: 0x000027F3 File Offset: 0x000009F3
        public override void Reset()
        {
            base.Reset();
            base.IsChecked = false;
        }

        // Token: 0x04000003 RID: 3
        private List<PopulationInfo> ResultPopulationInfos;

        // Token: 0x04000004 RID: 4
        private List<IDisplayLayer> _layers = new List<IDisplayLayer>();

        // Token: 0x04000005 RID: 5
        private KeyValuePair<string, IDisplayLayer> _oldSelectFc;

        // Token: 0x04000008 RID: 8
        private string _filterText;

        // Token: 0x0400000A RID: 10
        private string _address;

        // Token: 0x0400000B RID: 11
        private List<PopulationInfo> _populationInfo;

        // Token: 0x0400000C RID: 12

        // private FindPeopleView _view;
        private FindPeopleWebView _view;

        // Token: 0x0400000D RID: 13
        private int _x;

        // Token: 0x0400000E RID: 14
        private int _y;
    }
}
