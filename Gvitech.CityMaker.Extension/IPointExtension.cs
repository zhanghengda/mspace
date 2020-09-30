using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using System;

public static class IPointExtension
{
	public static IVector3 ToVector3(this IPoint @this)
	{
		return new Vector3
		{
			X = @this.X,
			Y = @this.Y,
			Z = @this.Z
		};
	}

	public static IPoint SetByPoint(this IPoint @this, IPoint sourcePoint)
	{
		bool flag = @this == null || sourcePoint == null;
		IPoint result;
		if (flag)
		{
			result = @this;
		}
		else
		{
			@this.X = sourcePoint.X;
			@this.Y = sourcePoint.Y;
			@this.Z = sourcePoint.Z;
			@this.Id = sourcePoint.Id;
			@this.M = sourcePoint.M;
			result = @this;
		}
		return result;
	}

	public static IPoint SetPostion(this IPoint @this, double x, double y, double z = 0.0)
	{
		bool flag = @this == null;
		IPoint result;
		if (flag)
		{
			result = @this;
		}
		else
		{
			@this.X = x;
			@this.Y = y;
			@this.Z = z;
			result = @this;
		}
		return result;
	}

	public static IPoint SetPostion(this IPoint @this, IVector3 vector)
	{
		bool flag = @this == null;
		IPoint result;
		if (flag)
		{
			result = @this;
		}
		else
		{
			@this.X = vector.X;
			@this.Y = vector.Y;
			@this.Z = vector.Z;
			result = @this;
		}
		return result;
	}

	public static bool SetPostion(this IPoint @this, string str)
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
			IVector3 vector = new Vector3();
			bool flag3 = !vector.SetXYZ(str);
			if (flag3)
			{
				result = false;
			}
			else
			{
				@this.SetPostion(vector);
				result = true;
			}
		}
		return result;
	}

	public static double GetDistance(this IPoint @this, IPoint endPoint)
	{
		return @this.Position.GetDistance(endPoint.Position);
	}

	public static double GetStandardDistance(this IPoint @this, IPoint endPoint)
	{
		return @this.Position.StandardDistance(endPoint.Position);
	}
}
