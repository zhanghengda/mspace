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
    public class NAServerClosestFacilityParams : NAServerSolverParams
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public NAServerLocations Incidents
        {
            get
            {
                return this.incidentsField;
            }
            set
            {
                this.incidentsField = value;
                base.RaisePropertyChanged("Incidents");
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
        public bool ReturnCFRouteGeometries
        {
            get
            {
                return this.returnCFRouteGeometriesField;
            }
            set
            {
                this.returnCFRouteGeometriesField = value;
                base.RaisePropertyChanged("ReturnCFRouteGeometries");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public bool ReturnCFRoutes
        {
            get
            {
                return this.returnCFRoutesField;
            }
            set
            {
                this.returnCFRoutesField = value;
                base.RaisePropertyChanged("ReturnCFRoutes");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public bool ReturnIncidents
        {
            get
            {
                return this.returnIncidentsField;
            }
            set
            {
                this.returnIncidentsField = value;
                base.RaisePropertyChanged("ReturnIncidents");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public bool ReturnDirections
        {
            get
            {
                return this.returnDirectionsField;
            }
            set
            {
                this.returnDirectionsField = value;
                base.RaisePropertyChanged("ReturnDirections");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public string DirectionsLanguage
        {
            get
            {
                return this.directionsLanguageField;
            }
            set
            {
                this.directionsLanguageField = value;
                base.RaisePropertyChanged("DirectionsLanguage");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public esriNetworkAttributeUnits DirectionsLengthUnits
        {
            get
            {
                return this.directionsLengthUnitsField;
            }
            set
            {
                this.directionsLengthUnitsField = value;
                base.RaisePropertyChanged("DirectionsLengthUnits");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public string DirectionsTimeAttributeName
        {
            get
            {
                return this.directionsTimeAttributeNameField;
            }
            set
            {
                this.directionsTimeAttributeNameField = value;
                base.RaisePropertyChanged("DirectionsTimeAttributeName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public int DefaultTargetFacilityCount
        {
            get
            {
                return this.defaultTargetFacilityCountField;
            }
            set
            {
                this.defaultTargetFacilityCountField = value;
                base.RaisePropertyChanged("DefaultTargetFacilityCount");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 15)]
        public bool CreateTraversalResult
        {
            get
            {
                return this.createTraversalResultField;
            }
            set
            {
                this.createTraversalResultField = value;
                base.RaisePropertyChanged("CreateTraversalResult");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 16)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 17)]
        public bool ReturnCompactDirections
        {
            get
            {
                return this.returnCompactDirectionsField;
            }
            set
            {
                this.returnCompactDirectionsField = value;
                base.RaisePropertyChanged("ReturnCompactDirections");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 18)]
        public esriDirectionsOutputType DirectionsOutputType
        {
            get
            {
                return this.directionsOutputTypeField;
            }
            set
            {
                this.directionsOutputTypeField = value;
                base.RaisePropertyChanged("DirectionsOutputType");
            }
        }

        [XmlIgnore]
        public bool DirectionsOutputTypeSpecified
        {
            get
            {
                return this.directionsOutputTypeFieldSpecified;
            }
            set
            {
                this.directionsOutputTypeFieldSpecified = value;
                base.RaisePropertyChanged("DirectionsOutputTypeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 19)]
        public string DirectionsStyleName
        {
            get
            {
                return this.directionsStyleNameField;
            }
            set
            {
                this.directionsStyleNameField = value;
                base.RaisePropertyChanged("DirectionsStyleName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 20)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 21)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 22)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 23)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 24)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 25)]
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

        private NAServerLocations facilitiesField;

        private NAServerLocations incidentsField;

        private NAServerLocations barriersField;

        private bool returnCFRouteGeometriesField;

        private bool returnCFRoutesField;

        private bool returnFacilitiesField;

        private bool returnIncidentsField;

        private bool returnBarriersField;

        private bool returnDirectionsField;

        private string directionsLanguageField;

        private esriNetworkAttributeUnits directionsLengthUnitsField;

        private string directionsTimeAttributeNameField;

        private int defaultTargetFacilityCountField;

        private object defaultCutoffField;

        private esriNATravelDirection travelDirectionField;

        private bool createTraversalResultField;

        private esriNAOutputLineType outputLinesField;

        private bool returnCompactDirectionsField;

        private esriDirectionsOutputType directionsOutputTypeField;

        private bool directionsOutputTypeFieldSpecified;

        private string directionsStyleNameField;

        private NAServerLocations polygonBarriersField;

        private NAServerLocations polylineBarriersField;

        private bool returnPolygonBarriersField;

        private bool returnPolygonBarriersFieldSpecified;

        private bool returnPolylineBarriersField;

        private bool returnPolylineBarriersFieldSpecified;

        private DateTime timeOfDayField;

        private bool timeOfDayFieldSpecified;

        private esriNATimeOfDayUsage timeOfDayUsageField;

        private bool timeOfDayUsageFieldSpecified;
    }
}