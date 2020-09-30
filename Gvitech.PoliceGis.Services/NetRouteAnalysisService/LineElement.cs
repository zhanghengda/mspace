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
    public class LineElement : GraphicElement
    {
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
                base.RaisePropertyChanged("Name");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
                base.RaisePropertyChanged("Type");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public bool AutoTransform
        {
            get
            {
                return this.autoTransformField;
            }
            set
            {
                this.autoTransformField = value;
                base.RaisePropertyChanged("AutoTransform");
            }
        }

        [XmlIgnore]
        public bool AutoTransformSpecified
        {
            get
            {
                return this.autoTransformFieldSpecified;
            }
            set
            {
                this.autoTransformFieldSpecified = value;
                base.RaisePropertyChanged("AutoTransformSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public double ReferenceScale
        {
            get
            {
                return this.referenceScaleField;
            }
            set
            {
                this.referenceScaleField = value;
                base.RaisePropertyChanged("ReferenceScale");
            }
        }

        [XmlIgnore]
        public bool ReferenceScaleSpecified
        {
            get
            {
                return this.referenceScaleFieldSpecified;
            }
            set
            {
                this.referenceScaleFieldSpecified = value;
                base.RaisePropertyChanged("ReferenceScaleSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public Geometry Line
        {
            get
            {
                return this.lineField;
            }
            set
            {
                this.lineField = value;
                base.RaisePropertyChanged("Line");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public bool Locked
        {
            get
            {
                return this.lockedField;
            }
            set
            {
                this.lockedField = value;
                base.RaisePropertyChanged("Locked");
            }
        }

        [XmlIgnore]
        public bool LockedSpecified
        {
            get
            {
                return this.lockedFieldSpecified;
            }
            set
            {
                this.lockedFieldSpecified = value;
                base.RaisePropertyChanged("LockedSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public bool FixedAspectRatio
        {
            get
            {
                return this.fixedAspectRatioField;
            }
            set
            {
                this.fixedAspectRatioField = value;
                base.RaisePropertyChanged("FixedAspectRatio");
            }
        }

        [XmlIgnore]
        public bool FixedAspectRatioSpecified
        {
            get
            {
                return this.fixedAspectRatioFieldSpecified;
            }
            set
            {
                this.fixedAspectRatioFieldSpecified = value;
                base.RaisePropertyChanged("FixedAspectRatioSpecified");
            }
        }

        private string nameField;

        private string typeField;

        private bool autoTransformField;

        private bool autoTransformFieldSpecified;

        private double referenceScaleField;

        private bool referenceScaleFieldSpecified;

        private Symbol symbolField;

        private Geometry lineField;

        private bool lockedField;

        private bool lockedFieldSpecified;

        private bool fixedAspectRatioField;

        private bool fixedAspectRatioFieldSpecified;
    }
}