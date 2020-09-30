using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(HsvColor))]
    [XmlInclude(typeof(HlsColor))]
    [XmlInclude(typeof(CmykColor))]
    [XmlInclude(typeof(RgbColor))]
    [XmlInclude(typeof(GrayColor))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class Color : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public bool UseWindowsDithering
        {
            get
            {
                return this.useWindowsDitheringField;
            }
            set
            {
                this.useWindowsDitheringField = value;
                this.RaisePropertyChanged("UseWindowsDithering");
            }
        }

        [XmlIgnore]
        public bool UseWindowsDitheringSpecified
        {
            get
            {
                return this.useWindowsDitheringFieldSpecified;
            }
            set
            {
                this.useWindowsDitheringFieldSpecified = value;
                this.RaisePropertyChanged("UseWindowsDitheringSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public byte AlphaValue
        {
            get
            {
                return this.alphaValueField;
            }
            set
            {
                this.alphaValueField = value;
                this.RaisePropertyChanged("AlphaValue");
            }
        }

        [XmlIgnore]
        public bool AlphaValueSpecified
        {
            get
            {
                return this.alphaValueFieldSpecified;
            }
            set
            {
                this.alphaValueFieldSpecified = value;
                this.RaisePropertyChanged("AlphaValueSpecified");
            }
        }

        // (add) Token: 0x06000312 RID: 786 RVA: 0x000093BC File Offset: 0x000075BC
        // (remove) Token: 0x06000313 RID: 787 RVA: 0x000093F4 File Offset: 0x000075F4
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            bool flag = propertyChanged != null;
            if (flag)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool useWindowsDitheringField;

        private bool useWindowsDitheringFieldSpecified;

        private byte alphaValueField;

        private bool alphaValueFieldSpecified;
    }
}