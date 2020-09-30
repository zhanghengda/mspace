using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gvitech.Windows.Utils
{
    public class EncryptUtil
    {
        private static readonly string AESKey = "Mmc&(mspace)@1_Fly*2>skY"; // 24字节

        public static string AESEncrypt(string value)
        {
            try
            {
                using (RijndaelManaged rijndael = new RijndaelManaged())
                {
                    byte[] keyArray = Encoding.UTF8.GetBytes(AESKey);
                    byte[] toEncryptArray = Encoding.UTF8.GetBytes(value);

                    rijndael.Key = keyArray;
                    rijndael.Mode = CipherMode.ECB;
                    rijndael.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = rijndael.CreateEncryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string AESDecrypt(string value)
        {
            try
            {
                using (RijndaelManaged rijndael = new RijndaelManaged())
                {
                    byte[] keyArray = Encoding.UTF8.GetBytes(AESKey);
                    byte[] toEncryptArray = Convert.FromBase64String(value);

                    rijndael.Key = keyArray;
                    rijndael.Mode = CipherMode.ECB;
                    rijndael.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = rijndael.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    return Encoding.UTF8.GetString(resultArray);
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
