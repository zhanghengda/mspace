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
    public class PointN : Point
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public double X
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
                base.RaisePropertyChanged("X");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public double Y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
                base.RaisePropertyChanged("Y");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public double M
        {
            get
            {
                return this.mField;
            }
            set
            {
                this.mField = value;
                base.RaisePropertyChanged("M");
            }
        }

        [XmlIgnore]
        public bool MSpecified
        {
            get
            {
                return this.mFieldSpecified;
            }
            set
            {
                this.mFieldSpecified = value;
                base.RaisePropertyChanged("MSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public double Z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
                base.RaisePropertyChanged("Z");
            }
        }

        [XmlIgnore]
        public bool ZSpecified
        {
            get
            {
                return this.zFieldSpecified;
            }
            set
            {
                this.zFieldSpecified = value;
                base.RaisePropertyChanged("ZSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public int ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                base.RaisePropertyChanged("ID");
            }
        }

        [XmlIgnore]
        public bool IDSpecified
        {
            get
            {
                return this.idFieldSpecified;
            }
            set
            {
                this.idFieldSpecified = value;
                base.RaisePropertyChanged("IDSpecified");
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

        private double xField;

        private double yField;

        private double mField;

        private bool mFieldSpecified;

        private double zField;

        private bool zFieldSpecified;

        private int idField;

        private bool idFieldSpecified;

        private SpatialReference spatialReferenceField;
    }
}