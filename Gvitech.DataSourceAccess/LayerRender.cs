using System;
using Gvitech.CityMaker.RenderControl;

namespace Mmc.DataSourceAccess
{
	
	public class LayerRender
	{
		
		public LayerRender(IGeometryRender geoRender, ITextRender txtRender)
		{
			this.GeometryRender = geoRender;
			this.TextRender = txtRender;
		}

		
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003265 File Offset: 0x00001465
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000326D File Offset: 0x0000146D
		public IGeometryRender GeometryRender { get; private set; }

		
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003276 File Offset: 0x00001476
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000327E File Offset: 0x0000147E
		public ITextRender TextRender { get; private set; }

		
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003287 File Offset: 0x00001487
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000328F File Offset: 0x0000148F
		public float MaxVisibleDistance { get; set; }

		
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003298 File Offset: 0x00001498
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000032A0 File Offset: 0x000014A0
		public float MinVisibleDistance { get; set; }

		
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000032A9 File Offset: 0x000014A9
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000032B1 File Offset: 0x000014B1
		public float MinVisiblePixels { get; set; }
	}
}
