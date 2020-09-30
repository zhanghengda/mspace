using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Platform.Core;
using System;

namespace Mmc.Platform.Commands
{
	public class LookAtNorthCmd : SimpleCommandWithMap
	{
		public override void Execute(object parameter)
		{
			IPoint position;
			IEulerAngle angle;
			base.MapControl.Camera.GetCamera2(out position, out angle);
			angle.Heading = 0.0;
			base.MapControl.Camera.SetCamera2(position, angle, gviSetCameraFlags.gviSetCameraNoFlags);
		}
	}
}
