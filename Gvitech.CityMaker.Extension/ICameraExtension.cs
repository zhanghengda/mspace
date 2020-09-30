using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.Models;
using Gvitech.CityMaker.RenderControl;
using System;

public static class ICameraExtension
{
	private static readonly IGeometryFactory Geofactory = new GeometryFactory();

	public static bool LookAt(this ICamera @this, IPoint startPoint, IPoint endPoint, double distance = -1.0)
	{
		return @this.LookAt2(startPoint, endPoint, distance);
	}

	public static bool LookAt(this ICamera @this, IVector3 watchPoint, IVector3 targetPoint, double distance = -1.0)
	{
		IEulerAngle aimingAngles = @this.GetAimingAngles(watchPoint, targetPoint);
		bool flag = distance == -1.0;
		if (flag)
		{
			distance = watchPoint.GetDistance(targetPoint);
		}
		@this.LookAt(watchPoint, distance, aimingAngles);
		return true;
	}

	public static bool LookAt2(this ICamera @this, IPoint startPoint, IPoint endPoint, double distance = -1.0)
	{
		IEulerAngle aimingAngles = @this.GetAimingAngles2(startPoint, endPoint);
		bool flag = distance == -1.0;
		if (flag)
		{
			distance = startPoint.GetDistance(endPoint);
		}
		@this.LookAt2(startPoint, distance, aimingAngles);
		return true;
	}

	public static bool SetCamera(this ICamera @this, double x, double y, double z, double heading, double tilt, double roll, ISpatialCRS crs = null, gviSetCameraFlags flag = gviSetCameraFlags.gviSetCameraNoFlags)
	{
		IVector3 this2 = new Vector3
		{
			X = x,
			Y = y,
			Z = z
		};
		IEulerAngle angle = new EulerAngle
		{
			Heading = heading,
			Tilt = tilt,
			Roll = roll
		};
		@this.SetCamera2(this2.ToPoint(ICameraExtension.Geofactory, crs), angle, flag);
		return true;
	}

	public static void FlyToEnvelope(this ICamera @this, IEnvelope enp, ISpatialCRS crs = null, float multiDistance = 2f, double times = 0.5, IEulerAngle angle = null)
	{
		bool flag = @this == null;
		if (flag)
		{
			throw new NullReferenceException("@this");
		}
		bool flag2 = enp == null;
		if (!flag2)
		{
			@this.FlyTime = times;
			IEulerAngle arg_72_0;
			if (angle != null)
			{
				arg_72_0 = angle;
			}
			else
			{
				IEulerAngle eulerAngle = new EulerAngle
				{
					Heading = 45.0,
					Roll = 0.0,
					Tilt = -45.0
				};
				arg_72_0 = eulerAngle;
			}
			angle = arg_72_0;
			double num = (double)(enp.DiagonalDistance() * multiDistance);
			num = ((num == 0.0) ? 1.0 : num);
			num = ((num > 3000.0) ? 3000.0 : num);
			IVector3 this2 = new Vector3
			{
				X = (enp.MaxX + enp.MinX) / 2.0,
				Y = (enp.MaxY + enp.MinY) / 2.0,
				Z = (enp.MaxZ + enp.MinZ) / 2.0
			};
			@this.LookAt2(this2.ToPoint(ICameraExtension.Geofactory, crs), num, angle);
		}
	}

	public static bool LookAtGeometry(this ICamera @this, IGeometry geo, double distance = 100.0, string wkt = "")
	{
		bool flag = geo == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			gviGeometryType geometryType = geo.GeometryType;
			if (geometryType != gviGeometryType.gviGeometryPoint)
			{
				bool flag2 = string.IsNullOrEmpty(wkt);
				if (flag2)
				{
					@this.LookAtEnvelope(geo.Envelope);
				}
				else
				{
					@this.LookAtEnvelope2(wkt, geo.Envelope);
				}
			}
			else
			{
				IVector3 vector;
				IEulerAngle angle;
				@this.GetCamera(out vector, out angle);
				IGeometry geometry = geo.Clone();
				bool flag3 = !string.IsNullOrEmpty(wkt);
				if (flag3)
				{
					bool flag4 = !geometry.ProjectEx(wkt);
					if (flag4)
					{
						result = false;
						return result;
					}
				}
				@this.LookAt2(geometry as IPoint, distance, angle);
			}
			result = true;
		}
		return result;
	}

	public static bool LookAt(this ICamera @this, IFeatureLayer featureLayer, double distance = 1000.0, double heading = 0.0, double tilt = -20.0, double roll = 0.0)
	{
		IEulerAngle eulerAngle = new EulerAngle();
		eulerAngle.Set(heading, tilt, roll);
		@this.LookAt(featureLayer.Envelope.Center, distance, eulerAngle);
		return true;
	}

	public static bool LookAt(this ICamera @this, double x, double y, double z, double distance = 1000.0, double heading = 0.0, double tilt = -20.0)
	{
		IVector3 vector = new Vector3();
		vector.Set(x, y, z);
		IEulerAngle eulerAngle = new EulerAngle();
		eulerAngle.Set(heading, tilt, 0.0);
		@this.LookAt(vector, distance, eulerAngle);
		return true;
	}

	public static bool LookAt(this ICamera @this, CameraProperty cameraProperty, ISpatialCRS crs = null, gviSetCameraFlags flag = gviSetCameraFlags.gviSetCameraNoFlags)
	{
		bool flag2 = cameraProperty == null;
		bool result;
		if (flag2)
		{
			result = false;
		}
		else
		{
			@this.SetCamera(cameraProperty.X, cameraProperty.Y, cameraProperty.Z, cameraProperty.Heading, cameraProperty.Tilt, cameraProperty.Roll, crs, flag);
			result = true;
		}
		return result;
	}

	public static void GetCamera(this ICamera @this, out double x, out double y, out double z, out double heading, out double tilt, out double roll)
	{
		IVector3 vector;
		IEulerAngle eulerAngle;
		@this.GetCamera(out vector, out eulerAngle);
		x = vector.X;
		y = vector.Y;
		z = vector.Z;
		heading = eulerAngle.Heading;
		tilt = eulerAngle.Tilt;
		roll = eulerAngle.Roll;
	}

	public static IEulerAngle GetWatchAngle(this ICamera @this, IPoint startPoint, IPoint endPoint)
	{
		bool flag = startPoint == null || endPoint == null;
		if (flag)
		{
			throw new ArgumentNullException("参数为空");
		}
		bool flag2 = startPoint == endPoint;
		if (flag2)
		{
			throw new ArgumentException("startPoint==endPoint");
		}
		IVector3 vector = new Vector3
		{
			X = endPoint.X - startPoint.X,
			Y = endPoint.Y - startPoint.Y,
			Z = endPoint.Z - startPoint.Z
		};
		IVector3 vector2 = vector.Clone();
		vector2.Z = 0.0;
		IVector3 vector3 = vector.Normal(vector2);
		IVector3 vector4 = vector.Clone();
		IVector3 vector5 = vector4;
		vector5.X /= 2.0;
		vector5 = vector4;
		vector5.Y /= 2.0;
		vector5 = vector4;
		vector5.Z /= 2.0;
		IVector3 vector6 = vector4.Clone();
		vector5 = vector6;
		vector5.X += vector3.X;
		vector5 = vector6;
		vector5.Y += vector3.Y;
		vector5 = vector6;
		vector5.Z += vector3.Z;
		return @this.GetAimingAngles(vector6, vector4);
	}

	public static void FlyToPlan(this ICamera @this, IFeatureClass featureClass, int planId, string crs = null)
	{
		bool flag = planId < 0 || featureClass == null;
		if (!flag)
		{
			featureClass.FlyToPlan(@this, planId, crs);
		}
	}
}
