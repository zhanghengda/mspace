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
    public class GeometryDef : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int AvgNumPoints
        {
            get
            {
                return this.avgNumPointsField;
            }
            set
            {
                this.avgNumPointsField = value;
                this.RaisePropertyChanged("AvgNumPoints");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public esriGeometryType GeometryType
        {
            get
            {
                return this.geometryTypeField;
            }
            set
            {
                this.geometryTypeField = value;
                this.RaisePropertyChanged("GeometryType");
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
                this.RaisePropertyChanged("HasM");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public bool HasZ
        {
            get
            {
                return this.hasZField;
            }
            set
            {
                this.hasZField = value;
                this.RaisePropertyChanged("HasZ");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public SpatialReference SpatialReference
        {
            get
            {
                return this.spatialReferenceField;
            }
            set
            {
                this.spatialReferenceField = value;
                this.RaisePropertyChanged("SpatialReference");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public double GridSize0
        {
            get
            {
                return this.gridSize0Field;
            }
            set
            {
                this.gridSize0Field = value;
                this.RaisePropertyChanged("GridSize0");
            }
        }

        [XmlIgnore]
        public bool GridSize0Specified
        {
            get
            {
                return this.gridSize0FieldSpecified;
            }
            set
            {
                this.gridSize0FieldSpecified = value;
                this.RaisePropertyChanged("GridSize0Specified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public double GridSize1
        {
            get
            {
                return this.gridSize1Field;
            }
            set
            {
                this.gridSize1Field = value;
                this.RaisePropertyChanged("GridSize1");
            }
        }

        [XmlIgnore]
        public bool GridSize1Specified
        {
            get
            {
                return this.gridSize1FieldSpecified;
            }
            set
            {
                this.gridSize1FieldSpecified = value;
                this.RaisePropertyChanged("GridSize1Specified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public double GridSize2
        {
            get
            {
                return this.gridSize2Field;
            }
            set
            {
                this.gridSize2Field = value;
                this.RaisePropertyChanged("GridSize2");
            }
        }

        [XmlIgnore]
        public bool GridSize2Specified
        {
            get
            {
                return this.gridSize2FieldSpecified;
            }
            set
            {
                this.gridSize2FieldSpecified = value;
                this.RaisePropertyChanged("GridSize2Specified");
            }
        }

        // (add) Token: 0x06000966 RID: 2406 RVA: 0x000132E8 File Offset: 0x000114E8
        // (remove) Token: 0x06000967 RID: 2407 RVA: 0x00013320 File Offset: 0x00011520
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

        private int avgNumPointsField;

        private esriGeometryType geometryTypeField;

        private bool hasMField;

        private bool hasZField;

        private SpatialReference spatialReferenceField;

        private double gridSize0Field;

        private bool gridSize0FieldSpecified;

        private double gridSize1Field;

        private bool gridSize1FieldSpecified;

        private double gridSize2Field;

        private bool gridSize2FieldSpecified;
    }
}