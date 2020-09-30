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
    public class GPMessage : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int MessageType
        {
            get
            {
                return this.messageTypeField;
            }
            set
            {
                this.messageTypeField = value;
                this.RaisePropertyChanged("MessageType");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public int MessageCode
        {
            get
            {
                return this.messageCodeField;
            }
            set
            {
                this.messageCodeField = value;
                this.RaisePropertyChanged("MessageCode");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public string MessageDesc
        {
            get
            {
                return this.messageDescField;
            }
            set
            {
                this.messageDescField = value;
                this.RaisePropertyChanged("MessageDesc");
            }
        }

        // (add) Token: 0x060008E1 RID: 2273 RVA: 0x000124B0 File Offset: 0x000106B0
        // (remove) Token: 0x060008E2 RID: 2274 RVA: 0x000124E8 File Offset: 0x000106E8
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

        private int messageTypeField;

        private int messageCodeField;

        private string messageDescField;
    }
}