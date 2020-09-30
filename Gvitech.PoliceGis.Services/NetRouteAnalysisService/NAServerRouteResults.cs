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
    public class NAServerRouteResults : NAServerSolverResults
    {
        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 0)]
        [XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public double[] TotalImpedances
        {
            get
            {
                return this.totalImpedancesField;
            }
            set
            {
                this.totalImpedancesField = value;
                base.RaisePropertyChanged("TotalImpedances");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 1)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public Polyline[] RouteGeometries
        {
            get
            {
                return this.routeGeometriesField;
            }
            set
            {
                this.routeGeometriesField = value;
                base.RaisePropertyChanged("RouteGeometries");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public RecordSet Stops
        {
            get
            {
                return this.stopsField;
            }
            set
            {
                this.stopsField = value;
                base.RaisePropertyChanged("Stops");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
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

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 5)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public NAStreetDirections[] Directions
        {
            get
            {
                return this.directionsField;
            }
            set
            {
                this.directionsField = value;
                base.RaisePropertyChanged("Directions");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 6)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
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

        private double[] totalImpedancesField;

        private Polyline[] routeGeometriesField;

        private RecordSet routesField;

        private RecordSet stopsField;

        private RecordSet barriersField;

        private NAStreetDirections[] directionsField;

        private NACompactStreetDirections[] compactDirectionsField;

        private RecordSet polygonBarriersField;

        private RecordSet polylineBarriersField;
    }
}