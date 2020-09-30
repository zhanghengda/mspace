using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using System;

namespace Mmc.Framework.Services
{
	public interface IBaseFeatureLayer
	{
		void Init(IFeatureClass fc, IFeatureLayer fl);
	}
}
