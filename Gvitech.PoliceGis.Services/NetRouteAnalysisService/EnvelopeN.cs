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
    public class EnvelopeN : Envelope
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public double XMin
        {
            get
            {
                return this.xMinField;
            }
            set
            {
                this.xMinField = value;
                base.RaisePropertyChanged("XMin");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public double YMin
        {
            get
            {
                return this.yMinField;
            }
            set
            {
                this.yMinField = value;
                base.RaisePropertyChanged("YMin");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public double XMax
        {
            get
            {
                return this.xMaxField;
            }
            set
            {
                this.xMaxField = value;
                base.RaisePropertyChanged("XMax");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public double YMax
        {
            get
            {
                return this.yMaxField;
            }
            set
            {
                this.yMaxField = value;
                base.RaisePropertyChanged("YMax");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public double ZMin
        {
            get
            {
                return this.zMinField;
            }
            set
            {
                this.zMinField = value;
                base.RaisePropertyChanged("ZMin");
            }
        }

        [XmlIgnore]
        public bool ZMinSpecified
        {
            get
            {
                return this.zMinFieldSpecified;
            }
            set
            {
                this.zMinFieldSpecified = value;
                base.RaisePropertyChanged("ZMinSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public double ZMax
        {
            get
            {
                return this.zMaxField;
            }
            set
            {
                this.zMaxField = value;
                base.RaisePropertyChanged("ZMax");
            }
        }

        [XmlIgnore]
        public bool ZMaxSpecified
        {
            get
            {
                return this.zMaxFieldSpecified;
            }
            set
            {
                this.zMaxFieldSpecified = value;
                base.RaisePropertyChanged("ZMaxSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public double MMin
        {
            get
            {
                return this.mMinField;
            }
            set
            {
                this.mMinField = value;
                base.RaisePropertyChanged("MMin");
            }
        }

        [XmlIgnore]
        public bool MMinSpecified
        {
            get
            {
                return this.mMinFieldSpecified;
            }
            set
            {
                this.mMinFieldSpecified = value;
                base.RaisePropertyChanged("MMinSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public double MMax
        {
            get
            {
                return this.mMaxField;
            }
            set
            {
                this.mMaxField = value;
                base.RaisePropertyChanged("MMax");
            }
        }

        [XmlIgnore]
        public bool MMaxSpecified
        {
            get
            {
                return this.mMaxFieldSpecified;
            }
            set
            {
                this.mMaxFieldSpecified = value;
                base.RaisePropertyChanged("MMaxSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
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

        private double xMinField;

        private double yMinField;

        private double xMaxField;

        private double yMaxField;

        private double zMinField;

        private bool zMinFieldSpecified;

        private double zMaxField;

        private bool zMaxFieldSpecified;

        private double mMinField;

        private bool mMinFieldSpecified;

        private double mMaxField;

        private bool mMaxFieldSpecified;

        private SpatialReference spatialReferenceField;
    }
}