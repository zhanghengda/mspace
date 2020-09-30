using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(NAServerLocationAllocationParams))]
    [XmlInclude(typeof(NAServerVRPParams))]
    [XmlInclude(typeof(NAServerODCostMatrixParams))]
    [XmlInclude(typeof(NAServerServiceAreaParams))]
    [XmlInclude(typeof(NAServerClosestFacilityParams))]
    [XmlInclude(typeof(NAServerRouteParams))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class NAServerSolverParams : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string NALayerName
        {
            get
            {
                return this.nALayerNameField;
            }
            set
            {
                this.nALayerNameField = value;
                this.RaisePropertyChanged("NALayerName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public double SnapTolerance
        {
            get
            {
                return this.snapToleranceField;
            }
            set
            {
                this.snapToleranceField = value;
                this.RaisePropertyChanged("SnapTolerance");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public double MaxSnapTolerance
        {
            get
            {
                return this.maxSnapToleranceField;
            }
            set
            {
                this.maxSnapToleranceField = value;
                this.RaisePropertyChanged("MaxSnapTolerance");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public esriUnits SnapToleranceUnits
        {
            get
            {
                return this.snapToleranceUnitsField;
            }
            set
            {
                this.snapToleranceUnitsField = value;
                this.RaisePropertyChanged("SnapToleranceUnits");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 4)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public NAClassCandidateFieldMap[] NAClassCandidateFieldMaps
        {
            get
            {
                return this.nAClassCandidateFieldMapsField;
            }
            set
            {
                this.nAClassCandidateFieldMapsField = value;
                this.RaisePropertyChanged("NAClassCandidateFieldMaps");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public bool ReturnMap
        {
            get
            {
                return this.returnMapField;
            }
            set
            {
                this.returnMapField = value;
                this.RaisePropertyChanged("ReturnMap");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public MapDescription MapDescription
        {
            get
            {
                return this.mapDescriptionField;
            }
            set
            {
                this.mapDescriptionField = value;
                this.RaisePropertyChanged("MapDescription");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public ImageDescription ImageDescription
        {
            get
            {
                return this.imageDescriptionField;
            }
            set
            {
                this.imageDescriptionField = value;
                this.RaisePropertyChanged("ImageDescription");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public SpatialReference OutputSpatialReference
        {
            get
            {
                return this.outputSpatialReferenceField;
            }
            set
            {
                this.outputSpatialReferenceField = value;
                this.RaisePropertyChanged("OutputSpatialReference");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public bool ReturnGeometriesAsBinary
        {
            get
            {
                return this.returnGeometriesAsBinaryField;
            }
            set
            {
                this.returnGeometriesAsBinaryField = value;
                this.RaisePropertyChanged("ReturnGeometriesAsBinary");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 10)]
        [XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public string[] AccumulateAttributeNames
        {
            get
            {
                return this.accumulateAttributeNamesField;
            }
            set
            {
                this.accumulateAttributeNamesField = value;
                this.RaisePropertyChanged("AccumulateAttributeNames");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public string ImpedanceAttributeName
        {
            get
            {
                return this.impedanceAttributeNameField;
            }
            set
            {
                this.impedanceAttributeNameField = value;
                this.RaisePropertyChanged("ImpedanceAttributeName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public bool IgnoreInvalidLocations
        {
            get
            {
                return this.ignoreInvalidLocationsField;
            }
            set
            {
                this.ignoreInvalidLocationsField = value;
                this.RaisePropertyChanged("IgnoreInvalidLocations");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public esriNetworkForwardStarBacktrack RestrictUTurns
        {
            get
            {
                return this.restrictUTurnsField;
            }
            set
            {
                this.restrictUTurnsField = value;
                this.RaisePropertyChanged("RestrictUTurns");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 14)]
        [XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public string[] RestrictionAttributeNames
        {
            get
            {
                return this.restrictionAttributeNamesField;
            }
            set
            {
                this.restrictionAttributeNamesField = value;
                this.RaisePropertyChanged("RestrictionAttributeNames");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 15)]
        public bool UseHierarchy
        {
            get
            {
                return this.useHierarchyField;
            }
            set
            {
                this.useHierarchyField = value;
                this.RaisePropertyChanged("UseHierarchy");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 16)]
        public string HierarchyAttributeName
        {
            get
            {
                return this.hierarchyAttributeNameField;
            }
            set
            {
                this.hierarchyAttributeNameField = value;
                this.RaisePropertyChanged("HierarchyAttributeName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 17)]
        public int HierarchyLevelCount
        {
            get
            {
                return this.hierarchyLevelCountField;
            }
            set
            {
                this.hierarchyLevelCountField = value;
                this.RaisePropertyChanged("HierarchyLevelCount");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 18)]
        [XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public int[] HierarchyMaxValues
        {
            get
            {
                return this.hierarchyMaxValuesField;
            }
            set
            {
                this.hierarchyMaxValuesField = value;
                this.RaisePropertyChanged("HierarchyMaxValues");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 19)]
        [XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public int[] HierarchyNumTransitions
        {
            get
            {
                return this.hierarchyNumTransitionsField;
            }
            set
            {
                this.hierarchyNumTransitionsField = value;
                this.RaisePropertyChanged("HierarchyNumTransitions");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 20)]
        public bool ResetHierarchyRangesOnBind
        {
            get
            {
                return this.resetHierarchyRangesOnBindField;
            }
            set
            {
                this.resetHierarchyRangesOnBindField = value;
                this.RaisePropertyChanged("ResetHierarchyRangesOnBind");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 21)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public NAAttributeParameterValue[] AttributeParameterValues
        {
            get
            {
                return this.attributeParameterValuesField;
            }
            set
            {
                this.attributeParameterValuesField = value;
                this.RaisePropertyChanged("AttributeParameterValues");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 22)]
        public object OutputGeometryPrecision
        {
            get
            {
                return this.outputGeometryPrecisionField;
            }
            set
            {
                this.outputGeometryPrecisionField = value;
                this.RaisePropertyChanged("OutputGeometryPrecision");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 23)]
        public esriUnits OutputGeometryPrecisionUnits
        {
            get
            {
                return this.outputGeometryPrecisionUnitsField;
            }
            set
            {
                this.outputGeometryPrecisionUnitsField = value;
                this.RaisePropertyChanged("OutputGeometryPrecisionUnits");
            }
        }

        [XmlIgnore]
        public bool OutputGeometryPrecisionUnitsSpecified
        {
            get
            {
                return this.outputGeometryPrecisionUnitsFieldSpecified;
            }
            set
            {
                this.outputGeometryPrecisionUnitsFieldSpecified = value;
                this.RaisePropertyChanged("OutputGeometryPrecisionUnitsSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 24)]
        public bool ReturnPartialResultsOnError
        {
            get
            {
                return this.returnPartialResultsOnErrorField;
            }
            set
            {
                this.returnPartialResultsOnErrorField = value;
                this.RaisePropertyChanged("ReturnPartialResultsOnError");
            }
        }

        [XmlIgnore]
        public bool ReturnPartialResultsOnErrorSpecified
        {
            get
            {
                return this.returnPartialResultsOnErrorFieldSpecified;
            }
            set
            {
                this.returnPartialResultsOnErrorFieldSpecified = value;
                this.RaisePropertyChanged("ReturnPartialResultsOnErrorSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 25)]
        public bool SaveLayerOnServer
        {
            get
            {
                return this.saveLayerOnServerField;
            }
            set
            {
                this.saveLayerOnServerField = value;
                this.RaisePropertyChanged("SaveLayerOnServer");
            }
        }

        [XmlIgnore]
        public bool SaveLayerOnServerSpecified
        {
            get
            {
                return this.saveLayerOnServerFieldSpecified;
            }
            set
            {
                this.saveLayerOnServerFieldSpecified = value;
                this.RaisePropertyChanged("SaveLayerOnServerSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 26)]
        public string LayerToken
        {
            get
            {
                return this.layerTokenField;
            }
            set
            {
                this.layerTokenField = value;
                this.RaisePropertyChanged("LayerToken");
            }
        }

        // (add) Token: 0x0600015F RID: 351 RVA: 0x00006C14 File Offset: 0x00004E14
        // (remove) Token: 0x06000160 RID: 352 RVA: 0x00006C4C File Offset: 0x00004E4C
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

        private string nALayerNameField;

        private double snapToleranceField;

        private double maxSnapToleranceField;

        private esriUnits snapToleranceUnitsField;

        private NAClassCandidateFieldMap[] nAClassCandidateFieldMapsField;

        private bool returnMapField;

        private MapDescription mapDescriptionField;

        private ImageDescription imageDescriptionField;

        private SpatialReference outputSpatialReferenceField;

        private bool returnGeometriesAsBinaryField;

        private string[] accumulateAttributeNamesField;

        private string impedanceAttributeNameField;

        private bool ignoreInvalidLocationsField;

        private esriNetworkForwardStarBacktrack restrictUTurnsField;

        private string[] restrictionAttributeNamesField;

        private bool useHierarchyField;

        private string hierarchyAttributeNameField;

        private int hierarchyLevelCountField;

        private int[] hierarchyMaxValuesField;

        private int[] hierarchyNumTransitionsField;

        private bool resetHierarchyRangesOnBindField;

        private NAAttributeParameterValue[] attributeParameterValuesField;

        private object outputGeometryPrecisionField;

        private esriUnits outputGeometryPrecisionUnitsField;

        private bool outputGeometryPrecisionUnitsFieldSpecified;

        private bool returnPartialResultsOnErrorField;

        private bool returnPartialResultsOnErrorFieldSpecified;

        private bool saveLayerOnServerField;

        private bool saveLayerOnServerFieldSpecified;

        private string layerTokenField;
    }
}