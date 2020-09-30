using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(RasterLayerDrawingDescription))]
    [XmlInclude(typeof(FeatureLayerDrawingDescription))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class LayerDrawingDescription : INotifyPropertyChanged
    {
        // (add) Token: 0x0600048B RID: 1163 RVA: 0x0000B7E0 File Offset: 0x000099E0
        // (remove) Token: 0x0600048C RID: 1164 RVA: 0x0000B818 File Offset: 0x00009A18
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