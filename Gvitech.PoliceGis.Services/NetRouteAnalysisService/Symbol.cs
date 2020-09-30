using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(XMLBinarySymbol))]
    [XmlInclude(typeof(MarkerSymbol))]
    [XmlInclude(typeof(CartographicMarkerSymbol))]
    [XmlInclude(typeof(PictureMarkerSymbol))]
    [XmlInclude(typeof(CharacterMarkerSymbol))]
    [XmlInclude(typeof(SimpleMarkerSymbol))]
    [XmlInclude(typeof(LineSymbol))]
    [XmlInclude(typeof(SimpleLineSymbol))]
    [XmlInclude(typeof(FillSymbol))]
    [XmlInclude(typeof(XMLBinaryFillSymbol))]
    [XmlInclude(typeof(PictureFillSymbol))]
    [XmlInclude(typeof(SimpleFillSymbol))]
    [XmlInclude(typeof(TextSymbol))]
    [XmlInclude(typeof(SimpleTextSymbol))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class Symbol : INotifyPropertyChanged
    {
        // (add) Token: 0x0600033F RID: 831 RVA: 0x000097C4 File Offset: 0x000079C4
        // (remove) Token: 0x06000340 RID: 832 RVA: 0x000097FC File Offset: 0x000079FC
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