using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public class PolylineB : Polyline
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "base64Binary", Order = 0)]
        public byte[] Bytes
        {
            get
            {
                return this.bytesField;
            }
            set
            {
                this.bytesField = value;
                base.RaisePropertyChanged("Bytes");
            }
        }

        private byte[] bytesField;
    }
}