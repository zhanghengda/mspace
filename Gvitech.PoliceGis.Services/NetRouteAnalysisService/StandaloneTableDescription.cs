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
    public class StandaloneTableDescription : MapTableDescription
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                base.RaisePropertyChanged("ID");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string DefinitionExpression
        {
            get
            {
                return this.definitionExpressionField;
            }
            set
            {
                this.definitionExpressionField = value;
                base.RaisePropertyChanged("DefinitionExpression");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public string SourceID
        {
            get
            {
                return this.sourceIDField;
            }
            set
            {
                this.sourceIDField = value;
                base.RaisePropertyChanged("SourceID");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public bool UseTime
        {
            get
            {
                return this.useTimeField;
            }
            set
            {
                this.useTimeField = value;
                base.RaisePropertyChanged("UseTime");
            }
        }

        [XmlIgnore]
        public bool UseTimeSpecified
        {
            get
            {
                return this.useTimeFieldSpecified;
            }
            set
            {
                this.useTimeFieldSpecified = value;
                base.RaisePropertyChanged("UseTimeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public bool TimeDataCumulative
        {
            get
            {
                return this.timeDataCumulativeField;
            }
            set
            {
                this.timeDataCumulativeField = value;
                base.RaisePropertyChanged("TimeDataCumulative");
            }
        }

        [XmlIgnore]
        public bool TimeDataCumulativeSpecified
        {
            get
            {
                return this.timeDataCumulativeFieldSpecified;
            }
            set
            {
                this.timeDataCumulativeFieldSpecified = value;
                base.RaisePropertyChanged("TimeDataCumulativeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public double TimeOffset
        {
            get
            {
                return this.timeOffsetField;
            }
            set
            {
                this.timeOffsetField = value;
                base.RaisePropertyChanged("TimeOffset");
            }
        }

        [XmlIgnore]
        public bool TimeOffsetSpecified
        {
            get
            {
                return this.timeOffsetFieldSpecified;
            }
            set
            {
                this.timeOffsetFieldSpecified = value;
                base.RaisePropertyChanged("TimeOffsetSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public esriTimeUnits TimeOffsetUnits
        {
            get
            {
                return this.timeOffsetUnitsField;
            }
            set
            {
                this.timeOffsetUnitsField = value;
                base.RaisePropertyChanged("TimeOffsetUnits");
            }
        }

        [XmlIgnore]
        public bool TimeOffsetUnitsSpecified
        {
            get
            {
                return this.timeOffsetUnitsFieldSpecified;
            }
            set
            {
                this.timeOffsetUnitsFieldSpecified = value;
                base.RaisePropertyChanged("TimeOffsetUnitsSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public MapServerSourceDescription Source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
                base.RaisePropertyChanged("Source");
            }
        }

        private int idField;

        private string definitionExpressionField;

        private string sourceIDField;

        private bool useTimeField;

        private bool useTimeFieldSpecified;

        private bool timeDataCumulativeField;

        private bool timeDataCumulativeFieldSpecified;

        private double timeOffsetField;

        private bool timeOffsetFieldSpecified;

        private esriTimeUnits timeOffsetUnitsField;

        private bool timeOffsetUnitsFieldSpecified;

        private MapServerSourceDescription sourceField;
    }
}