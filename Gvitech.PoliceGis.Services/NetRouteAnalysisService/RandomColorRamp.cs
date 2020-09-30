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
    public class RandomColorRamp : ColorRamp
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int NumColors
        {
            get
            {
                return this.numColorsField;
            }
            set
            {
                this.numColorsField = value;
                base.RaisePropertyChanged("NumColors");
            }
        }

        [XmlIgnore]
        public bool NumColorsSpecified
        {
            get
            {
                return this.numColorsFieldSpecified;
            }
            set
            {
                this.numColorsFieldSpecified = value;
                base.RaisePropertyChanged("NumColorsSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public bool UseSeed
        {
            get
            {
                return this.useSeedField;
            }
            set
            {
                this.useSeedField = value;
                base.RaisePropertyChanged("UseSeed");
            }
        }

        [XmlIgnore]
        public bool UseSeedSpecified
        {
            get
            {
                return this.useSeedFieldSpecified;
            }
            set
            {
                this.useSeedFieldSpecified = value;
                base.RaisePropertyChanged("UseSeedSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public int Seed
        {
            get
            {
                return this.seedField;
            }
            set
            {
                this.seedField = value;
                base.RaisePropertyChanged("Seed");
            }
        }

        [XmlIgnore]
        public bool SeedSpecified
        {
            get
            {
                return this.seedFieldSpecified;
            }
            set
            {
                this.seedFieldSpecified = value;
                base.RaisePropertyChanged("SeedSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public short MinValue
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

        [XmlIgnore]
        public bool MinValueSpecified
        {
            get
            {
                return this.minValueFieldSpecified;
            }
            set
            {
                this.minValueFieldSpecified = value;
                base.RaisePropertyChanged("MinValueSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public short MaxValue
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

        [XmlIgnore]
        public bool MaxValueSpecified
        {
            get
            {
                return this.maxValueFieldSpecified;
            }
            set
            {
                this.maxValueFieldSpecified = value;
                base.RaisePropertyChanged("MaxValueSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public short MinSaturation
        {
            get
            {
                return this.minSaturationField;
            }
            set
            {
                this.minSaturationField = value;
                base.RaisePropertyChanged("MinSaturation");
            }
        }

        [XmlIgnore]
        public bool MinSaturationSpecified
        {
            get
            {
                return this.minSaturationFieldSpecified;
            }
            set
            {
                this.minSaturationFieldSpecified = value;
                base.RaisePropertyChanged("MinSaturationSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public short MaxSaturation
        {
            get
            {
                return this.maxSaturationField;
            }
            set
            {
                this.maxSaturationField = value;
                base.RaisePropertyChanged("MaxSaturation");
            }
        }

        [XmlIgnore]
        public bool MaxSaturationSpecified
        {
            get
            {
                return this.maxSaturationFieldSpecified;
            }
            set
            {
                this.maxSaturationFieldSpecified = value;
                base.RaisePropertyChanged("MaxSaturationSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public short StartHue
        {
            get
            {
                return this.startHueField;
            }
            set
            {
                this.startHueField = value;
                base.RaisePropertyChanged("StartHue");
            }
        }

        [XmlIgnore]
        public bool StartHueSpecified
        {
            get
            {
                return this.startHueFieldSpecified;
            }
            set
            {
                this.startHueFieldSpecified = value;
                base.RaisePropertyChanged("StartHueSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public short EndHue
        {
            get
            {
                return this.endHueField;
            }
            set
            {
                this.endHueField = value;
                base.RaisePropertyChanged("EndHue");
            }
        }

        [XmlIgnore]
        public bool EndHueSpecified
        {
            get
            {
                return this.endHueFieldSpecified;
            }
            set
            {
                this.endHueFieldSpecified = value;
                base.RaisePropertyChanged("EndHueSpecified");
            }
        }

        private int numColorsField;

        private bool numColorsFieldSpecified;

        private bool useSeedField;

        private bool useSeedFieldSpecified;

        private int seedField;

        private bool seedFieldSpecified;

        private short minValueField;

        private bool minValueFieldSpecified;

        private short maxValueField;

        private bool maxValueFieldSpecified;

        private short minSaturationField;

        private bool minSaturationFieldSpecified;

        private short maxSaturationField;

        private bool maxSaturationFieldSpecified;

        private short startHueField;

        private bool startHueFieldSpecified;

        private short endHueField;

        private bool endHueFieldSpecified;
    }
}