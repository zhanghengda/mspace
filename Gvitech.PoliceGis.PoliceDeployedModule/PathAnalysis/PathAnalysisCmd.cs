using System;
using System.Collections.Generic;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Services;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.PoliceDeployedModule.PathAnalysis
{
	// Token: 0x02000014 RID: 20
	public class PathAnalysisCmd : SimpleCommand
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000690C File Offset: 0x00004B0C
		private void SniperProtecton()
		{
			try
			{
				bool flag = this._rObjects == null;
				if (flag)
				{
					ServiceManager.GetService<ICameraInfoService>(null).LoadData();
					this._rObjects = ServiceManager.GetService<ICameraInfoService>(null).GetDrawCameraViews();
				}
				else
				{
					this._rObjects.ForEach(delegate(IRenderable P)
					{
						GviMap.MapControl.ObjectManager.DeleteObject(P.Guid);
					});
					this._rObjects.Clear();
					this._rObjects = ServiceManager.GetService<ICameraInfoService>(null).GetDrawCameraViews();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000069AC File Offset: 0x00004BAC
		public override void Execute(object parameter)
		{
			bool flag = StringExtension.ParseTo<bool>(parameter, false);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = this._rObjects == null;
				if (flag3)
				{
					ServiceManager.GetService<ICameraInfoService>(null).LoadData();
					this._rObjects = ServiceManager.GetService<ICameraInfoService>(null).GetDrawCameraViews();
				}
				this._rObjects.ForEach(delegate(IRenderable P)
				{
					P.VisibleMask = gviViewportMask.gviViewAllNormalView;
				});
			}
			else
			{
				bool flag4 = this._rObjects != null;
				if (flag4)
				{
					this._rObjects.ForEach(delegate(IRenderable P)
					{
						P.VisibleMask = gviViewportMask.gviViewNone;
					});
				}
			}
		}

		// Token: 0x04000057 RID: 87
		private List<IRenderable> _rObjects;
	}
}
