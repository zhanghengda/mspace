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
    public class FeatureExtent : MapArea
    {
        public FeatureExtent()
        {
            this.expandRatioField = 1.0;
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public double DefaultScale
        {
            get
            {
                return this.defaultScaleField;
            }
            set
            {
                this.defaultScaleField = value;
                base.RaisePropertyChanged("DefaultScale");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public double ExpandRatio
        {
            get
            {
                return this.expandRatioField;
            }
            set
            {
                this.expandRatioField = value;
                base.RaisePropertyChanged("ExpandRatio");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 2)]
        [XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public int[] FeatureIDs
        {
            get
            {
                return this.featureIDsField;
            }
            set
            {
                this.featureIDsField = value;
                base.RaisePropertyChanged("FeatureIDs");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public int LayerID
        {
            get
            {
                return this.layerIDField;
            }
            set
            {
                this.layerIDField = value;
                base.RaisePropertyChanged("LayerID");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public string MapName
        {
            get
            {
                return this.mapNameField;
            }
            set
            {
                this.mapNameField = value;
                base.RaisePropertyChanged("MapName");
            }
        }

        private double defaultScaleField;

        private double expandRatioField;

        private int[] featureIDsField;

        private int layerIDField;

        private string mapNameField;
    }
}