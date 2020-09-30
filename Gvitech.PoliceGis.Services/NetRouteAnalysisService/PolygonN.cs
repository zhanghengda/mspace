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
    public class PolygonN : Polygon
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public bool HasID
        {
            get
            {
                return this.hasIDField;
            }
            set
            {
                this.hasIDField = value;
                base.RaisePropertyChanged("HasID");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public bool HasZ
        {
            get
            {
                return this.hasZField;
            }
            set
            {
                this.hasZField = value;
                base.RaisePropertyChanged("HasZ");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public bool HasM
        {
            get
            {
                return this.hasMField;
            }
            set
            {
                this.hasMField = value;
                base.RaisePropertyChanged("HasM");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public Envelope Extent
        {
            get
            {
                return this.extentField;
            }
            set
            {
                this.extentField = value;
                base.RaisePropertyChanged("Extent");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 4)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public Ring[] RingArray
        {
            get
            {
                return this.ringArrayField;
            }
            set
            {
                this.ringArrayField = value;
                base.RaisePropertyChanged("RingArray");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public SpatialReference SpatialReference
        {
            get
            {
                return this.spatialReferenceField;
            }
            set
            {
                this.spatialReferenceField = value;
                base.RaisePropertyChanged("SpatialReference");
            }
        }

        private bool hasIDField;

        private bool hasZField;

        private bool hasMField;

        private Envelope extentField;

        private Ring[] ringArrayField;

        private SpatialReference spatialReferenceField;
    }
}