using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using Mmc.Mspace.Const.ConstDataBase;

namespace Mmc.DataSourceAccess
{
    
    public class POIFeatureDataSet : GvitechFeatureDataSet
    {
        
        public POIFeatureDataSet(IFeatureDataSet dst) : base(dst)
        {
            this._renders = new Dictionary<string, LayerRender>();
        }

        
        public override bool CreateFdsFeatureLayers(IObjectManager om, IGeometryRender geoRender, ITextRender txtRender, string geoField = "Geometry")
        {
            bool flag = om == null;
            if (flag)
            {
                throw new ArgumentNullException("om");
            }
            bool flag2 = !this._isRenderGot;
            if (flag2)
            {
                this.GetRenderFromObject(base.FeatureDataSet, om);
            }
            bool flag3 = !IEnumerableExtension.HasValues<IDisplayLayer>(base.Layers);
            if (flag3)
            {
                this.OpenFdsFeatureClasss();
            }
            bool flag4 = IEnumerableExtension.HasValues<IDisplayLayer>(base.Layers);
            if (flag4)
            {
                base.Layers.ForEach(delegate (IDisplayLayer dly)
                {
                    this.CreateFdsFeatureLayerAction(dly, om, geoRender, txtRender, geoField);
                });
            }
            return true;
        }

        
        private void CreateFdsFeatureLayerAction(IDisplayLayer dly, IObjectManager om, IGeometryRender geoRender, ITextRender txtRender, string geoField = "Geometry")
        {
            bool flag = dly == null;
            if (!flag)
            {
                IFeatureClass fc = dly.Fc;
                bool flag2 = fc == null;
                if (!flag2)
                {
                    string[] array = IFeatureClassExtension.GeometryFields(dly.Fc);
                    bool flag3 = !CollectionExtension.HasValues<string>(array);
                    if (!flag3)
                    {
                        bool flag4 = string.IsNullOrEmpty(geoField) || !array.Contains(geoField);
                        if (flag4)
                        {
                            geoField = array.First<string>();
                        }
                        LayerRender layerRender = null;
                        bool flag5 = IDictionaryExtension.HasValues<string, LayerRender>(this._renders) && this._renders.ContainsKey(fc.Guid.ToString());
                        if (flag5)
                        {
                            layerRender = this._renders[dly.Fc.Guid.ToString()];
                        }
                        IFeatureLayer featureLayer = (layerRender != null) ? om.CreateFeatureLayer(dly.Fc, geoField, layerRender.TextRender, layerRender.GeometryRender) : om.CreateFeatureLayer(dly.Fc, geoField, txtRender, geoRender);
                        bool flag6 = featureLayer == null;
                        if (!flag6)
                        {
                            featureLayer.MaxVisibleDistance = (double)((layerRender != null) ? layerRender.MaxVisibleDistance : 800f);
                            featureLayer.MinVisiblePixels = ((layerRender != null) ? layerRender.MinVisibleDistance : 1f);
                            featureLayer.MinVisiblePixels = ((layerRender != null) ? layerRender.MinVisiblePixels : 1f);
                            featureLayer.VisibleMask = gviViewportMask.gviViewAllNormalView;
                            dly.AddFeatureLayer(featureLayer);
                        }
                    }
                }
            }
        }

        
        private void GetRenderFromObject(IFeatureDataSet fds, IObjectManager om)
        {
            bool flag = fds == null;
            if (!flag)
            {
                bool flag2 = om == null;
                if (!flag2)
                {
                    bool isRenderGot = this._isRenderGot;
                    if (!isRenderGot)
                    {
                        IObjectClass objectClass = null;
                        IFdeCursor fdeCursor = null;
                        IQueryFilter queryFilter = null;
                        IRowBuffer rowBuffer = null;
                        float minVisiblePixels = 1f;
                        try
                        {
                            string[] names = fds.GetNamesByType(gviDataSetType.gviDataSetObjectClassTable);
                            var fc = fds.OpenFeatureClass("视频监控");
                            if (!names.Contains<string>(FeatureDataSetNamescs.Table_POISymbol))
                            {
                                //
                                string text1 = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\shp\重点场所\视频监控几何样式.xml";
                                string text2 = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\shp\重点场所\视频监控文本样式.xml";
                                
                                var geometryRender3 = ObjectManagerExtension.CreateGeometryRenderFromXmlFile(om, text1);
                                var txtRender = ObjectManagerExtension.CreateTextRenderFromXmlFile(om, text2);
                                CollectionExtension.AddEx<string, LayerRender>(this._renders, fc.Guid.ToString(), new LayerRender(geometryRender3, txtRender)
                                {
                                    MaxVisibleDistance = 3000,
                                    MinVisibleDistance = 3000,
                                    MinVisiblePixels = minVisiblePixels
                                });
                                return;
                            }
                            objectClass = fds.OpenObjectClass(FeatureDataSetNamescs.Table_POISymbol);
                            queryFilter = new QueryFilter();
                            fdeCursor = objectClass.Search(queryFilter, false);
                            IFieldInfoCollection fields = objectClass.GetFields();
                            int num = fields.IndexOf(FeatureDataSetNamescs.Field_OC_Guid);
                            int num2 = fields.IndexOf(FeatureDataSetNamescs.Field_OC_GeoRenderSytle);
                            int num3 = fields.IndexOf(FeatureDataSetNamescs.Field_OC_TextRenderSytle);
                            int num4 = fields.IndexOf(FeatureDataSetNamescs.Field_OC_MaxDistance);
                            int num5 = fields.IndexOf(FeatureDataSetNamescs.Field_OC_MinDistance);
                            bool flag3 = num < 0 || num2 < 0 || num3 < 0 || num4 < 0 || num5 < 0;
                            if (!flag3)
                            {
                                string field_FC_Geometry = FeatureDataSetNamescs.Field_FC_Geometry;
                                while ((rowBuffer = fdeCursor.NextRow()) != null)
                                {
                                    string text = rowBuffer.GetValue(num) as string;
                                    bool flag4 = string.IsNullOrEmpty(text);
                                    if (!flag4)
                                    {
                                        IBinaryBuffer binaryBuffer = rowBuffer.GetValue(num3) as IBinaryBuffer;
                                        string text2 = (binaryBuffer != null) ? Encoding.UTF8.GetString(binaryBuffer.AsByteArray()) : string.Empty;
                                        ITextRender txtRender = (!string.IsNullOrEmpty(text2)) ? om.CreateTextRenderFromXML(text2) : null;
                                        binaryBuffer = (rowBuffer.GetValue(num2) as IBinaryBuffer);
                                        text2 = ((binaryBuffer != null) ? Encoding.UTF8.GetString(binaryBuffer.AsByteArray()) : string.Empty);
                                        IGeometryRender geometryRender2;
                                        if (string.IsNullOrEmpty(text2))
                                        {
                                            IGeometryRender geometryRender = new SimpleGeometryRender();
                                            geometryRender2 = geometryRender;
                                        }
                                        else
                                        {
                                            geometryRender2 = om.CreateGeometryRenderFromXML(text2);
                                        }
                                        IGeometryRender geometryRender3 = geometryRender2;
                                        ISimpleGeometryRender simpleGeometryRender = geometryRender3 as ISimpleGeometryRender;
                                        bool flag5 = simpleGeometryRender != null;
                                        if (flag5)
                                        {
                                            ImagePointSymbol imagePointSymbol = simpleGeometryRender.Symbol as ImagePointSymbol;
                                            bool flag6 = POIFeatureDataSet._imageSize == 0;
                                            if (flag6)
                                            {
                                                string text3 = ConfigurationManager.AppSettings["poiSize"];
                                                POIFeatureDataSet._imageSize = ((!string.IsNullOrEmpty(text3)) ? StringExtension.ParseTo<int>(text3, 48) : 48);
                                            }
                                            bool flag7 = imagePointSymbol != null;
                                            if (flag7)
                                            {
                                                imagePointSymbol.Size = POIFeatureDataSet._imageSize;
                                            }
                                        }
                                        float maxVisibleDistance = StringExtension.ParseTo<float>(rowBuffer.GetValue(num4), 10000f);
                                        float minVisibleDistance = StringExtension.ParseTo<float>(rowBuffer.GetValue(num5), 1f);
                                        CollectionExtension.AddEx<string, LayerRender>(this._renders, text, new LayerRender(geometryRender3, txtRender)
                                        {
                                            MaxVisibleDistance = maxVisibleDistance,
                                            MinVisibleDistance = minVisibleDistance,
                                            MinVisiblePixels = minVisiblePixels
                                        });
                                    }
                                }
                                this._isRenderGot = true;
                            }
                        }
                        catch (Exception innerException)
                        {
                            throw new Exception("GetRenderFromObject Function Error", innerException);
                        }
                        finally
                        {
                            FdeCoreExtension.ReleaseComObject(rowBuffer);
                            FdeCoreExtension.ReleaseComObject(queryFilter);
                            if (fdeCursor != null)
                                fdeCursor.Close();
                            if (objectClass != null)
                                objectClass.Close();
                        }
                    }
                }
            }
        }

        
        private static int _imageSize;

        
        private bool _isRenderGot;

        
        private readonly Dictionary<string, LayerRender> _renders;
    }
}
