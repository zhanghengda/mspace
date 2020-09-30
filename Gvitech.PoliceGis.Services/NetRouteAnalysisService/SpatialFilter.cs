using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(TimeQueryFilter))]
    [XmlInclude(typeof(ImageQueryFilter))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public class SpatialFilter : QueryFilter
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public esriSearchOrder SearchOrder
        {
            get
            {
                return this.searchOrderField;
            }
            set
            {
                this.searchOrderField = value;
                base.RaisePropertyChanged("SearchOrder");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public esriSpatialRelEnum SpatialRel
        {
            get
            {
                return this.spatialRelField;
            }
            set
            {
                this.spatialRelField = value;
                base.RaisePropertyChanged("SpatialRel");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public string SpatialRelDescription
        {
            get
            {
                return this.spatialRelDescriptionField;
            }
            set
            {
                this.spatialRelDescriptionField = value;
                base.RaisePropertyChanged("SpatialRelDescription");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public Geometry FilterGeometry
        {
            get
            {
                return this.filterGeometryField;
            }
            set
            {
                this.filterGeometryField = value;
                base.RaisePropertyChanged("FilterGeometry");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public string GeometryFieldName
        {
            get
            {
                return this.geometryFieldNameField;
            }
            set
            {
                this.geometryFieldNameField = value;
                base.RaisePropertyChanged("GeometryFieldName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public bool FilterOwnsGeometry
        {
            get
            {
                return this.filterOwnsGeometryField;
            }
            set
            {
                this.filterOwnsGeometryField = value;
                base.RaisePropertyChanged("FilterOwnsGeometry");
            }
        }

        private esriSearchOrder searchOrderField;

        private esriSpatialRelEnum spatialRelField;

        private string spatialRelDescriptionField;

        private Geometry filterGeometryField;

        private string geometryFieldNameField;

        private bool filterOwnsGeometryField;
    }
}