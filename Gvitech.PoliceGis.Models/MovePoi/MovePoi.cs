using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;

namespace Mmc.Mspace.Models.MovePoi
{
	// Token: 0x0200000B RID: 11
	public class MovePoi : IMovePoi
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000024AC File Offset: 0x000006AC
		public MovePoi(IMoveObject obj)
		{
			bool flag = obj == null;
			if (flag)
			{
				throw new ArgumentNullException("obj");
			}
			Interlocked.CompareExchange<IGeometryFactory>(ref MovePoi.GeoFactory, new GeometryFactory(), null);
			Interlocked.CompareExchange<ISpatialCRS>(ref MovePoi.Crs, ((ICRSFactory)new CRSFactory()).CreateCGCS2000(), null);
			Interlocked.CompareExchange<IObjectManager>(ref MovePoi.Om, GviMap.MapControl.ObjectManager, null);
			string text = ConfigurationManager.AppSettings["poiHighlightSize"];
			MovePoi._highlightSize = ((!string.IsNullOrEmpty(text)) ? StringExtension.ParseTo<int>(text, 64) : 64);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000253A File Offset: 0x0000073A
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002542 File Offset: 0x00000742
		protected IMoveObject MoveObject { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000254B File Offset: 0x0000074B
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002553 File Offset: 0x00000753
		protected IRenderPOI RenderPoi { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000255C File Offset: 0x0000075C
		public string Name
		{
			get
			{
				return (this.MoveObject != null) ? this.MoveObject.Id : string.Empty;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002588 File Offset: 0x00000788
		public string Id
		{
			get
			{
				return (this.MoveObject != null) ? this.MoveObject.Id : string.Empty;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000025B4 File Offset: 0x000007B4
		public string RenderId
		{
			get
			{
				return (this.RenderPoi != null) ? this.RenderPoi.Guid.ToString() : string.Empty;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000025F0 File Offset: 0x000007F0
		public MoveObjectType MoveObjectType
		{
			get
			{
				return (this.MoveObject != null) ? this.MoveObject.MoveObjectType : MoveObjectType.MoveNone;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002618 File Offset: 0x00000818
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002620 File Offset: 0x00000820
		public DateTime LastUpdateTime { get; protected set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000262C File Offset: 0x0000082C
		public DataTable ShowDataTable
		{
			get
			{
				return (this.MoveObject != null) ? this.MoveObject.ToDataTable() : null;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002654 File Offset: 0x00000854
		public IPoint Position
		{
			get
			{
				bool flag = this.RenderPoi == null;
				IPoint result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = (this.RenderPoi.GetFdeGeometry() as IPoint);
				}
				return result;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002688 File Offset: 0x00000888
		public bool IsMatchKey(string key)
		{
			return this.MoveObject.IsMatchKey(key);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000026A8 File Offset: 0x000008A8
		public virtual bool UpdatePoi(IMoveObject newObj)
		{
			bool flag = newObj == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = newObj.Equals(this.MoveObject);
				if (flag2)
				{
					result = true;
				}
				else
				{
					this.MoveObject = newObj;
					this.LastUpdateTime = DateTime.Now;
					result = this.CreateOrRefreshPoi(MovePoi.Om, MovePoi.GeoFactory);
				}
			}
			return result;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002700 File Offset: 0x00000900
		public virtual bool Show(bool visible, gviViewportMask mask = gviViewportMask.gviViewAllNormalView)
		{
			bool flag = this.RenderPoi == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.RenderPoi.VisibleMask = (visible ? mask : gviViewportMask.gviViewNone);
				result = true;
			}
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002738 File Offset: 0x00000938
		public virtual bool FlyTo(ICamera camera)
		{
			bool flag = camera == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.RenderPoi == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					camera.FlyTime = 0.5;
					IEnvelope envelope = this.RenderPoi.Envelope;
					bool flag3 = IEnvelopeExtension.DiagonalDistance(envelope) < 500f;
					if (flag3)
					{
						camera.LookAt2(IVector3Extension.ToPoint(new Vector3
						{
							X = envelope.Center.X,
							Y = envelope.Center.Y,
							Z = envelope.Center.Z
						}, GviMap.GeoFactory, this.RenderPoi.GetFdeGeometry().SpatialCRS), 1000.0, new EulerAngle
						{
							Heading = 45.0,
							Roll = 0.0,
							Tilt = -45.0
						});
					}
					else
					{
						ICameraExtension.FlyToEnvelope(camera, envelope, null, 2f, 0.5, null);
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002862 File Offset: 0x00000A62
		public virtual void Release()
		{
            Om.ReleaseRenderObject(this.RenderPoi);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002880 File Offset: 0x00000A80
		public virtual bool RefreshPosition()
		{
			return this.CreateOrRefreshPoi(MovePoi.Om, MovePoi.GeoFactory);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000028A4 File Offset: 0x00000AA4
		public virtual void HightLight(uint color = 4294901760u)
		{
			bool flag = this.RenderPoi == null;
			if (!flag)
			{
				IPOI ipoi = this.RenderPoi.GetFdeGeometry() as IPOI;
				bool flag2 = ipoi == null;
				if (!flag2)
				{
					ipoi.Size = MovePoi._highlightSize;
					this.RenderPoi.SetFdeGeometry(ipoi);
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000028F8 File Offset: 0x00000AF8
		public virtual void UnHightLight()
		{
			bool flag = this.RenderPoi == null;
			if (!flag)
			{
				IPOI ipoi = this.RenderPoi.GetFdeGeometry() as IPOI;
				bool flag2 = ipoi == null;
				if (!flag2)
				{
					ipoi.Size = MovePoi._imageSize;
					this.RenderPoi.SetFdeGeometry(ipoi);
				}
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000294C File Offset: 0x00000B4C
		public PropertyInfo[] GetMoveObjectProperties()
		{
			return (this.MoveObject == null) ? null : this.MoveObject.GetProperties();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002974 File Offset: 0x00000B74
		public object GetPropertyValue(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = ((this.MoveObject == null) ? null : this.MoveObject.GetPropertyValue(name));
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000029AC File Offset: 0x00000BAC
		protected virtual bool CreateOrRefreshPoi(IObjectManager om, IGeometryFactory geoFactory)
		{
			bool flag = om == null;
			if (flag)
			{
				throw new ArgumentNullException("om");
			}
			bool flag2 = geoFactory == null;
			if (flag2)
			{
				throw new ArgumentNullException("geoFactory");
			}
			bool flag3 = this.RenderPoi == null;
			IPOI ipoi;
			if (flag3)
			{
				ipoi = this.CreatePOi(om, geoFactory);
				bool flag4 = ipoi == null;
				if (flag4)
				{
					return false;
				}
				this.RenderPoi = om.CreateRenderPOI(ipoi);
				bool flag5 = this.RenderPoi != null;
				if (flag5)
				{
					this.RenderPoi.VisibleMask = gviViewportMask.gviViewAllNormalView;
					this.RenderPoi.MaxVisibleDistance = 100000.0;
					this.RenderPoi.MinVisibleDistance = 1.0;
				}
			}
			else
			{
				ipoi = (this.RenderPoi.GetFdeGeometry() as IPOI);
				bool flag6 = ipoi == null;
				if (flag6)
				{
					return false;
				}
				IPointExtension.SetPostion(ipoi, this.MoveObject.Coordinate);
				this.RenderPoi.SetFdeGeometry(ipoi);
			}
			FdeGeometryRelease.ReleaseComObject(ipoi);
			return true;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002AB4 File Offset: 0x00000CB4
		protected virtual IPOI CreatePOi(IObjectManager om, IGeometryFactory geoFactory)
		{
			IPOI ipoi = null;
			try
			{
				ipoi = (geoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ ) as IPOI);
				IPointExtension.SetPostion(ipoi, this.MoveObject.Coordinate);
				bool flag = ipoi != null;
				if (flag)
				{
					//ipoi.SpatialCRS = MovePoi.Crs;
					ipoi.Name = this.MoveObject.Name;
					bool flag2 = MovePoi._imageSize == 0;
					if (flag2)
					{
						string text = ConfigurationManager.AppSettings["poiSize"];
						MovePoi._imageSize = ((!string.IsNullOrEmpty(text)) ? StringExtension.ParseTo<int>(text, 48) : 48);
					}
					ipoi.Size = MovePoi._imageSize;
					MoveObjectType moveObjectType = this.MoveObjectType;
					if (moveObjectType != MoveObjectType.MovePoliceMan)
					{
						if (moveObjectType == MoveObjectType.MovePoliceCar)
						{
							ipoi.ImageName = MovePoi.CarImage;
						}
					}
					else
					{
						ipoi.ImageName = MovePoi.PoliceImage;
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return ipoi;
		}

		// Token: 0x04000026 RID: 38
		public static IGeometryFactory GeoFactory;

		// Token: 0x04000027 RID: 39
		public static ISpatialCRS Crs;

		// Token: 0x04000028 RID: 40
		public static IObjectManager Om;

		// Token: 0x04000029 RID: 41
		private static int _imageSize;

		// Token: 0x0400002A RID: 42
		private static readonly string PoliceImage = Application.StartupPath + "\\data\\poi\\police.png";

		// Token: 0x0400002B RID: 43
		private static readonly string CarImage = Application.StartupPath + "\\data\\poi\\car.gif";

		// Token: 0x0400002C RID: 44
		private static int _highlightSize = 64;
	}
}
