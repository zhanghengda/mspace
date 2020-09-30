using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(StandaloneTableDescription))]
    [XmlInclude(typeof(LayerDescription))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class MapTableDescription : INotifyPropertyChanged
    {
        // (add) Token: 0x060006E9 RID: 1769 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
        // (remove) Token: 0x060006EA RID: 1770 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
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