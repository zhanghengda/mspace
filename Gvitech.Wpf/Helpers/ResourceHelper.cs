using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Helpers
{
    public class ResourceHelper
    {
        public static string FindKey(string keyName)
        {
            try
            {                  
                if (string.IsNullOrWhiteSpace(keyName)) return "";
                return (string)Application.Current.FindResource(keyName);
            }
            catch (Exception ex)
            {
                //todo 打印日志   
                SystemLog.Log(ex);
                return null;
            }
        }

        public static object FindResourceByKey(string keyName)
        {
            var resourceDict = Application.Current.Resources;
            if (resourceDict == null || resourceDict.Count == 0)
                return null;
            var result = Application.Current.TryFindResource(keyName);
            return result;
        }
    }
}
