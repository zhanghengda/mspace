using Mmc.Windows.Services;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Mmc.Windows.Utils
{
	public class SerializationUtil
	{
		public static void SerializeToXml<T>(string filePath, T obj)
		{
			using (StreamWriter streamWriter = new StreamWriter(filePath))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				xmlSerializer.Serialize(streamWriter, obj);
			}
		}

		public static T DeserializeFromXml<T>(string filePath)
		{
			bool flag = !File.Exists(filePath);
            SystemLog.Log(filePath, LogMessageType.INFO);
			if (flag)
			{
				throw new ArgumentNullException(filePath + " not Exists");
			}
			T result;
            using (StreamReader streamReader = new StreamReader(filePath))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				T t = (T)(xmlSerializer.Deserialize(streamReader));
				result = t;
			}
            SystemLog.Log(result.ToString(), LogMessageType.INFO);
            return result;
		}

		public static string SerializeToXmlString<T>(T obj)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				XmlWriterSettings settings = new XmlWriterSettings
				{
					Encoding = Encoding.UTF8,
					NewLineChars = "\r\n",
					Indent = true,
					IndentChars = "  "
				};
				using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, settings))
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
					xmlSerializer.Serialize(xmlWriter, obj);
					xmlWriter.Close();
					memoryStream.Position = 0L;
					using (StreamReader streamReader = new StreamReader(memoryStream))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			return result;
		}

		public static T DeserializeFromXmlString<T>(string xmlString)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
			bool flag = bytes.Length < 1;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				using (MemoryStream memoryStream = new MemoryStream(bytes))
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
					using (StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8))
					{
						result = (T)((object)xmlSerializer.Deserialize(streamReader));
					}
				}
			}
			return result;
		}

		public static byte[] SerializeObject(object obj)
		{
			bool flag = obj == null;
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					new BinaryFormatter().Serialize(memoryStream, obj);
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		public static object DeserializeObject(byte[] bytes)
		{
			bool flag = bytes == null || bytes.Length == 0;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				using (MemoryStream memoryStream = new MemoryStream(bytes)
				{
					Position = 0L
				})
				{
					result = new BinaryFormatter().Deserialize(memoryStream);
				}
			}
			return result;
		}

		public static bool Deserialize<T>(byte[] objArray, out T result)
		{
			result = default(T);
			bool flag = objArray == null || objArray.Length == 0;
			bool result2;
			if (flag)
			{
				result2 = false;
			}
			else
			{
				try
				{
					using (MemoryStream memoryStream = new MemoryStream(objArray))
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						result = (T)((object)binaryFormatter.Deserialize(memoryStream));
						result2 = true;
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			return result2;
		}
	}
}
