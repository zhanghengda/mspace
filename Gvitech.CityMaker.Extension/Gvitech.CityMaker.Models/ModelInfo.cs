using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using System;

namespace Gvitech.CityMaker.Models
{
	public class ModelInfo
	{
		public IModel Model
		{
			get;
			set;
		}

		public IModelPoint ModelPoint
		{
			get;
			set;
		}

		public IMultiTriMesh MultiTriMesh
		{
			get;
			set;
		}

		public string FeatureClassGuid
		{
			get;
			set;
		}

		public int FeatureId
		{
			get;
			set;
		}

		public void ReleaseModel(IObjectManager omg)
		{
			bool flag = this.Model != null && this.ModelPoint != null;
			if (flag)
			{
				omg.DeleteModel(this.ModelPoint.ModelName);
				this.ModelPoint.ReleaseComObject();
				this.Model = null;
			}
			bool flag2 = this.MultiTriMesh != null;
			if (flag2)
			{
				this.MultiTriMesh.ReleaseComObject();
			}
		}
	}
}
