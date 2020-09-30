using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Models;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;


public static class IFeatureClassExtension
{
    public static string[] GeometryFields(this IFeatureClass @this)
    {
        bool flag = @this == null;
        string[] result;
        if (flag)
        {
            result = null;
        }
        else
        {
            List<string> list = new List<string>();
            IFieldInfoCollection fields = @this.GetFields();
            int num;
            for (int i = fields.Count - 1; i >= 0; i = num - 1)
            {
                IFieldInfo fieldInfo = fields.Get(i);
                bool flag2 = fieldInfo.FieldType == gviFieldType.gviFieldGeometry;
                if (flag2)
                {
                    list.AddEx(fieldInfo.Name);
                }
                num = i;
            }
            result = list.ToArray();
        }
        return result;
    }

    public static IList<string> GetSpecifiedFieldsName(this IFeatureClass @this, gviFieldType fieldType)
    {
        bool flag = @this == null;
        IList<string> result;
        if (flag)
        {
            result = null;
        }
        else
        {
            List<string> list = new List<string>();
            IFieldInfoCollection fields = @this.GetFields();
            int num;
            for (int i = 0; i < @this.GetFields().Count; i = num + 1)
            {
                IFieldInfo fieldInfo = fields.Get(i);
                bool flag2 = fieldInfo.FieldType == fieldType;
                if (flag2)
                {
                    list.Add(fieldInfo.Name);
                }
                num = i;
            }
            result = list;
        }
        return result;
    }

    public static IList<string> GetFieldsName(this IFeatureClass @this, string ingoreFiledsString = "")
    {
        bool flag = @this == null;
        IList<string> result;
        if (flag)
        {
            result = null;
        }
        else
        {
            List<string> list = new List<string>();
            int num;
            for (int i = 0; i < @this.GetFields().Count; i = num + 1)
            {
                string name = @this.GetFields().Get(i).Name;
                bool flag2 = !string.IsNullOrEmpty(ingoreFiledsString) && ingoreFiledsString.Contains(name);
                if (!flag2)
                {
                    list.Add(name);
                }
                num = i;
            }
            result = list;
        }
        return result;
    }

    public static int GetFeatureCount(this IFeatureClass @this, string whereClause = "")
    {
        bool flag = @this == null;
        int result;
        if (flag)
        {
            result = -1;
        }
        else
        {
            IQueryFilter queryFilter = new QueryFilter();
            queryFilter.WhereClause = whereClause;
            int count = @this.GetCount(queryFilter);
            ComFactory.ReleaseComObject(queryFilter);
            result = count;
        }
        return result;
    }

    public static IRowBufferCollection GetRowBufferCollection(this IFeatureClass @this, string whereClause = "")
    {
        bool flag = @this == null;
        IRowBufferCollection result;
        if (flag)
        {
            result = null;
        }
        else
        {
            IRowBufferCollection rowBufferCollection = new RowBufferCollection();
            IQueryFilter queryFilter = new QueryFilter();
            queryFilter.WhereClause = whereClause;
            IFdeCursor fdeCursor = @this.Search(queryFilter, false);
            IRowBuffer value;
            while ((value = fdeCursor.NextRow()) != null)
            {
                rowBufferCollection.Add(value);
            }
            ComFactory.ReleaseComObjects(new object[]
            {
                queryFilter,
                fdeCursor
            });
            result = rowBufferCollection;
        }
        return result;
    }

    public static int[] GetRowIds(this IFeatureClass @this, string whereClause = "")
    {
        bool flag = @this == null;
        int[] result;
        if (flag)
        {
            result = null;
        }
        else
        {
            List<int> list = new List<int>();
            IQueryFilter queryFilter = new QueryFilter();
            queryFilter.WhereClause = whereClause;
            IFdeCursor fdeCursor = @this.Search(queryFilter, true);
            IRowBuffer rowBuffer;
            while ((rowBuffer = fdeCursor.NextRow()) != null)
            {
                list.Add(rowBuffer.GetFid());
            }
            ComFactory.ReleaseComObjects(new object[]
            {
                queryFilter,
                fdeCursor,
                rowBuffer
            });
            result = list.ToArray();
        }
        return result;
    }

    public static void AddRowBuffer(this IFeatureClass @this, IRowBuffer rowBuffer, IGeometry geometry)
    {
        bool flag = @this == null || rowBuffer == null || geometry == null;
        if (!flag)
        {
            IRowBuffer rowBuffer2 = @this.CreateRowBuffer();
            int num2;
            for (int i = 0; i < rowBuffer.FieldCount; i = num2 + 1)
            {
                IFieldInfo fieldInfo = rowBuffer.Fields.Get(i);
                bool flag2 = fieldInfo.Name == "oid";
                if (!flag2)
                {
                    int position = rowBuffer.FieldIndex(fieldInfo.Name);
                    int num = rowBuffer2.FieldIndex(fieldInfo.Name);
                    bool flag3 = num > -1;
                    if (flag3)
                    {
                        rowBuffer2.SetValue(num, fieldInfo.Name.Equals("Geometry") ? geometry : rowBuffer.GetValue(position));
                    }
                }
                num2 = i;
            }
            IFdeCursor fdeCursor = @this.Insert();
            fdeCursor.InsertRow(rowBuffer2);
            ComFactory.ReleaseComObjects(new object[]
            {
                rowBuffer2,
                fdeCursor
            });
        }
    }

    public static bool IsSpecifiedGeoType(this IFeatureClass @this, gviGeometryType geoType)
    {
        IRowBufferCollection rowBufferCollection = @this.GetRowBufferCollection("");
        bool flag = rowBufferCollection != null && rowBufferCollection.Count > 0;
        bool result;
        if (flag)
        {
            IRowBuffer rowBuffer = rowBufferCollection.Get(0);
            bool flag2 = rowBuffer != null;
            if (flag2)
            {
                int num = rowBuffer.FieldIndex("Geometry");
                bool flag3 = num < 0;
                if (flag3)
                {
                    num = rowBuffer.FieldIndex("Shape");
                }
                IGeometry geometry = rowBuffer.GetValue(num) as IGeometry;
                bool flag4 = geometry != null && geometry.GeometryType == geoType;
                if (flag4)
                {
                    result = true;
                    return result;
                }
            }
        }
        result = false;
        return result;
    }

    public static bool FlyToPlan(this IFeatureClass @this, ICamera camera, int planId, string crs = null)
    {
        bool flag = planId < 0;
        bool result;
        if (flag)
        {
            result = false;
        }
        else
        {
            int[] rowIds = @this.GetRowIds(string.Format("GroupId={0}", planId));
            bool flag2 = !rowIds.HasValues<int>();
            if (flag2)
            {
                result = false;
            }
            else
            {
                IRowBuffer row = @this.GetRow(rowIds.First<int>());
                IGeometry value = row.GetValue<IGeometry>("Geometry");
                bool flag3 = value == null;
                if (flag3)
                {
                    result = false;
                }
                else
                {
                    bool flag4 = string.IsNullOrEmpty(crs);
                    if (flag4)
                    {
                        camera.LookAtEnvelope(value.Envelope);
                    }
                    else
                    {
                        camera.LookAtEnvelope2(crs, value.Envelope);
                    }
                    result = true;
                }
            }
        }
        return result;
    }

    public static IMultiTriMesh GetMultiTriMesh(this IFeatureClass @this, IRowBuffer rowBuffer, IModelPoint modelPoint, IModel model)
    {
        IMultiTriMesh multiTriMesh = null;
        bool flag = rowBuffer == null;
        IMultiTriMesh result;
        if (flag)
        {
            result = null;
        }
        else
        {
            bool flag2 = modelPoint == null || modelPoint.GeometryType != gviGeometryType.gviGeometryModelPoint;
            if (flag2)
            {
                result = null;
            }
            else
            {
                IGeometryConvertor geometryConvertor = new GeometryConvertor();
                int num;
                bool flag3 = (num = rowBuffer.FieldIndex("MultiTriMesh")) != -1;
                if (flag3)
                {
                    multiTriMesh = (rowBuffer.GetValue(num) as IMultiTriMesh);
                }
                bool flag4 = multiTriMesh != null;
                if (flag4)
                {
                    result = multiTriMesh;
                }
                else
                {
                    multiTriMesh = geometryConvertor.ModelPointToTriMesh(model, modelPoint, false);
                    bool flag5 = num == -1;
                    if (flag5)
                    {
                        result = multiTriMesh;
                    }
                    else
                    {
                        rowBuffer.SetValue(num, multiTriMesh);
                        IRowBufferCollection rowBufferCollection = new RowBufferCollection();
                        rowBufferCollection.Add(rowBuffer);
                        @this.UpdateRows(rowBufferCollection, false);
                        result = multiTriMesh;
                    }
                }
            }
        }
        return result;
    }

    public static IEnumerable<ModelInfo> SearchModels(this IFeatureClass fc, IGeometry buffer, gviSpatialRel spatialRel = gviSpatialRel.gviSpatialRelIntersects)
    {
        bool flag = fc == null || buffer == null || buffer.IsEmpty || !buffer.IsValid;
        IEnumerable<ModelInfo> result;
        if (flag)
        {
            result = null;
        }
        else
        {
            SpatialFilter filter = new SpatialFilter
            {
                Geometry = buffer,
                GeometryField = (fc.GetFields().IndexOf("FootPrint") > -1) ? "FootPrint" : "Geometry",
                SpatialRel = spatialRel
            };
            IFdeCursor fdeCursor = fc.Search(filter, false);
            bool flag2 = fdeCursor == null;
            if (flag2)
            {
                result = null;
            }
            else
            {
                List<ModelInfo> list = new List<ModelInfo>();
                IResourceManager resourceManager = (IResourceManager)fc.FeatureDataSet;
                int num = fc.GetFields().IndexOf("Geometry");
                bool flag3 = num < 0;
                if (flag3)
                {
                    result = null;
                }
                else
                {
                    try
                    {
                        fdeCursor = fc.Search(filter, true);
                        bool flag4 = fdeCursor == null;
                        if (flag4)
                        {
                            result = null;
                            return result;
                        }
                        IRowBuffer @this;
                        while ((@this = fdeCursor.NextRow()) != null)
                        {
                            IModelPoint value = @this.GetValue(num) as IModelPoint;
                            bool flag5 = !resourceManager.ModelExist(value.ModelName);
                            if (flag5)
                            {
                                value.ReleaseComObject();
                            }
                            else
                            {
                                IModel model = resourceManager.GetModel(value.ModelName);
                                list.Add(new ModelInfo
                                {
                                    ModelPoint = value,
                                    Model = model,
                                    FeatureClassGuid = fc.Guid.ToString(),
                                    FeatureId = @this.GetFid()
                                });
                            }
                        }
                    }
                    catch (Exception message)
                    {
                        SystemLog.Log(message);
                    }
                    finally
                    {
                        fdeCursor.ReleaseComObject();
                    }
                    result = list;
                }
            }
        }
        return result;
    }

    public static IModelPoint GetModelPoint(this IFeatureClass fc, int featureId)
    {
        bool flag = fc == null || featureId < 1;
        IModelPoint result;
        if (flag)
        {
            result = null;
        }
        else
        {
            IRowBuffer rowBuffer = null;
            IModelPoint modelPoint = null;
            try
            {
                rowBuffer = fc.GetRow(featureId);
                modelPoint = rowBuffer.GetValue<IModelPoint>("Geometry");
            }
            catch
            {
            }
            finally
            {
                bool flag2 = rowBuffer != null;
                if (flag2)
                {
                    rowBuffer.ReleaseComObject();
                }
            }
            result = modelPoint;
        }
        return result;
    }

    public static IModel GetModelByModelName(this IFeatureClass fc, string modelName)
    {
        bool flag = fc == null || string.IsNullOrEmpty(modelName);
        IModel result;
        if (flag)
        {
            result = null;
        }
        else
        {
            result = ((IResourceManager)fc.FeatureDataSet).GetModel(modelName);
        }
        return result;
    }

    public static Tuple<IModelPoint, IModel> GetModelInfo(this IFeatureClass fc, int featureId)
    {
        IModelPoint modelPoint = fc.GetModelPoint(featureId);
        IModel item = (modelPoint != null) ? fc.GetModelByModelName(modelPoint.ModelName) : null;
        return new Tuple<IModelPoint, IModel>(modelPoint, item);
    }

    public static IMultiPolygon GetFootPrint(this IFeatureClass fc, int featureId)
    {
        bool flag = fc == null || featureId < 1;
        IMultiPolygon result;
        if (flag)
        {
            result = null;
        }
        else
        {
            IRowBuffer rowBuffer = null;
            IMultiPolygon multiPolygon = null;
            try
            {
                rowBuffer = fc.GetRow(featureId);
                multiPolygon = rowBuffer.GetValue<IMultiPolygon>("FootPrint");
            }
            catch
            {
            }
            finally
            {
                bool flag2 = rowBuffer != null;
                if (flag2)
                {
                    rowBuffer.ReleaseComObject();
                }
            }
            result = multiPolygon;
        }
        return result;
    }

    
}
