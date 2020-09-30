using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections.Generic;

namespace Mmc.Framework.Draw
{
    /// <summary>
    ///     绘制工具交互拾取产生的点对象
    /// </summary>
    public class DrawPoint
    {
        public bool beFeature;
        public int drawGroupIndex;
        public gviMouseSelectMode eventSender;
        public gviModKeyMask mask;
        public IRenderModelPoint modelPoint;
        public IRenderMultiPolygon multiPolygon;
        public Guid pickFeatureClassGUID;
        public int pickFeatureID;
        public IFeatureLayer pickFeatureLayer;
        public gviObjectType pickFeatureType;
        public IRenderPolygon polygon;
        public int primitiveIndex;
        public double x, y, z;

        public DrawPoint()
        {
            beFeature = false;
        }

        public bool IsZero()
        {
            if (x == 0.0 && y == 0.0 && z == 0.0)
                return true;
            return false;
        }

        /// <summary>
        ///     UPPoint比较器
        /// </summary>
        public class UPPointComparer : IEqualityComparer<DrawPoint>
        {
            public static UPPointComparer Default = new UPPointComparer();

            #region IEqualityComparer<UPPoint> 成员

            public bool Equals(DrawPoint x, DrawPoint y)
            {
                return x.pickFeatureID == y.pickFeatureID;
            }

            public int GetHashCode(DrawPoint obj)
            {
                return obj.GetHashCode();
            }

            #endregion
        }
    }


    /// <summary>
    ///     简单的点对象
    /// </summary>
    public class SimplePoint
    {
        public const double minValue = 0.00001;
        public double x, y, z;

        public bool IsValid()
        {
            if (Math.Abs(x) < minValue || Math.Abs(y) < minValue)
                return false;
            return true;
        }
    }
}