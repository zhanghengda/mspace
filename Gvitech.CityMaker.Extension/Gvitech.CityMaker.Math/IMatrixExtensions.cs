using System;

namespace Gvitech.CityMaker.Math
{
	public static class IMatrixExtensions
	{
		public static IMatrix PlusMatrix(this IMatrix matrix1, IMatrix matrix2)
		{
			bool flag = matrix1 == null;
			if (flag)
			{
				throw new NullReferenceException("matrix1 maynot be null");
			}
			bool flag2 = matrix2 == null;
			IMatrix result;
			if (flag2)
			{
				result = matrix1.Clone();
			}
			else
			{
				result = new Matrix
				{
					M11 = matrix1.M11 + matrix2.M11,
					M12 = matrix1.M12 + matrix2.M12,
					M13 = matrix1.M13 + matrix2.M13,
					M14 = matrix1.M14 + matrix2.M14,
					M21 = matrix1.M21 + matrix2.M21,
					M22 = matrix1.M22 + matrix2.M22,
					M23 = matrix1.M23 + matrix2.M23,
					M24 = matrix1.M24 + matrix2.M24,
					M31 = matrix1.M31 + matrix2.M31,
					M32 = matrix1.M32 + matrix2.M32,
					M33 = matrix1.M33 + matrix2.M33,
					M34 = matrix1.M34 + matrix2.M34,
					M41 = matrix1.M41 + matrix2.M41,
					M42 = matrix1.M42 + matrix2.M42,
					M43 = matrix1.M43 + matrix2.M43,
					M44 = matrix1.M44 + matrix2.M44
				};
			}
			return result;
		}

		public static IMatrix MinusMatrix(this IMatrix matrix1, IMatrix matrix2)
		{
			bool flag = matrix1 == null;
			if (flag)
			{
				throw new NullReferenceException("matrix1 maynot be null");
			}
			bool flag2 = matrix2 == null;
			IMatrix result;
			if (flag2)
			{
				result = matrix1.Clone();
			}
			else
			{
				result = new Matrix
				{
					M11 = matrix1.M11 - matrix2.M11,
					M12 = matrix1.M12 - matrix2.M12,
					M13 = matrix1.M13 - matrix2.M13,
					M14 = matrix1.M14 - matrix2.M14,
					M21 = matrix1.M21 - matrix2.M21,
					M22 = matrix1.M22 - matrix2.M22,
					M23 = matrix1.M23 - matrix2.M23,
					M24 = matrix1.M24 - matrix2.M24,
					M31 = matrix1.M31 - matrix2.M31,
					M32 = matrix1.M32 - matrix2.M32,
					M33 = matrix1.M33 - matrix2.M33,
					M34 = matrix1.M34 - matrix2.M34,
					M41 = matrix1.M41 - matrix2.M41,
					M42 = matrix1.M42 - matrix2.M42,
					M43 = matrix1.M43 - matrix2.M43,
					M44 = matrix1.M44 - matrix2.M44
				};
			}
			return result;
		}

		public static IMatrix MultiplyMatrix(this IMatrix matrix1, IMatrix matrix2)
		{
			bool flag = matrix1 == null;
			if (flag)
			{
				throw new NullReferenceException("matrix1 maynot be null");
			}
			bool flag2 = matrix2 == null;
			IMatrix result;
			if (flag2)
			{
				result = matrix1.Clone();
			}
			else
			{
				result = new Matrix
				{
					M11 = matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21 + matrix1.M13 * matrix2.M31 + matrix1.M14 * matrix2.M41,
					M12 = matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22 + matrix1.M13 * matrix2.M32 + matrix1.M14 * matrix2.M42,
					M13 = matrix1.M11 * matrix2.M13 + matrix1.M12 * matrix2.M23 + matrix1.M13 * matrix2.M33 + matrix1.M14 * matrix2.M43,
					M14 = matrix1.M11 * matrix2.M14 + matrix1.M12 * matrix2.M24 + matrix1.M13 * matrix2.M34 + matrix1.M14 * matrix2.M44,
					M21 = matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21 + matrix1.M23 * matrix2.M31 + matrix1.M24 * matrix2.M41,
					M22 = matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22 + matrix1.M23 * matrix2.M32 + matrix1.M24 * matrix2.M42,
					M23 = matrix1.M21 * matrix2.M13 + matrix1.M22 * matrix2.M23 + matrix1.M23 * matrix2.M33 + matrix1.M24 * matrix2.M43,
					M24 = matrix1.M21 * matrix2.M14 + matrix1.M22 * matrix2.M24 + matrix1.M23 * matrix2.M34 + matrix1.M24 * matrix2.M44,
					M31 = matrix1.M31 * matrix2.M11 + matrix1.M32 * matrix2.M21 + matrix1.M33 * matrix2.M31 + matrix1.M34 * matrix2.M41,
					M32 = matrix1.M31 * matrix2.M12 + matrix1.M32 * matrix2.M22 + matrix1.M33 * matrix2.M32 + matrix1.M34 * matrix2.M42,
					M33 = matrix1.M31 * matrix2.M13 + matrix1.M32 * matrix2.M23 + matrix1.M33 * matrix2.M33 + matrix1.M34 * matrix2.M43,
					M34 = matrix1.M31 * matrix2.M14 + matrix1.M32 * matrix2.M24 + matrix1.M33 * matrix2.M34 + matrix1.M34 * matrix2.M44,
					M41 = matrix1.M41 * matrix2.M11 + matrix1.M42 * matrix2.M21 + matrix1.M43 * matrix2.M31 + matrix1.M44 * matrix2.M41,
					M42 = matrix1.M41 * matrix2.M12 + matrix1.M42 * matrix2.M22 + matrix1.M43 * matrix2.M32 + matrix1.M44 * matrix2.M42,
					M43 = matrix1.M41 * matrix2.M13 + matrix1.M42 * matrix2.M23 + matrix1.M43 * matrix2.M33 + matrix1.M44 * matrix2.M43,
					M44 = matrix1.M41 * matrix2.M14 + matrix1.M42 * matrix2.M24 + matrix1.M43 * matrix2.M34 + matrix1.M44 * matrix2.M44
				};
			}
			return result;
		}

		public static IMatrix DevideMatrix(this IMatrix matrix1, IMatrix matrix2)
		{
			bool flag = matrix1 == null;
			if (flag)
			{
				throw new NullReferenceException("matrix1 maynot be null");
			}
			bool flag2 = matrix2 == null;
			IMatrix result;
			if (flag2)
			{
				result = matrix1.Clone();
			}
			else
			{
				IMatrix matrix3 = matrix2.Clone();
				matrix3.Inverse();
				result = matrix1.MultiplyMatrix(matrix3);
			}
			return result;
		}
	}
}
