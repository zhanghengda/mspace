using Microsoft.Win32;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule.FaceDetection
{
    class FaceDetectionProcess
    {

        public string StreamUrl { get; set; }
        public string ProcessPythonFile { get; set; }

        public void DetectionStart()
        {
            char[] spliter = { '\r' };
            string progToRun = ProcessPythonFile;
            Process proc = new Process();
            proc.StartInfo.FileName = "python.exe";
            //proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            //proc.StartInfo.CreateNoWindow = true;

            if (!string.IsNullOrEmpty(StreamUrl))
                proc.StartInfo.Arguments = string.Concat(progToRun, " ", 0);
            else
                proc.StartInfo.Arguments = string.Concat(progToRun, " ", StreamUrl.ToString());
            proc.Start();
            proc.WaitForExit();
        }

        private string CmdPath = @"C:\Windows\System32\cmd.exe";

        /// <summary>
        /// 执行cmd命令
        /// 多命令请使用批处理命令连接符：
        /// <![CDATA[
        /// &:同时执行两个命令
        /// |:将上一个命令的输出,作为下一个命令的输入
        /// &&：当&&前的命令成功时,才执行&&后的命令
        /// ||：当||前的命令失败时,才执行||后的命令]]>
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="output"></param>
        public void RunCmd()
        {
            //cmd = cmd.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
            string output = "";
            string pythonCmd = "";
            string progToRun = ProcessPythonFile;
            var filePath = AppDomain.CurrentDomain.BaseDirectory + @"PyPlugIn\face-detection";

            if (string.IsNullOrEmpty(StreamUrl))
                pythonCmd = string.Concat("python ",progToRun, " ", 0 , " ", filePath);
            else
                pythonCmd = string.Concat("python ",progToRun, " ", StreamUrl.ToString() , " ", filePath);

            using (Process p = new Process())
            {
                p.StartInfo.FileName = CmdPath;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();

                p.StandardInput.WriteLine(pythonCmd);
                p.StandardInput.AutoFlush = true;

                output = p.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
                p.WaitForExit();
                p.Close();
            }
        }
    }
}
