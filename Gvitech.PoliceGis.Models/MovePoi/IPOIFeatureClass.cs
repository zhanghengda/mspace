using System;
using System.Collections.Generic;
using Gvitech.CityMaker.RenderControl;
using Mmc.Mspace.Common;

namespace Mmc.Mspace.Models.MovePoi
{
	// Token: 0x02000009 RID: 9
	public interface IPOIFeatureClass : IShowLayer
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000067 RID: 103
		bool Visible { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000068 RID: 104
		gviViewportMask ViewportMask { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000069 RID: 105
		// (set) Token: 0x0600006A RID: 106
		string Name { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006B RID: 107
		string GuidString { get; }

		// Token: 0x0600006C RID: 108
		void UpdateData(IMoveObject moveObject);

		// Token: 0x0600006D RID: 109
		void UpdateData(List<IMoveObject> moveObjects);

		// Token: 0x0600006E RID: 110
		void UpdateData(IMoveObject[] moveObjects);

		// Token: 0x0600006F RID: 111
		void HideOutTimePoi();

		// Token: 0x06000070 RID: 112
		void Close();

		// Token: 0x06000071 RID: 113
		IMovePoi SearchByRenderId(string guid);

		// Token: 0x06000072 RID: 114
		IMovePoi SearchById(string id);
	}
}
