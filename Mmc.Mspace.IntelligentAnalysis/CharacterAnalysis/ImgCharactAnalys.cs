using Mmc.Mspace.IntelligentAnalysisModule.CharacterAnalysis;
using Mmc.Windows.Services;
using System;

namespace Mmc.Mspace.IntelligentAnalysisModule
{
    public class ImgCharactAnalys
    {
        public ImgCharactAnalys()
        {
            PythonProcess = new CharacAnalyPythonProcess();
        }
        public bool Analys()
        {
            try
            {
                var file = AppDomain.CurrentDomain.BaseDirectory + @"\PythonScript\ImgCharacterAnalysis.py";
                PythonProcess.ExcutePyFile = "\"" + file + "\""; //处理空格路径
                PythonProcess.DoCalc();
                return true;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                return false;
            }

        }

        public CharacAnalyPythonProcess PythonProcess { get; set; }

    }



}
