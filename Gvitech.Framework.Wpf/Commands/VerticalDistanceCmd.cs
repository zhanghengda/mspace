using System;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Wpf.Core;

namespace Mmc.Framework.Wpf.Commands
{
	/// <summary>
	/// </summary>
	// Token: 0x0200000D RID: 13
	public class VerticalDistanceCmd : MapCommand
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000258C File Offset: 0x0000078C
		public override void Execute(object parameter)
		{
			bool flag = parameter.ParseTo(false);
			bool flag2 = flag;
			if (flag2)
			{

				if (base.MapControl.InteractMode != gviInteractMode.gviInteractMeasurement)
					base.MapControl.InteractMode = gviInteractMode.gviInteractMeasurement;
				if (base.MapControl.MeasurementMode != gviMeasurementMode.gviMeasureAerialDistance)
					base.MapControl.MeasurementMode = gviMeasurementMode.gviMeasureAerialDistance;
			}
			else
			{
				//base.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
			}
		}
	}
}
