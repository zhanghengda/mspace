using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Mmc.Mspace.Common
{
    public  class ImageHelper
    {

        /// <summary>
        /// 标注模块截图保存
        /// </summary>
        /// <param name="imagepath"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static bool SaveImage(string imagepath, BitmapSource bit)
        {
            try
            {
                BitmapSource BS = bit;
                JpegBitmapEncoder PBE = new JpegBitmapEncoder();
                PBE.Frames.Add(BitmapFrame.Create(BS));
                using (FileStream file = new FileStream(imagepath, FileMode.Create, FileAccess.Write))
                {
                    PBE.Save(file);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
    }
}
