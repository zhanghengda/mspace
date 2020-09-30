using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Mmc.Windows.Utils
{
    public static class ComFactory
    {
        public static bool ReleaseComObject(object comObj)
        {
            bool flag = comObj == null;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                if (comObj is IDisposable)
                {
                    ((IDisposable)comObj).Dispose();
                }
                else
                {
                    //ÔÝÊ±ÆÁ±Î
                    //try
                    //{
                    //    Marshal.ReleaseComObject(comObj);
                    //}
                    //catch (Exception ex)
                    //{
                    //}
                  
                }
                comObj = null;
                result = true;
            }
            return result;
        }

        public static bool ReleaseComObjects<T>(this IEnumerable<T> comObjects)
        {
            bool flag = comObjects == null || comObjects.Count<T>() <= 0;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                foreach (T current in comObjects)
                {
                    bool flag2 = current != null;
                    if (flag2)
                    {
                        ComFactory.ReleaseComObject(current as IDisposable);
                    }
                }
                comObjects = null;
                result = true;
            }
            return result;
        }

        public static bool ReleaseComObjects(params object[] comObjects)
        {
            bool flag = comObjects == null || comObjects.Length == 0;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                object[] array = comObjects;
                for (int i = 0; i < array.Length; i++)
                {
                    object obj = array[i];
                    bool flag2 = obj != null;
                    if (flag2)
                    {
                        ComFactory.ReleaseComObject(obj as IDisposable);
                    }
                }
                comObjects = null;
                result = true;
            }
            return result;
        }
    }
}
