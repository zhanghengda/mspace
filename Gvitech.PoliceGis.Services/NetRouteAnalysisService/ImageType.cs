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
    public class ImageType : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public esriImageFormat ImageFormat
        {
            get
            {
                return this.imageFormatField;
            }
            set
            {
                this.imageFormatField = value;
                this.RaisePropertyChanged("ImageFormat");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public esriImageReturnType ImageReturnType
        {
            get
            {
                return this.imageReturnTypeField;
            }
            set
            {
                this.imageReturnTypeField = value;
                this.RaisePropertyChanged("ImageReturnType");
            }
        }

        // (add) Token: 0x06000861 RID: 2145 RVA: 0x000116B0 File Offset: 0x0000F8B0
        // (remove) Token: 0x06000862 RID: 2146 RVA: 0x000116E8 File Offset: 0x0000F8E8
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

        private esriImageFormat imageFormatField;

        private esriImageReturnType imageReturnTypeField;
    }
}