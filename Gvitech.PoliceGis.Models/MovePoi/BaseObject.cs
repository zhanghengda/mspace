using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Mmc.Windows.Utils;

namespace Mmc.Mspace.Models.MovePoi
{
	// Token: 0x02000014 RID: 20
	public abstract class BaseObject : IBaseObject
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x000041E8 File Offset: 0x000023E8
		public DataTable ToDataTable()
		{
			DataTable dt = DataTableFactory.CreateDataTable(new List<Tuple<string, string, Type>>(2)
			{
				new Tuple<string, string, Type>(BaseObject.PropertyField, BaseObject.PropertyField, typeof(string)),
				new Tuple<string, string, Type>(BaseObject.ValueField, BaseObject.ValueField, typeof(string))
			});
			PropertyInfo[] properties = base.GetType().GetProperties();
			bool flag = !CollectionExtension.HasValues<PropertyInfo>(properties);
			DataTable result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DataRow dr;
				properties.ToList<PropertyInfo>().ForEach(delegate(PropertyInfo p)
				{
					dr = dt.NewRow();
					dr.ItemArray = new object[]
					{
						PropertyInfoExtension.GetAliasName(p),
						p.GetValue(this)
					};
					dt.Rows.Add(dr);
				});
				result = dt;
			}
			return result;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004298 File Offset: 0x00002498
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = obj.GetType() != base.GetType();
				if (flag2)
				{
					result = false;
				}
				else
				{
					Policeman policeman = obj as Policeman;
					PropertyInfo[] properties = base.GetType().GetProperties();
					bool flag3 = !CollectionExtension.HasValues<PropertyInfo>(properties);
					result = (flag3 || properties.ToList<PropertyInfo>().TrueForAll((PropertyInfo p) => p.GetValue(this) == p.GetValue(obj)));
				}
			}
			return result;
		}

		// Token: 0x0400004E RID: 78
		private static readonly string PropertyField = "类别";

		// Token: 0x0400004F RID: 79
		private static readonly string ValueField = "信息";
	}
}
