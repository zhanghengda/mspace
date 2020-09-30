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
    public class GrayColor : Color
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public byte GrayLevel
        {
            get
            {
                return this.grayLevelField;
            }
            set
            {
                this.grayLevelField = value;
                base.RaisePropertyChanged("GrayLevel");
            }
        }

        private byte grayLevelField;
    }
}