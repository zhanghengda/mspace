using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using System;

namespace Mmc.Framework.Services
{
	public class BaseFeatureLayer : IBaseFeatureLayer
	{
		public string DataSourceGuid
		{
			get;
			set;
		}

		public string FcName
		{
			get;
			set;
		}

		public string FcGuid
		{
			get;
			set;
		}

		public string FcAliasName
		{
			get;
			set;
		}

		public IFeatureClass FeatureClass
		{
			get;
			set;
		}

		public IFeatureLayer FeatureLayer
		{
			get;
			set;
		}

		public virtual void Init(IFeatureClass fc, IFeatureLayer layer)
		{
			bool flag = fc != null;
			if (flag)
			{
				this.FeatureClass = fc;
				this.FeatureLayer = layer;
				this.FcName = fc.Name;
				this.FcGuid = fc.Guid.ToString();
				this.FcAliasName = fc.Alias;
				this.DataSourceGuid = fc.FeatureDataSet.DataSource.Guid.ToString();
			}
		}
	}
}
