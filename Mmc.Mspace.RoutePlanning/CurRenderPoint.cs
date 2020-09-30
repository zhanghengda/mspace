using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RoutePlanning
{
    /// <summary>
    /// 已渲染航点类（可点击捕捉的黄绿色的点）
    /// </summary>
    public class CurRenderPoint
    {
        /// <summary>
        /// Guid
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// 渲染航点
        /// </summary>
        public IRenderPoint RenderPoint { get; set; }
    }
}
