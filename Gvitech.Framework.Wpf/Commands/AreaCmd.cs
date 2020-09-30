using System;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Wpf.Core;

namespace Mmc.Framework.Wpf.Commands
{
	/// <summary>
	/// </summary>
	// Token: 0x02000007 RID: 7
	public class AreaCmd : MapCommand
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000218C File Offset: 0x0000038C
		public override void Execute(object parameter)
		{
			bool position = parameter.ParseTo(false);
			bool angle = position;
			if (angle)
			{
				base.MapControl.InteractMode = gviInteractMode.gviInteractMeasurement;
				base.MapControl.MeasurementMode = gviMeasurementMode.gviMeasureArea;
			}
			else
			{
				base.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
			}
		}
	}
}
