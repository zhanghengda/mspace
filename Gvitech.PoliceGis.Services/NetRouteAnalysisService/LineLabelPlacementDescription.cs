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
    public class LineLabelPlacementDescription : LabelPlacementDescription
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public esriServerLineLabelPlacementType Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
                base.RaisePropertyChanged("Type");
            }
        }

        private esriServerLineLabelPlacementType typeField;
    }
}