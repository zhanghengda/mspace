using System;

using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Wpf.Core;
using Gvitech.CityMaker.Controls;

namespace Mmc.Framework.Wpf.Commands
{
	/// <summary>
	///     人视角
	/// </summary>
	// Token: 0x0200000A RID: 10
	public class PersonAngleCmd : MapCommand
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000226F File Offset: 0x0000046F
		public override void Execute(object parameter)
		{
			this._viewHeigth = 3.7;
			this.RegisterEvent();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002288 File Offset: 0x00000488
		private void RegisterEvent()
		{
			this.AxMapControl.InteractMode = gviInteractMode.gviInteractSelect;
			this.AxMapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
			this.AxMapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            this.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect;
            this.AxMapControl.RcMouseClickSelect += AxMapControl_RcMouseClickSelect; 
            
        }

        private void AxMapControl_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            if (IntersectPoint != null)
            {
                int mouseClickNum = this._mouseClickNum;
                if (mouseClickNum != 0)
                {
                    if (mouseClickNum == 1)
                    {
                        this.gvObject = new Vector3();
                        this.gvObject.X = IntersectPoint.X;
                        this.gvObject.Y = IntersectPoint.Y;
                        this.gvObject.Z = IntersectPoint.Z;
                        this.SetCamera(this.gvViewer, this.gvObject);
                        this.UnRegisterEvent();
                    }
                }
                else
                {
                    this.gvViewer = new Vector3();
                    this.gvViewer.X = IntersectPoint.X;
                    this.gvViewer.Y = IntersectPoint.Y;
                    this.gvViewer.Z = IntersectPoint.Z + this._viewHeigth;
                    int mouseClickNum2 = this._mouseClickNum;
                    this._mouseClickNum = mouseClickNum2 + 1;
                }
            }
        }

        

		// Token: 0x06000019 RID: 25 RVA: 0x0000240C File Offset: 0x0000060C
		private void UnRegisterEvent()
		{
			this.AxMapControl.InteractMode = gviInteractMode.gviInteractNormal;
			this.AxMapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectMove;
			this.AxMapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
			this.AxMapControl.RcMouseClickSelect -= this.AxMapControl_RcMouseClickSelect;
			this._mouseClickNum = 0;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002460 File Offset: 0x00000660
		private void SetCamera(IVector3 startV3, IVector3 endV3)
		{
			double distance = Math.Sqrt((startV3.X - endV3.X) * (startV3.X - endV3.X) + (startV3.Y - endV3.Y) * (startV3.Y - endV3.Y) + (startV3.Z - endV3.Z) * (startV3.Z - endV3.Z));
			IEulerAngle aimingAngles = this.AxMapControl.Camera.GetAimingAngles(startV3, endV3);
			this.AxMapControl.Camera.LookAt2(endV3.ToPoint(new GeometryFactory(), null), distance, aimingAngles);
		}

		// Token: 0x04000004 RID: 4
		private int _mouseClickNum;

		// Token: 0x04000005 RID: 5
		private double _viewHeigth;

		// Token: 0x04000006 RID: 6
		private IVector3 gvObject;

		// Token: 0x04000007 RID: 7
		private IVector3 gvViewer;
	}
}
