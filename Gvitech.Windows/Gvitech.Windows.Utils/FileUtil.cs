using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gvitech.Windows.Utils
{
    public class FileUtil
    {
        /// <summary>
        /// 读取文件md5码
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取文件 Sha1 码
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetSHA1FromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.SHA1 sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                byte[] retval = sha1.ComputeHash(file);
                file.Close();
                file.Dispose();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retval.Length; i++)
                {
                    sb.Append(retval[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch(IOException ioex)
            {
                Console.WriteLine(ioex.Message);
                return "DATAOCCUPIED";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }

        }

    }
}
