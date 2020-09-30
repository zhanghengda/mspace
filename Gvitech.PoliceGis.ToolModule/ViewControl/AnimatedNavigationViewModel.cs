using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class AnimatedNavigationViewModel: CheckedToolItemModel
    {
        //public AnimatedNavigationViewModel()
        //{

        //}

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;
        }
        public override void OnChecked()
        {
            try
            {
                base.OnChecked();
                //if (_drawCustomer == null)
                //    _drawCustomer = new DrawCustomerUC(_drawCustomerName, DrawCustomerType.MenuCommand);
                ////注册绘制多边形事件
                //RCDrawManager.Instance.PolygonDraw.Register(GviMap.AxMapControl, _drawCustomer, RCMouseOperType.PickPoint);
                //RCDrawManager.Instance.PolygonDraw.OnDrawFinished += PolygonDraw_OnDrawFinished; ;
            }
            catch (Exception ex)
            {
                //SystemLog.Log(ex);
            }

        }
    }
}
