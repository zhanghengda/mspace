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
    public class SimpleRenderer : FeatureRenderer
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public Symbol Symbol
        {
            get
            {
                return this.symbolField;
            }
            set
            {
                this.symbolField = value;
                base.RaisePropertyChanged("Symbol");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string Label
        {
            get
            {
                return this.labelField;
            }
            set
            {
                this.labelField = value;
                base.RaisePropertyChanged("Label");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
                base.RaisePropertyChanged("Description");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
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

        private Symbol symbolField;

        private string labelField;

        private string descriptionField;

        private string rotationFieldField;

        private esriRotationType rotationTypeField;

        private bool rotationTypeFieldSpecified;

        private string transparencyFieldField;
    }
}