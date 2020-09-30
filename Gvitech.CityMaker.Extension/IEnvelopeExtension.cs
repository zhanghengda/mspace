using Gvitech.CityMaker.Math;
using System;

public static class IEnvelopeExtension
{
	public static float DiagonalDistance(this IEnvelope @this)
	{
		bool flag = @this == null;
		if (flag)
		{
			throw new NullReferenceException("@this");
		}
		return (float)Math.Sqrt(@this.Width * @this.Width + @this.Depth * @this.Depth + @this.Height * @this.Height);
	}
}
