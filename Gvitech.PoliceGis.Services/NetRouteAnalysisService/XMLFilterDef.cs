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
    public class XMLFilterDef : FilterDef
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string FieldName
        {
            get
            {
                return this.fieldNameField;
            }
            set
            {
                this.fieldNameField = value;
                base.RaisePropertyChanged("FieldName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string Expression
        {
            get
            {
                return this.expressionField;
            }
            set
            {
                this.expressionField = value;
                base.RaisePropertyChanged("Expression");
            }
        }

        private string fieldNameField;

        private string expressionField;
    }
}