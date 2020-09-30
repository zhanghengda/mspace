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
    public class MultiPartColorRamp : ColorRamp
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int NumColorRamps
        {
            get
            {
                return this.numColorRampsField;
            }
            set
            {
                this.numColorRampsField = value;
                base.RaisePropertyChanged("NumColorRamps");
            }
        }

        [XmlIgnore]
        public bool NumColorRampsSpecified
        {
            get
            {
                return this.numColorRampsFieldSpecified;
            }
            set
            {
                this.numColorRampsFieldSpecified = value;
                base.RaisePropertyChanged("NumColorRampsSpecified");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 1)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public ColorRamp[] ColorRamps
        {
            get
            {
                return this.colorRampsField;
            }
            set
            {
                this.colorRampsField = value;
                base.RaisePropertyChanged("ColorRamps");
            }
        }

        private int numColorRampsField;

        private bool numColorRampsFieldSpecified;

        private ColorRamp[] colorRampsField;
    }
}