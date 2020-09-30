using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Mmc.Business.Utils;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;

// Token: 0x02000003 RID: 3
public static class IFeatureClassBusinessExtention
{
    // Token: 0x06000002 RID: 2 RVA: 0x00002150 File Offset: 0x00000350
    public static DataTable FuzzySearch(this IFeatureClass fc, string key, string showFields = null, IGeometry geo = null)
    {
        bool flag = fc == null;
        if (flag)
        {
            throw new NullReferenceException("fc");
        }
        IFdeCursor fdeCursor = null;
        DataTable result;
        try
        {
            string whereClause = fc.GetFuzzySearchCause(key);
            ISQLCheck sqlcheck = fc.DataSource.SQLCheck;
            whereClause = sqlcheck.CheckWhereClause(whereClause);
            bool flag2 = geo == null;
            IQueryFilter filter;
            if (flag2)
            {
                filter = QueryFilterFactory.CreateQueryFilter<QueryFilter>(whereClause, null, null, null, null);
            }
            else
            {
                filter = QueryFilterFactory.CreateSpatialFilter<SpatialFilter>(whereClause, null, null, null, null, geo);
            }
            fdeCursor = fc.Search(filter, false);
            string[] showFields2 = string.IsNullOrEmpty(showFields) ? null : showFields.Split(new char[]
            {
                ',',
                ';'
            }, StringSplitOptions.RemoveEmptyEntries);
            result = fdeCursor.ToDataTable(showFields2);
        }
        catch (COMException innerException)
        {
            throw new Exception("FuzzySearch error", innerException);
        }
        catch (Exception innerException2)
        {
            throw new Exception("FuzzySearch error", innerException2);
        }
        finally
        {
            bool flag3 = fdeCursor != null;
            if (flag3)
            {
                fdeCursor.Close();
            }
        }
        return result;
    }

    // Token: 0x06000003 RID: 3 RVA: 0x00002250 File Offset: 0x00000450
    public static bool FlyToFeatue(this IFeatureClass fc, int fid, ICamera camera)
    {
        bool result;
        if (camera == null)
        {
            result = false;
        }
        else
        {
            if (fid < 0)
                return false;
            IRowBuffer rowBuffer = null;
            IGeometry geometry = null;
            try
            {
                string[] array = fc.GeometryFields();
                bool flag3 = !array.HasValues<string>();
                if (flag3)
                {
                    result = false;
                }
                else
                {
                    rowBuffer = fc.GetRow(fid);
                    bool flag4 = rowBuffer == null;
                    if (flag4)
                    {
                        result = false;
                    }
                    else
                    {
                        geometry = (rowBuffer.GetValue(rowBuffer.Fields.IndexOf(array.First<string>())) as IGeometry);
                        bool flag5 = geometry == null;
                        if (flag5)
                        {
                            result = false;
                        }
                        else
                        {
                            IEnvelope envelope = geometry.Envelope;
                            double num = (double)envelope.DiagonalDistance();
                            camera.FlyTime = 0.5;
                            if (num < 500.0)
                            {
                                if (num <= 10)
                                    num = 20;
                                else if(num > 10&& num<100)
                                    num = 100;
                                else
                                    num = 200;
                                camera.LookAt2(new Vector3
                                {
                                    X = envelope.Center.X,
                                    Y = envelope.Center.Y,
                                    Z = envelope.Center.Z
                                }.ToPoint(new GeometryFactory(), geometry.SpatialCRS), num, new EulerAngle
                                {
                                    Heading = 45.0,
                                    Roll = 0.0,
                                    Tilt = -45.0
                                });
                            }
                            else
                            {
                                if (num < 2000.0)
                                    camera.FlyToEnvelope(envelope, geometry.SpatialCRS, 2f, 0.5, null);
                                else
                                    camera.FlyToEnvelope(envelope, geometry.SpatialCRS, 0.5f, 0.5, null);
                            }
                            result = true;
                        }
                    }
                }
            }
            finally
            {
                rowBuffer.ReleaseComObject();
                geometry.ReleaseComObject();
            }
        }
        return result;
    }

    // Token: 0x06000004 RID: 4 RVA: 0x0000244C File Offset: 0x0000064C
    private static string GetFuzzySearchCause(this IFeatureClass fc, string key)
    {
        bool flag = fc == null;
        if (flag)
        {
            throw new NullReferenceException("fc");
        }
        bool flag2 = string.IsNullOrEmpty(key);
        string result;
        if (flag2)
        {
            result = "1=1";
        }
        else
        {
            IFieldInfoCollection fields = fc.GetFields();
            bool flag3 = fields == null || fields.Count < 1;
            if (flag3)
            {
                result = null;
            }
            else
            {
                Dictionary<IFieldInfo, Type> canBeShowFields = fields.GetCanBeShowFields();
                bool flag4 = !canBeShowFields.HasValues<IFieldInfo, Type>();
                if (flag4)
                {
                    result = null;
                }
                else
                {
                    double num = 0.0;
                    bool flag5 = double.TryParse(key, out num);
                    IEnumerable<KeyValuePair<IFieldInfo, Type>> source;
                    if (flag5)
                    {
                        source = from kvp in canBeShowFields
                                 where kvp.Value.Equals(typeof(string)) || kvp.Value.Equals(typeof(int)) || kvp.Value.Equals(typeof(short)) || kvp.Value.Equals(typeof(long)) || kvp.Value.Equals(typeof(float)) || kvp.Value.Equals(typeof(double))
                                 select kvp;
                    }
                    else
                    {
                        source = from kvp in canBeShowFields
                                 where kvp.Value.Equals(typeof(string))
                                 select kvp;
                    }
                    Dictionary<string, Type> dictionary = source.ToDictionary((KeyValuePair<IFieldInfo, Type> kvp) => kvp.Key.Name, (KeyValuePair<IFieldInfo, Type> kvp) => kvp.Value);
                    bool flag6 = !canBeShowFields.HasValues<IFieldInfo, Type>();
                    if (flag6)
                    {
                        result = null;
                    }
                    else
                    {
                        key = IFeatureClassBusinessExtention.SqlEnCode(key);
                        StringBuilder sb = new StringBuilder();
                        dictionary.Keys.ToList<string>().ForEach(delegate (string field)
                        {
                            sb.Append(string.Concat(new string[]
                            {
                                " or \"",
                                field,
                                "\" like '%",
                                key,
                                "%'"
                            }));
                        });
                        bool flag7 = sb.Length > 4;
                        if (flag7)
                        {
                            sb.Remove(0, 4);
                        }
                        string text = sb.ToString();
                        sb.Clear();
                        result = text;
                    }
                }
            }
        }
        return result;
    }

    // Token: 0x06000005 RID: 5 RVA: 0x00002628 File Offset: 0x00000828
    private static string SqlEnCode(string likeString)
    {
        bool flag = string.IsNullOrEmpty(likeString);
        string result;
        if (flag)
        {
            result = string.Empty;
        }
        else
        {
            result = likeString.Replace("[", "[[]").Replace("_", "[_]").Replace("%", "[%]");
        }
        return result;
    }

    // Token: 0x06000006 RID: 6 RVA: 0x0000267C File Offset: 0x0000087C
    public static IRowBufferCollection SpatialQuery(this IFeatureClass fc, IGeometry geometry, string geoField)
    {
        bool flag = fc == null;
        if (flag)
        {
            throw new NullReferenceException("fc");
        }
        IFdeCursor fdeCursor = null;
        IRowBufferCollection result;
        try
        {
            IRowBufferCollection rowBufferCollection = new RowBufferCollection();
            ISpatialFilter spatialFilter = new SpatialFilter();
            spatialFilter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
            spatialFilter.Geometry = geometry;
            spatialFilter.GeometryField = geoField;
            fdeCursor = fc.Search(spatialFilter, false);
            IRowBuffer value;
            while ((value = fdeCursor.NextRow()) != null)
            {
                rowBufferCollection.Add(value);
            }
            spatialFilter.ReleaseComObject();
            result = rowBufferCollection;
        }
        catch (COMException innerException)
        {
            throw new Exception("SpatialQuery error", innerException);
        }
        catch (Exception innerException2)
        {
            throw new Exception("SpatialQuery error", innerException2);
        }
        finally
        {
            fdeCursor.ReleaseComObject();
        }
        return result;
    }
}
