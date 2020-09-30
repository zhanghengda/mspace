using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(ClassBreaksRenderer))]
    [XmlInclude(typeof(UniqueValueRenderer))]
    [XmlInclude(typeof(SimpleRenderer))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class FeatureRenderer : INotifyPropertyChanged
    {
        // (add) Token: 0x06000633 RID: 1587 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
        // (remove) Token: 0x06000634 RID: 1588 RVA: 0x0000E118 File Offset: 0x0000C318
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