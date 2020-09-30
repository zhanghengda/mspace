using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using System;

public static class ILabelExtension
{
	public static bool SetPosition(this ILabel @this, double x, double y, double z = 0.0)
	{
		bool flag = @this == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			IGeometryFactory geometryFactory = new GeometryFactory();
			IPoint point = geometryFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
			point.X = x;
			point.Y = y;
			point.Z = z;
			@this.Position = point;
			geometryFactory.ReleaseComObject();
			result = true;
		}
		return result;
	}

	public static bool SetPosition(this ILabel @this, IGeometryFactory geoFactory, double x, double y, double z = 0.0)
	{
		bool flag = @this == null || geoFactory == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			IPoint point = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
			point.X = x;
			point.Y = y;
			point.Z = z;
			@this.Position = point;
			result = true;
		}
		return result;
	}

	public static ILabel SetPosition(this ILabel @this, IPoint position)
	{
		bool flag = position != null;
		if (flag)
		{
			@this.Position = position;
		}
		return @this;
	}

    public static ILabel Clone(this ILabel @this, AxRenderControl renderCtl)
    {
        var label = renderCtl.ObjectManager.CreateLabel(renderCtl.ProjectTree.RootID);
        label.Position = @this.Position;
        label.Text = @this.Text;
        label.TextSymbol = @this.TextSymbol;
        return label;
    }
}
