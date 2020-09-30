using System;
using System.Collections.Generic;
using System.IO;
using Gvitech.Windows.Utils;
using Mmc.Windows.Utils;

namespace Mmc.Mspace.ToolModule.DynamicClip
{
	// Token: 0x02000007 RID: 7
	public class ClipDataXMLProvider:IClipDataProvider
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000022E3 File Offset: 0x000004E3
		public ClipDataXMLProvider()
		{
			if (!File.Exists(this._fileName))
			{
				File.Create(this._fileName);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002323 File Offset: 0x00000523
		public bool Save(List<ClipData> clipDataSet)
		{
			SerializationUtil.SerializeToXml<List<ClipData>>(this._fileName, clipDataSet);
			return true;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002332 File Offset: 0x00000532
		public List<ClipData> GetClipDataSet()
		{
			return SerializationUtil.DeserializeFromXml<List<ClipData>>(this._fileName);
		}

		// Token: 0x0400000E RID: 14
		private readonly string _fileName = System.Windows.Forms.Application.LocalUserAppDataPath + "\\ClipData.xml";
	}
}
