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
    public class MapDescription : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("Name");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public MapArea MapArea
        {
            get
            {
                return this.mapAreaField;
            }
            set
            {
                this.mapAreaField = value;
                this.RaisePropertyChanged("MapArea");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 2)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public LayerDescription[] LayerDescriptions
        {
            get
            {
                return this.layerDescriptionsField;
            }
            set
            {
                this.layerDescriptionsField = value;
                this.RaisePropertyChanged("LayerDescriptions");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public double Rotation
        {
            get
            {
                return this.rotationField;
            }
            set
            {
                this.rotationField = value;
                this.RaisePropertyChanged("Rotation");
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
        public Color TransparentColor
        {
            get
            {
                return this.transparentColorField;
            }
            set
            {
                this.transparentColorField = value;
                this.RaisePropertyChanged("TransparentColor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public Color SelectionColor
        {
            get
            {
                return this.selectionColorField;
            }
            set
            {
                this.selectionColorField = value;
                this.RaisePropertyChanged("SelectionColor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public FillSymbol BackgroundSymbol
        {
            get
            {
                return this.backgroundSymbolField;
            }
            set
            {
                this.backgroundSymbolField = value;
                this.RaisePropertyChanged("BackgroundSymbol");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 8)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public GraphicElement[] CustomGraphics
        {
            get
            {
                return this.customGraphicsField;
            }
            set
            {
                this.customGraphicsField = value;
                this.RaisePropertyChanged("CustomGraphics");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public GeoTransformation GeoTransformation
        {
            get
            {
                return this.geoTransformationField;
            }
            set
            {
                this.geoTransformationField = value;
                this.RaisePropertyChanged("GeoTransformation");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public TimeReference TimeReference
        {
            get
            {
                return this.timeReferenceField;
            }
            set
            {
                this.timeReferenceField = value;
                this.RaisePropertyChanged("TimeReference");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public TimeValue TimeValue
        {
            get
            {
                return this.timeValueField;
            }
            set
            {
                this.timeValueField = value;
                this.RaisePropertyChanged("TimeValue");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public bool HonorLayerReordering
        {
            get
            {
                return this.honorLayerReorderingField;
            }
            set
            {
                this.honorLayerReorderingField = value;
                this.RaisePropertyChanged("HonorLayerReordering");
            }
        }

        [XmlIgnore]
        public bool HonorLayerReorderingSpecified
        {
            get
            {
                return this.honorLayerReorderingFieldSpecified;
            }
            set
            {
                this.honorLayerReorderingFieldSpecified = value;
                this.RaisePropertyChanged("HonorLayerReorderingSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public esriTimeRelation TimeRelation
        {
            get
            {
                return this.timeRelationField;
            }
            set
            {
                this.timeRelationField = value;
                this.RaisePropertyChanged("TimeRelation");
            }
        }

        [XmlIgnore]
        public bool TimeRelationSpecified
        {
            get
            {
                return this.timeRelationFieldSpecified;
            }
            set
            {
                this.timeRelationFieldSpecified = value;
                this.RaisePropertyChanged("TimeRelationSpecified");
            }
        }

        // (add) Token: 0x0600019F RID: 415 RVA: 0x00007314 File Offset: 0x00005514
        // (remove) Token: 0x060001A0 RID: 416 RVA: 0x0000734C File Offset: 0x0000554C
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

        private string nameField;

        private MapArea mapAreaField;

        private LayerDescription[] layerDescriptionsField;

        private double rotationField;

        private SpatialReference spatialReferenceField;

        private Color transparentColorField;

        private Color selectionColorField;

        private FillSymbol backgroundSymbolField;

        private GraphicElement[] customGraphicsField;

        private GeoTransformation geoTransformationField;

        private TimeReference timeReferenceField;

        private TimeValue timeValueField;

        private bool honorLayerReorderingField;

        private bool honorLayerReorderingFieldSpecified;

        private esriTimeRelation timeRelationField;

        private bool timeRelationFieldSpecified;
    }
}