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
    public class AlgorithmicColorRamp : ColorRamp
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
                base.RaisePropertyChanged("Algorithm");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public HsvColor FromColor
        {
            get
            {
                return this.fromColorField;
            }
            set
            {
                this.fromColorField = value;
                base.RaisePropertyChanged("FromColor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public HsvColor ToColor
        {
            get
            {
                return this.toColorField;
            }
            set
            {
                this.toColorField = value;
                base.RaisePropertyChanged("ToColor");
            }
        }

        private string algorithmField;

        private HsvColor fromColorField;

        private HsvColor toColorField;
    }
}