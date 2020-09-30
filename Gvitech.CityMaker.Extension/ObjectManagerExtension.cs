using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static class ObjectManagerExtension
{
    private static Guid _guid;
    private static bool _getedGuid;

    public static Guid GetGuid(this IObjectManager objManager)
    {
        if (!_getedGuid)
        {
            _getedGuid = true;
            _guid = objManager.GetProjectTree().RootID;
        }
        return _guid;
    }

    public static I3DTileLayer Create3DTileLayer(this IObjectManager @this, string layerInfo, string password)
	{
		bool flag = @this == null;
		I3DTileLayer result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.Create3DTileLayer(layerInfo, password, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static ICameraTour CreateCameraTour(this IObjectManager @this)
	{
		bool flag = @this == null;
		ICameraTour result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateCameraTour(guid);
		}
		return result;
	}

	public static IDynamicObject CreateDynamicObject(this IObjectManager @this)
	{
		bool flag = @this == null;
		IDynamicObject result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateDynamicObject(guid);
		}
		return result;
	}

	public static IFeatureLayer CreateFeatureLayer(this IObjectManager @this, IFeatureClass featureClass, string geoField, ITextRender textRender, IGeometryRender geoRender)
	{
		bool flag = @this == null;
		IFeatureLayer result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateFeatureLayer(featureClass, geoField, textRender, geoRender, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static IImageryLayer CreateImageryLayer(this IObjectManager @this, string connectionString)
	{
		bool flag = @this == null;
		IImageryLayer result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateImageryLayer(connectionString, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static ILabel CreateLabel(this IObjectManager @this, IPoint position = null)
	{
		bool flag = @this == null;
		ILabel result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateLabel(guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0).SetPosition(position);
		}
		return result;
	}

	public static IMotionPath CreateMotionPath(this IObjectManager @this)
	{
		bool flag = @this == null;
		IMotionPath result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateMotionPath(guid);
		}
		return result;
	}

	public static IOverlayLabel CreateOverlayLabel(this IObjectManager @this)
	{
		bool flag = @this == null;
		IOverlayLabel result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateOverlayLabel(guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static IParticleEffect CreateParticleEffect(this IObjectManager @this)
	{
		bool flag = @this == null;
		IParticleEffect result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateParticleEffect(guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static IParticleEffect CreateParticleEffectFromFdb(this IObjectManager @this, IFeatureDataSet featureDataSet)
	{
		bool flag = @this == null;
		IParticleEffect result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateParticleEffectFromFDB(featureDataSet, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static IRenderArrow CreateRenderArrow(this IObjectManager @this, Guid groupId)
	{
		bool flag = @this == null;
		IRenderArrow result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateRenderArrow(guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static IRenderModelPoint CreateRenderModelPoint(this IObjectManager @this, IModelPoint modelPoint, IModelPointSymbol symbol)
	{
		bool flag = @this == null;
		IRenderModelPoint result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateRenderModelPoint(modelPoint, symbol, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static IRenderMultiPoint CreateRenderMultiPoint(this IObjectManager @this, IMultiPoint multiPoint, IPointSymbol symbol)
	{
		bool flag = @this == null;
		IRenderMultiPoint result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateRenderMultiPoint(multiPoint, symbol, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static IRenderMultiPolygon CreateRenderMultiPolygon(this IObjectManager @this, IMultiPolygon multiPolygon, ISurfaceSymbol symbol)
	{
		bool flag = @this == null;
		IRenderMultiPolygon result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateRenderMultiPolygon(multiPolygon, symbol, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static IRenderMultiPolyline CreateRenderMultiPolyline(this IObjectManager @this, IMultiPolyline multiPolyline, ICurveSymbol symbol)
	{
		bool flag = @this == null;
		IRenderMultiPolyline result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateRenderMultiPolyline(multiPolyline, symbol, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static IRenderMultiTriMesh CreateRenderMultiTriMesh(this IObjectManager @this, IMultiTriMesh multiTriMesh, ISurfaceSymbol symbol)
	{
		bool flag = @this == null;
		IRenderMultiTriMesh result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateRenderMultiTriMesh(multiTriMesh, symbol, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static IRenderPoint CreateRenderPoint(this IObjectManager @this, IPoint point, IPointSymbol symbol)
	{
		bool flag = @this == null;
		IRenderPoint result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateRenderPoint(point, symbol, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static IRenderPolygon CreateRenderPolygon(this IObjectManager @this, IPolygon polygon, ISurfaceSymbol symbol)
	{
		bool flag = @this == null;
		IRenderPolygon result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateRenderPolygon(polygon, symbol, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static IRenderPolyline CreateRenderPolyline(this IObjectManager @this, IPolyline polyline, ICurveSymbol symbol)
	{
		bool flag = @this == null;
		IRenderPolyline result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateRenderPolyline(polyline, symbol, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static IRenderTriMesh CreateRenderTriMesh(this IObjectManager @this, ITriMesh triMesh, ISurfaceSymbol symbol)
	{
		bool flag = @this == null;
		IRenderTriMesh result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateRenderTriMesh(triMesh, symbol, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static ISkinnedMesh CreateSkinnedMesh(this IObjectManager @this, IModelPoint modelPoint)
	{
		bool flag = @this == null;
		ISkinnedMesh result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateSkinnedMesh(modelPoint, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static ITableLabel CreateTableLabel(this IObjectManager @this, int rowCount, int columnCount)
	{
		bool flag = @this == null;
		ITableLabel result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateTableLabel(rowCount, columnCount, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

    /// <summary>
    /// 创建多行两列的ableLabel
    /// </summary>
    /// <param name="objManager"></param>
    /// <param name="rowNum">行数</param>
    /// <param name="fisrtColWidth">第一列宽度</param>
    /// <param name="secendColWidth">第二列宽度</param>
    /// <returns></returns>
    public static ITableLabel CreateTableLabelWith2Col(this IObjectManager @this, int rowNum, int fisrtColWidth = 90, int secendColWidth = 50)
    {
        // 创建一个有多行2列的TableLabel
        var tableLabel = @this.CreateTableLabel(rowNum, 2);

        // 列宽度
        tableLabel.SetColumnWidth(0, fisrtColWidth);
        tableLabel.SetColumnWidth(1, secendColWidth);

        // 表的边框颜色
        tableLabel.BorderColor = ColorConvert.UintToColor(0xffffffff);
        // 表的边框的宽度
        tableLabel.BorderWidth = 2;
        // 表的背景色
        tableLabel.TableBackgroundColor = ColorConvert.UintToColor(4290707456);

        // 标题背景色
        tableLabel.TitleBackgroundColor = ColorConvert.UintToColor(0xff000000);

        // 第一列文本样式
        TextAttribute headerTextAttribute = new TextAttribute();
        headerTextAttribute.TextColor = ColorConvert.UintToColor(0xffffffff);
        headerTextAttribute.OutlineColor = ColorConvert.UintToColor(0xff000000);
        headerTextAttribute.Font = "微软雅黑";
        headerTextAttribute.Bold = true;
        headerTextAttribute.TextSize = 12;
        headerTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
        tableLabel.SetColumnTextAttribute(0, headerTextAttribute);

        // 第二列文本样式
        TextAttribute contentTextAttribute = new TextAttribute();
        contentTextAttribute.TextColor = ColorConvert.UintToColor(4293256677);
        contentTextAttribute.OutlineColor = ColorConvert.UintToColor(0xff000000);
        contentTextAttribute.Font = "微软雅黑";
        contentTextAttribute.TextSize = 12;
        contentTextAttribute.Bold = false;
        contentTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
        tableLabel.SetColumnTextAttribute(1, contentTextAttribute);

        // 标题文本样式
        TextAttribute capitalTextAttribute = new TextAttribute();
        capitalTextAttribute.TextColor = ColorConvert.UintToColor(0xffffffff);
        capitalTextAttribute.OutlineColor = ColorConvert.UintToColor(4279834905);
        capitalTextAttribute.Font = "微软雅黑";
        capitalTextAttribute.TextSize = 14;
        capitalTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
        capitalTextAttribute.Bold = true;
        tableLabel.TitleTextAttribute = capitalTextAttribute;

        return tableLabel;
    }

    public static IOverlayLabel CreateOverlayLabel(this IObjectManager @this, string labelText, ITextAttribute textAttribute, gviViewportMask gviViewportMask = gviViewportMask.gviViewAllNormalView, double maxObserveDistance = 10000.0, double minObserveDistance = 0.0)
	{
		IOverlayLabel overlayLabel = @this.CreateOverlayLabel();
		overlayLabel.SetHeight(45, 0f, 0f);
		overlayLabel.SetWidth(80, 0f, 0f);
		overlayLabel.Alignment = gviPivotAlignment.gviPivotAlignTopLeft;
		overlayLabel.AttributeMask = gviAttributeMask.gviAttributeHighlight;
		overlayLabel.TextStyle = (TextAttribute)textAttribute;
		overlayLabel.VisibleMask = gviViewportMask;
		overlayLabel.MinVisiblePixels = (float)minObserveDistance;
		overlayLabel.MaxVisibleDistance = maxObserveDistance;
		overlayLabel.Text = labelText;
		return overlayLabel;
	}

	public static ITerrainHole CreateTerrainHole(this IObjectManager @this, IPolygon polygon)
	{
		bool flag = @this == null;
		ITerrainHole result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateTerrainHole(polygon, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static ITerrainModifier CreateTerrainModifier(this IObjectManager @this, IPolygon polygon)
	{
		bool flag = @this == null;
		ITerrainModifier result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateTerrainModifier(polygon, guid).SetVisibleParam(100000.0, 15f, gviViewportMask.gviViewAllNormalView, gviDepthTestMode.gviDepthTestEnable, 0.0, -100000.0);
		}
		return result;
	}

	public static ITerrainRoute CreateTerrainRoute(this IObjectManager @this)
	{
		bool flag = @this == null;
		ITerrainRoute result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateTerrainRoute(guid);
		}
		return result;
	}

	public static ITerrainVideo CreateTerrainVideo(this IObjectManager @this, IPoint position)
	{
		bool flag = @this == null;
		ITerrainVideo result;
		if (flag)
		{
			result = null;
		}
		else
		{
			Guid guid = @this.GetGuid();
			result = @this.CreateTerrainVideo(position, guid);
		}
		return result;
	}

	public static IRenderPolyline CreateRenderPolyline(this IObjectManager @this, List<IVector3> pVectors, ICurveSymbol symb = null, ISpatialCRS crs = null)
	{
		bool flag = @this == null;
		IRenderPolyline result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = !pVectors.HasValues<IVector3>() || pVectors.Count < 2;
			if (flag2)
			{
				result = null;
			}
			else
			{
				bool flag3 = symb == null;
				if (flag3)
				{
					symb = new CurveSymbol
					{
						Color = ColorConvert.UintToColor(4294901760u),
						Width = 10f
					};
				}
				IGeometryFactory geoFactory = new GeometryFactory();
				List<IPoint> points = new List<IPoint>();
				pVectors.ForEach(delegate(IVector3 vector)
				{
					points.AddEx(geoFactory.CreatePoint(vector, null));
				});
				IPolyline polyline = geoFactory.CreatePolyline(points, crs);
				IRenderPolyline renderPolyline = @this.CreateRenderPolyline(polyline, symb);
				polyline.ReleaseComObject();
				geoFactory.ReleaseComObject();
				result = renderPolyline;
			}
		}
		return result;
	}

	public static IRenderPoint CreateRenderPoint(this IObjectManager @this, IVector3 pVector, IPointSymbol symb = null, ISpatialCRS crs = null, IGeometryFactory geoFactory = null)
	{
		bool flag = @this == null;
		IRenderPoint result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = pVector == null;
			if (flag2)
			{
				result = null;
			}
			else
			{
				IPoint point = null;
				IPointSymbol comObj = symb ?? new SimplePointSymbol
				{
					FillColor = ColorConvert.UintToColor(4278190335u),
					Size = 10
				};
				IGeometryFactory this2 = geoFactory ?? new GeometryFactory();
				try
				{
					result = @this.CreateRenderPoint(point = geoFactory.CreatePoint(pVector, crs), symb);
					return result;
				}
				catch (Exception )
				{
				}
				finally
				{
					bool flag3 = point != null;
					if (flag3)
					{
						point.ReleaseComObject();
					}
					bool flag4 = symb == null;
					if (flag4)
					{
						ComFactory.ReleaseComObject(comObj);
					}
					bool flag5 = geoFactory == null;
					if (flag5)
					{
						this2.ReleaseComObject();
					}
				}
				result = null;
			}
		}
		return result;
	}

	public static IGeometryRender CreateGeometryRenderFromXmlFile(this IObjectManager @this, string file)
	{
		IGeometryRender result;
		if (string.IsNullOrEmpty(file)|| !File.Exists(file))
		{
			result = null;
		}
		else
		{
			string text = file.ReadFile(Encoding.UTF8, FileMode.Open);
            result = (string.IsNullOrEmpty(text) ? null : @this.CreateGeometryRenderFromXML(text));
		}
		return result;
	}

	public static ITextRender CreateTextRenderFromXmlFile(this IObjectManager @this, string file)
	{
		ITextRender result;
		if (string.IsNullOrEmpty(file)|| !File.Exists(file))
		{
			result = null;
		}
		else
		{
			string text = file.ReadFile(Encoding.UTF8, FileMode.Open);
			result = (string.IsNullOrEmpty(text) ? null : @this.CreateTextRenderFromXML(text));
		}
		return result;
	}

	public static void ReleaseRenderObject(this IObjectManager @this, params IRenderable[] renders)
	{
		bool flag = @this == null;
		if (!flag)
		{
			bool flag2 = !renders.HasValues<IRenderable>();
			if (!flag2)
			{
				renders.ForEach(delegate(IRenderable r)
				{
					bool flag3 = r == null;
					if (!flag3)
					{
						@this.DeleteObject(r.Guid);
						r = null;
					}
				});
			}
		}
	}

    public static IRenderPOI CreateRPoi(this IObjectManager @this,IPOI poi, double maxVisibleDistance = 1500, gviDepthTestMode depthMode = gviDepthTestMode.gviDepthTestAdvance)
    {
        if (@this == null || poi == null) return null;
        var rPoi = @this.CreateRenderPOI(poi);
        rPoi.DepthTestMode = depthMode;
        rPoi.MaxVisibleDistance = maxVisibleDistance;
        rPoi.Name = poi.Name;
        return rPoi;
    }

}
