using Newtonsoft.Json;
using System;
using System.IO;

namespace Mmc.Windows.Utils
{
	public static class JsonUtil
	{
		public static bool SerializeToFile(string filePath, object value, Formatting formatting = Formatting.Indented)
		{
			bool result;
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(filePath))
				{
					string value2 = JsonConvert.SerializeObject(value, formatting);
					streamWriter.Write(value2);
				}
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public static string SerializeToString(object value, Formatting formatting = Formatting.Indented)
		{
			string result;
			try
			{
				result = JsonConvert.SerializeObject(value, formatting);
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}

		public static T DeserializeFromFile<T>(string filePath)
		{
			bool flag = !File.Exists(filePath);
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				string value = File.ReadAllText(filePath);
				result = (T)((object)JsonConvert.DeserializeObject(value, typeof(T)));
			}
			return result;
		}

        public static T DeserializeFromString<T>(string value) where T : new()
        {
            if (string.IsNullOrWhiteSpace(value))
                return new T();
            return (T) ((object) JsonConvert.DeserializeObject(value, typeof(T)));
        }
    }
}
