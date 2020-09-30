using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using Gvitech.Framework.Services;
using Mmc.Framework.Core;
using Mmc.Framework.enums;
using Mmc.Mspace.Common.Cache;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mmc.Framework.Services
{
    public static class GviMap
    {
        public static Dictionary<string, IFeatureDataSet> DataSets;

        public static Dictionary<string, List<IFeatureClass>> FeatureClasses;

        internal static Dictionary<string, IList<string>> FeatureClasseTags;

        public static Dictionary<string, List<IFeatureLayer>> FeatureLayers;

        public static Dictionary<string, List<int>> FeatureHighLightSet;

        public static List<string> OpenedFiles;

        public static List<string> Terrains;

        public static NameValueCollection CursorImages;

        public const string Wgs84 = "GEOGCS[\"WGS 84\",DATUM[\"WGS_1984\",SPHEROID[\"WGS 84\",6378137,298.257223563,AUTHORITY[\"EPSG\",\"7030\"]],TOWGS84[0,0,0,0,0,0,0],AUTHORITY[\"EPSG\",\"6326\"]],PRIMEM[\"Greenwich\",0,AUTHORITY[\"EPSG\",\"8901\"]],UNIT[\"degree\",0.0174532925199433,AUTHORITY[\"EPSG\",\"9108\"]],AUTHORITY[\"EPSG\",\"4326\"]]";

        public const string FieldGeometry = "Geometry";

        public const string FieldShape = "Shape";

        public const string FieldFootPrint = "FootPrint";

        public const string FieldMetadata = "Metadata";

        public const string FieldOid = "oid";

        public const string FieldGroupId = "GroupId";

        public static double RoMaxObserveDistance;

        public static double RoMinObserveDistance;

       // private static Dictionary<string, IRObject> _tempRObjectPool ;

        //[method: CompilerGenerated]
        //[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        public static event MapViewportModeChangedEventHandler MapViewportModeChanged;

        public static IRenderControl MapControl
        {
            get;
            set;
        }

        public static AxRenderControl AxMapControl
        {
            get;
            set;
        }

        public static ISpatialCRS SpatialCrs
        {
            get;
            private set;
        }

        public static IGeometryFactory GeoFactory
        {
            get;
            private set;
        }

        public static ICRSFactory CrsFactory { get; private set; }
        public static Dictionary<string, IRObject> TempRObjectPool { get; private set; }

        public static PoiManager PoiManager { get; private set; }
        public static PoiManager WirTowPoiMan { get; private set; }
        public static PoiManager TracePoiManager { get; private set; }

        public static LinePolyManager LinePolyManager { get; private set; }

        public static LinePolyManager TraceLinePolyManager { get; private set; }

        public static LinePolyManager GridPatrolPolyManager { get; private set; }

        public static PoiManager TowerManager { get; private set; }

        public static PointManager PointManager { get; private set; }

        public static RenderObjManager RPoiManager { get; private set; }
        public static RenderObjManager RPolylineManager { get; private set; }

        public static IDomainFactory DomainFactory
        {
            get;
            private set;
        }

        public static IResourceFactory ResourceFactory
        {
            get;
            private set;
        }

        public static IDataSourceFactory DataSourceFactory
        {
            get;
            private set;
        }

        public static IGeometryConvertor GeoConvertor
        {
            get;
            private set;
        }

        public static ICamera Camera
        {
            get
            {
                return GviMap.AxMapControl.Camera;
            }
        }

        public static ITerrain Terrain
        {
            get
            {
                return GviMap.AxMapControl.Terrain;
            }
        }

        public static IObjectEditor ObjectEditor
        {
            get
            {
                return GviMap.AxMapControl.ObjectEditor;
            }
        }

        public static ITransformHelper TransformHelper
        {
            get
            {
                return GviMap.AxMapControl.TransformHelper;
            }
        }

        public static IObjectManager ObjectManager
        {
            get
            {
                return GviMap.AxMapControl.ObjectManager;
            }
        }

        public static IFeatureManager FeatureManager
        {
            get
            {
                return GviMap.AxMapControl.FeatureManager;
            }
        }

        public static IHighlightHelper HighlightHelper
        {
            get
            {
                return GviMap.AxMapControl.HighlightHelper;
            }
        }

        public static ICacheManager CacheManager
        {
            get
            {
                return GviMap.AxMapControl.CacheManager;
            }
        }

        public static IExportManager ExportManager
        {
            get
            {
                return GviMap.AxMapControl.ExportManager;
            }
        }

        public static IViewport Viewport
        {
            get
            {
                return GviMap.AxMapControl.Viewport;
            }
        }

        public static gviInteractMode InteractMode
        {
            get
            {
                return GviMap.AxMapControl.InteractMode;
            }
            set
            {
                GviMap.AxMapControl.InteractMode = value;
            }
        }

        public static gviMouseSelectObjectMask MouseSelectObjectMask
        {
            get
            {
                return GviMap.AxMapControl.MouseSelectObjectMask;
            }
            set
            {
                GviMap.AxMapControl.MouseSelectObjectMask = value;
            }
        }

        public static gviMouseSelectMode MouseSelectMode
        {
            get
            {
                return GviMap.AxMapControl.MouseSelectMode;
            }
            set
            {
                GviMap.AxMapControl.MouseSelectMode = value;
            }
        }

        public static CursorStyle MouseCursor
        {
            set
            {
                bool flag = GviMap.CursorImages.HasKeys();
                if (flag)
                {
                    GviMap.AxMapControl.MouseCursor = GviMap.CursorImages[value.ToString()];
                }
            }
        }

        public static IProject Project
        {
            get
            {
                return GviMap.AxMapControl.Project;
            }
        }

        public static gviMeasurementMode MeasurementMode
        {
            get
            {
                return GviMap.AxMapControl.MeasurementMode;
            }
            set
            {
                GviMap.AxMapControl.MeasurementMode = value;
            }
        }

        public static gviMouseSnapMode MouseSnapMode
        {
            get
            {
                return GviMap.AxMapControl.MouseSnapMode;
            }
            set
            {
                GviMap.AxMapControl.MouseSnapMode = value;
            }
        }

        public static IProjectTree ProjectTree
        {
            get
            {
                return GviMap.AxMapControl.ProjectTree;
            }
        }

        public static ISunConfig SunConfig
        {
            get
            {
                return GviMap.AxMapControl.SunConfig;
            }
        }

        public static ITerrainVideoConfig TerrainVideoConfig
        {
            get
            {
                return GviMap.AxMapControl.TerrainVideoConfig;
            }
        }

        public static IVisualAnalysis VisualAnalysis
        {
            get
            {
                return GviMap.AxMapControl.VisualAnalysis;
            }
        }

        public static gviManipulatorMode UseEarthOrbitManipulator
        {
            get
            {
                return GviMap.AxMapControl.UseEarthOrbitManipulator;
            }
            set
            {
                GviMap.AxMapControl.UseEarthOrbitManipulator = value;
            }
        }

        public static bool IsMultiScreens
        {
            get
            {
                return GviMap.Viewport.ViewportMode != gviViewportMode.gviViewportSinglePerspective;
            }
        }

        public static double AverageElevation
        {
            get;
            set;
        }

        public static double ReferencePlaneHeight
        {
            get;
            set;
        }

        static GviMap()
        {
            GviMap.DataSets = new Dictionary<string, IFeatureDataSet>();
            GviMap.FeatureClasses = new Dictionary<string, List<IFeatureClass>>();
            GviMap.FeatureClasseTags = new Dictionary<string, IList<string>>();
            GviMap.FeatureLayers = new Dictionary<string, List<IFeatureLayer>>();
            GviMap.FeatureHighLightSet = new Dictionary<string, List<int>>();
            GviMap.OpenedFiles = new List<string>();
            GviMap.Terrains = new List<string>();
            GviMap.CursorImages = new NameValueCollection();
            GviMap.RoMaxObserveDistance = 1000000.0;
            GviMap.RoMinObserveDistance = 0.0;
            GviMap.GeoFactory = new GeometryFactory();
            GviMap.CrsFactory = new CRSFactory();
            GviMap.DomainFactory = new DomainFactory();
            GviMap.ResourceFactory = new ResourceFactory();
            GviMap.DataSourceFactory = new DataSourceFactory();
            GviMap.GeoConvertor = new GeometryConvertor();
            TempRObjectPool = new Dictionary<string, IRObject>();

            LinePolyManager = new LinePolyManager();
            TraceLinePolyManager = new LinePolyManager();
            GridPatrolPolyManager = new LinePolyManager();

            PoiManager = new PoiManager();
            TracePoiManager = new PoiManager();
            TowerManager = new PoiManager();
            PointManager = new PointManager();

            WirTowPoiMan = new PoiManager();
            RPoiManager=new RenderObjManager();
            RPolylineManager =new RenderObjManager();
        }

        public static void SetModelAmbient(uint value)
        {
            GviMap.AxMapControl.SetRenderParam(gviRenderControlParameters.gviRenderParamLightModelAmbient, value);                   
        }

        public static void SetDefaultSkybox(SkyBox skybox, params int[] viewIndex)
        {
            bool flag = skybox == null || !viewIndex.HasValues<int>();
            if (!flag)
            {
                for (int i = 0; i < viewIndex.Length; i++)
                {
                    int viewIndex2 = viewIndex[i];
                    ISkyBox skyBox = GviMap.AxMapControl.ObjectManager.GetSkyBox(viewIndex2);
                    bool flag2 = skyBox == null;
                    if (!flag2)
                    {
                        skyBox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, skybox.BackImage);
                        skyBox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, skybox.BottomImage);
                        skyBox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, skybox.FrontImage);
                        skyBox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, skybox.LeftImage);
                        skyBox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, skybox.RightImage);
                        skyBox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, skybox.TopImage);
                    }
                }
            }
        }

        public static void InitAxMapControl(AxRenderControl axMapControl, IPropertySet ps, string wkt)
        {
            try
            {
                GviMap.AxMapControl = axMapControl;                               
                GviMap.MapControl = axMapControl;
                bool flag = string.IsNullOrEmpty(wkt);
                bool flag2;
                if (flag)
                {
                    flag2 = axMapControl.Initialize(true, ps);
                }
                else
                {
                    GviMap.SpatialCrs = (ISpatialCRS)GviMap.CrsFactory.CreateFromWKT(wkt);
                    flag2 = axMapControl.Initialize2(wkt, ps);
                 
                }
                //设置语言
                if(CacheData.CurrentLanguage!= "zh-CN")
                {
                    GviMap.AxMapControl.SetRenderParam(gviRenderControlParameters.gviRenderParamLanguage, gviLanguage.gviLanguageEnglish);
                }
                
                bool flag3 = flag2;
                if (flag3)
                {
                    GviMap.AxMapControl.InteractMode = gviInteractMode.gviInteractNormal;
                    GviMap.AxMapControl.MouseSnapMode = gviMouseSnapMode.gviMouseSnapDisable;
                    SystemLog.Log("三维控件初始化成功。", LogMessageType.INFO);
                }
                else
                {
                    SystemLog.Log("三维控件初始化失败。", LogMessageType.INFO);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetViewportCount()
        {
            int result = 1;
            switch (GviMap.Viewport.ViewportMode)
            {
                case gviViewportMode.gviViewportSinglePerspective:
                    result = 1;
                    break;
                case gviViewportMode.gviViewportL1R1:
                case gviViewportMode.gviViewportT1B1:
                case gviViewportMode.gviViewportPIP:
                case gviViewportMode.gviViewportL1R1SingleFrustum:
                case gviViewportMode.gviViewportT1B1SingleFrustum:
                    result = 2;
                    break;
                case gviViewportMode.gviViewportL1M1R1:
                case gviViewportMode.gviViewportT1M1B1:
                case gviViewportMode.gviViewportL2R1:
                case gviViewportMode.gviViewportL1R2:
                    result = 3;
                    break;
                case gviViewportMode.gviViewportQuad:
                case gviViewportMode.gviViewportQuadH:
                    result = 4;
                    break;
            }
            return result;
        }

        public static void SetViewportMode(gviViewportMode gviViewportMode)
        {
            GviMap.Viewport.ViewportMode = gviViewportMode;
            GviMap.OnMapViewportModeChanged();
        }

        private static void OnMapViewportModeChanged()
        {
            MapViewportModeChangedEventHandler mapViewportModeChanged = GviMap.MapViewportModeChanged;
            bool flag = mapViewportModeChanged != null;
            if (flag)
            {
                mapViewportModeChanged(null, EventArgs.Empty);
            }
        }

        public static List<BaseFeatureLayer> GetBaseLayers(object layerEnum)
        {
            List<BaseFeatureLayer> list = new List<BaseFeatureLayer>();
            IList<IFeatureClass> featureClassesByTags = GviMap.GetFeatureClassesByTags(new string[]
            {
                layerEnum.ToString()
            });
            bool flag = featureClassesByTags != null && featureClassesByTags.Count > 0;
            if (flag)
            {
                foreach (IFeatureClass current in featureClassesByTags)
                {
                    BaseFeatureLayer baseFeatureLayer = new BaseFeatureLayer();
                    List<IFeatureLayer> list2 = GviMap.FeatureLayers[current.Guid.ToString()];
                    bool flag2 = list2 != null && list2.Count > 0;
                    if (flag2)
                    {
                        baseFeatureLayer.Init(current, list2[0]);
                        list.Add(baseFeatureLayer);
                    }
                }
            }
            return list;
        }

        public static List<BaseFeatureLayer> GetBaseLayersEx(params string[] tagStrings)
        {
            List<BaseFeatureLayer> list = new List<BaseFeatureLayer>();
            List<IFeatureClass> list2 = null;
            bool flag = tagStrings == null;
            if (flag)
            {
                bool flag2 = GviMap.FeatureClasses.HasValues<string, List<IFeatureClass>>();
                if (flag2)
                {
                    list2 = new List<IFeatureClass>();
                    foreach (List<IFeatureClass> current in GviMap.FeatureClasses.Values)
                    {
                        list2.AddRange(current);
                    }
                }
            }
            else
            {
                list2 = new List<IFeatureClass>();
                int num;
                for (int i = 0; i < tagStrings.Length; i = num + 1)
                {
                    IList<IFeatureClass> featureClassesByTags = GviMap.GetFeatureClassesByTags(new string[]
                    {
                        tagStrings[i]
                    });
                    bool flag3 = featureClassesByTags != null;
                    if (flag3)
                    {
                        list2.AddRange(featureClassesByTags);
                    }
                    num = i;
                }
            }
            bool flag4 = list2 != null && list2.Count > 0;
            if (flag4)
            {
                foreach (IFeatureClass current2 in list2)
                {
                    BaseFeatureLayer baseFeatureLayer = new BaseFeatureLayer();
                    List<IFeatureLayer> list3 = GviMap.FeatureLayers[current2.Guid.ToString()];
                    bool flag5 = list3 != null && list3.Count > 0;
                    if (flag5)
                    {
                        baseFeatureLayer.Init(current2, list3[0]);
                        list.Add(baseFeatureLayer);
                    }
                }
            }
            return list;
        }

        public static List<IFeatureDataSet> OpenConnection(IConnectionInfo connectionInfo)
        {
            IDataSource fdbDs = null;
            bool flag = GviMap.DataSourceFactory.HasDataSource(connectionInfo);
            List<IFeatureDataSet> result;
            if (flag)
            {
                try
                {
                    fdbDs = GviMap.DataSourceFactory.OpenDataSource(connectionInfo);
                }
                catch (Exception)
                {
                    result = null;
                    return result;
                }
            }
            bool flag2 = fdbDs == null;
            if (flag2)
            {
                result = null;
            }
            else
            {
                ComFactory.ReleaseComObjects(new object[]
                {
                    connectionInfo
                });
                string[] featureDatasetNames = fdbDs.GetFeatureDatasetNames();
                bool flag3 = featureDatasetNames == null || featureDatasetNames.Length == 0;
                if (flag3)
                {
                    result = null;
                }
                else
                {
                    result = (from name in featureDatasetNames
                              select fdbDs.OpenFeatureDataset(name)).ToList<IFeatureDataSet>();
                }
            }
            return result;
        }

        public static Tuple<IFeatureClass, IFeatureLayer> OpenConnection(IConnectionInfo conn, string dataSetName, string layerName, string geometryFieldName, IGeometryRender geoRender = null, ITextRender textRender = null)
        {
            List<IFeatureDataSet> list = GviMap.OpenConnection(conn);
            bool flag = list == null;
            Tuple<IFeatureClass, IFeatureLayer> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IFeatureDataSet featureDataSet = list.FirstOrDefault((IFeatureDataSet dataset) => dataset.Name == dataSetName);
                bool flag2 = featureDataSet != null;
                if (flag2)
                {
                    IFeatureClass featureClass = featureDataSet.OpenFeatureClass(layerName);
                    IFeatureLayer featureLayer = GviMap.MapControl.ObjectManager.CreateFeatureLayer(featureClass, geometryFieldName, textRender, geoRender);
                    GviMap.UpdateData(featureDataSet, featureClass, featureLayer);
                    result = Tuple.Create<IFeatureClass, IFeatureLayer>(featureClass, featureLayer);
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }

        public static Tuple<IFeatureClass, IFeatureLayer> OpenConnection(IConnectionInfo conn, string dataSetName, string layerName, string geometryFieldName, Func<object, Tuple<IGeometryRender, ITextRender>> funcRender)
        {
            List<IFeatureDataSet> list = GviMap.OpenConnection(conn);
            bool flag = list == null;
            Tuple<IFeatureClass, IFeatureLayer> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IFeatureDataSet featureDataSet = list.FirstOrDefault((IFeatureDataSet dataset) => dataset.Name == dataSetName);
                bool flag2 = featureDataSet != null;
                if (flag2)
                {
                    IFeatureClass featureClass = featureDataSet.OpenFeatureClass(layerName);
                    Tuple<IGeometryRender, ITextRender> tuple = funcRender(featureClass.Guid.ToString());
                    IGeometryRender geoRender = (tuple != null) ? tuple.Item1 : null;
                    ITextRender textRender = (tuple != null) ? tuple.Item2 : null;
                    IFeatureLayer featureLayer = GviMap.MapControl.ObjectManager.CreateFeatureLayer(featureClass, geometryFieldName, textRender, geoRender);
                    GviMap.UpdateData(featureDataSet, featureClass, featureLayer);
                    result = Tuple.Create<IFeatureClass, IFeatureLayer>(featureClass, featureLayer);
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }

        public static void UpdateData(IFeatureDataSet dataSet, IFeatureClass fc, IFeatureLayer fclayer)
        {
            GviMap.DataSets.AddEx(dataSet.Guid.ToString(), dataSet);
            bool flag = GviMap.FeatureClasses.ContainsKey(dataSet.Guid.ToString());
            if (flag)
            {
                List<IFeatureClass> list = GviMap.FeatureClasses[dataSet.Guid.ToString()];
                bool flag2 = !list.Contains(fc);
                if (flag2)
                {
                    list.Add(fc);
                }
            }
            else
            {
                List<IFeatureClass> value = new List<IFeatureClass>
                {
                    fc
                };
                GviMap.FeatureClasses.Add(dataSet.Guid.ToString(), value);
            }
            bool flag3 = GviMap.FeatureLayers.ContainsKey(fc.Guid.ToString());
            if (flag3)
            {
                List<IFeatureLayer> list2 = GviMap.FeatureLayers[fc.Guid.ToString()];
                bool flag4 = !list2.Contains(fclayer);
                if (flag4)
                {
                    list2.Add(fclayer);
                }
            }
            else
            {
                List<IFeatureLayer> value2 = new List<IFeatureLayer>
                {
                    fclayer
                };
                GviMap.FeatureLayers.Add(fc.Guid.ToString(), value2);
            }
        }

        public static IFeatureLayer GetFeatureLayer(string featureLayerGuid)
        {
            IFeatureLayer result = null;
            foreach (List<IFeatureLayer> current in GviMap.FeatureLayers.Values)
            {
                foreach (IFeatureLayer current2 in current)
                {
                    bool flag = current2 == null;
                    if (!flag)
                    {
                        bool flag2 = current2.Guid.ToString() == featureLayerGuid;
                        if (flag2)
                        {
                            result = current2;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public static List<IFeatureLayer> GetFeatureLayerBrothers(string featureLayerGuid, bool isAll)
        {
            List<IFeatureLayer> list = new List<IFeatureLayer>();
            IFeatureLayer featureLayer = GviMap.GetFeatureLayer(featureLayerGuid);
            bool flag = featureLayer != null;
            if (flag)
            {
                list.AddRange(isAll ? GviMap.FeatureLayers[featureLayer.FeatureClassId.ToString()] : GviMap.FeatureLayers[featureLayer.FeatureClassId.ToString()].FindAll((IFeatureLayer item) => item != featureLayer));
            }
            return list;
        }

        public static IFeatureClass GetFeatureClassByFeatureLayerGuid(string featureLayerGuid)
        {
            IFeatureClass result = null;
            IFeatureLayer featureLayer = GviMap.GetFeatureLayer(featureLayerGuid);
            bool flag = featureLayer != null;
            if (flag)
            {
                result = GviMap.GetFeatureClass(featureLayer.FeatureClassId.ToString());
            }
            return result;
        }

        public static IFeatureClass GetFeatureClass(string fcGuid)
        {
            IFeatureClass result = null;
            foreach (List<IFeatureClass> current in GviMap.FeatureClasses.Values)
            {
                foreach (IFeatureClass current2 in current)
                {
                    bool flag = current2.Guid.ToString() == fcGuid;
                    if (flag)
                    {
                        result = current2;
                        break;
                    }
                }
            }
            return result;
        }

        public static List<IFeatureClass> GetFeatureClassBrothers(string fcGuid, bool isAll)
        {
            List<IFeatureClass> list = new List<IFeatureClass>();
            IFeatureClass fc = GviMap.GetFeatureClass(fcGuid);
            bool flag = fc != null;
            if (flag)
            {
                list.AddRange(isAll ? GviMap.FeatureClasses[fc.FeatureDataSet.Guid.ToString()] : GviMap.FeatureClasses[fc.FeatureDataSet.Guid.ToString()].FindAll((IFeatureClass item) => item != fc));
            }
            return list;
        }

        public static IList<IFeatureClass> GetFeatureClassesByTags(params string[] tagStrings)
        {
            IList<IFeatureClass> list = new List<IFeatureClass>();
            foreach (List<IFeatureClass> current in GviMap.FeatureClasses.Values)
            {
                foreach (IFeatureClass current2 in current)
                {
                    bool flag = current2 == null;
                    if (flag)
                    {
                        break;
                    }
                    bool flag2 = !tagStrings.HasValues<string>();
                    if (flag2)
                    {
                        bool flag3 = !list.Contains(current2);
                        if (flag3)
                        {
                            list.Add(current2);
                        }
                    }
                    else
                    {
                        IList<string> featureClassTags = GviMap.GetFeatureClassTags(current2.Guid.ToString());
                        bool flag4 = featureClassTags.HasValues<string>();
                        if (flag4)
                        {
                            for (int i = 0; i < tagStrings.Length; i++)
                            {
                                string item = tagStrings[i];
                                bool flag5 = featureClassTags.Contains(item);
                                if (flag5)
                                {
                                    bool flag6 = !list.Contains(current2);
                                    if (flag6)
                                    {
                                        list.Add(current2);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        public static bool SetFeatureClassTags(string fcGuid, params string[] tags)
        {
            bool flag = !tags.HasValues<string>() || string.IsNullOrEmpty(fcGuid);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                IList<string> list = GviMap.GetFeatureClassTags(fcGuid) ?? new List<string>();
                bool flag2 = tags.HasValues<string>();
                if (flag2)
                {
                    for (int i = 0; i < tags.Length; i++)
                    {
                        string item = tags[i];
                        bool flag3 = !list.Contains(item);
                        if (flag3)
                        {
                            list.Add(item);
                        }
                    }
                }
                GviMap.FeatureClasseTags.AddUpdate(fcGuid, list);
                result = true;
            }
            return result;
        }

        public static bool RemoveFeatureClassTags(string fcGuid, params string[] tags)
        {
            bool flag = !tags.HasValues<string>() || string.IsNullOrEmpty(fcGuid);
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                IList<string> featureClassTags = GviMap.GetFeatureClassTags(fcGuid);
                bool flag2 = !featureClassTags.HasValues<string>();
                if (flag2)
                {
                    result = true;
                }
                else
                {
                    for (int i = 0; i < tags.Length; i++)
                    {
                        string item = tags[i];
                        bool flag3 = featureClassTags.Contains(item);
                        if (flag3)
                        {
                            featureClassTags.Remove(item);
                        }
                    }
                    GviMap.FeatureClasseTags.AddUpdate(fcGuid, featureClassTags);
                    result = true;
                }
            }
            return result;
        }

        public static IList<string> GetFeatureClassTags(string fcGuid)
        {
            bool flag = GviMap.HasFeatureClassTags(fcGuid);
            IList<string> result;
            if (flag)
            {
                result = GviMap.FeatureClasseTags[fcGuid];
            }
            else
            {
                result = null;
            }
            return result;
        }

        public static bool HasFeatureClassTags(string fcGuid)
        {
            return GviMap.FeatureClasseTags.ContainsKey(fcGuid);
        }
    }
}
