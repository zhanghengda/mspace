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
    public class RangeDomain : Domain
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public object MaxValue
        {
            get
            {
                return this.maxValueField;
            }
            set
            {
                this.maxValueField = value;
                base.RaisePropertyChanged("MaxValue");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public object MinValue
        {
            get
            {
                return this.minValueField;
            }
            set
            {
                this.minValueField = value;
                base.RaisePropertyChanged("MinValue");
            }
        }

        private object maxValueField;

        private object minValueField;
    }
}