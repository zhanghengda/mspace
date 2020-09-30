using System;
using System.IO;
using System.Text;
using System.Xml;
using Mmc.Windows.Design;

namespace Mmc.Business.Data
{
	// Token: 0x02000012 RID: 18
	public class PoiLayerXmlParser : Singleton<PoiLayerXmlParser>
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00003A68 File Offset: 0x00001C68
		public XmlDocument LoadXmlDocument(byte[] buf)
		{
			MemoryStream count = new MemoryStream(buf);
			XmlDocument field = new XmlDocument();
			field.Load(count);
			count.Close();
			return field;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003A98 File Offset: 0x00001C98
		public XmlDocument LoadXmlDocument(string xmlInfo)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(xmlInfo);
			return this.LoadXmlDocument(bytes);
		}
	}
}
