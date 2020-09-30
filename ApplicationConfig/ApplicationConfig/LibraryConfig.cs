using Gvitech.CityMaker.FdeCore;
using LiteDB;
using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ApplicationConfig
{
	public class LibraryConfig : ILibraryConfig
	{
		[XmlAttribute]
		public string ConnInfoString { get; set; }
        public string UserName { get; set ; }
        public int Id { get; set; }
        public int IsAdministrator { get; set; }
        public bool IsLocal { get; set; }
        [XmlAttribute]
        public string Guid { get; set; }
        //public string HashCode { get; set; }
        [XmlAttribute]
        public string AliasName { get; set; }

        public bool Is2DData { get; set; }

        public LibraryConfig()
		{
		}

		public LibraryConfig(string connInfoString)
		{
			bool flag = string.IsNullOrEmpty(connInfoString);
			if (flag)
			{
				throw new ArgumentNullException("connInfoString");
			}
			this.ConnInfoString = connInfoString;
		}

		public IConnectionInfo ToConnectionInfo()
		{
			Trace.Assert(!string.IsNullOrEmpty(this.ConnInfoString), "ConnInfoString may not be null");
			bool flag = string.IsNullOrEmpty(this.ConnInfoString);
			IConnectionInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IConnectionInfo connectionInfo = new ConnectionInfo();
				result = (connectionInfo.FromConnectionString(this.ConnInfoString) ? connectionInfo : null);
			}
			return result;
		}
	}
}
