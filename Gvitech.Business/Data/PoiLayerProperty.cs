using System;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;

namespace Mmc.Business.Data
{
	// Token: 0x02000010 RID: 16
	public class PoiLayerProperty
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000389C File Offset: 0x00001A9C
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000038B4 File Offset: 0x00001AB4
		public IGeometryRender GeoRender
		{
			get
			{
				return this._geoRender;
			}
			set
			{
				this._geoRender = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000038C0 File Offset: 0x00001AC0
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000038D8 File Offset: 0x00001AD8
		public gviViewportMask VisibleMask
		{
			get
			{
				return this._visibleMask;
			}
			set
			{
				this._visibleMask = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000038E4 File Offset: 0x00001AE4
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000038FC File Offset: 0x00001AFC
		public LayerStyleType LayerType
		{
			get
			{
				return this._layerType;
			}
			set
			{
				this._layerType = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003908 File Offset: 0x00001B08
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00003920 File Offset: 0x00001B20
		public ITextRender TextRender
		{
			get
			{
				return this._txtRender;
			}
			set
			{
				this._txtRender = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000392C File Offset: 0x00001B2C
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00003944 File Offset: 0x00001B44
		public double MaxVisibleDistance
		{
			get
			{
				return this._dMaxVisibleDistance;
			}
			set
			{
				this._dMaxVisibleDistance = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003950 File Offset: 0x00001B50
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00003968 File Offset: 0x00001B68
		public float MinVisiblePixels
		{
			get
			{
				return this._fMinVisiblePixels;
			}
			set
			{
				this._fMinVisiblePixels = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003974 File Offset: 0x00001B74
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000398C File Offset: 0x00001B8C
		public bool ForceCullMode
		{
			get
			{
				return this._bForceCullMode;
			}
			set
			{
				this._bForceCullMode = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003998 File Offset: 0x00001B98
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000039B0 File Offset: 0x00001BB0
		public gviCullFaceMode CullFaceMode
		{
			get
			{
				return this._cullFaceMode;
			}
			set
			{
				this._cullFaceMode = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000039BC File Offset: 0x00001BBC
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000039D4 File Offset: 0x00001BD4
		public string GeometryFiledName
		{
			get
			{
				return this._geometryFiledName;
			}
			set
			{
				this._geometryFiledName = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000039E0 File Offset: 0x00001BE0
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000039F8 File Offset: 0x00001BF8
		public string LayerName
		{
			get
			{
				return this._layerName;
			}
			set
			{
				this._layerName = value;
			}
		}

		// Token: 0x04000018 RID: 24
		private ITextRender _txtRender = null;

		// Token: 0x04000019 RID: 25
		private double _dMaxVisibleDistance = 150000.0;

		// Token: 0x0400001A RID: 26
		private float _fMinVisiblePixels = 0f;

		// Token: 0x0400001B RID: 27
		private bool _bForceCullMode = false;

		// Token: 0x0400001C RID: 28
		private gviCullFaceMode _cullFaceMode = gviCullFaceMode.gviCullNone;

		// Token: 0x0400001D RID: 29
		private string _geometryFiledName = string.Empty;

		// Token: 0x0400001E RID: 30
		private IGeometryRender _geoRender = null;

		// Token: 0x0400001F RID: 31
		private LayerStyleType _layerType = LayerStyleType.Style70;

		// Token: 0x04000020 RID: 32
		private string _layerName = null;

		// Token: 0x04000021 RID: 33
		private gviViewportMask _visibleMask;
	}
}
