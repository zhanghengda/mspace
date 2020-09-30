using Microsoft.Win32;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IntelligentAnalysisModule.DemCompare
{
    public class DsmSubStarct
    {
        public string ImputFile1 { get; set; }
        public string ImputFile2 { get; set; }
        public string OutputFile { get; set; }

        public string FileFilter { get; set; }

        public string ExcutePyFile { get; set; }


        public string GetOpenFile()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = FileFilter;
            if (openFile.ShowDialog() == true)
            {
                return openFile.FileName;
            }
            return string.Empty;
        }

        public string GetSaveFile()
        {
            SaveFileDialog openFile = new SaveFileDialog();
            openFile.Filter = FileFilter;
            if (openFile.ShowDialog() == true)
            {
                return openFile.FileName;
            }
            return string.Empty;
        }

        public bool Analys()
        {
            try
            {
                var file = AppDomain.CurrentDomain.BaseDirectory + @"\PythonScript\DsmSubstractAnalysis.py";
                ExcutePyFile = "\"" + file + "\"";//处理空格路径
                //ExcutePyFile = AppDomain.CurrentDomain.BaseDirectory + @"\PythonScript\DsmSubstractAnalysis2.py";
                DoCalc();
                return true;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                return false;
            }

        }

        public void DoCalc()
        {
            if (string.IsNullOrEmpty(ImputFile1) || string.IsNullOrEmpty(ImputFile2) || string.IsNullOrEmpty(OutputFile))
            {
                SystemLog.Log("参数不够");
                return;
            }                
            string progToRun = ExcutePyFile;
            char[] spliter = { '\r' };

            Process proc = new Process();
            proc.StartInfo.FileName = "python.exe";
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;

            var inputfile1 = ImputFile1.Replace(' ','?');
            var inputfile2 = ImputFile2.Replace(' ', '?');
            var outputfile = OutputFile.Replace(' ', '?');



            //文件路径+参数集合
            proc.StartInfo.Arguments = string.Concat(progToRun, " ", inputfile1.ToString(), " ", inputfile2.ToString(), " ", outputfile.ToString());
            proc.Start();

            StreamReader sReader = proc.StandardOutput;
            string[] output = sReader.ReadToEnd().Split(spliter);

            foreach (string s in output)
            {
                Console.WriteLine(s);
                SystemLog.Log(s);
            }

            proc.WaitForExit();

            //取出计算结果
            Console.WriteLine(output[0]);

            Console.Read();
        }

    }
}
