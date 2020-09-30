using System;
using System.Drawing;
using System.IO;
using Gvitech.CityMaker.RenderControl;

namespace Mmc.Business.Media.LocationScene
{
	// Token: 0x0200000B RID: 11
	public class LocationScene
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002CBF File Offset: 0x00000EBF
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002CC7 File Offset: 0x00000EC7
		public int ID { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002CD0 File Offset: 0x00000ED0
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public int GroupID { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002CE1 File Offset: 0x00000EE1
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002CE9 File Offset: 0x00000EE9
		public int PlanID { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002CF2 File Offset: 0x00000EF2
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002CFA File Offset: 0x00000EFA
		public int Index { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002D03 File Offset: 0x00000F03
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002D0B File Offset: 0x00000F0B
		public string Name { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002D14 File Offset: 0x00000F14
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002D1C File Offset: 0x00000F1C
		public string Location { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002D25 File Offset: 0x00000F25
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002D2D File Offset: 0x00000F2D
		public string Comment { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002D36 File Offset: 0x00000F36
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002D3E File Offset: 0x00000F3E
		public double Duration { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002D47 File Offset: 0x00000F47
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002D4F File Offset: 0x00000F4F
		public byte[] Image { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002D58 File Offset: 0x00000F58
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002D60 File Offset: 0x00000F60
		public ConnectionType ConnectionType { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002D69 File Offset: 0x00000F69
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002D71 File Offset: 0x00000F71
		public GroupType GroupType { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002D7C File Offset: 0x00000F7C
		public Image ThumbnailImage
		{
			get
			{
				return System.Drawing.Image.FromStream(new MemoryStream(this.Image));
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public void FlyToScene(ICamera camera)
		{
			bool flag = string.IsNullOrEmpty(this.Location);
			if (!flag)
			{
				string[] array = this.Location.Split(new char[]
				{
					';'
				});
				bool flag2 = array == null || array.Length != 6;
				if (!flag2)
				{
					double x = array[0].ParseTo(0.0);
					double y = array[1].ParseTo(0.0);
					double z = array[2].ParseTo(0.0);
					double heading = array[3].ParseTo(0.0);
					double tilt = array[4].ParseTo(0.0);
					double roll = array[5].ParseTo(0.0);
					camera.SetCamera(x, y, z, heading, tilt, roll, null, gviSetCameraFlags.gviSetCameraNoFlags);
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002E78 File Offset: 0x00001078
		public override string ToString()
		{
			bool color = string.IsNullOrEmpty(this.Name);
			string e;
			if (color)
			{
				e = string.Empty;
			}
			else
			{
				e = base.ToString();
			}
			return e;
		}
	}
}
