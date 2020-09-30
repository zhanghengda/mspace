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
    public class MapTableSourceDescription : MapServerSourceDescription
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int MapTableID
        {
            get
            {
                return this.mapTableIDField;
            }
            set
            {
                this.mapTableIDField = value;
                base.RaisePropertyChanged("MapTableID");
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

        private int mapTableIDField;

        private string versionNameField;
    }
}