using System;
using System.Collections.Generic;
using System.Linq;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using LayerPropConfig;

namespace Mmc.DataSourceAccess
{

    public class GvitechFeatureDataSet : IGvitechFeatureDataSet
    {

        public GvitechFeatureDataSet(IFeatureDataSet dst)
        {
            bool flag = dst == null;
            if (flag)
            {
                throw new ArgumentNullException("dst");
            }
            this.FeatureDataSet = dst;
        }

        public IFeatureDataSet FeatureDataSet { get; protected set; }

        public List<IObjectClass> ObjectClasss { get; protected set; }

        public List<IDisplayLayer> Layers { get; protected set; }

        public List<IFeatureClass> FeatureClasss
        {
            get
            {
                IEnumerable<IFeatureClass> enumerable = null;
                bool flag = IEnumerableExtension.HasValues<IDisplayLayer>(this.Layers);
                if (flag)
                {
                    enumerable = from ly in this.Layers
                                 select ly.Fc into fc
                                 where fc != null
                                 select fc;
                }
                return (enumerable != null) ? enumerable.ToList<IFeatureClass>() : null;
            }
        }


        // (get) Token: 0x06000040 RID: 64 RVA: 0x00003008 File Offset: 0x00001208
        public List<IFeatureLayer> FeatureLayer
        {
            get
            {
                List<IFeatureLayer> flys = null;
                bool flag = IEnumerableExtension.HasValues<IDisplayLayer>(this.Layers);
                if (flag)
                {
                    flys = new List<IFeatureLayer>();
                    this.Layers.ForEach(delegate (IDisplayLayer layer)
                    {
                        bool flag2 = IEnumerableExtension.HasValues<IFeatureLayer>(layer.FLyers);
                        if (flag2)
                        {
                            flys.AddRange(layer.FLyers);
                        }
                    });
                }
                return flys;
            }
        }

        public virtual bool OpenFdsObjectClasss()
        {
            bool flag = IEnumerableExtension.HasValues<IObjectClass>(this.ObjectClasss);
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                bool flag2 = this.FeatureDataSet == null;
                if (flag2)
                {
                    throw new NullReferenceException("FeatureDataSet");
                }
                this.ObjectClasss = new List<IObjectClass>();
                string[] namesByType = this.FeatureDataSet.GetNamesByType(gviDataSetType.gviDataSetObjectClassTable);
                bool flag3 = CollectionExtension.HasValues<string>(namesByType);
                if (flag3)
                {
                    namesByType.ToList<string>().ForEach(delegate (string ocName)
                    {
                        this.ObjectClasss.Add(this.FeatureDataSet.OpenObjectClass(ocName));
                    });
                }
                result = true;
            }
            return result;
        }


        public virtual bool OpenFdsFeatureClasss()
        {
            bool flag = IEnumerableExtension.HasValues<IDisplayLayer>(this.Layers);
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                bool flag2 = this.FeatureDataSet == null;
                if (flag2)
                {
                    throw new NullReferenceException("FeatureDataSet");
                }
                this.Layers = new List<IDisplayLayer>();
                List<IFeatureClass> list = IFeatureDataSetExtension.OpenAllFeatureClass(this.FeatureDataSet);
                bool flag3 = IEnumerableExtension.HasValues<IFeatureClass>(list);
                if (flag3)
                {
                    list.ForEach(delegate (IFeatureClass fc)
                    {
                        CollectionExtension.AddEx<IDisplayLayer>(this.Layers, new DisplayLayer(fc, new List<IFeatureLayer>()));
                    });
                }
                result = true;
            }
            return result;
        }


        public virtual bool CreateFdsFeatureLayers(IObjectManager om, IGeometryRender geoRender, ITextRender txtRender, string geoField = "Geometry")
        {
            bool flag = om == null;
            if (flag)
            {
                throw new ArgumentNullException("om");
            }
            bool flag2 = geoRender == null;
            if (flag2)
            {
                geoRender = new SimpleGeometryRender();
            }
            bool flag3 = !IEnumerableExtension.HasValues<IDisplayLayer>(this.Layers);
            if (flag3)
            {
                this.OpenFdsFeatureClasss();
            }
            bool flag4 = IEnumerableExtension.HasValues<IDisplayLayer>(this.Layers);
            if (flag4)
            {
                string[] geoNames = null;
                IFeatureLayer fly;
                this.Layers.ForEach((IDisplayLayer dly) =>
                {
                    bool flag5 = dly != null && dly.Fc != null && CollectionExtension.HasValues<string>(geoNames = dly.Fc.GeometryFields());
                    if (flag5)
                    {
                        bool flag6 = string.IsNullOrEmpty(geoField) || !geoNames.Contains(geoField);
                        if (flag6)
                        {
                            geoField = geoNames.First<string>();
                        }
                        //临时加上点云的数据渲染，去掉纹理
                        if (dly.Fc.DataSource.ConnectionInfo.ConnectionType ==
                            gviConnectionType.gviConnectionFireBird2x && (dly.Fc.Name == "点云" || dly.Fc.Alias == "点云"))
                        {
                            var fields = dly.Fc.GetFields();
                            var geoIndex = fields.IndexOf(geoField);
                            var field = fields.Get(geoIndex);
                            if (field.GeometryDef.GeometryColumnType !=
                                gviGeometryColumnType.gviGeometryColumnPointCloud)
                            {
                                if (geoRender.RenderType == gviRenderType.gviRenderSimple)
                                {
                                    var simGeoRender = geoRender as ISimpleGeometryRender;
                                    var symbol = new ModelPointSymbol();
                                    symbol.EnableTexture = false;
                                    simGeoRender.Symbol = symbol;
                                }
                            }
                            else
                            {
                                geoRender = null;
                            }
                        }

                    }
                    fly = om.CreateFeatureLayer(dly.Fc, geoField, txtRender, geoRender, om.GetProjectTree().RootID);


                    bool flag7 = fly == null;
                    if (fly != null)
                    {
                        if (dly.Fc.Alias == "绿化" || dly.Fc.Alias == "市政小品")
                        {
                            fly.MaxVisibleDistance = 500.0;
                            fly.MinVisiblePixels = 15f;
                        }
                        else if (dly.Fc.Name == "禁飞区")
                        {
                            fly.MaxVisibleDistance = 50000000.0;
                            fly.MinVisiblePixels = 2f;
                        }
                        else
                        {
                            fly.MaxVisibleDistance = 500000.0;
                            //fly.MinVisibleDistance = 0;
                            fly.MinVisiblePixels = 15f;
                        }

                        fly.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        dly.AddFeatureLayer(fly);
                    }
                });
            }
            return true;
        }


        public virtual bool CreateFdsFeatureLayers(IObjectManager om, Dictionary<string, Tuple<IGeometryRender, ITextRender, string>> layerProps)
        {
            var geoField = "Geometry";
            if (om == null)
            {
                throw new ArgumentNullException("om");
            }
            IGeometryRender geoRender = null;
            if (geoRender == null)
                geoRender = new SimpleGeometryRender();
            if (!this.Layers.HasValues())
                this.OpenFdsFeatureClasss();
            if (this.Layers.HasValues())
            {
                string[] geoNames = null;
                IFeatureLayer fly;
                this.Layers.ForEach((IDisplayLayer dly) =>
                {
                    geoNames = dly.Fc.GeometryFields();
                    var fcGuid = dly.Fc.Guid.ToString();
                    if (!layerProps.Keys.Contains(fcGuid))
                        return;
                    var lyrProp = layerProps[fcGuid];
                    geoRender = lyrProp.Item1;
                    var txtRender = lyrProp.Item2;
                    geoField = lyrProp.Item3;
                    if (dly != null && dly.Fc != null && geoNames.HasValues())
                    {
                        if (string.IsNullOrEmpty(geoField) || !geoNames.Contains(geoField))
                        {
                            geoField = geoNames.First();
                        }
                        //临时加上点云的数据渲染，去掉纹理
                        if (dly.Fc.DataSource.ConnectionInfo.ConnectionType ==
                            gviConnectionType.gviConnectionFireBird2x && (dly.Fc.Name == "点云" || dly.Fc.Alias == "点云"))
                        {
                            var fields = dly.Fc.GetFields();
                            var geoIndex = fields.IndexOf(geoField);
                            var field = fields.Get(geoIndex);
                            if (field.GeometryDef.GeometryColumnType !=
                                gviGeometryColumnType.gviGeometryColumnPointCloud)
                            {
                                if (geoRender.RenderType == gviRenderType.gviRenderSimple)
                                {
                                    var simGeoRender = geoRender as ISimpleGeometryRender;
                                    var symbol = new ModelPointSymbol();
                                    symbol.EnableTexture = false;
                                    simGeoRender.Symbol = symbol;
                                }
                            }
                            else
                            {
                                geoRender = null;
                            }
                        }

                        fly = om.CreateFeatureLayer(dly.Fc, geoField, txtRender, geoRender, om.GetProjectTree().RootID);
                        if (fly != null)
                        {
                            if (dly.Fc.Alias == "绿化" || dly.Fc.Alias == "市政小品")
                            {
                                fly.MaxVisibleDistance = 500.0;
                                fly.MinVisiblePixels = 15f;
                            }
                            else if (dly.Fc.Name == "禁飞区")
                            {
                                fly.MaxVisibleDistance = 50000000.0;
                                fly.MinVisiblePixels = 2f;
                            }
                            else
                            {
                                fly.MaxVisibleDistance = 500000.0;
                                //fly.MinVisibleDistance = 0;
                                fly.MinVisiblePixels = 15f;
                            }

                            fly.VisibleMask = gviViewportMask.gviViewAllNormalView;
                            dly.AddFeatureLayer(fly);
                        }
                    }

                });
            }
            return true;
        }
    }
}
