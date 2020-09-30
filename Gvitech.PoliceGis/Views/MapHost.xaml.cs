using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;
using DevExpress.Xpf.Core;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.JscriptInvokeService;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Services.LocalConfigService;
using ApplicationConfig;
using System.Threading.Tasks;
using System.Windows.Input;
using Mmc.Mspace.PoiManagerModule;

namespace MMC.MSpace.Views
{

    public partial class MapHost : Window
    {

        public MapHost()
        {
            this.InitializeComponent();
            bool flag = !StringExtension.ParseTo<bool>(ConfigurationManager.AppSettings["IsFullScreen"], false);
            if (flag)
            {
                base.MaxHeight = (base.Height = SystemParameters.WorkArea.Height);
                base.MaxWidth = (base.Width = SystemParameters.WorkArea.Width);
            }
            else
            {
                base.MaxHeight = (base.Height = SystemParameters.PrimaryScreenHeight);
                base.MaxWidth = (base.Width = SystemParameters.PrimaryScreenWidth);
            }
            base.Loaded += this.MapHost_Loaded;
        }

        private void MapHost_Loaded(object sender, RoutedEventArgs e)
        {
            DXSplashScreen.Progress(1.0);

            this.InitAxMapControl();
            this.FlytoFirstScene();
            this.SetSkybox();
            ServiceManager.GetService<IMaphostService>(null).MapWindow = this;
            //Task.Run(()=> {
                ServiceManager.GetService<IDataBaseService>(null).Init(this.axRenderControl);
            //});

        
            var shell = new Shell();
            LoginExcetiop.Logout += Logout;
            shell.Show();
            base.Activate();

            if (!LicenseUtil.ValidLicense(this.axRenderControl, out string resMsg))
                System.Windows.MessageBox.Show(resMsg);

            //int num;
       
            DXSplashScreen.Progress(2.0);

            MarkerHelper.Instance.Initialize();
        }

        public void Logout()
        {
            MessageBoxResult result = System.Windows.MessageBox.Show(Helpers.ResourceHelper.FindKey("ReLoginTootip"), Helpers.ResourceHelper.FindKey("ReTootip"), MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown(0);
            }
            else
            {

            }

        }

        private void FlytoFirstScene()
        {
            IPoint point = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
           
            string currentCrsWKT = GviMap.MapControl.GetCurrentCrsWKT();
            point.SpatialCRS = (ISpatialCRS)GviMap.CrsFactory.CreateFromWKT(string.IsNullOrEmpty(currentCrsWKT) ? WKTString.PROJ_CGCS2000_WKT : currentCrsWKT);
            string OriginCamera = ConfigurationManager.AppSettings["OriginCamera"];
            string[] array = OriginCamera.Split(new char[] { ';' });
            double X = 0, Y = 0, Z = 0;
            if (!(array == null || array.Length != 6))
            {
                X = StringExtension.ParseTo<double>(array[0], 0.0);
                Y = StringExtension.ParseTo<double>(array[1], 0.0);
                Z = StringExtension.ParseTo<double>(array[2], 0.0);
            }
            point.X = X;
            point.Y = Y;
            point.Z = Z;
            IEulerAngle angle = new EulerAngle
            {
                Heading = 0.0,
                Tilt = -45.0,
                Roll = 0.0
            };
            GviMap.MapControl.Camera.SetCamera2(point, angle, gviSetCameraFlags.gviSetCameraNoFlags);

            //设置飞入时间与飞入状态
            GviMap.Camera.FlyTime = 1;
            GviMap.Camera.FlyMode = gviFlyMode.gviFlyArc;
        }


        private void SetSkybox()
        {
            try
            {
                SkyBoxService.Init(System.Windows.Forms.Application.StartupPath + "\\skybox\\", "");
                SkyBox skyBox = SkyBoxService.GetSkyBox(22);
                GviMap.SetDefaultSkybox(skyBox, new int[]
                {
                    0,
                    1,
                    2,
                    3
                });
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }


        private void InitAxMapControl()
        {
            try
            {
                var crsConfig = ServiceManager.GetService<ILocalWsConfigService>().CrsConfig.Find();
                if (crsConfig?.CrsWkt == null)
                {
                    crsConfig = new CrsConfig() { CrsWkt = WKTString.WGS_84_WKT };
                    ServiceManager.GetService<ILocalWsConfigService>().CrsConfig.Add(crsConfig);
                }
                //string prj = WKTString.UNKNOWN_WKT;
                string prj = crsConfig.CrsWkt;
                IPropertySet propertySet = new PropertySet();
                propertySet.SetProperty("RenderSystem", StringExtension.ParseTo<int>(ConfigurationManager.AppSettings["RenderType"], 1));
                //propertySet.SetProperty("Language", 1);
                //暂时屏蔽多点触摸功能，避免出现水印
                //propertySet.SetProperty("MultiTouch", StringExtension.ParseTo<bool>(ConfigurationManager.AppSettings["MultiTouch"], false));
                GviMap.InitAxMapControl(this.axRenderControl, propertySet, prj);
                GviMap.Terrain.EnableAtmosphere = ConfigurationManager.AppSettings["EnableAtmosphere"].ParseTo<bool>();
                GviMap.CacheManager.FileCacheEnabled = true;
                GviMap.CacheManager.MemoryCacheEnabled = true;
                GviMap.CacheManager.MemoryCacheSize = 2000;
                //GviMap.CacheManager.FileCachePath = System.Windows.Forms.Application.LocalUserAppDataPath + "\\Cache";
                //GviMap.MapControl.Viewport.LogoVisible = false;                                                                       
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }
    }
}
