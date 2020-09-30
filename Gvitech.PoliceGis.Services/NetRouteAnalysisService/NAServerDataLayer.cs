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
    public class NAServerDataLayer : NAServerLocations
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string DataLayerName
        {
            get
            {
                return this.dataLayerNameField;
            }
            set
            {
                this.dataLayerNameField = value;
                base.RaisePropertyChanged("DataLayerName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public QueryFilter QueryFilter
        {
            get
            {
                return this.queryFilterField;
            }
            set
            {
                this.queryFilterField = value;
                base.RaisePropertyChanged("QueryFilter");
            }
        }

        private string dataLayerNameField;

        private QueryFilter queryFilterField;
    }
}