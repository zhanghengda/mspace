using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(RasterClassifyRenderer))]
    [XmlInclude(typeof(RasterStretchRenderer))]
    [XmlInclude(typeof(RasterRGBRenderer))]
    [XmlInclude(typeof(RasterUniqueValueRenderer))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class RasterRenderer : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public bool Indexed
        {
            get
            {
                return this.indexedField;
            }
            set
            {
                this.indexedField = value;
                this.RaisePropertyChanged("Indexed");
            }
        }

        [XmlIgnore]
        public bool IndexedSpecified
        {
            get
            {
                return this.indexedFieldSpecified;
            }
            set
            {
                this.indexedFieldSpecified = value;
                this.RaisePropertyChanged("IndexedSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public int Brightness
        {
            get
            {
                return this.brightnessField;
            }
            set
            {
                this.brightnessField = value;
                this.RaisePropertyChanged("Brightness");
            }
        }

        [XmlIgnore]
        public bool BrightnessSpecified
        {
            get
            {
                return this.brightnessFieldSpecified;
            }
            set
            {
                this.brightnessFieldSpecified = value;
                this.RaisePropertyChanged("BrightnessSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public int Contrast
        {
            get
            {
                return this.contrastField;
            }
            set
            {
                this.contrastField = value;
                this.RaisePropertyChanged("Contrast");
            }
        }

        [XmlIgnore]
        public bool ContrastSpecified
        {
            get
            {
                return this.contrastFieldSpecified;
            }
            set
            {
                this.contrastFieldSpecified = value;
                this.RaisePropertyChanged("ContrastSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public string ResamplingType
        {
            get
            {
                return this.resamplingTypeField;
            }
            set
            {
                this.resamplingTypeField = value;
                this.RaisePropertyChanged("ResamplingType");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public Color NoDataColor
        {
            get
            {
                return this.noDataColorField;
            }
            set
            {
                this.noDataColorField = value;
                this.RaisePropertyChanged("NoDataColor");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 5)]
        [XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public double[] NoDataValue
        {
            get
            {
                return this.noDataValueField;
            }
            set
            {
                this.noDataValueField = value;
                this.RaisePropertyChanged("NoDataValue");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public int AlphaBandIndex
        {
            get
            {
                return this.alphaBandIndexField;
            }
            set
            {
                this.alphaBandIndexField = value;
                this.RaisePropertyChanged("AlphaBandIndex");
            }
        }

        [XmlIgnore]
        public bool AlphaBandIndexSpecified
        {
            get
            {
                return this.alphaBandIndexFieldSpecified;
            }
            set
            {
                this.alphaBandIndexFieldSpecified = value;
                this.RaisePropertyChanged("AlphaBandIndexSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public bool UseAlphaBand
        {
            get
            {
                return this.useAlphaBandField;
            }
            set
            {
                this.useAlphaBandField = value;
                this.RaisePropertyChanged("UseAlphaBand");
            }
        }

        [XmlIgnore]
        public bool UseAlphaBandSpecified
        {
            get
            {
                return this.useAlphaBandFieldSpecified;
            }
            set
            {
                this.useAlphaBandFieldSpecified = value;
                this.RaisePropertyChanged("UseAlphaBandSpecified");
            }
        }

        // (add) Token: 0x060004B8 RID: 1208 RVA: 0x0000BC48 File Offset: 0x00009E48
        // (remove) Token: 0x060004B9 RID: 1209 RVA: 0x0000BC80 File Offset: 0x00009E80
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

        private bool indexedField;

        private bool indexedFieldSpecified;

        private int brightnessField;

        private bool brightnessFieldSpecified;

        private int contrastField;

        private bool contrastFieldSpecified;

        private string resamplingTypeField;

        private Color noDataColorField;

        private double[] noDataValueField;

        private int alphaBandIndexField;

        private bool alphaBandIndexFieldSpecified;

        private bool useAlphaBandField;

        private bool useAlphaBandFieldSpecified;
    }
}