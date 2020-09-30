using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(RasterDataSourceDescription))]
    [XmlInclude(typeof(QueryTableDataSourceDescription))]
    [XmlInclude(typeof(TableDataSourceDescription))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public class DataSourceDescription : MapServerSourceDescription
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string WorkspaceID
        {
            get
            {
                return this.workspaceIDField;
            }
            set
            {
                this.workspaceIDField = value;
                base.RaisePropertyChanged("WorkspaceID");
            }
        }

        private string workspaceIDField;
    }
}