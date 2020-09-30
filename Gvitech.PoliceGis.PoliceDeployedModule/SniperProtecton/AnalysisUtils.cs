using System;
using System.Collections.Generic;
using System.Linq;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Resource;
using Mmc.Framework.Services;
using Mmc.Mspace.Const.ConstDataBase;

namespace Mmc.Mspace.PoliceDeployedModule.SniperProtecton
{
	// Token: 0x02000007 RID: 7
	public static class AnalysisUtils
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public static double CaculateDistance(IPoint start, IPoint end)
		{
			bool flag = start != null && end != null;
			double result;
			if (flag)
			{
				result = Math.Sqrt((end.X - start.X) * (end.X - start.X) + (end.Y - start.Y) * (end.Y - start.Y) + (end.Z - start.Z) * (end.Z - start.Z));
			}
			else
			{
				result = 0.0;
			}
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003C48 File Offset: 0x00001E48
		public static bool PolylineIntersectModel(IPolyline polyline, Dictionary<IRowBuffer, IFeatureClass> rowBufferList, out IPoint outIntersectPoint)
		{
			outIntersectPoint = null;
			bool flag = polyline == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = rowBufferList == null || rowBufferList.Count <= 0;
				if (flag2)
				{
					result = false;
				}
				else
				{
					ICRSFactory icrsfactory = new CRSFactory();
					ISpatialCRS spatialCrs = GviMap.SpatialCrs;
					ISpatialCRS spatialCRS = icrsfactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;
					IGeometryFactory geometryFactory = new GeometryFactory();
					IPolyline polyline2 = polyline.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolyline;
					bool flag3 = polyline2 != null;
					if (flag3)
					{
					}
					Dictionary<double, IPoint> dictionary = new Dictionary<double, IPoint>();
					Dictionary<string, IModel> dictionary2 = new Dictionary<string, IModel>();
					foreach (IRowBuffer rowBuffer in rowBufferList.Keys)
					{
						try
						{
							int num = rowBuffer.FieldIndex("Geometry");
							bool flag4 = num < 0;
							if (!flag4)
							{
								IGeometry geometry = rowBuffer.GetValue(num) as IGeometry;
								IModelPoint modelPoint = geometry as IModelPoint;
								bool flag5 = modelPoint == null;
								if (!flag5)
								{
									bool flag6 = !dictionary2.ContainsKey(modelPoint.ModelName);
									IModel model;
									if (flag6)
									{
										IFeatureClass featureClass = rowBufferList[rowBuffer];
										IResourceManager resourceManager = featureClass.FeatureDataSet as IResourceManager;
										bool flag7 = resourceManager == null;
										if (flag7)
										{
											continue;
										}
										model = resourceManager.GetModel(modelPoint.ModelName);
										bool flag8 = model == null;
										if (flag8)
										{
											continue;
										}
										dictionary2.Add(modelPoint.ModelName, model);
									}
									model = dictionary2[modelPoint.ModelName];
									IVector3 vector = new Vector3();
									IPoint endPoint = polyline.EndPoint;
									endPoint.SpatialCRS = spatialCrs;
									endPoint.Project(spatialCRS);
									IPoint startPoint = polyline.StartPoint;
									startPoint.SpatialCRS = spatialCrs;
									startPoint.Project(spatialCRS);
									vector.X = endPoint.X - startPoint.X;
									vector.Y = endPoint.Y - startPoint.Y;
									vector.Z = endPoint.Z - startPoint.Z;
									modelPoint.SpatialCRS = spatialCrs;
									modelPoint.Project(spatialCRS);
									IVector3 vector2 = modelPoint.RayIntersect(model, startPoint, vector);
									bool flag9 = vector2 != null;
									if (flag9)
									{
										IPoint point = geometryFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
										point.X = vector2.X;
										point.Y = vector2.Y;
										point.Z = vector2.Z;
										double key = AnalysisUtils.CaculateDistance(startPoint, point);
										bool flag10 = !dictionary.ContainsKey(key);
										if (flag10)
										{
											point.SpatialCRS = spatialCRS;
											point.Project(spatialCrs);
											dictionary.Add(key, point);
										}
									}
									bool flag11 = geometry != null;
									if (flag11)
									{
										FdeGeometryRelease.ReleaseComObject(geometry);
									}
								}
							}
						}
						catch (Exception ex)
						{
						}
					}
					foreach (IModel model2 in dictionary2.Values)
					{
						model2.Clear();
					}
					dictionary2.Clear();
					bool flag12 = geometryFactory != null;
					if (flag12)
					{
						FdeGeometryRelease.ReleaseComObject(geometryFactory);
						geometryFactory = null;
					}
					bool flag13 = dictionary.Count > 0;
					if (flag13)
					{
						double num2 = 0.0;
						try
						{
							num2 = dictionary.Keys.Min();
						}
						catch (Exception ex2)
						{
						}
						IPoint endPoint2 = polyline.EndPoint;
						endPoint2.SpatialCRS = spatialCrs;
						endPoint2.Project(spatialCRS);
						IPoint startPoint2 = polyline.StartPoint;
						startPoint2.SpatialCRS = spatialCrs;
						startPoint2.Project(spatialCRS);
						double num3 = AnalysisUtils.CaculateDistance(startPoint2, endPoint2);
						bool flag14 = num3 - num2 > 0.1;
						if (flag14)
						{
							bool flag15 = dictionary.ContainsKey(num2);
							if (flag15)
							{
								outIntersectPoint = dictionary[num2];
								result = true;
							}
							else
							{
								outIntersectPoint = null;
								result = false;
							}
						}
						else
						{
							outIntersectPoint = null;
							result = false;
						}
					}
					else
					{
						outIntersectPoint = null;
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000040AC File Offset: 0x000022AC
		public static Dictionary<IRowBuffer, IFeatureClass> CombineDictionary(Dictionary<IRowBuffer, IFeatureClass> dic1, Dictionary<IRowBuffer, IFeatureClass> dic2)
		{
			bool flag = (dic1 == null || dic1.Count <= 0) && (dic2 == null || dic2.Count <= 0);
			Dictionary<IRowBuffer, IFeatureClass> result;
			if (flag)
			{
				result = new Dictionary<IRowBuffer, IFeatureClass>();
			}
			else
			{
				bool flag2 = dic1 == null || dic1.Count <= 0;
				if (flag2)
				{
					result = dic2;
				}
				else
				{
					bool flag3 = dic2 == null || dic2.Count <= 0;
					if (flag3)
					{
						result = dic1;
					}
					else
					{
						Dictionary<IRowBuffer, IFeatureClass> dictionary = new Dictionary<IRowBuffer, IFeatureClass>();
						foreach (IRowBuffer key in dic1.Keys)
						{
							bool flag4 = dictionary.ContainsKey(key);
							if (!flag4)
							{
								dictionary.Add(key, dic1[key]);
							}
						}
						foreach (IRowBuffer key2 in dic2.Keys)
						{
							bool flag5 = dictionary.ContainsKey(key2);
							if (!flag5)
							{
								dictionary.Add(key2, dic2[key2]);
							}
						}
						result = dictionary;
					}
				}
			}
			return result;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00004200 File Offset: 0x00002400
		public static Dictionary<IRowBuffer, IFeatureClass> SubtractDictionary(Dictionary<IRowBuffer, IFeatureClass> dic1, Dictionary<IRowBuffer, IFeatureClass> dic2)
		{
			bool flag = dic1 == null || dic1.Count <= 0;
			Dictionary<IRowBuffer, IFeatureClass> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = dic2 == null || dic2.Count <= 0;
				if (flag2)
				{
					result = dic1;
				}
				else
				{
					Dictionary<IRowBuffer, IFeatureClass> dictionary = new Dictionary<IRowBuffer, IFeatureClass>();
					foreach (IRowBuffer key in dic1.Keys)
					{
						bool flag3 = dictionary.ContainsKey(key);
						if (!flag3)
						{
							bool flag4 = dic2.ContainsKey(key);
							if (!flag4)
							{
								dictionary.Add(key, dic1[key]);
							}
						}
					}
					result = dictionary;
				}
			}
			return result;
		}
	}
}
