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
    public class ImageDescription : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public ImageType ImageType
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public ImageDisplay ImageDisplay
        {
            get
            {
                return this.imageDisplayField;
            }
            set
            {
                this.imageDisplayField = value;
                this.RaisePropertyChanged("ImageDisplay");
            }
        }

        // (add) Token: 0x06000869 RID: 2153 RVA: 0x000117B0 File Offset: 0x0000F9B0
        // (remove) Token: 0x0600086A RID: 2154 RVA: 0x000117E8 File Offset: 0x0000F9E8
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

        private ImageType imageTypeField;

        private ImageDisplay imageDisplayField;
    }
}