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
    public class CenterAndScale : MapArea
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public Point Center
        {
            get
            {
                return this.centerField;
            }
            set
            {
                this.centerField = value;
                base.RaisePropertyChanged("Center");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public double Scale
        {
            get
            {
                return this.scaleField;
            }
            set
            {
                this.scaleField = value;
                base.RaisePropertyChanged("Scale");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public double DPI
        {
            get
            {
                return this.dPIField;
            }
            set
            {
                this.dPIField = value;
                base.RaisePropertyChanged("DPI");
            }
        }

        [XmlIgnore]
        public bool DPISpecified
        {
            get
            {
                return this.dPIFieldSpecified;
            }
            set
            {
                this.dPIFieldSpecified = value;
                base.RaisePropertyChanged("DPISpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public int DevBottom
        {
            get
            {
                return this.devBottomField;
            }
            set
            {
                this.devBottomField = value;
                base.RaisePropertyChanged("DevBottom");
            }
        }

        [XmlIgnore]
        public bool DevBottomSpecified
        {
            get
            {
                return this.devBottomFieldSpecified;
            }
            set
            {
                this.devBottomFieldSpecified = value;
                base.RaisePropertyChanged("DevBottomSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public int DevLeft
        {
            get
            {
                return this.devLeftField;
            }
            set
            {
                this.devLeftField = value;
                base.RaisePropertyChanged("DevLeft");
            }
        }

        [XmlIgnore]
        public bool DevLeftSpecified
        {
            get
            {
                return this.devLeftFieldSpecified;
            }
            set
            {
                this.devLeftFieldSpecified = value;
                base.RaisePropertyChanged("DevLeftSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public int DevTop
        {
            get
            {
                return this.devTopField;
            }
            set
            {
                this.devTopField = value;
                base.RaisePropertyChanged("DevTop");
            }
        }

        [XmlIgnore]
        public bool DevTopSpecified
        {
            get
            {
                return this.devTopFieldSpecified;
            }
            set
            {
                this.devTopFieldSpecified = value;
                base.RaisePropertyChanged("DevTopSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public int DevRight
        {
            get
            {
                return this.devRightField;
            }
            set
            {
                this.devRightField = value;
                base.RaisePropertyChanged("DevRight");
            }
        }

        [XmlIgnore]
        public bool DevRightSpecified
        {
            get
            {
                return this.devRightFieldSpecified;
            }
            set
            {
                this.devRightFieldSpecified = value;
                base.RaisePropertyChanged("DevRightSpecified");
            }
        }

        private Point centerField;

        private double scaleField;

        private double dPIField;

        private bool dPIFieldSpecified;

        private int devBottomField;

        private bool devBottomFieldSpecified;

        private int devLeftField;

        private bool devLeftFieldSpecified;

        private int devTopField;

        private bool devTopFieldSpecified;

        private int devRightField;

        private bool devRightFieldSpecified;
    }
}