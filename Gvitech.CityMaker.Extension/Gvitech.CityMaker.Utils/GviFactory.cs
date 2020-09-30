using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using Mmc.Windows.Utils;
using System;
using System.IO;

namespace Gvitech.CityMaker.Utils
{
	public static class GviFactory
	{
		public static IConnectionInfo CreateConnectionInfo(gviConnectionType connectionType, string database, uint port = 0u, string server = "", string instance = "", string userName = "", string password = "", string version = "")
		{
			return new ConnectionInfo
			{
				ConnectionType = connectionType,
				Database = database,
				Port = port,
				Server = server,
				Instance = instance,
				UserName = userName,
				Password = password,
				Version = version
			};
		}

		public static IConnectionInfo CreateConnectionInfoFromFile(string database, uint port = 0u, string server = "", string instance = "", string userName = "", string password = "", string version = "")
		{
			bool flag = string.IsNullOrEmpty(database) || !File.Exists(database);
			IConnectionInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IConnectionInfo connectionInfo = new ConnectionInfo();
				connectionInfo.Database = database;
				string text = Path.GetExtension(database).ToLower();
				string a = text;
				if (!(a == ".fdb"))
				{
					if (!(a == ".sdb"))
					{
						if (a == ".shp")
						{
							connectionInfo.ConnectionType = gviConnectionType.gviConnectionShapeFile;
						}
					}
					else
					{
						connectionInfo.ConnectionType = gviConnectionType.gviConnectionSQLite3;
					}
				}
				else
				{
					connectionInfo.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
				}
				connectionInfo.Port = port;
				connectionInfo.Server = server;
				connectionInfo.Instance = instance;
				connectionInfo.UserName = userName;
				connectionInfo.Password = password;
				connectionInfo.Version = version;
				result = connectionInfo;
			}
			return result;
		}

		public static ITextAttribute CreateTextAttribute(int textSize = 10, string fontName = "微软雅黑", bool bold = false, uint textColor = 4294967040u, uint backColor = 4278190080u)
		{
			return new TextAttribute
			{
				TextSize = textSize,
				Bold = bold,
				Font = fontName,
				TextColor = ColorConvert.UintToColor(textColor),
				BackgroundColor = ColorConvert.UintToColor(backColor)
			};
		}

		public static IFieldInfo CreateField(string name, string aliasName, gviFieldType type, int length, int precision, bool nullable)
		{
			IFieldInfo fieldInfo = new FieldInfo();
			fieldInfo.Name = name;
			fieldInfo.FieldType = type;
			fieldInfo.Alias = aliasName;
			bool flag = length > -1;
			if (flag)
			{
				fieldInfo.Length = length;
			}
			fieldInfo.Nullable = nullable;
			bool flag2 = precision > -1;
			if (flag2)
			{
				fieldInfo.Precision = precision;
			}
			return fieldInfo;
		}

		public static ISimpleGeometryRender CreateSimpleGeoRender(string imageName, int size, gviPivotAlignment alignment = gviPivotAlignment.gviPivotAlignCenterCenter)
		{
			return new SimpleGeometryRender
			{
				Symbol = new ImagePointSymbol
				{
					ImageName = imageName,
					Size = size,
					Alignment = alignment
				}
			};
		}

		public static ISimpleTextRender CreateSimpleTextRender(string fieldName, bool dynamicPlacement, bool minimizeOverlap, ITextSymbol textSymbol)
		{
			return new SimpleTextRender
			{
				DynamicPlacement = dynamicPlacement,
				MinimizeOverlap = minimizeOverlap,
				Expression = "$(" + fieldName + ")",
				Symbol = textSymbol
			};
		}
	}
}
