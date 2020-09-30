using Gvitech.CityMaker.FdeCore;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.JscriptInvokeService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace FireControlModule
{
    public class HiddenDangerExViewModel : CheckedToolItemModel
    {
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        private ArchivesView _view;
        private string titleName;

        public string TitleName
        {
            get { return this.titleName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.titleName, value, "TitleName"); }
        }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            //_view = new ArchivesView()
            //{
            //    DataContext = this,
            //    Owner = ServiceManager.GetService<IShellService>(null).ShellWindow,
            //};

            //this.TitleName = "单位列表";
            //base.Command = new UnitListCmd(_view);
            //this.CloseCmd = new RelayCommand(() =>
            //{
            //    if (this._view != null)
            //        ((Window)this._view).Hide();
            //    IsChecked = false;
            //});
        }

        public override void OnChecked()
        {
            base.OnChecked();
            //_view.Width = 800;
            //_view.Height = 700;
            //SetLayerVisible(true);

            var webView = ServiceManager.GetService<IShellService>(null).LeftWindow as IWebView;
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = string.Format("{0}fireApplication/?pageName={1}", json1.leftViewUrl, "hiddenDanger");
            webView.RequestUrl(url);
        }

        private static void SetLayerVisible(bool isVisilbe)
        {
            var shpLayers = ServiceManager.GetService<IDataBaseService>(null).GetShpLayers();
            var layerName = "三小场所";
            shpLayers.ForEach(p =>
            {
                if (p.Fc.Name == layerName || p.Fc.Alias == layerName)
                    p.SetVisibleMask(isVisilbe);
            });
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            //SetLayerVisible(false);
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            var webView = ServiceManager.GetService<IShellService>(null).LeftWindow as IWebView;
            string url = json1.leftViewUrl;
            webView.RequestUrl(url);
            GviMap.HighlightHelper.SetRegion(null);
        }
    }

    [System.Runtime.InteropServices.ComVisibleAttribute(true)]//将该类设置为com可访问
    public class ObjectForScriptingHelper
    {
        private Window mainWindow;
        private List<IDisplayLayer> _allLayers = null;

        public ObjectForScriptingHelper()
        {
            if (_allLayers == null)
            {
                var actalLayers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
                var shpLayers = ServiceManager.GetService<IDataBaseService>(null).GetShpLayers();
                _allLayers = new List<Mmc.DataSourceAccess.IDisplayLayer>();
                if (shpLayers != null)
                    _allLayers.AddRange(shpLayers);
                if(actalLayers!=null)
                _allLayers.AddRange(actalLayers);
            }
        }

        public Window LeftWindow
        {
            get
            {
                return mainWindow;
            }

            set
            {
                mainWindow = value;
            }
        }

        public void flyToUnit(string buildCode)
        {
            var layers = ServiceManager.GetService<IDataBaseService>(null).GetShpLayers();
            layers.ForEach(p =>
            {
                if (p.Fc.Name == "三小场所")
                {
                    //先清除高亮
                    p.UnHighLightFeatureClass(GviMap.FeatureManager);
                    //高亮飞入
                    var fc = p.Fc;
                    var filter = new QueryFilter();
                    filter.WhereClause = string.Format("{0}='{1}'", "UNIT_ID", buildCode);
                    var cursor = fc.Search(filter, false);
                    IRowBuffer row = null;
                    while ((row = cursor.NextRow()) != null)
                    {
                        var fid = row.GetFid().ParseTo<string>();
                        p.HighLightFeature(fid, GviMap.FeatureManager);
                        p.FlyToFeature(fid, GviMap.Camera);
                        row.ReleaseComObject();
                    }
                    cursor.ReleaseComObject();
                    filter.ReleaseComObject();
                }
            });
        }
    }
}