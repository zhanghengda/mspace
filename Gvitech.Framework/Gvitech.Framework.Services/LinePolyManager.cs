using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Framework.Services
{
    public class LinePolyManager
    {
        /// <summary>
        /// int类型，0代表点，1代表线，2代表面
        /// </summary>
        private Dictionary<string, Tuple<int, ILabel, IRenderable>> _linePolyMaps = new Dictionary<string, Tuple<int, ILabel, IRenderable>>();

        public LinePolyManager()
        {
            SurfaceSym = new SurfaceSymbol();
            SurfaceSym.Color = Color.FromArgb(66, Color.Orange);
            CurveSym = new CurveSymbol() { Width = 1.5f,Color=Color.Green};
        }

        /// <summary>
        /// 默认面标注的样式
        /// </summary>
        public ISurfaceSymbol SurfaceSym { get; private set; }
        public ISurfaceSymbol CreateSurfaceSymbol(CurveSymbol lineSym,Color color)
        {
            return new SurfaceSymbol() { BoundarySymbol =lineSym,Color =color};
        }

        /// <summary>
        /// 默认线标注的样式
        /// </summary>
        public ICurveSymbol CurveSym { get; private set; }

        public ICurveSymbol CreateCurveSymbol(float width,Color color, gviDashStyle dashStyle =gviDashStyle.gviDashSolid )
        {
            return new CurveSymbol() { Width = width, Color = color, Pattern = dashStyle };
        }


        public Tuple<int, ILabel, IRenderable> GetPoi(string key)
        {
            if (_linePolyMaps.ContainsKey(key))
                return _linePolyMaps[key];
            return null;
        }

        public bool UpdatePoi(string key, int type, ILabel label, IRenderable renderable)
        {
            if (_linePolyMaps.ContainsKey(key))
            {
                _linePolyMaps[key] = new Tuple<int, ILabel, IRenderable>(type, label, renderable);
                return true;
            }
            return false;
        }

        public bool DeletePoi(string key)
        {
            try
            {
                var item = GetItemByKey(key);
                if (item == null) return false;
                var label = item.Item2;

                if (item.Item2 != null)
                {
                    GviMap.ObjectManager.DeleteObject(item.Item2.Guid);
                    item.Item2.Position.Dispose();
                }
                if (item.Item3 != null)
                {
                    GviMap.ObjectManager.DeleteObject(item.Item3.Guid);
                }
                    
                else
                    return false;
                _linePolyMaps.Remove(key);
                return true;
            }
            catch { return false; }
        }

        public void Flyto(string key)
        {
            var item = GetItemByKey(key);
            if (item != null)
            {
                GviMap.Camera.LookAtEnvelope(item.Item3.Envelope);
                HightLight((IRenderGeometry)item.Item3);
            }

        }

        public void HightLight(IRenderGeometry render)
        {
            if (render != null)
                ((IRenderGeometry)render).Glow(1500);
        }

        public bool ContainsKey(string key)
        {
            return _linePolyMaps.ContainsKey(key);
        }


        public Tuple<int, ILabel, IRenderable> GetItemByKey(string key)
        {
            if (_linePolyMaps.ContainsKey(key))
                return _linePolyMaps[key];
            return null;
        }



        public bool AddPoi(string key, int type, ILabel label, IRenderable renderable)
        {
            if (!_linePolyMaps.ContainsKey(key))
            {
                _linePolyMaps.Add(key, new Tuple<int, ILabel, IRenderable>(type, label, renderable));
                return true;
            }
            return false;
        }

        public void SetPoiVisible(string key, bool isVisible)
        {
            var item = GetItemByKey(key);
            if (item != null)
            {
                item.Item2.VisibleMask = isVisible ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
                item.Item3.VisibleMask = isVisible ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
            }
        }


        public void Clear()
        {
            var listKey = _linePolyMaps.Keys.ToList<string>();
            foreach (var key in listKey)
                DeletePoi(key);
            listKey.Clear();
            listKey = null;
            _linePolyMaps.Clear();
        }


    }
}
