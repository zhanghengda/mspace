using System;
using System.IO;
using System.Text;

namespace Mmc.Windows.Utils
{
    public class TxtFileUtil
    {
        public static string Read(string path)
        {
            try
            {
                StreamReader sr = new StreamReader(path, Encoding.Default);
                var res = new StringBuilder();
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    res.AppendLine(line.ToString());
                }
                sr.Close();
                return res.ToString();
            }
            catch (Exception ex)
            {
                Services.SystemLog.Log(ex);
                return string.Empty;
            }
        }

        public static void Write(string path, string content)
        {
            try
            {
                //先清空
                FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);
                //再写入
                StreamWriter sw = new StreamWriter(stream);
                sw.Write(content);
                sw.Flush();
                sw.Close();
                stream.Close();
            }
            catch (Exception ex)
            {
                Services.SystemLog.Log(ex);
            }
        }
    }
}