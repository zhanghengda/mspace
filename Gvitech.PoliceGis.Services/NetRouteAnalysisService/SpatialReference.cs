using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(UnknownCoordinateSystem))]
    [XmlInclude(typeof(GeographicCoordinateSystem))]
    [XmlInclude(typeof(ProjectedCoordinateSystem))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class SpatialReference : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string WKT
        {
            get
            {
                return this.wKTField;
            }
            set
            {
                this.wKTField = value;
                this.RaisePropertyChanged("WKT");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public double XOrigin
        {
            get
            {
                return this.xOriginField;
            }
            set
            {
                this.xOriginField = value;
                this.RaisePropertyChanged("XOrigin");
            }
        }

        [XmlIgnore]
        public bool XOriginSpecified
        {
            get
            {
                return this.xOriginFieldSpecified;
            }
            set
            {
                this.xOriginFieldSpecified = value;
                this.RaisePropertyChanged("XOriginSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public double YOrigin
        {
            get
            {
                return this.yOriginField;
            }
            set
            {
                this.yOriginField = value;
                this.RaisePropertyChanged("YOrigin");
            }
        }

        [XmlIgnore]
        public bool YOriginSpecified
        {
            get
            {
                return this.yOriginFieldSpecified;
            }
            set
            {
                this.yOriginFieldSpecified = value;
                this.RaisePropertyChanged("YOriginSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public double XYScale
        {
            get
            {
                return this.xYScaleField;
            }
            set
            {
                this.xYScaleField = value;
                this.RaisePropertyChanged("XYScale");
            }
        }

        [XmlIgnore]
        public bool XYScaleSpecified
        {
            get
            {
                return this.xYScaleFieldSpecified;
            }
            set
            {
                this.xYScaleFieldSpecified = value;
                this.RaisePropertyChanged("XYScaleSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public double ZOrigin
        {
            get
            {
                return this.zOriginField;
            }
            set
            {
                this.zOriginField = value;
                this.RaisePropertyChanged("ZOrigin");
            }
        }

        [XmlIgnore]
        public bool ZOriginSpecified
        {
            get
            {
                return this.zOriginFieldSpecified;
            }
            set
            {
                this.zOriginFieldSpecified = value;
                this.RaisePropertyChanged("ZOriginSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public double ZScale
        {
            get
            {
                return this.zScaleField;
            }
            set
            {
                this.zScaleField = value;
                this.RaisePropertyChanged("ZScale");
            }
        }

        [XmlIgnore]
        public bool ZScaleSpecified
        {
            get
            {
                return this.zScaleFieldSpecified;
            }
            set
            {
                this.zScaleFieldSpecified = value;
                this.RaisePropertyChanged("ZScaleSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public double MOrigin
        {
            get
            {
                return this.mOriginField;
            }
            set
            {
                this.mOriginField = value;
                this.RaisePropertyChanged("MOrigin");
            }
        }

        [XmlIgnore]
        public bool MOriginSpecified
        {
            get
            {
                return this.mOriginFieldSpecified;
            }
            set
            {
                this.mOriginFieldSpecified = value;
                this.RaisePropertyChanged("MOriginSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public double MScale
        {
            get
            {
                return this.mScaleField;
            }
            set
            {
                this.mScaleField = value;
                this.RaisePropertyChanged("MScale");
            }
        }

        [XmlIgnore]
        public bool MScaleSpecified
        {
            get
            {
                return this.mScaleFieldSpecified;
            }
            set
            {
                this.mScaleFieldSpecified = value;
                this.RaisePropertyChanged("MScaleSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public double XYTolerance
        {
            get
            {
                return this.xYToleranceField;
            }
            set
            {
                this.xYToleranceField = value;
                this.RaisePropertyChanged("XYTolerance");
            }
        }

        [XmlIgnore]
        public bool XYToleranceSpecified
        {
            get
            {
                return this.xYToleranceFieldSpecified;
            }
            set
            {
                this.xYToleranceFieldSpecified = value;
                this.RaisePropertyChanged("XYToleranceSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public double ZTolerance
        {
            get
            {
                return this.zToleranceField;
            }
            set
            {
                this.zToleranceField = value;
                this.RaisePropertyChanged("ZTolerance");
            }
        }

        [XmlIgnore]
        public bool ZToleranceSpecified
        {
            get
            {
                return this.zToleranceFieldSpecified;
            }
            set
            {
                this.zToleranceFieldSpecified = value;
                this.RaisePropertyChanged("ZToleranceSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public double MTolerance
        {
            get
            {
                return this.mToleranceField;
            }
            set
            {
                this.mToleranceField = value;
                this.RaisePropertyChanged("MTolerance");
            }
        }

        [XmlIgnore]
        public bool MToleranceSpecified
        {
            get
            {
                return this.mToleranceFieldSpecified;
            }
            set
            {
                this.mToleranceFieldSpecified = value;
                this.RaisePropertyChanged("MToleranceSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public bool HighPrecision
        {
            get
            {
                return this.highPrecisionField;
            }
            set
            {
                this.highPrecisionField = value;
                this.RaisePropertyChanged("HighPrecision");
            }
        }

        [XmlIgnore]
        public bool HighPrecisionSpecified
        {
            get
            {
                return this.highPrecisionFieldSpecified;
            }
            set
            {
                this.highPrecisionFieldSpecified = value;
                this.RaisePropertyChanged("HighPrecisionSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public double LeftLongitude
        {
            get
            {
                return this.leftLongitudeField;
            }
            set
            {
                this.leftLongitudeField = value;
                this.RaisePropertyChanged("LeftLongitude");
            }
        }

        [XmlIgnore]
        public bool LeftLongitudeSpecified
        {
            get
            {
                return this.leftLongitudeFieldSpecified;
            }
            set
            {
                this.leftLongitudeFieldSpecified = value;
                this.RaisePropertyChanged("LeftLongitudeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public int WKID
        {
            get
            {
                return this.wKIDField;
            }
            set
            {
                this.wKIDField = value;
                this.RaisePropertyChanged("WKID");
            }
        }

        [XmlIgnore]
        public bool WKIDSpecified
        {
            get
            {
                return this.wKIDFieldSpecified;
            }
            set
            {
                this.wKIDFieldSpecified = value;
                this.RaisePropertyChanged("WKIDSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
        public int LatestWKID
        {
            get
            {
                return this.latestWKIDField;
            }
            set
            {
                this.latestWKIDField = value;
                this.RaisePropertyChanged("LatestWKID");
            }
        }

        [XmlIgnore]
        public bool LatestWKIDSpecified
        {
            get
            {
                return this.latestWKIDFieldSpecified;
            }
            set
            {
                this.latestWKIDFieldSpecified = value;
                this.RaisePropertyChanged("LatestWKIDSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 15)]
        public int VCSWKID
        {
            get
            {
                return this.vCSWKIDField;
            }
            set
            {
                this.vCSWKIDField = value;
                this.RaisePropertyChanged("VCSWKID");
            }
        }

        [XmlIgnore]
        public bool VCSWKIDSpecified
        {
            get
            {
                return this.vCSWKIDFieldSpecified;
            }
            set
            {
                this.vCSWKIDFieldSpecified = value;
                this.RaisePropertyChanged("VCSWKIDSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 16)]
        public int LatestVCSWKID
        {
            get
            {
                return this.latestVCSWKIDField;
            }
            set
            {
                this.latestVCSWKIDField = value;
                this.RaisePropertyChanged("LatestVCSWKID");
            }
        }

        [XmlIgnore]
        public bool LatestVCSWKIDSpecified
        {
            get
            {
                return this.latestVCSWKIDFieldSpecified;
            }
            set
            {
                this.latestVCSWKIDFieldSpecified = value;
                this.RaisePropertyChanged("LatestVCSWKIDSpecified");
            }
        }

        // (add) Token: 0x0600020A RID: 522 RVA: 0x00007D74 File Offset: 0x00005F74
        // (remove) Token: 0x0600020B RID: 523 RVA: 0x00007DAC File Offset: 0x00005FAC
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            bool flag = propertyChanged != null;
            if (flag)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string wKTField;

        private double xOriginField;

        private bool xOriginFieldSpecified;

        private double yOriginField;

        private bool yOriginFieldSpecified;

        private double xYScaleField;

        private bool xYScaleFieldSpecified;

        private double zOriginField;

        private bool zOriginFieldSpecified;

        private double zScaleField;

        private bool zScaleFieldSpecified;

        private double mOriginField;

        private bool mOriginFieldSpecified;

        private double mScaleField;

        private bool mScaleFieldSpecified;

        private double xYToleranceField;

        private bool xYToleranceFieldSpecified;

        private double zToleranceField;

        private bool zToleranceFieldSpecified;

        private double mToleranceField;

        private bool mToleranceFieldSpecified;

        private bool highPrecisionField;

        private bool highPrecisionFieldSpecified;

        private double leftLongitudeField;

        private bool leftLongitudeFieldSpecified;

        private int wKIDField;

        private bool wKIDFieldSpecified;

        private int latestWKIDField;

        private bool latestWKIDFieldSpecified;

        private int vCSWKIDField;

        private bool vCSWKIDFieldSpecified;

        private int latestVCSWKIDField;

        private bool latestVCSWKIDFieldSpecified;
    }
}