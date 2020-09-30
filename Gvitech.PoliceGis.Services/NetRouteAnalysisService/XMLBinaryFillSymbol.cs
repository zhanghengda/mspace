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
    public class XMLBinaryFillSymbol : FillSymbol
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public XMLPersistedObject Data
        {
            get
            {
                return this.dataField;
            }
            set
            {
                this.dataField = value;
                base.RaisePropertyChanged("Data");
            }
        }

        private XMLPersistedObject dataField;
    }
}