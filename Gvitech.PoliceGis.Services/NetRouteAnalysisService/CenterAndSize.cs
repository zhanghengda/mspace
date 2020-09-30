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
    public class CenterAndSize : MapArea
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public Point Center
        {
            get
            {
                return this.centerField;
            }
            set
            {
                this.centerField = value;
                base.RaisePropertyChanged("Center");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public double Height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
                base.RaisePropertyChanged("Height");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public double Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
                base.RaisePropertyChanged("Width");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public string Units
        {
            get
            {
                return this.unitsField;
            }
            set
            {
                this.unitsField = value;
                base.RaisePropertyChanged("Units");
            }
        }

        private Point centerField;

        private double heightField;

        private double widthField;

        private string unitsField;
    }
}