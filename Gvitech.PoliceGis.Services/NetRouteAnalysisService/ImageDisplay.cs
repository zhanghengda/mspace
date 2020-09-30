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
    public class ImageDisplay : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int ImageHeight
        {
            get
            {
                return this.imageHeightField;
            }
            set
            {
                this.imageHeightField = value;
                this.RaisePropertyChanged("ImageHeight");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public int ImageWidth
        {
            get
            {
                return this.imageWidthField;
            }
            set
            {
                this.imageWidthField = value;
                this.RaisePropertyChanged("ImageWidth");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public double ImageDPI
        {
            get
            {
                return this.imageDPIField;
            }
            set
            {
                this.imageDPIField = value;
                this.RaisePropertyChanged("ImageDPI");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public Color TransparentColor
        {
            get
            {
                return this.transparentColorField;
            }
            set
            {
                this.transparentColorField = value;
                this.RaisePropertyChanged("TransparentColor");
            }
        }

        // (add) Token: 0x06000859 RID: 2137 RVA: 0x000115B0 File Offset: 0x0000F7B0
        // (remove) Token: 0x0600085A RID: 2138 RVA: 0x000115E8 File Offset: 0x0000F7E8
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

        private int imageHeightField;

        private int imageWidthField;

        private double imageDPIField;

        private Color transparentColorField;
    }
}