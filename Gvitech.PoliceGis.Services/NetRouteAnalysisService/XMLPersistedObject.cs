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
    public class XMLPersistedObject : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "base64Binary", Order = 0)]
        public byte[] Bytes
        {
            get
            {
                return this.bytesField;
            }
            set
            {
                this.bytesField = value;
                this.RaisePropertyChanged("Bytes");
            }
        }

        // (add) Token: 0x06000348 RID: 840 RVA: 0x000098CC File Offset: 0x00007ACC
        // (remove) Token: 0x06000349 RID: 841 RVA: 0x00009904 File Offset: 0x00007B04
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

        private byte[] bytesField;
    }
}