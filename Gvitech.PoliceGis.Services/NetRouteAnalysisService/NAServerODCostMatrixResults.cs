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
    public class NAServerODCostMatrixResults : NAServerSolverResults
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public RecordSet ODLines
        {
            get
            {
                return this.oDLinesField;
            }
            set
            {
                this.oDLinesField = value;
                base.RaisePropertyChanged("ODLines");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public RecordSet Origins
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public RecordSet Destinations
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

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 6)]
        [XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public int[] OriginOIDIndex
        {
            get
            {
                return this.originOIDIndexField;
            }
            set
            {
                this.originOIDIndexField = value;
                base.RaisePropertyChanged("OriginOIDIndex");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 7)]
        [XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public int[] DestinationOIDIndex
        {
            get
            {
                return this.destinationOIDIndexField;
            }
            set
            {
                this.destinationOIDIndexField = value;
                base.RaisePropertyChanged("DestinationOIDIndex");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 8)]
        [XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public string[] CostAttributeNameIndex
        {
            get
            {
                return this.costAttributeNameIndexField;
            }
            set
            {
                this.costAttributeNameIndexField = value;
                base.RaisePropertyChanged("CostAttributeNameIndex");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "base64Binary", Order = 9)]
        public byte[] ODMatrix
        {
            get
            {
                return this.oDMatrixField;
            }
            set
            {
                this.oDMatrixField = value;
                base.RaisePropertyChanged("ODMatrix");
            }
        }

        private RecordSet oDLinesField;

        private RecordSet originsField;

        private RecordSet destinationsField;

        private RecordSet barriersField;

        private RecordSet polygonBarriersField;

        private RecordSet polylineBarriersField;

        private int[] originOIDIndexField;

        private int[] destinationOIDIndexField;

        private string[] costAttributeNameIndexField;

        private byte[] oDMatrixField;
    }
}