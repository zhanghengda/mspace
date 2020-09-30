using System;
using System.Data;
using System.Reflection;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;

namespace Mmc.Mspace.Models.MovePoi
{
	// Token: 0x02000008 RID: 8
	public interface IMovePoi
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000056 RID: 86
		string Id { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000057 RID: 87
		string Name { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000058 RID: 88
		string RenderId { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000059 RID: 89
		MoveObjectType MoveObjectType { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005A RID: 90
		DataTable ShowDataTable { get; }

		// Token: 0x0600005B RID: 91
		bool IsMatchKey(string key);

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005C RID: 92
		DateTime LastUpdateTime { get; }

		// Token: 0x0600005D RID: 93
		bool UpdatePoi(IMoveObject obj);

		// Token: 0x0600005E RID: 94
		bool Show(bool visible, gviViewportMask mask = gviViewportMask.gviViewAllNormalView);

		// Token: 0x0600005F RID: 95
		bool FlyTo(ICamera camera);

		// Token: 0x06000060 RID: 96
		bool RefreshPosition();

		// Token: 0x06000061 RID: 97
		void HightLight(uint color = 4294901760u);

		// Token: 0x06000062 RID: 98
		void UnHightLight();

		// Token: 0x06000063 RID: 99
		void Release();

		// Token: 0x06000064 RID: 100
		PropertyInfo[] GetMoveObjectProperties();

		// Token: 0x06000065 RID: 101
		object GetPropertyValue(string name);

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000066 RID: 102
		IPoint Position { get; }
	}
}
