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
    public class NAServerVRPResults : NAServerSolverResults
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public RecordSet Routes
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public RecordSet Orders
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public RecordSet Depots
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
        public RecordSet DepotVisits
        {
            get
            {
                return this.depotVisitsField;
            }
            set
            {
                this.depotVisitsField = value;
                base.RaisePropertyChanged("DepotVisits");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public RecordSet Breaks
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public RecordSet RouteRenewals
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public RecordSet RouteSeedPoints
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public RecordSet RouteZones
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public RecordSet Specialties
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public RecordSet OrderPairs
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public RecordSet Barriers
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public RecordSet PolygonBarriers
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public RecordSet PolylineBarriers
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

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 13)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public NACompactStreetDirections[] CompactDirections
        {
            get
            {
                return this.compactDirectionsField;
            }
            set
            {
                this.compactDirectionsField = value;
                base.RaisePropertyChanged("CompactDirections");
            }
        }

        private RecordSet routesField;

        private RecordSet ordersField;

        private RecordSet depotsField;

        private RecordSet depotVisitsField;

        private RecordSet breaksField;

        private RecordSet routeRenewalsField;

        private RecordSet routeSeedPointsField;

        private RecordSet routeZonesField;

        private RecordSet specialtiesField;

        private RecordSet orderPairsField;

        private RecordSet barriersField;

        private RecordSet polygonBarriersField;

        private RecordSet polylineBarriersField;

        private NACompactStreetDirections[] compactDirectionsField;
    }
}