using System;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Wpf.Core;

namespace Mmc.Framework.Wpf.Commands
{
	/// <summary>
	///     透视
	/// </summary>
	// Token: 0x0200000B RID: 11
	public class PerspectiveCmd : MapCommand
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000024FC File Offset: 0x000006FC
		public override void Execute(object parameter)
		{
			IPoint position;
			IEulerAngle eulerAngle;
			base.MapControl.Camera.GetCamera2(out position, out eulerAngle);
			eulerAngle.Tilt = -45.0;
			base.MapControl.Camera.SetCamera2(position, eulerAngle, gviSetCameraFlags.gviSetCameraNoFlags);
		}
	}
}
