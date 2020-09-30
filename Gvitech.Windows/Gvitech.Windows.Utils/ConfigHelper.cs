using System;
using System.IO;

namespace Mmc.Windows.Utils
{
    public static class ConfigHelper<T> where T : class
    {
        public static string AsXml(T config)
        {
            return SerializationUtil.SerializeToXmlString<T>(config);
        }

        public static bool SaveXml(string xmlFile, T config)
        {
            bool flag = string.IsNullOrEmpty(xmlFile);
            if (flag)
            {
                throw new ArgumentNullException(xmlFile);
            }
            SerializationUtil.SerializeToXml<T>(xmlFile, config);
            return true;
        }

        public static T ResovleConfig(string configXml)
        {
            bool flag = string.IsNullOrEmpty(configXml);
            if (flag)
            {
                throw new ArgumentNullException("configXml");
            }
            return SerializationUtil.DeserializeFromXmlString<T>(configXml);
        }

        public static T ResovleConfigFromFile(string configFile)
        {
            bool flag = string.IsNullOrEmpty(configFile);
            if (flag)
            {
                throw new ArgumentNullException("configFile");
            }
            bool flag2 = !File.Exists(configFile);
            if (flag2)
            {
                throw new FileNotFoundException(configFile);
            }
            return SerializationUtil.DeserializeFromXml<T>(configFile);
        }

        public static void SaveWsConfig(string configFile, T obj)
        {
            bool flag = string.IsNullOrEmpty(configFile);
            if (flag)
            {
                throw new ArgumentNullException("configFile");
            }
            bool flag2 = !File.Exists(configFile);
            if (flag2)
            {
                throw new FileNotFoundException(configFile);
            }
            SerializationUtil.SerializeToXml<T>(configFile, obj);
        }
    }
}
