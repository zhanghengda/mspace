using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(BitMaskCodedValueDomain))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public class CodedValueDomain : Domain
    {
        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 0)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public CodedValue[] CodedValues
        {
            get
            {
                return this.codedValuesField;
            }
            set
            {
                this.codedValuesField = value;
                base.RaisePropertyChanged("CodedValues");
            }
        }

        private CodedValue[] codedValuesField;
    }
}