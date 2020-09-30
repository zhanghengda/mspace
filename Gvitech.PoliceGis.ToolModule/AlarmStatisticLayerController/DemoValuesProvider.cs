using DevExpress.Xpf.Charts;
using System.Collections.Generic;

namespace Mmc.Mspace.ToolModule.AlarmStatisticLayerController
{
    public class DemoValuesProvider
    {
        public IEnumerable<Pie2DKind> PredefinedPie2DKinds
        {
            get
            {
                return Pie2DModel.GetPredefinedKinds();
            }
        }

        public IEnumerable<Bar2DKind> PredefinedBar2DKinds
        {
            get
            {
                return Bar2DModel.GetPredefinedKinds();
            }
        }
    }
}