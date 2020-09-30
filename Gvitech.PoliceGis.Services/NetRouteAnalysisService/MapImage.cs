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
    public class MapImage : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "base64Binary", Order = 0)]
        public byte[] ImageData
        {
            get
            {
                return this.imageDataField;
            }
            set
            {
                this.imageDataField = value;
                this.RaisePropertyChanged("ImageData");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string ImageURL
        {
            get
            {
                return this.imageURLField;
            }
            set
            {
                this.imageURLField = value;
                this.RaisePropertyChanged("ImageURL");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public Envelope Extent
        {
            get
            {
                return this.extentField;
            }
            set
            {
                this.extentField = value;
                this.RaisePropertyChanged("Extent");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 3)]
        [XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public int[] VisibleLayerIDs
        {
            get
            {
                return this.visibleLayerIDsField;
            }
            set
            {
                this.visibleLayerIDsField = value;
                this.RaisePropertyChanged("VisibleLayerIDs");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public double MapScale
        {
            get
            {
                return this.mapScaleField;
            }
            set
            {
                this.mapScaleField = value;
                this.RaisePropertyChanged("MapScale");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public string ImageType
        {
            get
            {
                return this.imageTypeField;
            }
            set
            {
                this.imageTypeField = value;
                this.RaisePropertyChanged("ImageType");
            }
        }

        // (add) Token: 0x060008FD RID: 2301 RVA: 0x000127D0 File Offset: 0x000109D0
        // (remove) Token: 0x060008FE RID: 2302 RVA: 0x00012808 File Offset: 0x00010A08
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

        private byte[] imageDataField;

        private string imageURLField;

        private Envelope extentField;

        private int[] visibleLayerIDsField;

        private double mapScaleField;

        private int imageHeightField;

        private int imageWidthField;

        private double imageDPIField;

        private string imageTypeField;
    }
}