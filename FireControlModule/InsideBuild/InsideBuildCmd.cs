using Mmc.Mspace.Common.Commands;
using Mmc.Mspace.Theme.Pop;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace FireControlModule.InsideBuild
{
    public class InsideBuildCmd : BarCmd
    {
        private static Process p;

        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            var buildCode = parameter == null ? null : parameter.ToString();
            string strBuildCodes = "4403050080040500010|4403050080040500071|4403050080040500072|4403050080040500017";
            string[] buildCodes = strBuildCodes.Split("|");
            if (!buildCodes.Contains(buildCode))
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Indoor3dmodel"));
                return;
            }
            OpenInside3D(buildCode);
        }

        public static void OpenInside3D(string buildCode)
        {
            ChangeTxtBuildCode(buildCode);
            if (p == null)
            {
                string fileName = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\ZHNS_JG_12_8_1\1219_JG.exe";
                p = new Process();
                p.StartInfo.FileName = fileName;
                p.Start();
            }
            else
            {
                if (p.HasExited) //是否正在运行
                {
                    p.Start();
                }
            }
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        }

        public static void ChangeTxtBuildCode(string buildCode)
        {
            try
            {
                string txtFile = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\ZHNS_JG_12_8_1\1219_JG_Data\StreamingAssets\buildid.txt";
                //先清空
                FileStream stream = new FileStream(txtFile, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);
                //再写入
                StreamWriter sw = new StreamWriter(stream);
                sw.Write(buildCode);
                sw.Flush();
                sw.Close();
                stream.Close();
            }
            catch (Exception ex)
            {
                Mmc.Windows.Services.SystemLog.Log(ex);
            }
        }
    }
}