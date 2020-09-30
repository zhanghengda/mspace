using System;
using System.Collections.Generic;

namespace Gvitech.CityMaker.Models
{
	public class GeometryRenderMetadata
	{
		public string LookUpField
		{
			get;
			set;
		}

		public List<string> TypeValues
		{
			get;
			set;
		}

		public Dictionary<string, string> Legend
		{
			get;
			set;
		}

		public GeometryRenderMetadata()
		{
			this.TypeValues = new List<string>();
			this.Legend = new Dictionary<string, string>();
		}
	}
}
