using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using System;

public static class IVector3Extension
{
    public static IVector3 Add(this IVector3 @this, IVector3 v2)
    {
        return new Vector3
        {
            X = @this.X + v2.X,
            Y = @this.Y + v2.Y,
            Z = @this.Z + v2.Z
        };
    }

    public static IVector3 Subtract(this IVector3 @this, IVector3 v2)
    {
        return new Vector3
        {
            X = @this.X - v2.X,
            Y = @this.Y - v2.Y,
            Z = @this.Z - v2.Z
        };
    }


    /// <summary>
    /// 计算向量正交向量（返回单位向量）
    /// 重构算法，避免正交向量的结果随意性
    /// u：X轴线 w:Y轴线 v:Z轴线(u：右 v:上)
    /// </summary>
    /// <param name="w"></param>
    /// <param name="v"></param>
    /// <param name="u"></param>
    public static void GenerateComplementBasis(this IVector3 w, out IVector3 u, out IVector3 v)
    {
        u = new Vector3();
        v = new Vector3();

        if (u == null)
        {
            v = u.Multiply(w);
        }
        else if (Math.Abs(w.X) < 0.005
            && Math.Abs(w.Y) < 0.005)
        {
            if (w.Z > 0)
            {
                u = new Vector3(); u.Set(-1.0, 0.0, 0.0);
                v = new Vector3(); v.Set(0.0, 1.0, 0.0);
            }
            else
            {
                u = new Vector3(); u.Set(1.0, 0.0, 0.0);
                v = new Vector3(); v.Set(0.0, 1.0, 0.0);
            }
        }
        else
        {
            u.X = w.Y;
            u.Y = -w.X;
            u.Z = 0.0;

            v = u.Multiply(w);
        }
        //单位向量
        v.Normalize();
        u.Normalize();
    }

    public static IVector3 Multiply(this IVector3 @this, IVector3 v2)
    {
        return new Vector3
        {
            X = @this.Y * v2.Z - v2.Y * @this.Z,
            Y = @this.Z * v2.X - @this.X * v2.Z,
            Z = @this.X * v2.Y - v2.X * @this.Y
        };
    }

    public static double DotProduct(this IVector3 @this, IVector3 v2)
    {
        return @this.X * v2.X + @this.Y * v2.Y + @this.Z * v2.Z;
    }

    public static IVector3 Multiply(this IVector3 @this, double f)
    {
        return new Vector3
        {
            X = @this.X * f,
            Y = @this.Y * f,
            Z = @this.Z * f
        };
    }

    public static IVector3 Divide(this IVector3 @this, double f)
    {
        bool flag = f == 0.0;
        if (flag)
        {
            throw new DivideByZeroException("f");
        }
        return new Vector3
        {
            X = @this.X / f,
            Y = @this.Y / f,
            Z = @this.Z / f
        };
    }

    public static IVector3 UnitVector(this IVector3 @this)
    {
        double f = 1.0 / Math.Sqrt(@this.X * @this.X + @this.Y * @this.Y + @this.Z * @this.Z);
        return @this.Multiply(f);
    }
    public static IVector3 Normal(this IVector3 @this, IVector3 that)
    {
        return new Vector3
        {
            X = @this.Y * that.Z - @this.Z * that.Y,
            Y = @this.Z * that.X - @this.X * that.Z,
            Z = @this.X * that.Y - @this.Y * that.X
        };
    }

    public static double GetDistance(this IVector3 @this, IVector3 endV3)
    {
        return Math.Sqrt(Math.Pow(@this.X - endV3.X, 2.0) + Math.Pow(@this.Y - endV3.Y, 2.0) + Math.Pow(@this.Z - endV3.Z, 2.0));
    }

    public static double StandardDistance(this IVector3 @this, IVector3 other)
    {
        return Math.Sqrt(Math.Pow(@this.X - other.X, 2.0) + Math.Pow(@this.Y - other.Y, 2.0));
    }

    public static bool SetXYZ(this IVector3 @this, string str)
    {
        bool flag = @this == null;
        if (flag)
        {
            throw new NullReferenceException("@this");
        }
        bool flag2 = string.IsNullOrEmpty(str);
        bool result;
        if (flag2)
        {
            result = false;
        }
        else
        {
            string[] array = str.Split(new char[]
            {
                ',',
                ';'
            }, StringSplitOptions.RemoveEmptyEntries);
            bool flag3 = !array.HasValues<string>() || array.Length < 3;
            if (flag3)
            {
                result = false;
            }
            else
            {
                float num;
                float num2 = 0;
                float num3 = 0;
                bool flag4 = !float.TryParse(array[0], out num) || !float.TryParse(array[1], out num2) || !float.TryParse(array[2], out num3);
                if (flag4)
                {
                    result = false;
                }
                else
                {
                    @this.Set((double)num, (double)num2, (double)num3);
                    result = true;
                }
            }
        }
        return result;
    }

    public static IPoint ToPoint(this IVector3 @this, IGeometryFactory geofactory, ISpatialCRS crs = null)
    {
        bool flag = @this == null;
        IPoint result;
        if (flag)
        {
            result = null;
        }
        else
        {
            bool flag2 = geofactory == null;
            if (flag2)
            {
                result = null;
            }
            else
            {
                IPoint point = (IPoint)geofactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ);
                point.SpatialCRS = crs;
                point.SetPostion(@this.X, @this.Y, @this.Z);
                result = point;
            }
        }
        return result;
    }
}
