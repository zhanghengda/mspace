using Gvitech.CityMaker.FdeGeometry;
namespace Gvitech.CityMaker.Utils
{
    public static class GviMath
	{
		public static double CalcDistance(IPoint start, IPoint end)
		{
			bool flag = start != null && end != null;
			double result;
			if (flag)
			{
				result = System.Math.Sqrt((end.X - start.X) * (end.X - start.X) + (end.Y - start.Y) * (end.Y - start.Y) + (end.Z - start.Z) * (end.Z - start.Z));
			}
			else
			{
				result = 0.0;
			}
			return result;
		}

		public static double RadToAngle(double rad)
		{
			return rad / 3.1415926535897931 * 180.0;
		}

		public static double AngleToRad(double angle)
		{
			return angle / 180.0 * 3.1415926535897931;
		}
	}
}
