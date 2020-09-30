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
    public class PictureMarkerSymbol : CartographicMarkerSymbol
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "base64Binary", Order = 2)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
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

        private Color bgColorField;

        private Color bitmapTransColorField;

        private byte[] pictureField;

        private string pictureUriField;

        private double widthField;

        private bool widthFieldSpecified;

        private Color fgColorField;

        private bool swap1BitColorField;

        private bool swap1BitColorFieldSpecified;
    }
}