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
    public class NAServerLocationAllocationParams : NAServerSolverParams
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public NAServerLocations DemandPoints
        {
            get
            {
                return this.demandPointsField;
            }
            set
            {
                this.demandPointsField = value;
                base.RaisePropertyChanged("DemandPoints");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public NAServerLocations Facilities
        {
            get
            {
                return this.facilitiesField;
            }
            set
            {
                this.facilitiesField = value;
                base.RaisePropertyChanged("Facilities");
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
        public bool ReturnLALines
        {
            get
            {
                return this.returnLALinesField;
            }
            set
            {
                this.returnLALinesField = value;
                base.RaisePropertyChanged("ReturnLALines");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public bool ReturnDemandPoints
        {
            get
            {
                return this.returnDemandPointsField;
            }
            set
            {
                this.returnDemandPointsField = value;
                base.RaisePropertyChanged("ReturnDemandPoints");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public bool ReturnFacilities
        {
            get
            {
                return this.returnFacilitiesField;
            }
            set
            {
                this.returnFacilitiesField = value;
                base.RaisePropertyChanged("ReturnFacilities");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public esriNATravelDirection TravelDirection
        {
            get
            {
                return this.travelDirectionField;
            }
            set
            {
                this.travelDirectionField = value;
                base.RaisePropertyChanged("TravelDirection");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
        public esriNAImpedanceTransformationType ImpedanceTransformation
        {
            get
            {
                return this.impedanceTransformationField;
            }
            set
            {
                this.impedanceTransformationField = value;
                base.RaisePropertyChanged("ImpedanceTransformation");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 15)]
        public int NumberFacilitiesToLocate
        {
            get
            {
                return this.numberFacilitiesToLocateField;
            }
            set
            {
                this.numberFacilitiesToLocateField = value;
                base.RaisePropertyChanged("NumberFacilitiesToLocate");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 16)]
        public esriNALocationAllocationProblemType ProblemType
        {
            get
            {
                return this.problemTypeField;
            }
            set
            {
                this.problemTypeField = value;
                base.RaisePropertyChanged("ProblemType");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 17)]
        public double TargetMarketSharePercentage
        {
            get
            {
                return this.targetMarketSharePercentageField;
            }
            set
            {
                this.targetMarketSharePercentageField = value;
                base.RaisePropertyChanged("TargetMarketSharePercentage");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 18)]
        public double TransformationParameter
        {
            get
            {
                return this.transformationParameterField;
            }
            set
            {
                this.transformationParameterField = value;
                base.RaisePropertyChanged("TransformationParameter");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 19)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 20)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 21)]
        public double DefaultCapacity
        {
            get
            {
                return this.defaultCapacityField;
            }
            set
            {
                this.defaultCapacityField = value;
                base.RaisePropertyChanged("DefaultCapacity");
            }
        }

        [XmlIgnore]
        public bool DefaultCapacitySpecified
        {
            get
            {
                return this.defaultCapacityFieldSpecified;
            }
            set
            {
                this.defaultCapacityFieldSpecified = value;
                base.RaisePropertyChanged("DefaultCapacitySpecified");
            }
        }

        private NAServerLocations demandPointsField;

        private NAServerLocations facilitiesField;

        private NAServerLocations barriersField;

        private NAServerLocations polygonBarriersField;

        private NAServerLocations polylineBarriersField;

        private bool returnLALinesField;

        private bool returnDemandPointsField;

        private bool returnFacilitiesField;

        private bool returnBarriersField;

        private bool returnPolygonBarriersField;

        private bool returnPolygonBarriersFieldSpecified;

        private bool returnPolylineBarriersField;

        private bool returnPolylineBarriersFieldSpecified;

        private object defaultCutoffField;

        private esriNAOutputLineType outputLinesField;

        private esriNATravelDirection travelDirectionField;

        private esriNAImpedanceTransformationType impedanceTransformationField;

        private int numberFacilitiesToLocateField;

        private esriNALocationAllocationProblemType problemTypeField;

        private double targetMarketSharePercentageField;

        private double transformationParameterField;

        private DateTime timeOfDayField;

        private bool timeOfDayFieldSpecified;

        private esriNATimeOfDayUsage timeOfDayUsageField;

        private bool timeOfDayUsageFieldSpecified;

        private double defaultCapacityField;

        private bool defaultCapacityFieldSpecified;
    }
}