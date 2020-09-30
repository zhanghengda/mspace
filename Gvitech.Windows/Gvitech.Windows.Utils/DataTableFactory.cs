using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mmc.Windows.Utils
{
	public static class DataTableFactory
	{
		//[CompilerGenerated]
		//[Serializable]
		//private sealed class <>c
		//{
		//	public static readonly DataTableFactory.<>c <>9 = new DataTableFactory.<>c();

		//	public static Func<Tuple<string, string, Type>, bool> <>9__0_0;

		//	internal bool <CreateDataTable>b__0_0(Tuple<string, string, Type> tup)
		//	{
		//		return !string.IsNullOrEmpty(tup.Item1);
		//	}
		//}

		public static DataTable CreateDataTable(List<Tuple<string, string, Type>> lst)
		{
            if (lst == null || lst.Count < 1) return null;
            var dt = new DataTable();
            var dnyEnumerable = lst.Where(tup => !string.IsNullOrEmpty(tup.Item1));
            if (dnyEnumerable != null)
                dnyEnumerable.ToList().ForEach(tup => dt.Columns.Add(tup.Item1, tup.Item2, tup.Item3));
            return dt;

        }
	}
}
