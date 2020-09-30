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
    public class NumericFormat : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public esriRoundingOptionEnum RoundingOption
        {
            get
            {
                return this.roundingOptionField;
            }
            set
            {
                this.roundingOptionField = value;
                this.RaisePropertyChanged("RoundingOption");
            }
        }

        [XmlIgnore]
        public bool RoundingOptionSpecified
        {
            get
            {
                return this.roundingOptionFieldSpecified;
            }
            set
            {
                this.roundingOptionFieldSpecified = value;
                this.RaisePropertyChanged("RoundingOptionSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public int RoundingValue
        {
            get
            {
                return this.roundingValueField;
            }
            set
            {
                this.roundingValueField = value;
                this.RaisePropertyChanged("RoundingValue");
            }
        }

        [XmlIgnore]
        public bool RoundingValueSpecified
        {
            get
            {
                return this.roundingValueFieldSpecified;
            }
            set
            {
                this.roundingValueFieldSpecified = value;
                this.RaisePropertyChanged("RoundingValueSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public esriNumericAlignmentEnum AlignmentOption
        {
            get
            {
                return this.alignmentOptionField;
            }
            set
            {
                this.alignmentOptionField = value;
                this.RaisePropertyChanged("AlignmentOption");
            }
        }

        [XmlIgnore]
        public bool AlignmentOptionSpecified
        {
            get
            {
                return this.alignmentOptionFieldSpecified;
            }
            set
            {
                this.alignmentOptionFieldSpecified = value;
                this.RaisePropertyChanged("AlignmentOptionSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public int AlignmentWidth
        {
            get
            {
                return this.alignmentWidthField;
            }
            set
            {
                this.alignmentWidthField = value;
                this.RaisePropertyChanged("AlignmentWidth");
            }
        }

        [XmlIgnore]
        public bool AlignmentWidthSpecified
        {
            get
            {
                return this.alignmentWidthFieldSpecified;
            }
            set
            {
                this.alignmentWidthFieldSpecified = value;
                this.RaisePropertyChanged("AlignmentWidthSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public bool UseSeparator
        {
            get
            {
                return this.useSeparatorField;
            }
            set
            {
                this.useSeparatorField = value;
                this.RaisePropertyChanged("UseSeparator");
            }
        }

        [XmlIgnore]
        public bool UseSeparatorSpecified
        {
            get
            {
                return this.useSeparatorFieldSpecified;
            }
            set
            {
                this.useSeparatorFieldSpecified = value;
                this.RaisePropertyChanged("UseSeparatorSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public bool ZeroPad
        {
            get
            {
                return this.zeroPadField;
            }
            set
            {
                this.zeroPadField = value;
                this.RaisePropertyChanged("ZeroPad");
            }
        }

        [XmlIgnore]
        public bool ZeroPadSpecified
        {
            get
            {
                return this.zeroPadFieldSpecified;
            }
            set
            {
                this.zeroPadFieldSpecified = value;
                this.RaisePropertyChanged("ZeroPadSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public bool ShowPlus
        {
            get
            {
                return this.showPlusField;
            }
            set
            {
                this.showPlusField = value;
                this.RaisePropertyChanged("ShowPlus");
            }
        }

        [XmlIgnore]
        public bool ShowPlusSpecified
        {
            get
            {
                return this.showPlusFieldSpecified;
            }
            set
            {
                this.showPlusFieldSpecified = value;
                this.RaisePropertyChanged("ShowPlusSpecified");
            }
        }

        // (add) Token: 0x06000549 RID: 1353 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
        // (remove) Token: 0x0600054A RID: 1354 RVA: 0x0000CB20 File Offset: 0x0000AD20
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            bool flag = propertyChanged != null;
            if (flag)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private esriRoundingOptionEnum roundingOptionField;

        private bool roundingOptionFieldSpecified;

        private int roundingValueField;

        private bool roundingValueFieldSpecified;

        private esriNumericAlignmentEnum alignmentOptionField;

        private bool alignmentOptionFieldSpecified;

        private int alignmentWidthField;

        private bool alignmentWidthFieldSpecified;

        private bool useSeparatorField;

        private bool useSeparatorFieldSpecified;

        private bool zeroPadField;

        private bool zeroPadFieldSpecified;

        private bool showPlusField;

        private bool showPlusFieldSpecified;
    }
}