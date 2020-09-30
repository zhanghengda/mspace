using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gvitech.Framework.Services
{
    public class SymbolManager
    {
        public SymbolManager()
        {
            SurfaceSym = new SurfaceSymbol();
            SurfaceSym.Color = Color.FromArgb(66, Color.Orange);
            CurveSym = new CurveSymbol() { Width = 1.5f, Color = Color.Green };
        }
        /// <summary>
        /// 默认面标注的样式
        /// </summary>
        public ISurfaceSymbol SurfaceSym { get; private set; }
        public ISurfaceSymbol CreateSurfaceSymbol(CurveSymbol lineSym, Color color)
        {
            return new SurfaceSymbol() { BoundarySymbol = lineSym, Color = color };
        }

        public ICurveSymbol CurveSym { get; private set; }
        public ICurveSymbol CreateCurveSymbol(float width, Color color, gviDashStyle dashStyle = gviDashStyle.gviDashSolid)
        {
            return new CurveSymbol() { Width = width, Color = color, Pattern = dashStyle };
        }
    }
}
