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
    public class QueryTableDataSourceDescription : DataSourceDescription
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string Query
        {
            get
            {
                return this.queryField;
            }
            set
            {
                this.queryField = value;
                base.RaisePropertyChanged("Query");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string OIDFields
        {
            get
            {
                return this.oIDFieldsField;
            }
            set
            {
                this.oIDFieldsField = value;
                base.RaisePropertyChanged("OIDFields");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public esriGeometryType GeometryType
        {
            get
            {
                return this.geometryTypeField;
            }
            set
            {
                this.geometryTypeField = value;
                base.RaisePropertyChanged("GeometryType");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
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

        private string queryField;

        private string oIDFieldsField;

        private esriGeometryType geometryTypeField;

        private SpatialReference spatialReferenceField;
    }
}