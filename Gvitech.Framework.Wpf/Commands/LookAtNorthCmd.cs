using System;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Wpf.Core;

namespace Mmc.Framework.Wpf.Commands
{
	/// <summary>
	///     指北
	/// </summary>
	// Token: 0x02000009 RID: 9
	public class LookAtNorthCmd : MapCommand
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002228 File Offset: 0x00000428
		public override void Execute(object parameter)
		{
			IPoint state;
			IEulerAngle eulerAngle;
			base.MapControl.Camera.GetCamera2(out state, out eulerAngle);
			eulerAngle.Heading = 0.0;
			base.MapControl.Camera.SetCamera2(state, eulerAngle, gviSetCameraFlags.gviSetCameraNoFlags);
		}
	}
}
