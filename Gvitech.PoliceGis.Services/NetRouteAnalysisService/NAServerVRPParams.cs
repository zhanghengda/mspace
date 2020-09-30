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
    public class NAServerVRPParams : NAServerSolverParams
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public NAServerLocations Orders
        {
            get
            {
                return this.ordersField;
            }
            set
            {
                this.ordersField = value;
                base.RaisePropertyChanged("Orders");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public NAServerLocations OrderPairs
        {
            get
            {
                return this.orderPairsField;
            }
            set
            {
                this.orderPairsField = value;
                base.RaisePropertyChanged("OrderPairs");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public NAServerLocations Depots
        {
            get
            {
                return this.depotsField;
            }
            set
            {
                this.depotsField = value;
                base.RaisePropertyChanged("Depots");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public NAServerLocations Routes
        {
            get
            {
                return this.routesField;
            }
            set
            {
                this.routesField = value;
                base.RaisePropertyChanged("Routes");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public NAServerLocations RouteSeedPoints
        {
            get
            {
                return this.routeSeedPointsField;
            }
            set
            {
                this.routeSeedPointsField = value;
                base.RaisePropertyChanged("RouteSeedPoints");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public NAServerLocations RouteZones
        {
            get
            {
                return this.routeZonesField;
            }
            set
            {
                this.routeZonesField = value;
                base.RaisePropertyChanged("RouteZones");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public NAServerLocations RouteRenewals
        {
            get
            {
                return this.routeRenewalsField;
            }
            set
            {
                this.routeRenewalsField = value;
                base.RaisePropertyChanged("RouteRenewals");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public NAServerLocations Breaks
        {
            get
            {
                return this.breaksField;
            }
            set
            {
                this.breaksField = value;
                base.RaisePropertyChanged("Breaks");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public NAServerLocations Specialties
        {
            get
            {
                return this.specialtiesField;
            }
            set
            {
                this.specialtiesField = value;
                base.RaisePropertyChanged("Specialties");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public bool ReturnRoutes
        {
            get
            {
                return this.returnRoutesField;
            }
            set
            {
                this.returnRoutesField = value;
                base.RaisePropertyChanged("ReturnRoutes");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public bool ReturnOrders
        {
            get
            {
                return this.returnOrdersField;
            }
            set
            {
                this.returnOrdersField = value;
                base.RaisePropertyChanged("ReturnOrders");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
        public bool ReturnDepots
        {
            get
            {
                return this.returnDepotsField;
            }
            set
            {
                this.returnDepotsField = value;
                base.RaisePropertyChanged("ReturnDepots");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 15)]
        public bool ReturnDepotVisits
        {
            get
            {
                return this.returnDepotVisitsField;
            }
            set
            {
                this.returnDepotVisitsField = value;
                base.RaisePropertyChanged("ReturnDepotVisits");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 16)]
        public bool ReturnBreaks
        {
            get
            {
                return this.returnBreaksField;
            }
            set
            {
                this.returnBreaksField = value;
                base.RaisePropertyChanged("ReturnBreaks");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 17)]
        public bool ReturnRouteRenewals
        {
            get
            {
                return this.returnRouteRenewalsField;
            }
            set
            {
                this.returnRouteRenewalsField = value;
                base.RaisePropertyChanged("ReturnRouteRenewals");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 18)]
        public bool ReturnRouteSeedPoints
        {
            get
            {
                return this.returnRouteSeedPointsField;
            }
            set
            {
                this.returnRouteSeedPointsField = value;
                base.RaisePropertyChanged("ReturnRouteSeedPoints");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 19)]
        public bool ReturnRouteZones
        {
            get
            {
                return this.returnRouteZonesField;
            }
            set
            {
                this.returnRouteZonesField = value;
                base.RaisePropertyChanged("ReturnRouteZones");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 20)]
        public bool ReturnSpecialties
        {
            get
            {
                return this.returnSpecialtiesField;
            }
            set
            {
                this.returnSpecialtiesField = value;
                base.RaisePropertyChanged("ReturnSpecialties");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 21)]
        public bool ReturnOrderPairs
        {
            get
            {
                return this.returnOrderPairsField;
            }
            set
            {
                this.returnOrderPairsField = value;
                base.RaisePropertyChanged("ReturnOrderPairs");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 22)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 23)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 24)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 25)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 26)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 27)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 28)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 29)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 30)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 31)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 32)]
        public DateTime DefaultDate
        {
            get
            {
                return this.defaultDateField;
            }
            set
            {
                this.defaultDateField = value;
                base.RaisePropertyChanged("DefaultDate");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 33)]
        public esriNetworkAttributeUnits DistanceFieldUnits
        {
            get
            {
                return this.distanceFieldUnitsField;
            }
            set
            {
                this.distanceFieldUnitsField = value;
                base.RaisePropertyChanged("DistanceFieldUnits");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 34)]
        public double ExcessTransitTimePenaltyFactor
        {
            get
            {
                return this.excessTransitTimePenaltyFactorField;
            }
            set
            {
                this.excessTransitTimePenaltyFactorField = value;
                base.RaisePropertyChanged("ExcessTransitTimePenaltyFactor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 35)]
        public double TimeWindowViolationPenaltyFactor
        {
            get
            {
                return this.timeWindowViolationPenaltyFactorField;
            }
            set
            {
                this.timeWindowViolationPenaltyFactorField = value;
                base.RaisePropertyChanged("TimeWindowViolationPenaltyFactor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 36)]
        public int CapacityCount
        {
            get
            {
                return this.capacityCountField;
            }
            set
            {
                this.capacityCountField = value;
                base.RaisePropertyChanged("CapacityCount");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 37)]
        public bool GenerateInternalRouteContext
        {
            get
            {
                return this.generateInternalRouteContextField;
            }
            set
            {
                this.generateInternalRouteContextField = value;
                base.RaisePropertyChanged("GenerateInternalRouteContext");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 38)]
        public esriNAODCostMatrixType InternalODCostMatrixType
        {
            get
            {
                return this.internalODCostMatrixTypeField;
            }
            set
            {
                this.internalODCostMatrixTypeField = value;
                base.RaisePropertyChanged("InternalODCostMatrixType");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 39)]
        public esriNetworkAttributeUnits TimeFieldUnits
        {
            get
            {
                return this.timeFieldUnitsField;
            }
            set
            {
                this.timeFieldUnitsField = value;
                base.RaisePropertyChanged("TimeFieldUnits");
            }
        }

        private NAServerLocations ordersField;

        private NAServerLocations orderPairsField;

        private NAServerLocations depotsField;

        private NAServerLocations routesField;

        private NAServerLocations routeSeedPointsField;

        private NAServerLocations routeZonesField;

        private NAServerLocations routeRenewalsField;

        private NAServerLocations barriersField;

        private NAServerLocations polygonBarriersField;

        private NAServerLocations polylineBarriersField;

        private NAServerLocations breaksField;

        private NAServerLocations specialtiesField;

        private bool returnRoutesField;

        private bool returnOrdersField;

        private bool returnDepotsField;

        private bool returnDepotVisitsField;

        private bool returnBreaksField;

        private bool returnRouteRenewalsField;

        private bool returnRouteSeedPointsField;

        private bool returnRouteZonesField;

        private bool returnSpecialtiesField;

        private bool returnOrderPairsField;

        private bool returnBarriersField;

        private bool returnPolygonBarriersField;

        private bool returnPolygonBarriersFieldSpecified;

        private bool returnPolylineBarriersField;

        private bool returnPolylineBarriersFieldSpecified;

        private bool returnCompactDirectionsField;

        private string directionsLanguageField;

        private esriNetworkAttributeUnits directionsLengthUnitsField;

        private esriDirectionsOutputType directionsOutputTypeField;

        private bool directionsOutputTypeFieldSpecified;

        private string directionsStyleNameField;

        private string directionsTimeAttributeNameField;

        private esriNAOutputLineType outputLinesField;

        private DateTime defaultDateField;

        private esriNetworkAttributeUnits distanceFieldUnitsField;

        private double excessTransitTimePenaltyFactorField;

        private double timeWindowViolationPenaltyFactorField;

        private int capacityCountField;

        private bool generateInternalRouteContextField;

        private esriNAODCostMatrixType internalODCostMatrixTypeField;

        private esriNetworkAttributeUnits timeFieldUnitsField;
    }
}