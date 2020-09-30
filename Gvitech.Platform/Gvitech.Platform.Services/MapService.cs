using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using System;

namespace Mmc.Platform.Services
{
	public class MapService : Singleton<MapService>
	{
		public IRenderControl RenderControl
		{
			get;
			set;
		}

		public AxRenderControl AxMapControl
		{
			get;
			private set;
		}

		public ISpatialCRS SpatialCRS
		{
			get;
			private set;
		}

		public IGeometryFactory GeoFactory
		{
			get;
			private set;
		}

		public ICRSFactory CrsFactory
		{
			get;
			private set;
		}

		public MapService()
		{
			this.AxMapControl = null;
			this.RenderControl = null;
			this.GeoFactory = new GeometryFactory();
			this.CrsFactory = new CRSFactory();
		}

		public void InitAxMapControl(AxRenderControl _axMapControl, IPropertySet ps, string wkt)
		{
			try
			{
				this.AxMapControl = _axMapControl;
				this.RenderControl = (_axMapControl as IRenderControl);
				bool isSuccess;
				if (string.IsNullOrEmpty(wkt))
				{
					isSuccess = _axMapControl.Initialize(true, ps);
				}
				else
				{
					this.SpatialCRS = (ISpatialCRS)this.CrsFactory.CreateFromWKT(wkt);
					isSuccess = _axMapControl.Initialize2(wkt, ps);
				}
				if (isSuccess)
				{
					this.AxMapControl.InteractMode = gviInteractMode.gviInteractNormal;
					this.AxMapControl.MouseSnapMode = gviMouseSnapMode.gviMouseSnapDisable;
					this.AxMapControl.UseEarthOrbitManipulator = gviManipulatorMode.gviCityMakerManipulator;
					this.AxMapControl.Viewport.CompassVisibleMask = gviViewportMask.gviViewNone;
					SystemLog.Log("三维控件初始化成功。", LogMessageType.INFO);
				}
				else
				{
					SystemLog.Log("三维控件初始化失败。", LogMessageType.INFO);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void SetDefaultSkybox(string defaultPath, int skyboxIndex = 0)
		{
			ISkyBox skybox = this.AxMapControl.ObjectManager.GetSkyBox(skyboxIndex);
			skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBack, defaultPath + "\\1_BK.jpg");
			skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageBottom, defaultPath + "\\1_DN.jpg");
			skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageFront, defaultPath + "\\1_FR.jpg");
			skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageLeft, defaultPath + "\\1_LF.jpg");
			skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageRight, defaultPath + "\\1_RT.jpg");
			skybox.SetImagePath(gviSkyboxImageIndex.gviSkyboxImageTop, defaultPath + "\\1_UP.jpg");
		}
	}
}
