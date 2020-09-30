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
    public class SimpleTextSymbol : Symbol
    {
        public SimpleTextSymbol()
        {
            this.rightToLeftField = false;
            this.angleField = 0.0;
            this.xOffsetField = 0.0;
            this.yOffsetField = 0.0;
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public Color Color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
                base.RaisePropertyChanged("Color");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public Color BackgroundColor
        {
            get
            {
                return this.backgroundColorField;
            }
            set
            {
                this.backgroundColorField = value;
                base.RaisePropertyChanged("BackgroundColor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public Color OutlineColor
        {
            get
            {
                return this.outlineColorField;
            }
            set
            {
                this.outlineColorField = value;
                base.RaisePropertyChanged("OutlineColor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public esriSimpleTextVerticalAlignment VerticalAlignment
        {
            get
            {
                return this.verticalAlignmentField;
            }
            set
            {
                this.verticalAlignmentField = value;
                base.RaisePropertyChanged("VerticalAlignment");
            }
        }

        [XmlIgnore]
        public bool VerticalAlignmentSpecified
        {
            get
            {
                return this.verticalAlignmentFieldSpecified;
            }
            set
            {
                this.verticalAlignmentFieldSpecified = value;
                base.RaisePropertyChanged("VerticalAlignmentSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public esriSimpleTextHorizontalAlignment HorizontalAlignment
        {
            get
            {
                return this.horizontalAlignmentField;
            }
            set
            {
                this.horizontalAlignmentField = value;
                base.RaisePropertyChanged("HorizontalAlignment");
            }
        }

        [XmlIgnore]
        public bool HorizontalAlignmentSpecified
        {
            get
            {
                return this.horizontalAlignmentFieldSpecified;
            }
            set
            {
                this.horizontalAlignmentFieldSpecified = value;
                base.RaisePropertyChanged("HorizontalAlignmentSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        [DefaultValue(false)]
        public bool RightToLeft
        {
            get
            {
                return this.rightToLeftField;
            }
            set
            {
                this.rightToLeftField = value;
                base.RaisePropertyChanged("RightToLeft");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        [DefaultValue(0.0)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        [DefaultValue(0.0)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        [DefaultValue(0.0)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public double Size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
                base.RaisePropertyChanged("Size");
            }
        }

        [XmlIgnore]
        public bool SizeSpecified
        {
            get
            {
                return this.sizeFieldSpecified;
            }
            set
            {
                this.sizeFieldSpecified = value;
                base.RaisePropertyChanged("SizeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public string FontFamilyName
        {
            get
            {
                return this.fontFamilyNameField;
            }
            set
            {
                this.fontFamilyNameField = value;
                base.RaisePropertyChanged("FontFamilyName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public esriFontStyle FontStyle
        {
            get
            {
                return this.fontStyleField;
            }
            set
            {
                this.fontStyleField = value;
                base.RaisePropertyChanged("FontStyle");
            }
        }

        [XmlIgnore]
        public bool FontStyleSpecified
        {
            get
            {
                return this.fontStyleFieldSpecified;
            }
            set
            {
                this.fontStyleFieldSpecified = value;
                base.RaisePropertyChanged("FontStyleSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public esriFontWeight FontWeight
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public esriFontDecoration FontDecoration
        {
            get
            {
                return this.fontDecorationField;
            }
            set
            {
                this.fontDecorationField = value;
                base.RaisePropertyChanged("FontDecoration");
            }
        }

        [XmlIgnore]
        public bool FontDecorationSpecified
        {
            get
            {
                return this.fontDecorationFieldSpecified;
            }
            set
            {
                this.fontDecorationFieldSpecified = value;
                base.RaisePropertyChanged("FontDecorationSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
        public double OutlineWidth
        {
            get
            {
                return this.outlineWidthField;
            }
            set
            {
                this.outlineWidthField = value;
                base.RaisePropertyChanged("OutlineWidth");
            }
        }

        [XmlIgnore]
        public bool OutlineWidthSpecified
        {
            get
            {
                return this.outlineWidthFieldSpecified;
            }
            set
            {
                this.outlineWidthFieldSpecified = value;
                base.RaisePropertyChanged("OutlineWidthSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 15)]
        public bool Kerning
        {
            get
            {
                return this.kerningField;
            }
            set
            {
                this.kerningField = value;
                base.RaisePropertyChanged("Kerning");
            }
        }

        [XmlIgnore]
        public bool KerningSpecified
        {
            get
            {
                return this.kerningFieldSpecified;
            }
            set
            {
                this.kerningFieldSpecified = value;
                base.RaisePropertyChanged("KerningSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 16)]
        public esriMaskStyle MaskStyle
        {
            get
            {
                return this.maskStyleField;
            }
            set
            {
                this.maskStyleField = value;
                base.RaisePropertyChanged("MaskStyle");
            }
        }

        [XmlIgnore]
        public bool MaskStyleSpecified
        {
            get
            {
                return this.maskStyleFieldSpecified;
            }
            set
            {
                this.maskStyleFieldSpecified = value;
                base.RaisePropertyChanged("MaskStyleSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 17)]
        public int MaskSize
        {
            get
            {
                return this.maskSizeField;
            }
            set
            {
                this.maskSizeField = value;
                base.RaisePropertyChanged("MaskSize");
            }
        }

        [XmlIgnore]
        public bool MaskSizeSpecified
        {
            get
            {
                return this.maskSizeFieldSpecified;
            }
            set
            {
                this.maskSizeFieldSpecified = value;
                base.RaisePropertyChanged("MaskSizeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 18)]
        public Symbol MaskSymbol
        {
            get
            {
                return this.maskSymbolField;
            }
            set
            {
                this.maskSymbolField = value;
                base.RaisePropertyChanged("MaskSymbol");
            }
        }

        private Color colorField;

        private Color backgroundColorField;

        private Color outlineColorField;

        private esriSimpleTextVerticalAlignment verticalAlignmentField;

        private bool verticalAlignmentFieldSpecified;

        private esriSimpleTextHorizontalAlignment horizontalAlignmentField;

        private bool horizontalAlignmentFieldSpecified;

        private bool rightToLeftField;

        private double angleField;

        private double xOffsetField;

        private double yOffsetField;

        private double sizeField;

        private bool sizeFieldSpecified;

        private string fontFamilyNameField;

        private esriFontStyle fontStyleField;

        private bool fontStyleFieldSpecified;

        private esriFontWeight fontWeightField;

        private bool fontWeightFieldSpecified;

        private esriFontDecoration fontDecorationField;

        private bool fontDecorationFieldSpecified;

        private double outlineWidthField;

        private bool outlineWidthFieldSpecified;

        private bool kerningField;

        private bool kerningFieldSpecified;

        private esriMaskStyle maskStyleField;

        private bool maskStyleFieldSpecified;

        private int maskSizeField;

        private bool maskSizeFieldSpecified;

        private Symbol maskSymbolField;
    }
}