using Microsoft.Win32;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IntelligentAnalysisModule.CharacterAnalysis
{
    public class CharacAnalyPythonProcess
    {
        public string ImputFile { get; set; }
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


        public void DoCalc()
        {
            DateTime start = DateTime.Now;
            string progToRun = ExcutePyFile;
            char[] spliter = { '\r' };

            Process proc = new Process();
            proc.StartInfo.FileName = "python.exe";
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            string inputfile = string.Empty;
            string outputfile = string.Empty;
            if(!string.IsNullOrEmpty(ImputFile))
                inputfile = ImputFile.Replace(' ', '?');
            if(!string.IsNullOrEmpty(OutputFile))
                outputfile = OutputFile.Replace(' ', '?');

            //文件路径+参数集合
            if (string.IsNullOrEmpty(outputfile))
                proc.StartInfo.Arguments = string.Concat(progToRun, " ", inputfile.ToString());
            else
                proc.StartInfo.Arguments = string.Concat(progToRun, " ", inputfile.ToString(), " ", outputfile.ToString());
            proc.Start();

            StreamReader sReader = proc.StandardOutput;
            string[] output = sReader.ReadToEnd().Split(spliter);

            foreach (string s in output)
            {
                Console.WriteLine(s);
                SystemLog.Log(s);
            }

            proc.WaitForExit();

            DateTime end = DateTime.Now;

            //取出计算结果
            Console.WriteLine(output[0]);

            Console.WriteLine(end - start);

            Console.Read();

        }
    }
}
