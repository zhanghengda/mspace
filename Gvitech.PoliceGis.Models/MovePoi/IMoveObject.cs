using System;
using System.Data;
using System.Reflection;

namespace Mmc.Mspace.Models.MovePoi
{
	// Token: 0x02000007 RID: 7
	public interface IMoveObject
	{
		// Token: 0x0600004C RID: 76
		DataTable ToDataTable();

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004D RID: 77
		string Id { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004E RID: 78
		string Name { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004F RID: 79
		string Coordinate { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000050 RID: 80
		MoveObjectType MoveObjectType { get; }

		// Token: 0x06000051 RID: 81
		bool IsMatchKey(string key);

		// Token: 0x06000052 RID: 82
		PropertyInfo[] GetProperties();

		// Token: 0x06000053 RID: 83
		object GetPropertyValue(string propName);

		// Token: 0x06000054 RID: 84
		bool Refresh(string jsonStr);

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000055 RID: 85
		bool IsSelectedIfo { get; }
	}
}
