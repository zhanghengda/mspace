using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Mmc.Windows.Utils
{
	public static class JsonTool
	{
		public static string ObjectToJson(object item)
		{
			string result;
			try
			{
				bool flag = item == null;
				if (flag)
				{
					result = "";
				}
				else
				{
					DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(item.GetType());
					MemoryStream memoryStream = new MemoryStream();
					dataContractJsonSerializer.WriteObject(memoryStream, item);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(Encoding.UTF8.GetString(memoryStream.ToArray()));
					memoryStream.Close();
					result = stringBuilder.ToString();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public static T JsonToObject<T>(string jsonString)
		{
			T result;
			try
			{
				bool flag = string.IsNullOrEmpty(jsonString);
				if (flag)
				{
					result = default(T);
				}
				else
				{
					DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
					MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
					T t = (T)((object)dataContractJsonSerializer.ReadObject(memoryStream));
					memoryStream.Close();
					result = t;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
	}
}
