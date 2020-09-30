using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using LayerPropConfig;
using Mmc.Windows.Services;

namespace Mmc.DataSourceAccess
{

    public class DataSourceService<TFeatureDataSet> : IDataSourceService where TFeatureDataSet : class, IGvitechFeatureDataSet
    {

        protected IConnectionInfo Ci;

        protected IDataSource Ds;

        protected List<IGvitechFeatureDataSet> FeatureDataSets;

        protected AxRenderControl Rc;

        public DataSourceService(AxRenderControl rc, IConnectionInfo ci)
        {
            this.Rc = rc;
            this.Ci = ci;
            this.IsNetLib = this.IsNetServer(ci);
            this.OpenDataSource();
        }

        public DataSourceService(AxRenderControl rc, IConnectionInfo ci, string guid)
        {
            this.Rc = rc;
            this.Ci = ci;
            this.IsNetLib = this.IsNetServer(ci);
            this.OpenDataSource(guid);
        }


        // (get) Token: 0x06000004 RID: 4 RVA: 0x000020C0 File Offset: 0x000002C0
        public IDataSource DataSource
        {
            get
            {
                return this.Ds;
            }
        }

        // (get) Token: 0x06000002 RID: 2 RVA: 0x000020AE File Offset: 0x000002AE
        // (set) Token: 0x06000003 RID: 3 RVA: 0x000020B6 File Offset: 0x000002B6
        public Dictionary<IGvitechFeatureDataSet, List<IDisplayLayer>> DisplayLayerDny { get; protected set; }
        // (get) Token: 0x06000005 RID: 5 RVA: 0x000020D8 File Offset: 0x000002D8
        // (set) Token: 0x06000006 RID: 6 RVA: 0x000020E0 File Offset: 0x000002E0
        public List<IDisplayLayer> DisplayLayers { get; protected set; }


        // (get) Token: 0x06000007 RID: 7 RVA: 0x000020E9 File Offset: 0x000002E9
        // (set) Token: 0x06000008 RID: 8 RVA: 0x000020F1 File Offset: 0x000002F1
        public IGeometryRender GeometryRender { get; set; }


        // (get) Token: 0x0600000B RID: 11 RVA: 0x0000210B File Offset: 0x0000030B
        // (set) Token: 0x0600000C RID: 12 RVA: 0x00002113 File Offset: 0x00000313
        public bool IsNetLib { get; private set; }

        // (get) Token: 0x06000009 RID: 9 RVA: 0x000020FA File Offset: 0x000002FA
        // (set) Token: 0x0600000A RID: 10 RVA: 0x00002102 File Offset: 0x00000302
        public ITextRender TextRender { get; set; }
        public bool CreateFeatureLayers()
        {
            bool flag2 = !IEnumerableExtension.HasValues<IGvitechFeatureDataSet>(this.FeatureDataSets);
            bool result;
            if (flag2)
            {
                result = false;
            }
            else
            {
                bool flag3 = this.GeometryRender == null;
                if (flag3)
                {
                    this.GeometryRender = this.CreateDefaultGeoRender();
                }
                bool flag4 = this.TextRender == null;
                if (flag4)
                {
                    this.TextRender = this.CreateDefaultTextRender();
                }
                this.FeatureDataSets.ForEach(delegate (IGvitechFeatureDataSet fds)
                {
                    fds.CreateFdsFeatureLayers(this.Rc.ObjectManager, this.GeometryRender, this.TextRender, "Geometry");
                });
                result = true;
            }
            return result;
        }

        public bool CreateFeatureLayers(Dictionary<string, List<FeatureLayerProp>> LayerProps)
        {
            bool result;
            if (!this.FeatureDataSets.HasValues())
            {
                return false;
            }
            else
            {
                if (this.GeometryRender == null)
                    this.GeometryRender = this.CreateDefaultGeoRender();
                if (this.TextRender == null)
                    this.TextRender = this.CreateDefaultTextRender();
                if (!LayerProps.HasValues())
                {
                    this.FeatureDataSets.ForEach(delegate (IGvitechFeatureDataSet fds)
                    {
                        fds.CreateFdsFeatureLayers(this.Rc.ObjectManager, this.GeometryRender, this.TextRender, "Geometry");
                    });
                }
                else
                {
                    foreach (var fds in FeatureDataSets)
                    {
                        var fdsGuid = fds.FeatureDataSet.Guid.ToString();
                        if (LayerProps.ContainsKey(fdsGuid))
                        {
                            var lyrP = LayerProps[fdsGuid];
                            if (lyrP.HasValues())
                            {
                                var dic = new Dictionary<string, Tuple<IGeometryRender, ITextRender, string>>();
                                foreach (var fc in fds.FeatureClasss)
                                {
                                    var fcGuid = fc.Guid.ToString();
                                    var prop = lyrP.Find(p => p.FcGuid == fcGuid);
                                    if (prop != null)
                                    {
                                        var geoRender = this.Rc.ObjectManager.CreateGeometryRenderFromXML(prop?.GeoRender);
                                        var txtRender = this.Rc.ObjectManager.CreateTextRenderFromXML(prop?.TxtRender);
                                        var geoName = prop.GeometryFiledName;
                                        dic.Add(prop.FcGuid, new Tuple<IGeometryRender, ITextRender, string>(geoRender, txtRender, geoName));
                                    }
                                    else
                                        dic.Add(fcGuid, new Tuple<IGeometryRender, ITextRender, string>(this.GeometryRender, this.TextRender, "Geometry"));
                                }
                                fds.CreateFdsFeatureLayers(this.Rc.ObjectManager, dic);
                            }
                            else
                            {
                                fds.CreateFdsFeatureLayers(this.Rc.ObjectManager, this.GeometryRender, this.TextRender, "Geometry");
                            }
                        }
                        else
                        {
                            fds.CreateFdsFeatureLayers(this.Rc.ObjectManager, this.GeometryRender, this.TextRender, "Geometry");
                        }
                    }
                }
                result = true;
            }
            return result;
        }

        public void FlyToDefault()
        {
            bool flag = !IEnumerableExtension.HasValues<IDisplayLayer>(this.DisplayLayers);
            if (!flag)
            {
                IDisplayLayer displayLayer = this.DisplayLayers.FirstOrDefault((IDisplayLayer dly) => dly.FLyers != null && IEnumerableExtension.HasValues<IFeatureLayer>(dly.FLyers));
                bool flag2 = displayLayer == null;
                if (!flag2)
                {
                    IFeatureLayer featureLayer = displayLayer.FLyers.FirstOrDefault<IFeatureLayer>();
                    bool flag3 = featureLayer == null;
                    if (!flag3)
                    {
                        this.Rc.Camera.FlyTime = 0.0;
                        this.Rc.Camera.FlyToObject(featureLayer.Guid, gviActionCode.gviActionFollowBehindAndAbove);
                    }
                }
            }
        }

        public List<string> GetFeatueDataSetGuid()
        {
            var list = new List<string>();
            FeatureDataSets.ForEach(p => list.Add(p.FeatureDataSet.Guid.ToString()));
            return list;
        }

        public string[] GetFeatueDataSetNames()
        {
            return this.Ds.GetFeatureDatasetNames();
        }

        public IFeatureClass GetFeatureClass(string guid)
        {
            bool flag = string.IsNullOrEmpty(guid);
            IFeatureClass result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IDisplayLayer displayLayer = this.DisplayLayers.FirstOrDefault((IDisplayLayer dly) => dly != null && dly.Fc.Guid.ToString().Equals(guid));
                result = ((displayLayer != null) ? displayLayer.Fc : null);
            }
            return result;
        }

        public string[] GetFeatureClassNames(string dataSetName)
        {
            bool flag = string.IsNullOrEmpty(dataSetName);
            string[] result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = this.Ds == null;
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    string[] featureDatasetNames = this.Ds.GetFeatureDatasetNames();
                    bool flag3 = !CollectionExtension.HasValues<string>(featureDatasetNames);
                    if (flag3)
                    {
                        result = null;
                    }
                    else
                    {
                        bool flag4 = !featureDatasetNames.Contains(dataSetName);
                        if (flag4)
                        {
                            result = null;
                        }
                        else
                        {
                            IFeatureDataSet featureDataSet = this.Ds.OpenFeatureDataset(dataSetName);
                            result = featureDataSet.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
                        }
                    }
                }
            }
            return result;
        }

        public IFeatureLayer GetFeatureLayer(string guid)
        {
            bool flag = string.IsNullOrEmpty(guid);
            IFeatureLayer result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IFeatureLayer featureLayer;
                if (!IEnumerableExtension.HasValues<IDisplayLayer>(this.DisplayLayers))
                {
                    featureLayer = null;
                }
                else
                {
                    Predicate<IFeatureLayer> layers = null;
                    featureLayer = this.DisplayLayers.Select(delegate (IDisplayLayer dly)
                    {
                        IFeatureLayer result2;
                        if (dly == null)
                        {
                            result2 = null;
                        }
                        else
                        {
                            List<IFeatureLayer> flyers = dly.FLyers;
                            Predicate<IFeatureLayer> match;
                            if ((match = layers) == null)
                            {
                                match = (layers = ((IFeatureLayer fly) => fly.Guid.ToString().Equals(guid)));
                            }
                            result2 = flyers.Find(match);
                        }
                        return result2;
                    }).FirstOrDefault((IFeatureLayer fly) => fly != null);
                }
                result = featureLayer;
            }
            return result;
        }

        public List<IFeatureLayer> GetFeatureLayers(string featureClassId)
        {
            bool flag = string.IsNullOrEmpty(featureClassId);
            List<IFeatureLayer> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IDisplayLayer displayLayer = this.DisplayLayers.FirstOrDefault((IDisplayLayer dly) => dly != null && dly.Fc != null && dly.Fc.Guid.ToString().Equals(featureClassId));
                result = ((displayLayer != null) ? displayLayer.FLyers : null);
            }
            return result;
        }

        public string GetShpName()
        {
            return this.FeatureDataSets[0].FeatureClasss[0].Name;
        }

        public bool IsPointShpFile()
        {
            if (this.FeatureDataSets == null || this.FeatureDataSets.Count == 0)
                return false;
            if (this.FeatureDataSets[0].FeatureClasss == null || this.FeatureDataSets[0].FeatureClasss.Count == 0)
                return false;
            if (this.FeatureDataSets[0].FeatureClasss[0].IsSpecifiedGeoType(Gvitech.CityMaker.FdeGeometry.gviGeometryType.gviGeometryPoint))
                return true;
            return false;
        }

        public bool IsPolygonShpFlie()
        {
            if (this.FeatureDataSets == null || this.FeatureDataSets.Count == 0)
                return false;
            if (this.FeatureDataSets[0].FeatureClasss == null || this.FeatureDataSets[0].FeatureClasss.Count == 0)
                return false;
            if (this.FeatureDataSets[0].FeatureClasss[0].IsSpecifiedGeoType(Gvitech.CityMaker.FdeGeometry.gviGeometryType.gviGeometryPolygon) || this.FeatureDataSets[0].FeatureClasss[0].IsSpecifiedGeoType(Gvitech.CityMaker.FdeGeometry.gviGeometryType.gviGeometryMultiPolygon))
                return true;
            return false;
        }

        public bool IsPolyLineShpFlie()
        {
            if (this.FeatureDataSets == null || this.FeatureDataSets.Count == 0)
                return false;
            if (this.FeatureDataSets[0].FeatureClasss == null || this.FeatureDataSets[0].FeatureClasss.Count == 0)
                return false;
            if (this.FeatureDataSets[0].FeatureClasss[0].IsSpecifiedGeoType(Gvitech.CityMaker.FdeGeometry.gviGeometryType.gviGeometryPolyline) || this.FeatureDataSets[0].FeatureClasss[0].IsSpecifiedGeoType(Gvitech.CityMaker.FdeGeometry.gviGeometryType.gviGeometryMultiPolyline))
                return true;
            return false;
        }

        public virtual bool OpenDataSource()
        {
            bool flag = this.Ci == null;
            if (flag)
            {
                throw new NullReferenceException("[IConnectionInfo]ci");
            }
            bool flag2 = this.HasOpenDataSource();
            bool result;
            if (flag2)
            {
                result = true;
            }
            else
            {
                IDataSourceFactory dataSourceFactory = new DataSourceFactory();
                SystemLog.Log(string.Format("开始打开数据源--[{0}]:...", this.Ci.ToConnectionString()), 0);
                try
                {
                    this.Ds = dataSourceFactory.OpenDataSource(this.Ci);
                    bool flag3 = this.Ds == null;
                    if (flag3)
                    {
                        SystemLog.Log(string.Format("打开数据源失败--[{0}]:...", this.Ci.ToConnectionString()), 0);
                        return false;
                    }
                    List<IFeatureDataSet> list = IDataSourceExtension.OpenAllFeatureDataSet(this.Ds);
                    bool flag4 = list != null;
                    if (flag4)
                    {
                        bool flag5 = this.FeatureDataSets == null;
                        if (flag5)
                        {
                            this.FeatureDataSets = new List<IGvitechFeatureDataSet>();
                        }
                        list.ForEach(delegate (IFeatureDataSet fds)
                        {
                            TFeatureDataSet tfeatureDataSet = (TFeatureDataSet)((object)Activator.CreateInstance(typeof(TFeatureDataSet), new object[]
                            {
                                fds
                            }));
                            tfeatureDataSet.OpenFdsFeatureClasss();
                            CollectionExtension.AddEx<IGvitechFeatureDataSet>(this.FeatureDataSets, tfeatureDataSet);
                        });
                        this.OnOpenFeatureClasss();
                    }
                    return true;
                }
                catch (AccessViolationException ex)
                {
                    SystemLog.Log(string.Format("打开数据源发生程序异常--[{0}]:...", this.Ci.ToConnectionString()), 0);
                    SystemLog.Log(ex);
                }
                catch (COMException ex2)
                {
                    SystemLog.Log(string.Format("打开数据源发生组件异常--[{0}]:...", this.Ci.ToConnectionString()), 0);
                    SystemLog.Log(ex2);
                }
                catch (Exception ex3)
                {
                    SystemLog.Log(string.Format("打开数据源发生程序异常--[{0}]:...", this.Ci.ToConnectionString()), 0);
                    SystemLog.Log(ex3);
                }
                finally
                {
                    FdeCoreExtension.ReleaseComObject(dataSourceFactory);
                }
                result = false;
            }
            return result;
        }


        public virtual bool OpenDataSource(string guid)
        {
            bool flag = this.Ci == null;
            if (flag)
            {
                throw new NullReferenceException("[IConnectionInfo]ci");
            }
            bool flag2 = this.HasOpenDataSource();
            bool result;
            if (flag2)
            {
                result = true;
            }
            else
            {
                IDataSourceFactory dataSourceFactory = new DataSourceFactory();
                SystemLog.Log(string.Format("开始打开数据源--[{0}]:...", this.Ci.ToConnectionString()), 0);
                try
                {
                    if (dataSourceFactory.HasDataSource(this.Ci))
                    {
                        this.Ds = dataSourceFactory.OpenDataSource(this.Ci);
                        bool flag3 = this.Ds == null;
                        if (flag3)
                        {
                            SystemLog.Log(string.Format("打开数据源失败--[{0}]:...", this.Ci.ToConnectionString()), 0);
                            return false;
                        }

                        List<IFeatureDataSet> list = IDataSourceExtension.OpenAllFeatureDataSet(this.Ds);
                        bool flag4 = list != null;
                        if (flag4)
                        {
                            bool flag5 = this.FeatureDataSets == null;
                            if (flag5)
                            {
                                this.FeatureDataSets = new List<IGvitechFeatureDataSet>();
                            }

                            list.ForEach(delegate(IFeatureDataSet fds)
                            {
                                if (fds.Guid.ToString() == guid)
                                {
                                    TFeatureDataSet tfeatureDataSet =
                                        (TFeatureDataSet) ((object) Activator.CreateInstance(typeof(TFeatureDataSet),
                                            new object[]
                                            {
                                                fds
                                            }));
                                    tfeatureDataSet.OpenFdsFeatureClasss();
                                    CollectionExtension.AddEx<IGvitechFeatureDataSet>(this.FeatureDataSets,
                                        tfeatureDataSet);
                                }

                            });
                            this.OnOpenFeatureClasss();
                        }

                        return true;
                    }
                }
                catch (AccessViolationException ex)
                {
                    SystemLog.Log(string.Format("打开数据源发生程序异常--[{0}]:...", this.Ci.ToConnectionString()), 0);
                    SystemLog.Log(ex);
                }
                catch (COMException ex2)
                {
                    SystemLog.Log(string.Format("打开数据源发生组件异常--[{0}]:...", this.Ci.ToConnectionString()), 0);
                    SystemLog.Log(ex2);
                }
                catch (Exception ex3)
                {
                    SystemLog.Log(string.Format("打开数据源发生程序异常--[{0}]:...", this.Ci.ToConnectionString()), 0);
                    SystemLog.Log(ex3);
                }
                finally
                {
                    FdeCoreExtension.ReleaseComObject(dataSourceFactory);
                }
                result = false;
            }
            return result;
        }
        protected virtual IGeometryRender CreateDefaultGeoRender()
        {
            //return new SimpleGeometryRender
            //{
            //    HeightStyle = gviHeightStyle.gviHeightRelative
            //};
            return null;
        }

        protected virtual ITextRender CreateDefaultTextRender()
        {

            return null;
        }

        protected bool HasOpenDataSource()
        {
            return this.Ds != null && IEnumerableExtension.HasValues<IGvitechFeatureDataSet>(this.FeatureDataSets);
        }

        protected void OnOpenFeatureClasss()
        {
            bool flag = IEnumerableExtension.HasValues<IGvitechFeatureDataSet>(this.FeatureDataSets);
            if (flag)
            {
                bool flag2 = this.DisplayLayers == null;
                if (flag2)
                {
                    this.DisplayLayers = new List<IDisplayLayer>();
                }
                bool flag3 = this.DisplayLayerDny == null;
                if (flag3)
                {
                    this.DisplayLayerDny = new Dictionary<IGvitechFeatureDataSet, List<IDisplayLayer>>();
                }
                this.FeatureDataSets.ForEach(delegate (IGvitechFeatureDataSet fds)
                {
                    bool flag4 = !IEnumerableExtension.HasValues<IDisplayLayer>(fds.Layers);
                    if (!flag4)
                    {
                        this.DisplayLayers.AddRange(fds.Layers);
                        CollectionExtension.AddEx<IGvitechFeatureDataSet, List<IDisplayLayer>>(this.DisplayLayerDny, fds, fds.Layers);
                    }
                });
            }
        }

        private bool IsNetServer(IConnectionInfo ci)
        {
            bool result = false;
            gviConnectionType connectionType = ci.ConnectionType;
            switch (connectionType)
            {
                case gviConnectionType.gviConnectionMySql5x:
                case gviConnectionType.gviConnectionOCI11:
                case gviConnectionType.gviConnectionPg9:
                case gviConnectionType.gviConnectionMSClient:
                case gviConnectionType.gviConnectionSQLite3:
                case gviConnectionType.gviConnectionArcGISServer10:
                case gviConnectionType.gviConnectionArcSDE9x:
                case gviConnectionType.gviConnectionArcSDE10x:
                case gviConnectionType.gviConnectionWFS:
                    break;
                case gviConnectionType.gviConnectionFireBird2x:
                case (gviConnectionType)7:
                case (gviConnectionType)8:
                case (gviConnectionType)9:
                case gviConnectionType.gviConnectionGBase8t:
                case gviConnectionType.gviConnectionShapeFile:
                    return result;
                default:
                    if (connectionType != gviConnectionType.gviConnectionCms7Http && connectionType != gviConnectionType.gviConnectionCms7Https)
                    {
                        return result;
                    }
                    break;
            }
            result = true;
            return result;
        }
    }
}
