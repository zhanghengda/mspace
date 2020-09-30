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
    public class NAServerODCostMatrixParams : NAServerSolverParams
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public NAServerLocations Origins
        {
            get
            {
                return this.originsField;
            }
            set
            {
                this.originsField = value;
                base.RaisePropertyChanged("Origins");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public NAServerLocations Destinations
        {
            get
            {
                return this.destinationsField;
            }
            set
            {
                this.destinationsField = value;
                base.RaisePropertyChanged("Destinations");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public NAServerLocations Barriers
        {
            get
            {
                return this.barriersField;
            }
            set
            {
                this.barriersField = value;
                base.RaisePropertyChanged("Barriers");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public NAServerLocations PolygonBarriers
        {
            get
            {
                return this.polygonBarriersField;
            }
            set
            {
                this.polygonBarriersField = value;
                base.RaisePropertyChanged("PolygonBarriers");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public NAServerLocations PolylineBarriers
        {
            get
            {
                return this.polylineBarriersField;
            }
            set
            {
                this.polylineBarriersField = value;
                base.RaisePropertyChanged("PolylineBarriers");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public bool ReturnODLines
        {
            get
            {
                return this.returnODLinesField;
            }
            set
            {
                this.returnODLinesField = value;
                base.RaisePropertyChanged("ReturnODLines");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public bool ReturnODMatrix
        {
            get
            {
                return this.returnODMatrixField;
            }
            set
            {
                this.returnODMatrixField = value;
                base.RaisePropertyChanged("ReturnODMatrix");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public bool ReturnOrigins
        {
            get
            {
                return this.returnOriginsField;
            }
            set
            {
                this.returnOriginsField = value;
                base.RaisePropertyChanged("ReturnOrigins");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public bool ReturnDestinations
        {
            get
            {
                return this.returnDestinationsField;
            }
            set
            {
                this.returnDestinationsField = value;
                base.RaisePropertyChanged("ReturnDestinations");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public bool ReturnBarriers
        {
            get
            {
                return this.returnBarriersField;
            }
            set
            {
                this.returnBarriersField = value;
                base.RaisePropertyChanged("ReturnBarriers");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public bool ReturnPolygonBarriers
        {
            get
            {
                return this.returnPolygonBarriersField;
            }
            set
            {
                this.returnPolygonBarriersField = value;
                base.RaisePropertyChanged("ReturnPolygonBarriers");
            }
        }

        [XmlIgnore]
        public bool ReturnPolygonBarriersSpecified
        {
            get
            {
                return this.returnPolygonBarriersFieldSpecified;
            }
            set
            {
                this.returnPolygonBarriersFieldSpecified = value;
                base.RaisePropertyChanged("ReturnPolygonBarriersSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public bool ReturnPolylineBarriers
        {
            get
            {
                return this.returnPolylineBarriersField;
            }
            set
            {
                this.returnPolylineBarriersField = value;
                base.RaisePropertyChanged("ReturnPolylineBarriers");
            }
        }

        [XmlIgnore]
        public bool ReturnPolylineBarriersSpecified
        {
            get
            {
                return this.returnPolylineBarriersFieldSpecified;
            }
            set
            {
                this.returnPolylineBarriersFieldSpecified = value;
                base.RaisePropertyChanged("ReturnPolylineBarriersSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public object DefaultCutoff
        {
            get
            {
                return this.defaultCutoffField;
            }
            set
            {
                this.defaultCutoffField = value;
                base.RaisePropertyChanged("DefaultCutoff");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public object DefaultTargetDestinationCount
        {
            get
            {
                return this.defaultTargetDestinationCountField;
            }
            set
            {
                this.defaultTargetDestinationCountField = value;
                base.RaisePropertyChanged("DefaultTargetDestinationCount");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
        public esriNAOutputLineType OutputLines
        {
            get
            {
                return this.outputLinesField;
            }
            set
            {
                this.outputLinesField = value;
                base.RaisePropertyChanged("OutputLines");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 15)]
        public esriNAODCostMatrixType MatrixResultType
        {
            get
            {
                return this.matrixResultTypeField;
            }
            set
            {
                this.matrixResultTypeField = value;
                base.RaisePropertyChanged("MatrixResultType");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 16)]
        public bool PopulateODLines
        {
            get
            {
                return this.populateODLinesField;
            }
            set
            {
                this.populateODLinesField = value;
                base.RaisePropertyChanged("PopulateODLines");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 17)]
        public DateTime TimeOfDay
        {
            get
            {
                return this.timeOfDayField;
            }
            set
            {
                this.timeOfDayField = value;
                base.RaisePropertyChanged("TimeOfDay");
            }
        }

        [XmlIgnore]
        public bool TimeOfDaySpecified
        {
            get
            {
                return this.timeOfDayFieldSpecified;
            }
            set
            {
                this.timeOfDayFieldSpecified = value;
                base.RaisePropertyChanged("TimeOfDaySpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 18)]
        public esriNATimeOfDayUsage TimeOfDayUsage
        {
            get
            {
                return this.timeOfDayUsageField;
            }
            set
            {
                this.timeOfDayUsageField = value;
                base.RaisePropertyChanged("TimeOfDayUsage");
            }
        }

        [XmlIgnore]
        public bool TimeOfDayUsageSpecified
        {
            get
            {
                return this.timeOfDayUsageFieldSpecified;
            }
            set
            {
                this.timeOfDayUsageFieldSpecified = value;
                base.RaisePropertyChanged("TimeOfDayUsageSpecified");
            }
        }

        private NAServerLocations originsField;

        private NAServerLocations destinationsField;

        private NAServerLocations barriersField;

        private NAServerLocations polygonBarriersField;

        private NAServerLocations polylineBarriersField;

        private bool returnODLinesField;

        private bool returnODMatrixField;

        private bool returnOriginsField;

        private bool returnDestinationsField;

        private bool returnBarriersField;

        private bool returnPolygonBarriersField;

        private bool returnPolygonBarriersFieldSpecified;

        private bool returnPolylineBarriersField;

        private bool returnPolylineBarriersFieldSpecified;

        private object defaultCutoffField;

        private object defaultTargetDestinationCountField;

        private esriNAOutputLineType outputLinesField;

        private esriNAODCostMatrixType matrixResultTypeField;

        private bool populateODLinesField;

        private DateTime timeOfDayField;

        private bool timeOfDayFieldSpecified;

        private esriNATimeOfDayUsage timeOfDayUsageField;

        private bool timeOfDayUsageFieldSpecified;
    }
}