using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IntelligentAnalysisModule.CharacterAnalysis
{
    public class CharacAnalysModel
    {
        public string Name { get; set; }
        public CharacAnalysType AnalysType { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }

    public enum CharacAnalysType
    {
        /// <summary>
        /// 蓝顶棚
        /// </summary>
        BlueRoot = 0,
        /// <summary>
        /// 绝缘子
        /// </summary>
        insulator
    }
}
