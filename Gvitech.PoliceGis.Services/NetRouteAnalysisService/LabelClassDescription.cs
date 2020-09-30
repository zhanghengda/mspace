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
    public class LabelClassDescription : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public LabelPlacementDescription LabelPlacementDescription
        {
            get
            {
                return this.labelPlacementDescriptionField;
            }
            set
            {
                this.labelPlacementDescriptionField = value;
                this.RaisePropertyChanged("LabelPlacementDescription");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string LabelExpression
        {
            get
            {
                return this.labelExpressionField;
            }
            set
            {
                this.labelExpressionField = value;
                this.RaisePropertyChanged("LabelExpression");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public SimpleTextSymbol Symbol
        {
            get
            {
                return this.symbolField;
            }
            set
            {
                this.symbolField = value;
                this.RaisePropertyChanged("Symbol");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public bool UseCodedValue
        {
            get
            {
                return this.useCodedValueField;
            }
            set
            {
                this.useCodedValueField = value;
                this.RaisePropertyChanged("UseCodedValue");
            }
        }

        [XmlIgnore]
        public bool UseCodedValueSpecified
        {
            get
            {
                return this.useCodedValueFieldSpecified;
            }
            set
            {
                this.useCodedValueFieldSpecified = value;
                this.RaisePropertyChanged("UseCodedValueSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public double MaximumScale
        {
            get
            {
                return this.maximumScaleField;
            }
            set
            {
                this.maximumScaleField = value;
                this.RaisePropertyChanged("MaximumScale");
            }
        }

        [XmlIgnore]
        public bool MaximumScaleSpecified
        {
            get
            {
                return this.maximumScaleFieldSpecified;
            }
            set
            {
                this.maximumScaleFieldSpecified = value;
                this.RaisePropertyChanged("MaximumScaleSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public double MinimumScale
        {
            get
            {
                return this.minimumScaleField;
            }
            set
            {
                this.minimumScaleField = value;
                this.RaisePropertyChanged("MinimumScale");
            }
        }

        [XmlIgnore]
        public bool MinimumScaleSpecified
        {
            get
            {
                return this.minimumScaleFieldSpecified;
            }
            set
            {
                this.minimumScaleFieldSpecified = value;
                this.RaisePropertyChanged("MinimumScaleSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public esriLabelExpressionType LabelExpressionType
        {
            get
            {
                return this.labelExpressionTypeField;
            }
            set
            {
                this.labelExpressionTypeField = value;
                this.RaisePropertyChanged("LabelExpressionType");
            }
        }

        [XmlIgnore]
        public bool LabelExpressionTypeSpecified
        {
            get
            {
                return this.labelExpressionTypeFieldSpecified;
            }
            set
            {
                this.labelExpressionTypeFieldSpecified = value;
                this.RaisePropertyChanged("LabelExpressionTypeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public string WhereClause
        {
            get
            {
                return this.whereClauseField;
            }
            set
            {
                this.whereClauseField = value;
                this.RaisePropertyChanged("WhereClause");
            }
        }

        // (add) Token: 0x060006AE RID: 1710 RVA: 0x0000ED28 File Offset: 0x0000CF28
        // (remove) Token: 0x060006AF RID: 1711 RVA: 0x0000ED60 File Offset: 0x0000CF60
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

        private LabelPlacementDescription labelPlacementDescriptionField;

        private string labelExpressionField;

        private SimpleTextSymbol symbolField;

        private bool useCodedValueField;

        private bool useCodedValueFieldSpecified;

        private double maximumScaleField;

        private bool maximumScaleFieldSpecified;

        private double minimumScaleField;

        private bool minimumScaleFieldSpecified;

        private esriLabelExpressionType labelExpressionTypeField;

        private bool labelExpressionTypeFieldSpecified;

        private string whereClauseField;
    }
}