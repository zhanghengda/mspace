using System;
using System.Collections.Generic;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;

namespace Mmc.Business.Utils
{
	// Token: 0x02000008 RID: 8
	public class QueryFilterFactory
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002B28 File Offset: 0x00000D28
		public static T CreateQueryFilter<T>(string whereClause, List<string> subFields = null, int[] ids = null, string postfixClause = null, string prefixClause = null) where T : class, IQueryFilter, new()
		{
            //QueryFilterFactory.<>c__DisplayClass0_0<T> bufStyle = new QueryFilterFactory.<>c__DisplayClass0_0<T>();
            //QueryFilterFactory.<>c__DisplayClass0_0<T> <>c__DisplayClass0_ = bufStyle;
            //T index = Activator.CreateInstance<T>();
            //index.WhereClause = (string.IsNullOrEmpty(whereClause) ? "1=1" : whereClause);
            //index.PostfixClause = postfixClause;
            //index.PrefixClause = prefixClause;
            //<>c__DisplayClass0_.filter = index;
            //bool flag = ids != null && ids.Length != 0;
            //if (flag)
            //{
            //	bufStyle.filter.IdsFilter = ids;
            //}
            //bool flag2 = subFields.HasValues<string>();
            //if (flag2)
            //{
            //	subFields.ForEach(delegate(string field)
            //	{
            //		bufStyle.filter.AddSubField(field);
            //	});
            //}
            //return bufStyle.filter;


            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = (string.IsNullOrEmpty(whereClause) ? "1=1" : whereClause);
            filter.PostfixClause = postfixClause;
            filter.PrefixClause = prefixClause;
            if (ids != null && ids.Length != 0)
                filter.IdsFilter = ids;
            if (subFields.HasValues<string>())
            {
                subFields.ForEach(delegate (string field)
                {
                    filter.AddSubField(field);
                });
            }
            
            return (T)filter;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002BCC File Offset: 0x00000DCC
		public static T CreateSpatialFilter<T>(string whereClause, List<string> subFields = null, int[] ids = null, string postfixClause = null, string prefixClause = null, IGeometry geo = null) where T : class, ISpatialFilter, new()
		{
            ISpatialFilter filter = new SpatialFilter();
            filter.WhereClause = (string.IsNullOrEmpty(whereClause) ? "1=1" : whereClause);
            filter.PostfixClause = postfixClause;
            filter.PrefixClause = prefixClause;
            if (ids != null && ids.Length != 0)
                filter.IdsFilter = ids;
            if (subFields.HasValues<string>())
            {
                subFields.ForEach(delegate (string field)
                {
                    filter.AddSubField(field);
                });
            }
            bool flag2 = geo != null;
            if (geo != null)
            {
                filter.Geometry = geo;
                filter.GeometryField = "Geometry";
                filter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
            }
            return (T)filter;
            //QueryFilterFactory.<>c__DisplayClass1_0<T> lyrSty = new QueryFilterFactory.<>c__DisplayClass1_0<T>();
            //QueryFilterFactory.<>c__DisplayClass1_0<T> <>c__DisplayClass1_ = lyrSty;
            //T poiLayer = Activator.CreateInstance<T>();
            //poiLayer.WhereClause = (string.IsNullOrEmpty(whereClause) ? "1=1" : whereClause);
            //poiLayer.PostfixClause = postfixClause;
            //poiLayer.PrefixClause = prefixClause;
            //<>c__DisplayClass1_.filter = poiLayer;
            //bool xmlDoc = ids != null && ids.Length != 0;
            //if (xmlDoc)
            //{
            //	lyrSty.filter.IdsFilter = ids;
            //}
            //bool flag = subFields.HasValues<string>();
            //if (flag)
            //{
            //	subFields.ForEach(delegate(string field)
            //	{
            //		lyrSty.filter.AddSubField(field);
            //	});
            //}
            //bool flag2 = geo != null;
            //if (flag2)
            //{
            //	lyrSty.filter.Geometry = geo;
            //	lyrSty.filter.GeometryField = "Geometry";
            //	lyrSty.filter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
            //}
            //return lyrSty.filter;


        }
	}
}
