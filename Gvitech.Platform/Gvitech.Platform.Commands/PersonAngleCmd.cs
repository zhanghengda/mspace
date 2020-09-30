using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Platform.Core;
using Mmc.Platform.Services;
using Mmc.Windows.Design;
using System;

namespace Mmc.Platform.Commands
{
	public class PersonAngleCmd : SimpleCommandWithMap
	{
		private int _mouseClickNum;

		private double _viewHeigth;

		private IVector3 gvObject;

		private IVector3 gvViewer;

		public AxRenderControl axRenderControl
		{
			get
			{
				return Singleton<MapService>.Instance.AxMapControl;
			}
		}

		public override void Execute(object parameter)
		{
			this._viewHeigth = 3.7;
			this.RegisterEvent();
		}

		private void RegisterEvent()
		{
			this.axRenderControl.InteractMode = gviInteractMode.gviInteractSelect;
			this.axRenderControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
			this.axRenderControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;

            this.axRenderControl.RcMouseClickSelect -= AxRenderControl_RcMouseClickSelect;
            this.axRenderControl.RcMouseClickSelect += AxRenderControl_RcMouseClickSelect;


        }

        private void AxRenderControl_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            if ( IntersectPoint != null)
            {
                switch (this._mouseClickNum)
                {
                    case 0:
                        this.gvViewer = new Vector3();
                        this.gvViewer.X = IntersectPoint.X;
                        this.gvViewer.Y = IntersectPoint.Y;
                        this.gvViewer.Z = IntersectPoint.Z + this._viewHeigth;
                        this._mouseClickNum++;
                        break;
                    case 1:
                        this.gvObject = new Vector3();
                        this.gvObject.X = IntersectPoint.X;
                        this.gvObject.Y = IntersectPoint.Y;
                        this.gvObject.Z = IntersectPoint.Z;
                        this.SetCamera(this.gvViewer, this.gvObject);
                        this.UnRegisterEvent();
                        break;
                }
            }
        }


		private void UnRegisterEvent()
		{
			this.axRenderControl.InteractMode = gviInteractMode.gviInteractNormal;
			this.axRenderControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectMove;
			this.axRenderControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
			this.axRenderControl.RcMouseClickSelect -= AxRenderControl_RcMouseClickSelect;
			this._mouseClickNum = 0;
		}

		private void SetCamera(IVector3 startV3, IVector3 endV3)
		{
			double distance = Math.Sqrt((startV3.X - endV3.X) * (startV3.X - endV3.X) + (startV3.Y - endV3.Y) * (startV3.Y - endV3.Y) + (startV3.Z - endV3.Z) * (startV3.Z - endV3.Z));
			IEulerAngle angle = this.axRenderControl.Camera.GetAimingAngles(startV3, endV3);
			this.axRenderControl.Camera.LookAt2(endV3.ToPoint(new GeometryFactory(), null), distance, angle);
		}
	}
}
