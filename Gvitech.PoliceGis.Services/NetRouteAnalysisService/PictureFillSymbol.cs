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
    public class PictureFillSymbol : FillSymbol
    {
        public PictureFillSymbol()
        {
            this.xScaleField = 1.0;
            this.yScaleField = 1.0;
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "base64Binary", Order = 0)]
        public byte[] Picture
        {
            get
            {
                return this.pictureField;
            }
            set
            {
                this.pictureField = value;
                base.RaisePropertyChanged("Picture");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string PictureUri
        {
            get
            {
                return this.pictureUriField;
            }
            set
            {
                this.pictureUriField = value;
                base.RaisePropertyChanged("PictureUri");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public double Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
                base.RaisePropertyChanged("Width");
            }
        }

        [XmlIgnore]
        public bool WidthSpecified
        {
            get
            {
                return this.widthFieldSpecified;
            }
            set
            {
                this.widthFieldSpecified = value;
                base.RaisePropertyChanged("WidthSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public double Height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
                base.RaisePropertyChanged("Height");
            }
        }

        [XmlIgnore]
        public bool HeightSpecified
        {
            get
            {
                return this.heightFieldSpecified;
            }
            set
            {
                this.heightFieldSpecified = value;
                base.RaisePropertyChanged("HeightSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public Color BgColor
        {
            get
            {
                return this.bgColorField;
            }
            set
            {
                this.bgColorField = value;
                base.RaisePropertyChanged("BgColor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public Color FgColor
        {
            get
            {
                return this.fgColorField;
            }
            set
            {
                this.fgColorField = value;
                base.RaisePropertyChanged("FgColor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public Color BitmapTransColor
        {
            get
            {
                return this.bitmapTransColorField;
            }
            set
            {
                this.bitmapTransColorField = value;
                base.RaisePropertyChanged("BitmapTransColor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public double XSeparation
        {
            get
            {
                return this.xSeparationField;
            }
            set
            {
                this.xSeparationField = value;
                base.RaisePropertyChanged("XSeparation");
            }
        }

        [XmlIgnore]
        public bool XSeparationSpecified
        {
            get
            {
                return this.xSeparationFieldSpecified;
            }
            set
            {
                this.xSeparationFieldSpecified = value;
                base.RaisePropertyChanged("XSeparationSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public double YSeparation
        {
            get
            {
                return this.ySeparationField;
            }
            set
            {
                this.ySeparationField = value;
                base.RaisePropertyChanged("YSeparation");
            }
        }

        [XmlIgnore]
        public bool YSeparationSpecified
        {
            get
            {
                return this.ySeparationFieldSpecified;
            }
            set
            {
                this.ySeparationFieldSpecified = value;
                base.RaisePropertyChanged("YSeparationSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public bool Swap1BitColor
        {
            get
            {
                return this.swap1BitColorField;
            }
            set
            {
                this.swap1BitColorField = value;
                base.RaisePropertyChanged("Swap1BitColor");
            }
        }

        [XmlIgnore]
        public bool Swap1BitColorSpecified
        {
            get
            {
                return this.swap1BitColorFieldSpecified;
            }
            set
            {
                this.swap1BitColorFieldSpecified = value;
                base.RaisePropertyChanged("Swap1BitColorSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public double Angle
        {
            get
            {
                return this.angleField;
            }
            set
            {
                this.angleField = value;
                base.RaisePropertyChanged("Angle");
            }
        }

        [XmlIgnore]
        public bool AngleSpecified
        {
            get
            {
                return this.angleFieldSpecified;
            }
            set
            {
                this.angleFieldSpecified = value;
                base.RaisePropertyChanged("AngleSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public double XOffset
        {
            get
            {
                return this.xOffsetField;
            }
            set
            {
                this.xOffsetField = value;
                base.RaisePropertyChanged("XOffset");
            }
        }

        [XmlIgnore]
        public bool XOffsetSpecified
        {
            get
            {
                return this.xOffsetFieldSpecified;
            }
            set
            {
                this.xOffsetFieldSpecified = value;
                base.RaisePropertyChanged("XOffsetSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public double YOffset
        {
            get
            {
                return this.yOffsetField;
            }
            set
            {
                this.yOffsetField = value;
                base.RaisePropertyChanged("YOffset");
            }
        }

        [XmlIgnore]
        public bool YOffsetSpecified
        {
            get
            {
                return this.yOffsetFieldSpecified;
            }
            set
            {
                this.yOffsetFieldSpecified = value;
                base.RaisePropertyChanged("YOffsetSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        [DefaultValue(1.0)]
        public double XScale
        {
            get
            {
                return this.xScaleField;
            }
            set
            {
                this.xScaleField = value;
                base.RaisePropertyChanged("XScale");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
        [DefaultValue(1.0)]
        public double YScale
        {
            get
            {
                return this.yScaleField;
            }
            set
            {
                this.yScaleField = value;
                base.RaisePropertyChanged("YScale");
            }
        }

        private byte[] pictureField;

        private string pictureUriField;

        private double widthField;

        private bool widthFieldSpecified;

        private double heightField;

        private bool heightFieldSpecified;

        private Color bgColorField;

        private Color fgColorField;

        private Color bitmapTransColorField;

        private double xSeparationField;

        private bool xSeparationFieldSpecified;

        private double ySeparationField;

        private bool ySeparationFieldSpecified;

        private bool swap1BitColorField;

        private bool swap1BitColorFieldSpecified;

        private double angleField;

        private bool angleFieldSpecified;

        private double xOffsetField;

        private bool xOffsetFieldSpecified;

        private double yOffsetField;

        private bool yOffsetFieldSpecified;

        private double xScaleField;

        private double yScaleField;
    }
}