using System;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Wpf.Core;

namespace Mmc.Framework.Wpf.Commands
{
	/// <summary>
	///     顶视 命令
	/// </summary>
	// Token: 0x0200000C RID: 12
	public class TopViewCmd : MapCommand
	{
		/// <summary>
		///     顶视
		/// </summary>
		/// <param name="parameter"></param>
		// Token: 0x0600001E RID: 30 RVA: 0x00002544 File Offset: 0x00000744
		public override void Execute(object parameter)
		{
			IPoint position;
			IEulerAngle eulerAngle;
			base.MapControl.Camera.GetCamera2(out position, out eulerAngle);
			eulerAngle.Tilt = -90.0;
			base.MapControl.Camera.SetCamera2(position, eulerAngle, gviSetCameraFlags.gviSetCameraNoFlags);
		}
	}
}
