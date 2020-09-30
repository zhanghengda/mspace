using Mmc.Mspace.IntelligentAnalysisModule.CharacterAnalysis;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RegularInspectionModule.ViewModels
{
    public class DomReclassify
    {
        public CharacAnalyPythonProcess PythonProcess { get; set; }

        public DomReclassify()
        {
            PythonProcess = new CharacAnalyPythonProcess();
        }
        public bool Analys()
        {
            try
            {
                var file = AppDomain.CurrentDomain.BaseDirectory + @"\PythonScript\DomReclassify.py";
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

    }
}
