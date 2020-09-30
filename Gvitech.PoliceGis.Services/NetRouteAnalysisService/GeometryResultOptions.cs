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
    public class GeometryResultOptions : INotifyPropertyChanged
    {
        public GeometryResultOptions()
        {
            this.densifyGeometriesField = false;
            this.maximumSegmentLengthField = -1.0;
            this.maximumDeviationField = 0.0;
            this.generalizeGeometriesField = false;
            this.maximumAllowableOffsetField = 0.0;
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        [DefaultValue(false)]
        public bool DensifyGeometries
        {
            get
            {
                return this.densifyGeometriesField;
            }
            set
            {
                this.densifyGeometriesField = value;
                this.RaisePropertyChanged("DensifyGeometries");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        [DefaultValue(-1.0)]
        public double MaximumSegmentLength
        {
            get
            {
                return this.maximumSegmentLengthField;
            }
            set
            {
                this.maximumSegmentLengthField = value;
                this.RaisePropertyChanged("MaximumSegmentLength");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        [DefaultValue(0.0)]
        public double MaximumDeviation
        {
            get
            {
                return this.maximumDeviationField;
            }
            set
            {
                this.maximumDeviationField = value;
                this.RaisePropertyChanged("MaximumDeviation");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        [DefaultValue(false)]
        public bool GeneralizeGeometries
        {
            get
            {
                return this.generalizeGeometriesField;
            }
            set
            {
                this.generalizeGeometriesField = value;
                this.RaisePropertyChanged("GeneralizeGeometries");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        [DefaultValue(0.0)]
        public double MaximumAllowableOffset
        {
            get
            {
                return this.maximumAllowableOffsetField;
            }
            set
            {
                this.maximumAllowableOffsetField = value;
                this.RaisePropertyChanged("MaximumAllowableOffset");
            }
        }

        // (add) Token: 0x06000488 RID: 1160 RVA: 0x0000B740 File Offset: 0x00009940
        // (remove) Token: 0x06000489 RID: 1161 RVA: 0x0000B778 File Offset: 0x00009978
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

        private bool densifyGeometriesField;

        private double maximumSegmentLengthField;

        private double maximumDeviationField;

        private bool generalizeGeometriesField;

        private double maximumAllowableOffsetField;
    }
}