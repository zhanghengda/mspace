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
    public class TextSymbol : Symbol
    {
        public TextSymbol()
        {
            this.characterWidthField = 100.0;
            this.wordSpacingField = 100.0;
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
        public int BreakCharIndex
        {
            get
            {
                return this.breakCharIndexField;
            }
            set
            {
                this.breakCharIndexField = value;
                base.RaisePropertyChanged("BreakCharIndex");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public esriTextVerticalAlignment VerticalAlignment
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public esriTextHorizontalAlignment HorizontalAlignment
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public bool Clip
        {
            get
            {
                return this.clipField;
            }
            set
            {
                this.clipField = value;
                base.RaisePropertyChanged("Clip");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
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
        public Color ShadowColor
        {
            get
            {
                return this.shadowColorField;
            }
            set
            {
                this.shadowColorField = value;
                base.RaisePropertyChanged("ShadowColor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public double ShadowXOffset
        {
            get
            {
                return this.shadowXOffsetField;
            }
            set
            {
                this.shadowXOffsetField = value;
                base.RaisePropertyChanged("ShadowXOffset");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public double ShadowYOffset
        {
            get
            {
                return this.shadowYOffsetField;
            }
            set
            {
                this.shadowYOffsetField = value;
                base.RaisePropertyChanged("ShadowYOffset");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public esriTextPosition TextPosition
        {
            get
            {
                return this.textPositionField;
            }
            set
            {
                this.textPositionField = value;
                base.RaisePropertyChanged("TextPosition");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public esriTextCase TextCase
        {
            get
            {
                return this.textCaseField;
            }
            set
            {
                this.textCaseField = value;
                base.RaisePropertyChanged("TextCase");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
        public double CharacterSpacing
        {
            get
            {
                return this.characterSpacingField;
            }
            set
            {
                this.characterSpacingField = value;
                base.RaisePropertyChanged("CharacterSpacing");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 15)]
        public double CharacterWidth
        {
            get
            {
                return this.characterWidthField;
            }
            set
            {
                this.characterWidthField = value;
                base.RaisePropertyChanged("CharacterWidth");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 16)]
        public double WordSpacing
        {
            get
            {
                return this.wordSpacingField;
            }
            set
            {
                this.wordSpacingField = value;
                base.RaisePropertyChanged("WordSpacing");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 17)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 18)]
        public double Leading
        {
            get
            {
                return this.leadingField;
            }
            set
            {
                this.leadingField = value;
                base.RaisePropertyChanged("Leading");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 19)]
        public esriTextDirection TextDirection
        {
            get
            {
                return this.textDirectionField;
            }
            set
            {
                this.textDirectionField = value;
                base.RaisePropertyChanged("TextDirection");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 20)]
        public double FlipAngle
        {
            get
            {
                return this.flipAngleField;
            }
            set
            {
                this.flipAngleField = value;
                base.RaisePropertyChanged("FlipAngle");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 21)]
        public bool TypeSetting
        {
            get
            {
                return this.typeSettingField;
            }
            set
            {
                this.typeSettingField = value;
                base.RaisePropertyChanged("TypeSetting");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 22)]
        public string TextPathClass
        {
            get
            {
                return this.textPathClassField;
            }
            set
            {
                this.textPathClassField = value;
                base.RaisePropertyChanged("TextPathClass");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 23)]
        public Symbol FillSymbol
        {
            get
            {
                return this.fillSymbolField;
            }
            set
            {
                this.fillSymbolField = value;
                base.RaisePropertyChanged("FillSymbol");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 24)]
        public string Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
                base.RaisePropertyChanged("Text");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 25)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 26)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 27)]
        public double MaskSize
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 28)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 29)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 30)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 31)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 32)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 33)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 34)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 35)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 36)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 37)]
        public string TextParserClass
        {
            get
            {
                return this.textParserClassField;
            }
            set
            {
                this.textParserClassField = value;
                base.RaisePropertyChanged("TextParserClass");
            }
        }

        private Color colorField;

        private int breakCharIndexField;

        private esriTextVerticalAlignment verticalAlignmentField;

        private esriTextHorizontalAlignment horizontalAlignmentField;

        private bool clipField;

        private bool rightToLeftField;

        private double angleField;

        private double xOffsetField;

        private double yOffsetField;

        private Color shadowColorField;

        private double shadowXOffsetField;

        private double shadowYOffsetField;

        private esriTextPosition textPositionField;

        private esriTextCase textCaseField;

        private double characterSpacingField;

        private double characterWidthField;

        private double wordSpacingField;

        private bool kerningField;

        private double leadingField;

        private esriTextDirection textDirectionField;

        private double flipAngleField;

        private bool typeSettingField;

        private string textPathClassField;

        private Symbol fillSymbolField;

        private string textField;

        private double sizeField;

        private esriMaskStyle maskStyleField;

        private double maskSizeField;

        private Symbol maskSymbolField;

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

        private string textParserClassField;
    }
}