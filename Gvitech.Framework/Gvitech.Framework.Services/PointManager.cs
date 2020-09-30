using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Framework.Services
{
    public class PointManager
    {
        private Dictionary<string, Tuple<ILabel, IRenderPoint>> _renderPointDic = new Dictionary<string, Tuple<ILabel, IRenderPoint>>();

        public IPoint CreatePoint(double x, double y, double z = 0)
        {
            var tempPoint = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint;

            tempPoint.X = x;
            tempPoint.Y = y;
            tempPoint.Z = z;
            tempPoint.SpatialCRS = GviMap.SpatialCrs;
            return tempPoint;
        }

        public IPointSymbol CreateSymbol(Color color, int size = 5, gviSimplePointStyle style = gviSimplePointStyle.gviSimplePointCircle)
        {
            var symbol = new SimplePointSymbol() { Color = color, Size = size, Style = style };
            return symbol;
        }

        public IRenderPoint CreateRPoint(IPoint point, IPointSymbol symbol)
        {
            return GviMap.ObjectManager.CreateRenderPoint(point, symbol, GviMap.ProjectTree.RootID);
        }

        public bool DeleteRPoint(IRenderPoint rPoint)
        {
            if (rPoint != null)
            {
                GviMap.ObjectManager.DeleteObject(rPoint.Guid);
                return true;
            }
            else
                return false;
        }

        public bool DeleteLabel(ILabel label)
        {
            if (label != null)
            {
                GviMap.ObjectManager.DeleteObject(label.Guid);
                return true;
            }
            else
                return false;
        }

        public Tuple<ILabel, IRenderPoint> GetRPointItem(string key)
        {
            if (_renderPointDic.ContainsKey(key))
                return _renderPointDic[key];
            return null;
        }


        public void FlyToRPoint(string key)
        {
            foreach (var tag in _renderPointDic.Keys)
            {
                if (_renderPointDic.ContainsKey(key))
                {
                    var rPoi = _renderPointDic[key].Item2;
                    if (rPoi != null)
                    {
                        var poi = rPoi.GetFdeGeometry() as IPoint;

                        GviMap.Camera.GetCamera2(out IPoint Position, out IEulerAngle Angle);

                        GviMap.Camera.LookAt2(poi, 300, Angle);

                        ((IRenderGeometry)rPoi).Glow(1500);
                    }
                }
            }
        }

        public bool UpdateRPointItem(string key, ILabel label, IRenderPoint rPoint)
        {
            if (_renderPointDic.ContainsKey(key))
            {
                _renderPointDic[key] = new Tuple<ILabel, IRenderPoint>(label, rPoint);
                return true;
            }
            else
                return false;
        }

        public bool DeleteRPointItem(string key)
        {
            var item = GetRPointItem(key);
            if (item == null) return false;
            bool res1 = DeleteLabel(item.Item1);

            bool res2 = DeleteRPoint(item.Item2);
            if (res2)
            {
                _renderPointDic.Remove(key);
                return true;
            }
            else
                return false;
        }

        public bool AddRPointItem(string key, ILabel label, IRenderPoint rPoint)
        {
            if (!_renderPointDic.ContainsKey(key))
            {
                _renderPointDic.Add(key, new Tuple<ILabel, IRenderPoint>(label, rPoint));
                return true;
            }
            else
                return false;
        }


        public bool ContainsKey(string key)
        {
            return _renderPointDic.ContainsKey(key);
        }

        public void Clear()
        {
            try
            {
                var keys = _renderPointDic.Keys.ToList();
                if (keys.Count > 0)
                    foreach (var key in keys)
                        DeleteRPointItem(key);
                keys.Clear();
                keys = null;
                _renderPointDic.Clear();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }
    }
}
