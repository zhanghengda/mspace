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
    public class TableDataSourceDescription : DataSourceDescription
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string TableName
        {
            get
            {
                return this.tableNameField;
            }
            set
            {
                this.tableNameField = value;
                base.RaisePropertyChanged("TableName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string VersionName
        {
            get
            {
                return this.versionNameField;
            }
            set
            {
                this.versionNameField = value;
                base.RaisePropertyChanged("VersionName");
            }
        }

        private string tableNameField;

        private string versionNameField;
    }
}