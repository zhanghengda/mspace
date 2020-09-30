using Mmc.Windows.Services;
using System;

namespace Gvitech.CityMaker.Models
{
	[Serializable]
	public class CameraProperty
	{
		public double Heading;

		public double Roll;

		public double Tilt;

		public double X;

		public double Y;

		public double Z;

        public bool SetCameraProperty(string str)
		{
			bool result;
			try
			{
				bool flag = str == null;
				if (flag)
				{
					result = false;
				}
				else
				{
					string[] array = str.Split(new char[]
					{
						';'
					});
					double.TryParse(array[0], out this.X);
					double.TryParse(array[1], out this.Y);
					double.TryParse(array[2], out this.Z);
					double.TryParse(array[3], out this.Heading);
					double.TryParse(array[4], out this.Tilt);
					double.TryParse(array[5], out this.Roll);
					result = true;
				}
			}
			catch (Exception message)
			{
				SystemLog.Log(message);
				result = false;
			}
			return result;
		}

		public string PropertyStrings()
		{
			return string.Format("{0};{1};{2};{3};{4};{5}", new object[]
			{
				this.X,
				this.Y,
				this.Z,
				this.Heading,
				this.Tilt,
				this.Roll
			});
		}
	}
}
