using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(PictureMarkerSymbol))]
    [XmlInclude(typeof(CharacterMarkerSymbol))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class CartographicMarkerSymbol : MarkerSymbol
    {
        public CartographicMarkerSymbol()
        {
            this.xScaleField = 1.0;
            this.yScaleField = 1.0;
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public double XScale
        {
            get
            {
                return this.xScaleField;
            }
            set
            {
                this.xScaleField = value;
                base.RaisePropertyChanged("XScale");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public double YScale
        {
            get
            {
                return this.yScaleField;
            }
            set
            {
                this.yScaleField = value;
                base.RaisePropertyChanged("YScale");
            }
        }

        private double xScaleField;

        private double yScaleField;
    }
}