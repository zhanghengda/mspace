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
    public class Field : INotifyPropertyChanged
    {
        public Field()
        {
            this.editableField = true;
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("Name");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public esriFieldType Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
                this.RaisePropertyChanged("Type");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public bool IsNullable
        {
            get
            {
                return this.isNullableField;
            }
            set
            {
                this.isNullableField = value;
                this.RaisePropertyChanged("IsNullable");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public int Length
        {
            get
            {
                return this.lengthField;
            }
            set
            {
                this.lengthField = value;
                this.RaisePropertyChanged("Length");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public int Precision
        {
            get
            {
                return this.precisionField;
            }
            set
            {
                this.precisionField = value;
                this.RaisePropertyChanged("Precision");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public int Scale
        {
            get
            {
                return this.scaleField;
            }
            set
            {
                this.scaleField = value;
                this.RaisePropertyChanged("Scale");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public bool Required
        {
            get
            {
                return this.requiredField;
            }
            set
            {
                this.requiredField = value;
                this.RaisePropertyChanged("Required");
            }
        }

        [XmlIgnore]
        public bool RequiredSpecified
        {
            get
            {
                return this.requiredFieldSpecified;
            }
            set
            {
                this.requiredFieldSpecified = value;
                this.RaisePropertyChanged("RequiredSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        [DefaultValue(true)]
        public bool Editable
        {
            get
            {
                return this.editableField;
            }
            set
            {
                this.editableField = value;
                this.RaisePropertyChanged("Editable");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public bool DomainFixed
        {
            get
            {
                return this.domainFixedField;
            }
            set
            {
                this.domainFixedField = value;
                this.RaisePropertyChanged("DomainFixed");
            }
        }

        [XmlIgnore]
        public bool DomainFixedSpecified
        {
            get
            {
                return this.domainFixedFieldSpecified;
            }
            set
            {
                this.domainFixedFieldSpecified = value;
                this.RaisePropertyChanged("DomainFixedSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public GeometryDef GeometryDef
        {
            get
            {
                return this.geometryDefField;
            }
            set
            {
                this.geometryDefField = value;
                this.RaisePropertyChanged("GeometryDef");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public string AliasName
        {
            get
            {
                return this.aliasNameField;
            }
            set
            {
                this.aliasNameField = value;
                this.RaisePropertyChanged("AliasName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public string ModelName
        {
            get
            {
                return this.modelNameField;
            }
            set
            {
                this.modelNameField = value;
                this.RaisePropertyChanged("ModelName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public object DefaultValue
        {
            get
            {
                return this.defaultValueField;
            }
            set
            {
                this.defaultValueField = value;
                this.RaisePropertyChanged("DefaultValue");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public Domain Domain
        {
            get
            {
                return this.domainField;
            }
            set
            {
                this.domainField = value;
                this.RaisePropertyChanged("Domain");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
        public RasterDef RasterDef
        {
            get
            {
                return this.rasterDefField;
            }
            set
            {
                this.rasterDefField = value;
                this.RaisePropertyChanged("RasterDef");
            }
        }

        // (add) Token: 0x0600094D RID: 2381 RVA: 0x00013038 File Offset: 0x00011238
        // (remove) Token: 0x0600094E RID: 2382 RVA: 0x00013070 File Offset: 0x00011270
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

        private string nameField;

        private esriFieldType typeField;

        private bool isNullableField;

        private int lengthField;

        private int precisionField;

        private int scaleField;

        private bool requiredField;

        private bool requiredFieldSpecified;

        private bool editableField;

        private bool domainFixedField;

        private bool domainFixedFieldSpecified;

        private GeometryDef geometryDefField;

        private string aliasNameField;

        private string modelNameField;

        private object defaultValueField;

        private Domain domainField;

        private RasterDef rasterDefField;
    }
}