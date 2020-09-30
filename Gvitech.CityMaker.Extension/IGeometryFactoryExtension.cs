using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

public static class IGeometryFactoryExtension
{
	public static IPoint CreatePoint(this IGeometryFactory @this, double x, double y, double z = 0.0, ISpatialCRS crs = null)
	{
		IPoint point = @this.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
		point.X = x;
		point.Y = y;
		point.Z = z;
		bool flag = crs != null;
		if (flag)
		{
			point.SpatialCRS = crs;
		}
		return point;
	}

	public static IPoint CreatePoint(this IGeometryFactory @this, IVector3 vector, ISpatialCRS crs = null)
	{
		IPoint point = @this.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
		point.SetPostion(vector);
		bool flag = crs != null;
		if (flag)
		{
			point.SpatialCRS = crs;
		}
		return point;
	}

	public static IPolyline CreatePolyline(this IGeometryFactory geoFacotry, IPoint beginPoint, IPoint endPoint, ISpatialCRS crs = null)
	{
		IPolyline polyline = geoFacotry.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ ) as IPolyline;
		bool flag = polyline != null;
		if (flag)
		{
			bool flag2 = crs != null;
			if (flag2)
			{
				polyline.SpatialCRS = crs;
			}
			polyline.AppendPoint(beginPoint);
			polyline.AppendPoint(endPoint);
		}
		return polyline;
	}
    
    public static IPolyline CreatePolyline(this IGeometryFactory geoFacotry, List<IPoint> points, ISpatialCRS crs = null)
	{
		bool flag = !points.HasValues<IPoint>() || points.Count < 2;
		IPolyline result;
		if (flag)
		{
			result = null;
		}
		else
		{
			IPolyline line = geoFacotry.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ ) as IPolyline;
			bool flag2 = line != null;
			if (flag2)
			{
				bool flag3 = crs != null;
				if (flag3)
				{
					line.SpatialCRS = crs;
				}
				points.ForEach(delegate(IPoint p)
				{
					line.AppendPoint(p);
				});
			}
			result = line;
		}
		return result;
	}


    public static IPolyline CreatePolyline(this IGeometryFactory geoFacotry, string geom, ISpatialCRS crs = null)
    {

        //bool flag = !points.HasValues<IVector3>() || points.Count < 2;
        bool flag = !geom.HasValues();
        IPolyline result;
        if (flag)
        {
            result = null;
        }
        else
        {
            var line = geoFacotry.CreateFromWKT(geom) as IPolyline;
            //IPolyline line = geoFacotry.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;            
            bool flag2 = line != null;
            if (flag2)
            {
                bool flag3 = crs != null;
                if (flag3)
                {
                    line.SpatialCRS = crs;
                }
                //points.ForEach(delegate (IVector3 p)
                //{
                //    line.AppendPoint(geoFacotry.CreatePoint(p, crs));
                //});
            }
            result = line;
        }
        return result;
    }

    public static IPolyline CreatePolyline(this IGeometryFactory geoFacotry, List<IVector3> points, ISpatialCRS crs = null)
	{
		bool flag = !points.HasValues<IVector3>() || points.Count < 2;
		IPolyline result;
		if (flag)
		{
			result = null;
		}
		else
		{
			IPolyline line = geoFacotry.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ ) as IPolyline;
			bool flag2 = line != null;
			if (flag2)
			{
				bool flag3 = crs != null;
				if (flag3)
				{
					line.SpatialCRS = crs;
				}
				points.ForEach(delegate(IVector3 p)
				{
					line.AppendPoint(geoFacotry.CreatePoint(p, crs));
				});
			}
			result = line;
		}
		return result;
	}

	public static IPolygon CreatePolygon(this IGeometryFactory geoFacotry, string geom, ISpatialCRS crs = null)
	{
		bool flag = flag = !geom.HasValues(); ;
		IPolygon result;
		if (flag)
		{
			result = null;
		}
		else
		{
            IPolygon polygon = geoFacotry.CreateFromWKT(geom) as IPolygon;
            //IPolygon polygon = geoFacotry.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ ) as IPolygon;
            bool flag2 = polygon != null;
			if (flag2)
			{
				bool flag3 = crs != null;
				if (flag3)
				{
					polygon.SpatialCRS = crs;
				}
				//IRing ring = polygon.ExteriorRing;
				//points.ForEach(delegate(IVector3 p)
				//{
				//	ring.AppendPoint(p.ToPoint(geoFacotry, crs));
				//});
				//ring.Close();
			}
			result = polygon;
		}
		return result;
	}

	public static IPolygon CreatePolygon(this IGeometryFactory geoFacotry, List<IPoint> points)
	{
		bool flag = geoFacotry == null || !points.HasValues<IPoint>() || points.Count < 3;
		IPolygon result;
		if (flag)
		{
			result = null;
		}
		else
		{
			IPolygon polygon = geoFacotry.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ ) as IPolygon;
			bool flag2 = polygon != null;
			if (flag2)
			{
				IRing ring = polygon.ExteriorRing;
				points.ForEach(delegate(IPoint p)
				{
					ring.AppendPoint(p);
				});
				ring.Close();
			}
			result = polygon;
		}
		return result;
	}



    public static IPolyline CreateFromXml(this IGeometryFactory geoFacotry,string file, ISpatialCRS crs = null)
    {
        bool flag = geoFacotry == null || !File.Exists(file);
        IPolyline result;

        if (flag)
        {
            result= null;
        }
        else
        {
            IPolyline polyline = geoFacotry.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            polyline.SpatialCRS = crs;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(file);

            XmlElement root = xmlDoc.DocumentElement;
            XmlNode _document = root.GetElementsByTagName("Document")[0];
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);

            if (_document == null || _document.Attributes["xmlns"] == null)
            {
                nsmgr.AddNamespace("ns", root.Attributes["xmlns"].Value);
            }
            else
            {
                nsmgr.AddNamespace("ns", _document.Attributes["xmlns"].Value);
            }

            XmlNodeList xmlmark = root.GetElementsByTagName("Placemark");
            for (int m = 0; m < xmlmark.Count; m++)
            {
                XmlNodeList xmlmarkChilds = xmlmark[m].ChildNodes;
                for (int n = 0; n < xmlmarkChilds.Count; n++)
                {
                    XmlNode node = xmlmarkChilds[n];
                    if (node.Name == "LineString" || node.Name == "LineRing")
                    {
                        XmlNode coordsNode = node.FirstChild;
                        while (coordsNode != null && coordsNode.Name != "coordinates")
                        {
                            coordsNode = coordsNode.NextSibling;
                        }
                        if (coordsNode == null)
                            continue;
                        // 用正则表达式去除字符串首位的制表符、换行符等符号，然后用' '来划分为string[]
                        string tt = coordsNode.InnerText;
                        Regex reg = new Regex("\f|\n|\r|\t");
                        string modified = reg.Replace(tt, "");
                        string[] ss = modified.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);//清除空格
                        for (int cc = 0; cc < ss.Length; cc++)
                        {
                            //取点生成polyline
                            IPoint tempPoint = geoFacotry.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            string[] aa = ss[cc].Split(',');
                            if (aa.Length >= 3)
                                tempPoint.SetPostion(Convert.ToDouble(aa[0]), Convert.ToDouble(aa[1]), Convert.ToDouble(aa[2]));
                            else if (aa.Length == 2)
                                tempPoint.SetPostion(Convert.ToDouble(aa[0]), Convert.ToDouble(aa[1]), 0);

                            polyline.AppendPoint(tempPoint);
                        }
                    }
                }
            }

            result = polyline;
        }

        return result;
    }


    public static IPOI CreatePoi(this IGeometryFactory geoFacotry, double x, double y, double z, string iconPath, string name, int size = 32, bool isShow = true, ISpatialCRS crs = null)
    {
        if (geoFacotry == null) return null;
        var poi = geoFacotry.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
        poi.SetPostion(x, y, z);
        poi.ImageName = iconPath;
        poi.Size = size;
        poi.ShowName = isShow;
        poi.Name = name;
        poi.SpatialCRS = crs;
        return poi;
    }


    public static IPOI CreatePoiFromWkt(this IGeometryFactory geoFacotry, string wkt, string iconPath, string name, int size = 32, bool isShow = true, ISpatialCRS crs = null)
    {
        if (string.IsNullOrEmpty(wkt)) return null;
        var poi = geoFacotry.CreateFromWKT(wkt) as IPOI;
        poi.ImageName = iconPath;
        poi.Size = size;
        poi.ShowName = isShow;
        poi.Name = name;
        poi.SpatialCRS = crs;
        return poi;
    }
}
