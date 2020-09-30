using System;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Wpf.Core;

namespace Mmc.Framework.Wpf.Commands
{
	/// <summary>
	///     水平测量 命令
	/// </summary>
	// Token: 0x02000008 RID: 8
	public class HorizontalDistanceCmd : MapCommand
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000021E0 File Offset: 0x000003E0
		public override void Execute(object parameter)
		{
			bool position = parameter.ParseTo(false);
			bool angle = position;
			if (angle)
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
