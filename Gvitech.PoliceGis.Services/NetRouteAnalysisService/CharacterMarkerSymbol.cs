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
    public class CharacterMarkerSymbol : CartographicMarkerSymbol
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int CharacterIndex
        {
            get
            {
                return this.characterIndexField;
            }
            set
            {
                this.characterIndexField = value;
                base.RaisePropertyChanged("CharacterIndex");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string FontName
        {
            get
            {
                return this.fontNameField;
            }
            set
            {
                this.fontNameField = value;
                base.RaisePropertyChanged("FontName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public bool FontItalic
        {
            get
            {
                return this.fontItalicField;
            }
            set
            {
                this.fontItalicField = value;
                base.RaisePropertyChanged("FontItalic");
            }
        }

        [XmlIgnore]
        public bool FontItalicSpecified
        {
            get
            {
                return this.fontItalicFieldSpecified;
            }
            set
            {
                this.fontItalicFieldSpecified = value;
                base.RaisePropertyChanged("FontItalicSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public bool FontUnderline
        {
            get
            {
                return this.fontUnderlineField;
            }
            set
            {
                this.fontUnderlineField = value;
                base.RaisePropertyChanged("FontUnderline");
            }
        }

        [XmlIgnore]
        public bool FontUnderlineSpecified
        {
            get
            {
                return this.fontUnderlineFieldSpecified;
            }
            set
            {
                this.fontUnderlineFieldSpecified = value;
                base.RaisePropertyChanged("FontUnderlineSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public bool FontStrikethrough
        {
            get
            {
                return this.fontStrikethroughField;
            }
            set
            {
                this.fontStrikethroughField = value;
                base.RaisePropertyChanged("FontStrikethrough");
            }
        }

        [XmlIgnore]
        public bool FontStrikethroughSpecified
        {
            get
            {
                return this.fontStrikethroughFieldSpecified;
            }
            set
            {
                this.fontStrikethroughFieldSpecified = value;
                base.RaisePropertyChanged("FontStrikethroughSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public int FontWeight
        {
            get
            {
                return this.fontWeightField;
            }
            set
            {
                this.fontWeightField = value;
                base.RaisePropertyChanged("FontWeight");
            }
        }

        [XmlIgnore]
        public bool FontWeightSpecified
        {
            get
            {
                return this.fontWeightFieldSpecified;
            }
            set
            {
                this.fontWeightFieldSpecified = value;
                base.RaisePropertyChanged("FontWeightSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public int FontCharset
        {
            get
            {
                return this.fontCharsetField;
            }
            set
            {
                this.fontCharsetField = value;
                base.RaisePropertyChanged("FontCharset");
            }
        }

        [XmlIgnore]
        public bool FontCharsetSpecified
        {
            get
            {
                return this.fontCharsetFieldSpecified;
            }
            set
            {
                this.fontCharsetFieldSpecified = value;
                base.RaisePropertyChanged("FontCharsetSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public int FontSizeHi
        {
            get
            {
                return this.fontSizeHiField;
            }
            set
            {
                this.fontSizeHiField = value;
                base.RaisePropertyChanged("FontSizeHi");
            }
        }

        [XmlIgnore]
        public bool FontSizeHiSpecified
        {
            get
            {
                return this.fontSizeHiFieldSpecified;
            }
            set
            {
                this.fontSizeHiFieldSpecified = value;
                base.RaisePropertyChanged("FontSizeHiSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public int FontSizeLo
        {
            get
            {
                return this.fontSizeLoField;
            }
            set
            {
                this.fontSizeLoField = value;
                base.RaisePropertyChanged("FontSizeLo");
            }
        }

        [XmlIgnore]
        public bool FontSizeLoSpecified
        {
            get
            {
                return this.fontSizeLoFieldSpecified;
            }
            set
            {
                this.fontSizeLoFieldSpecified = value;
                base.RaisePropertyChanged("FontSizeLoSpecified");
            }
        }

        private int characterIndexField;

        private string fontNameField;

        private bool fontItalicField;

        private bool fontItalicFieldSpecified;

        private bool fontUnderlineField;

        private bool fontUnderlineFieldSpecified;

        private bool fontStrikethroughField;

        private bool fontStrikethroughFieldSpecified;

        private int fontWeightField;

        private bool fontWeightFieldSpecified;

        private int fontCharsetField;

        private bool fontCharsetFieldSpecified;

        private int fontSizeHiField;

        private bool fontSizeHiFieldSpecified;

        private int fontSizeLoField;

        private bool fontSizeLoFieldSpecified;
    }
}