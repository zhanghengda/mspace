using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(PolygonLabelPlacementDescription))]
    [XmlInclude(typeof(LineLabelPlacementDescription))]
    [XmlInclude(typeof(PointLabelPlacementDescription))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class LabelPlacementDescription : INotifyPropertyChanged
    {
        // (add) Token: 0x060006B2 RID: 1714 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
        // (remove) Token: 0x060006B3 RID: 1715 RVA: 0x0000EE00 File Offset: 0x0000D000
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            bool flag = propertyChanged != null;
            if (flag)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}