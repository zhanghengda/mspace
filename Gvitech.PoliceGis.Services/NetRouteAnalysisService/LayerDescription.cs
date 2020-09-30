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
    public class LayerDescription : MapTableDescription
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int LayerID
        {
            get
            {
                return this.layerIDField;
            }
            set
            {
                this.layerIDField = value;
                base.RaisePropertyChanged("LayerID");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public bool Visible
        {
            get
            {
                return this.visibleField;
            }
            set
            {
                this.visibleField = value;
                base.RaisePropertyChanged("Visible");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public bool ShowLabels
        {
            get
            {
                return this.showLabelsField;
            }
            set
            {
                this.showLabelsField = value;
                base.RaisePropertyChanged("ShowLabels");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public bool ScaleSymbols
        {
            get
            {
                return this.scaleSymbolsField;
            }
            set
            {
                this.scaleSymbolsField = value;
                base.RaisePropertyChanged("ScaleSymbols");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 4)]
        [XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public int[] SelectionFeatures
        {
            get
            {
                return this.selectionFeaturesField;
            }
            set
            {
                this.selectionFeaturesField = value;
                base.RaisePropertyChanged("SelectionFeatures");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public Color SelectionColor
        {
            get
            {
                return this.selectionColorField;
            }
            set
            {
                this.selectionColorField = value;
                base.RaisePropertyChanged("SelectionColor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public Symbol SelectionSymbol
        {
            get
            {
                return this.selectionSymbolField;
            }
            set
            {
                this.selectionSymbolField = value;
                base.RaisePropertyChanged("SelectionSymbol");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public bool SetSelectionSymbol
        {
            get
            {
                return this.setSelectionSymbolField;
            }
            set
            {
                this.setSelectionSymbolField = value;
                base.RaisePropertyChanged("SetSelectionSymbol");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public double SelectionBufferDistance
        {
            get
            {
                return this.selectionBufferDistanceField;
            }
            set
            {
                this.selectionBufferDistanceField = value;
                base.RaisePropertyChanged("SelectionBufferDistance");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public bool ShowSelectionBuffer
        {
            get
            {
                return this.showSelectionBufferField;
            }
            set
            {
                this.showSelectionBufferField = value;
                base.RaisePropertyChanged("ShowSelectionBuffer");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public string DefinitionExpression
        {
            get
            {
                return this.definitionExpressionField;
            }
            set
            {
                this.definitionExpressionField = value;
                base.RaisePropertyChanged("DefinitionExpression");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public string SourceID
        {
            get
            {
                return this.sourceIDField;
            }
            set
            {
                this.sourceIDField = value;
                base.RaisePropertyChanged("SourceID");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public FillSymbol SelectionBufferSymbol
        {
            get
            {
                return this.selectionBufferSymbolField;
            }
            set
            {
                this.selectionBufferSymbolField = value;
                base.RaisePropertyChanged("SelectionBufferSymbol");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public LayerResultOptions LayerResultOptions
        {
            get
            {
                return this.layerResultOptionsField;
            }
            set
            {
                this.layerResultOptionsField = value;
                base.RaisePropertyChanged("LayerResultOptions");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
        public bool UseTime
        {
            get
            {
                return this.useTimeField;
            }
            set
            {
                this.useTimeField = value;
                base.RaisePropertyChanged("UseTime");
            }
        }

        [XmlIgnore]
        public bool UseTimeSpecified
        {
            get
            {
                return this.useTimeFieldSpecified;
            }
            set
            {
                this.useTimeFieldSpecified = value;
                base.RaisePropertyChanged("UseTimeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 15)]
        public bool TimeDataCumulative
        {
            get
            {
                return this.timeDataCumulativeField;
            }
            set
            {
                this.timeDataCumulativeField = value;
                base.RaisePropertyChanged("TimeDataCumulative");
            }
        }

        [XmlIgnore]
        public bool TimeDataCumulativeSpecified
        {
            get
            {
                return this.timeDataCumulativeFieldSpecified;
            }
            set
            {
                this.timeDataCumulativeFieldSpecified = value;
                base.RaisePropertyChanged("TimeDataCumulativeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 16)]
        public double TimeOffset
        {
            get
            {
                return this.timeOffsetField;
            }
            set
            {
                this.timeOffsetField = value;
                base.RaisePropertyChanged("TimeOffset");
            }
        }

        [XmlIgnore]
        public bool TimeOffsetSpecified
        {
            get
            {
                return this.timeOffsetFieldSpecified;
            }
            set
            {
                this.timeOffsetFieldSpecified = value;
                base.RaisePropertyChanged("TimeOffsetSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 17)]
        public esriTimeUnits TimeOffsetUnits
        {
            get
            {
                return this.timeOffsetUnitsField;
            }
            set
            {
                this.timeOffsetUnitsField = value;
                base.RaisePropertyChanged("TimeOffsetUnits");
            }
        }

        [XmlIgnore]
        public bool TimeOffsetUnitsSpecified
        {
            get
            {
                return this.timeOffsetUnitsFieldSpecified;
            }
            set
            {
                this.timeOffsetUnitsFieldSpecified = value;
                base.RaisePropertyChanged("TimeOffsetUnitsSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 18)]
        public LayerDrawingDescription DrawingDescription
        {
            get
            {
                return this.drawingDescriptionField;
            }
            set
            {
                this.drawingDescriptionField = value;
                base.RaisePropertyChanged("DrawingDescription");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 19)]
        public MapServerSourceDescription Source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
                base.RaisePropertyChanged("Source");
            }
        }

        private int layerIDField;

        private bool visibleField;

        private bool showLabelsField;

        private bool scaleSymbolsField;

        private int[] selectionFeaturesField;

        private Color selectionColorField;

        private Symbol selectionSymbolField;

        private bool setSelectionSymbolField;

        private double selectionBufferDistanceField;

        private bool showSelectionBufferField;

        private string definitionExpressionField;

        private string sourceIDField;

        private FillSymbol selectionBufferSymbolField;

        private LayerResultOptions layerResultOptionsField;

        private bool useTimeField;

        private bool useTimeFieldSpecified;

        private bool timeDataCumulativeField;

        private bool timeDataCumulativeFieldSpecified;

        private double timeOffsetField;

        private bool timeOffsetFieldSpecified;

        private esriTimeUnits timeOffsetUnitsField;

        private bool timeOffsetUnitsFieldSpecified;

        private LayerDrawingDescription drawingDescriptionField;

        private MapServerSourceDescription sourceField;
    }
}