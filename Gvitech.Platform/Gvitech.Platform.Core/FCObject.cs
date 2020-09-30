using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using System;
using System.Collections;

namespace Mmc.Platform.Core
{
	public class FCObject
	{
		public Hashtable FieldColorMap
		{
			get;
			set;
		}

		public string FieldColorName
		{
			get;
			set;
		}

		public IMultiPolygon MuliPolygon
		{
			get;
			set;
		}

		public IRowBufferCollection RowBufferCollection
		{
			get;
			set;
		}
	}
}
