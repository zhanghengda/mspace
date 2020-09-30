using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Mmc.Mspace.IntelligentAnalysisModule.CharacterAnalysis
{
    public class VideoAnalys
    {
        CharacAnalyPythonProcess pythonProcess = new CharacAnalyPythonProcess();

        public void OnChecked()
        {
            try
            {
                pythonProcess.FileFilter = FileFilterStrings.Video;
                var fileName = pythonProcess.GetOpenFile();
                if (string.IsNullOrEmpty(fileName))
                    return;
                //if (this.HasChinese(fileName))
                //{
                //    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NotsupportChinesePath"));
                //    return;
                //}
                pythonProcess.ImputFile = fileName;
                var file = AppDomain.CurrentDomain.BaseDirectory + @"\PythonScript\VideoCharacterAnalysis.py";
                pythonProcess.ExcutePyFile = "\"" + file + "\""; //处理空格路径
                //pythonProcess.ExcutePyFile = AppDomain.CurrentDomain.BaseDirectory + @"\PythonScript\VideoCharacterAnalysis.py";
                ThreadStart threadStart = new ThreadStart(pythonProcess.DoCalc);
                Thread thread = new Thread(threadStart);
                thread.Start();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

        }
        public String OnChecked2(string address)
        {
            try
            {
                pythonProcess.FileFilter = FileFilterStrings.Video;
                var fileName = address;//pythonProcess.GetOpenFile();
                if (string.IsNullOrEmpty(fileName))
                    return "";
                //if (this.HasChinese(fileName))
                //{
                //    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NotsupportChinesePath"));
                //    return "";
                //}
                pythonProcess.ImputFile = address;// rtsp://:8554/
                var file = AppDomain.CurrentDomain.BaseDirectory + @"\PythonScript\VideoCharacterAnalysis.py";
                pythonProcess.ExcutePyFile = "\"" + file + "\""; //处理空格路径
                //pythonProcess.ExcutePyFile = AppDomain.CurrentDomain.BaseDirectory + @"\PythonScript\VideoCharacterAnalysis.py";
                ThreadStart threadStart = new ThreadStart(pythonProcess.DoCalc);
                Thread thread = new Thread(threadStart);
                thread.Start();

            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
            return "";
        }
        //private bool HasChinese(string str)
        //{
        //    return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        //}

    }
}
