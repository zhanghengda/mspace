using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(GroupElement))]
    [XmlInclude(typeof(GraphicElement))]
    [XmlInclude(typeof(TextElement))]
    [XmlInclude(typeof(RectangleElement))]
    [XmlInclude(typeof(PolygonElement))]
    [XmlInclude(typeof(ParagraphTextElement))]
    [XmlInclude(typeof(MarkerElement))]
    [XmlInclude(typeof(EllipseElement))]
    [XmlInclude(typeof(CircleElement))]
    [XmlInclude(typeof(LineElement))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class Element : INotifyPropertyChanged
    {
        // (add) Token: 0x06000707 RID: 1799 RVA: 0x0000F598 File Offset: 0x0000D798
        // (remove) Token: 0x06000708 RID: 1800 RVA: 0x0000F5D0 File Offset: 0x0000D7D0
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