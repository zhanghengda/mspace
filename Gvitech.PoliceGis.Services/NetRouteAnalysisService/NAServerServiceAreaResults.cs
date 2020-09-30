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
    public class NAServerServiceAreaResults : NAServerSolverResults
    {
        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 0)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public Polyline[] SALineGeometries
        {
            get
            {
                return this.sALineGeometriesField;
            }
            set
            {
                this.sALineGeometriesField = value;
                base.RaisePropertyChanged("SALineGeometries");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public RecordSet SALines
        {
            get
            {
                return this.sALinesField;
            }
            set
            {
                this.sALinesField = value;
                base.RaisePropertyChanged("SALines");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 2)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public Polygon[] SAPolygonGeometries
        {
            get
            {
                return this.sAPolygonGeometriesField;
            }
            set
            {
                this.sAPolygonGeometriesField = value;
                base.RaisePropertyChanged("SAPolygonGeometries");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public RecordSet SAPolygons
        {
            get
            {
                return this.sAPolygonsField;
            }
            set
            {
                this.sAPolygonsField = value;
                base.RaisePropertyChanged("SAPolygons");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
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

        private Polyline[] sALineGeometriesField;

        private RecordSet sALinesField;

        private Polygon[] sAPolygonGeometriesField;

        private RecordSet sAPolygonsField;

        private RecordSet facilitiesField;

        private RecordSet barriersField;

        private RecordSet polygonBarriersField;

        private RecordSet polylineBarriersField;
    }
}