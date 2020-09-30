using System;
using System.Data;
using System.IO;

namespace Mmc.Framework.Services
{
	public class SkyBoxService
	{
		public static string DataFolder = string.Empty;

		private static DataTable _dataTable;

		public static SkyBox GetSkyBox(int id)
		{
			bool flag = SkyBoxService._dataTable == null || SkyBoxService._dataTable.Rows == null;
			SkyBox result;
			if (flag)
			{
				result = null;
			}
			else
			{
				SkyBox skyBox = null;
				foreach (DataRow dataRow in SkyBoxService._dataTable.Rows)
				{
					int num = int.Parse(dataRow["Code"].ToString());
					bool flag2 = num == id;
					if (flag2)
					{
						skyBox = new SkyBox
						{
							Code = int.Parse(dataRow["Code"].ToString()),
							Name = dataRow["Name"].ToString(),
							ImgPath = Path.Combine(SkyBoxService.DataFolder, dataRow["ImgPath"].ToString()),
							TopImage = Path.Combine(SkyBoxService.DataFolder, dataRow["TopImage"].ToString()),
							BottomImage = Path.Combine(SkyBoxService.DataFolder, dataRow["BottomImage"].ToString()),
							LeftImage = Path.Combine(SkyBoxService.DataFolder, dataRow["LeftImage"].ToString()),
							RightImage = Path.Combine(SkyBoxService.DataFolder, dataRow["RightImage"].ToString()),
							FrontImage = Path.Combine(SkyBoxService.DataFolder, dataRow["FrontImage"].ToString()),
							BackImage = Path.Combine(SkyBoxService.DataFolder, dataRow["BackImage"].ToString())
						};
						break;
					}
				}
				result = skyBox;
			}
			return result;
		}

		public static void Init(string skyboxDataFolder, string languageType)
		{
			SkyBoxService.DataFolder = skyboxDataFolder;
			SkyBoxService._dataTable = new DataTable("SkyBox");
			string text = Path.Combine(new string[]
			{
				SkyBoxService.DataFolder + SkyBoxService.GetFileName(languageType)
			});
			bool flag = File.Exists(text);
			if (flag)
			{
				SkyBoxService._dataTable.ReadXml(text);
			}
		}

		private static string GetFileName(string languageType)
		{
			string result;
			if (!(languageType == "zh-CHS"))
			{
				if (!(languageType == "en"))
				{
					if (!(languageType == "zh-CHT"))
					{
						result = "SkyBox.xml";
					}
					else
					{
						result = "SkyBox.zh-CHT.xml";
					}
				}
				else
				{
					result = "SkyBox.en.xml";
				}
			}
			else
			{
				result = "SkyBox.zh-CHS.xml";
			}
			return result;
		}
	}
}
