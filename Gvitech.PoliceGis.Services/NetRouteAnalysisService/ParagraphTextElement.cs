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
    public class ParagraphTextElement : GraphicElement
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public double Scale
        {
            get
            {
                return this.scaleField;
            }
            set
            {
                this.scaleField = value;
                base.RaisePropertyChanged("Scale");
            }
        }

        [XmlIgnore]
        public bool ScaleSpecified
        {
            get
            {
                return this.scaleFieldSpecified;
            }
            set
            {
                this.scaleFieldSpecified = value;
                base.RaisePropertyChanged("ScaleSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public Geometry TextGeometry
        {
            get
            {
                return this.textGeometryField;
            }
            set
            {
                this.textGeometryField = value;
                base.RaisePropertyChanged("TextGeometry");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public Border FrameBorder
        {
            get
            {
                return this.frameBorderField;
            }
            set
            {
                this.frameBorderField = value;
                base.RaisePropertyChanged("FrameBorder");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public Background FrameBackground
        {
            get
            {
                return this.frameBackgroundField;
            }
            set
            {
                this.frameBackgroundField = value;
                base.RaisePropertyChanged("FrameBackground");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public Shadow FrameShadow
        {
            get
            {
                return this.frameShadowField;
            }
            set
            {
                this.frameShadowField = value;
                base.RaisePropertyChanged("FrameShadow");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public double ColumnGap
        {
            get
            {
                return this.columnGapField;
            }
            set
            {
                this.columnGapField = value;
                base.RaisePropertyChanged("ColumnGap");
            }
        }

        [XmlIgnore]
        public bool ColumnGapSpecified
        {
            get
            {
                return this.columnGapFieldSpecified;
            }
            set
            {
                this.columnGapFieldSpecified = value;
                base.RaisePropertyChanged("ColumnGapSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public int ColumnCount
        {
            get
            {
                return this.columnCountField;
            }
            set
            {
                this.columnCountField = value;
                base.RaisePropertyChanged("ColumnCount");
            }
        }

        [XmlIgnore]
        public bool ColumnCountSpecified
        {
            get
            {
                return this.columnCountFieldSpecified;
            }
            set
            {
                this.columnCountFieldSpecified = value;
                base.RaisePropertyChanged("ColumnCountSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
        public double Margin
        {
            get
            {
                return this.marginField;
            }
            set
            {
                this.marginField = value;
                base.RaisePropertyChanged("Margin");
            }
        }

        [XmlIgnore]
        public bool MarginSpecified
        {
            get
            {
                return this.marginFieldSpecified;
            }
            set
            {
                this.marginFieldSpecified = value;
                base.RaisePropertyChanged("MarginSpecified");
            }
        }

        private string nameField;

        private string typeField;

        private bool autoTransformField;

        private bool autoTransformFieldSpecified;

        private double referenceScaleField;

        private bool referenceScaleFieldSpecified;

        private string textField;

        private double scaleField;

        private bool scaleFieldSpecified;

        private Symbol symbolField;

        private Geometry textGeometryField;

        private bool lockedField;

        private bool lockedFieldSpecified;

        private Border frameBorderField;

        private Background frameBackgroundField;

        private Shadow frameShadowField;

        private double columnGapField;

        private bool columnGapFieldSpecified;

        private int columnCountField;

        private bool columnCountFieldSpecified;

        private double marginField;

        private bool marginFieldSpecified;
    }
}