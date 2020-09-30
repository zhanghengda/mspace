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
    public class NAServerClosestFacilityResults : NAServerSolverResults
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
        public Polyline[] CFRouteGeometries
        {
            get
            {
                return this.cFRouteGeometriesField;
            }
            set
            {
                this.cFRouteGeometriesField = value;
                base.RaisePropertyChanged("CFRouteGeometries");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public RecordSet CFRoutes
        {
            get
            {
                return this.cFRoutesField;
            }
            set
            {
                this.cFRoutesField = value;
                base.RaisePropertyChanged("CFRoutes");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public RecordSet Facilities
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public RecordSet Incidents
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
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

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 6)]
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

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 7)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
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

        private Polyline[] cFRouteGeometriesField;

        private RecordSet cFRoutesField;

        private RecordSet facilitiesField;

        private RecordSet incidentsField;

        private RecordSet barriersField;

        private NAStreetDirections[] directionsField;

        private NACompactStreetDirections[] compactDirectionsField;

        private RecordSet polygonBarriersField;

        private RecordSet polylineBarriersField;
    }
}