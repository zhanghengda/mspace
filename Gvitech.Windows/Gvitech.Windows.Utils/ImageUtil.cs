using System;
using System.Drawing;
using System.IO;

namespace Gvitech.Windows.Utils
{
    public class ImageUtil
    {

        /// <summary>
        /// 调用系统默认图片读取软件
        /// </summary>
        /// <param name="fileName"></param>
        public static void InvokeImageProcess(string fileName)
        {
            //建立新的系统进程
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            //设置图片的真实路径和文件名
            process.StartInfo.FileName = fileName;

            //设置进程运行参数，这里以最大化窗口方法显示图片。
            process.StartInfo.Arguments = "rundl132.exe C://WINDOWS//system32//shimgvw.dll,ImageView_Fullscreen";

            //此项为是否使用Shell执行程序，因系统默认为true，此项也可不设，但若设置必须为true
            process.StartInfo.UseShellExecute = true;

            //此处可以更改进程所打开窗体的显示样式，可以不设
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.Start();
            process.Close();
        }


        /// <summary>
        /// 图片显示成缩略图
        /// </summary>
        /// <param name="sourcePath" >图片原路径</param>
        /// <param name="newPath">生成的缩略图新路径</param>
        /// <param name="width">生成的缩略图宽度</param>
        /// <param name="height">生成的缩略图高度</param>
        public static void MakeThumbnail(string sourcePath, string newPath, int width, int height)
        {
            System.Drawing.Image ig = System.Drawing.Image.FromFile(sourcePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = ig.Width;
            int oh = ig.Height;
            if ((double)ig.Width / (double)ig.Height > (double)towidth / (double)toheight)
            {
                oh = ig.Height;
                ow = ig.Height * towidth / toheight;
                y = 0;
                x = (ig.Width - ow) / 2;

            }
            else
            {
                ow = ig.Width;
                oh = ig.Width * height / towidth;
                x = 0;
                y = (ig.Height - oh) / 2;
            }
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(System.Drawing.Color.Transparent);
            g.DrawImage(ig, new System.Drawing.Rectangle(0, 0, towidth, toheight), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);
            try
            {
                bitmap.Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ig.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        public static Image CreateThumbnail(string filePath ,int width=120,int height=120)
        {
            Image thumbImg;
            if (!File.Exists(filePath))
                return null;

            Image img = Image.FromFile(filePath);
            if (img == null)
                return null;

            thumbImg = img.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
            return thumbImg;
        }

        public static Image CutEllipse(Image img, Color bordercolor,int radius=120)
        {
            Bitmap bitmap = new Bitmap(radius, radius);
            Rectangle rectangle = new Rectangle(0, 0, radius, radius);
            Size size = new Size(radius, radius);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (TextureBrush br = new TextureBrush(img, System.Drawing.Drawing2D.WrapMode.Clamp, rectangle))
                {
                    br.ScaleTransform(bitmap.Width / (float)radius, bitmap.Height / (float)radius);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(br, new Rectangle(Point.Empty, size));
                }
                using (Pen pen = new Pen(bordercolor, 10))
                {
                    g.DrawEllipse(pen, new Rectangle(Point.Empty, size));//加椭圆边框
                    g.Dispose();
                }
            }
            return bitmap;
        }
    }
}
