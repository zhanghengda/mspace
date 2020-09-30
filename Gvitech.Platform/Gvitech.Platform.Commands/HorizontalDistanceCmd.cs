using Gvitech.CityMaker.RenderControl;
using Mmc.Platform.Core;
using System;

namespace Mmc.Platform.Commands
{
	public class HorizontalDistanceCmd : SimpleCommandWithMap
	{
		public override void Execute(object parameter)
		{
			bool state = parameter.ParseTo(false);
			if (state)
			{
				base.MapControl.InteractMode = gviInteractMode.gviInteractMeasurement;
				base.MapControl.MeasurementMode = gviMeasurementMode.gviMeasureHorizontalDistance;
			}
			else
			{
				base.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
			}
		}
	}
}
