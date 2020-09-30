using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(ImageQueryFilter))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public class TimeQueryFilter : SpatialFilter
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public TimeValue TimeValue
        {
            get
            {
                return this.timeValueField;
            }
            set
            {
                this.timeValueField = value;
                base.RaisePropertyChanged("TimeValue");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public TimeReference OutputTimeReference
        {
            get
            {
                return this.outputTimeReferenceField;
            }
            set
            {
                this.outputTimeReferenceField = value;
                base.RaisePropertyChanged("OutputTimeReference");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public esriTimeRelation TimeRelation
        {
            get
            {
                return this.timeRelationField;
            }
            set
            {
                this.timeRelationField = value;
                base.RaisePropertyChanged("TimeRelation");
            }
        }

        private TimeValue timeValueField;

        private TimeReference outputTimeReferenceField;

        private esriTimeRelation timeRelationField;
    }
}