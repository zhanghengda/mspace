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
    public class TimeExtent : TimeValue
    {
        public TimeExtent()
        {
            this.emptyField = false;
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public DateTime StartTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
                base.RaisePropertyChanged("StartTime");
            }
        }

        [XmlIgnore]
        public bool StartTimeSpecified
        {
            get
            {
                return this.startTimeFieldSpecified;
            }
            set
            {
                this.startTimeFieldSpecified = value;
                base.RaisePropertyChanged("StartTimeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public DateTime EndTime
        {
            get
            {
                return this.endTimeField;
            }
            set
            {
                this.endTimeField = value;
                base.RaisePropertyChanged("EndTime");
            }
        }

        [XmlIgnore]
        public bool EndTimeSpecified
        {
            get
            {
                return this.endTimeFieldSpecified;
            }
            set
            {
                this.endTimeFieldSpecified = value;
                base.RaisePropertyChanged("EndTimeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        [DefaultValue(false)]
        public bool Empty
        {
            get
            {
                return this.emptyField;
            }
            set
            {
                this.emptyField = value;
                base.RaisePropertyChanged("Empty");
            }
        }

        private DateTime startTimeField;

        private bool startTimeFieldSpecified;

        private DateTime endTimeField;

        private bool endTimeFieldSpecified;

        private bool emptyField;
    }
}