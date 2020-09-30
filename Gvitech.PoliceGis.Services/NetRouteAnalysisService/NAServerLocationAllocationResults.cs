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
    public class NAServerLocationAllocationResults : NAServerSolverResults
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public RecordSet LALines
        {
            get
            {
                return this.lALinesField;
            }
            set
            {
                this.lALinesField = value;
                base.RaisePropertyChanged("LALines");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public RecordSet DemandPoints
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
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

        private RecordSet lALinesField;

        private RecordSet demandPointsField;

        private RecordSet facilitiesField;

        private RecordSet barriersField;

        private RecordSet polygonBarriersField;

        private RecordSet polylineBarriersField;
    }
}