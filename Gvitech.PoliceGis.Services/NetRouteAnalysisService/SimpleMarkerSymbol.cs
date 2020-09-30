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
    public class SimpleMarkerSymbol : MarkerSymbol
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public bool Outline
        {
            get
            {
                return this.outlineField;
            }
            set
            {
                this.outlineField = value;
                base.RaisePropertyChanged("Outline");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public double OutlineSize
        {
            get
            {
                return this.outlineSizeField;
            }
            set
            {
                this.outlineSizeField = value;
                base.RaisePropertyChanged("OutlineSize");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public Color OutlineColor
        {
            get
            {
                return this.outlineColorField;
            }
            set
            {
                this.outlineColorField = value;
                base.RaisePropertyChanged("OutlineColor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public esriSimpleMarkerStyle Style
        {
            get
            {
                return this.styleField;
            }
            set
            {
                this.styleField = value;
                base.RaisePropertyChanged("Style");
            }
        }

        private bool outlineField;

        private double outlineSizeField;

        private Color outlineColorField;

        private esriSimpleMarkerStyle styleField;
    }
}