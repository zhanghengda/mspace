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
    public class UniqueValueRenderer : FeatureRenderer
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string Field1
        {
            get
            {
                return this.field1Field;
            }
            set
            {
                this.field1Field = value;
                base.RaisePropertyChanged("Field1");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string Field2
        {
            get
            {
                return this.field2Field;
            }
            set
            {
                this.field2Field = value;
                base.RaisePropertyChanged("Field2");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public string Field3
        {
            get
            {
                return this.field3Field;
            }
            set
            {
                this.field3Field = value;
                base.RaisePropertyChanged("Field3");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public string FieldDelimiter
        {
            get
            {
                return this.fieldDelimiterField;
            }
            set
            {
                this.fieldDelimiterField = value;
                base.RaisePropertyChanged("FieldDelimiter");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public Symbol DefaultSymbol
        {
            get
            {
                return this.defaultSymbolField;
            }
            set
            {
                this.defaultSymbolField = value;
                base.RaisePropertyChanged("DefaultSymbol");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public string DefaultLabel
        {
            get
            {
                return this.defaultLabelField;
            }
            set
            {
                this.defaultLabelField = value;
                base.RaisePropertyChanged("DefaultLabel");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 6)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public UniqueValueInfo[] UniqueValueInfos
        {
            get
            {
                return this.uniqueValueInfosField;
            }
            set
            {
                this.uniqueValueInfosField = value;
                base.RaisePropertyChanged("UniqueValueInfos");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public string RotationField
        {
            get
            {
                return this.rotationFieldField;
            }
            set
            {
                this.rotationFieldField = value;
                base.RaisePropertyChanged("RotationField");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public esriRotationType RotationType
        {
            get
            {
                return this.rotationTypeField;
            }
            set
            {
                this.rotationTypeField = value;
                base.RaisePropertyChanged("RotationType");
            }
        }

        [XmlIgnore]
        public bool RotationTypeSpecified
        {
            get
            {
                return this.rotationTypeFieldSpecified;
            }
            set
            {
                this.rotationTypeFieldSpecified = value;
                base.RaisePropertyChanged("RotationTypeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public string TransparencyField
        {
            get
            {
                return this.transparencyFieldField;
            }
            set
            {
                this.transparencyFieldField = value;
                base.RaisePropertyChanged("TransparencyField");
            }
        }

        private string field1Field;

        private string field2Field;

        private string field3Field;

        private string fieldDelimiterField;

        private Symbol defaultSymbolField;

        private string defaultLabelField;

        private UniqueValueInfo[] uniqueValueInfosField;

        private string rotationFieldField;

        private esriRotationType rotationTypeField;

        private bool rotationTypeFieldSpecified;

        private string transparencyFieldField;
    }
}