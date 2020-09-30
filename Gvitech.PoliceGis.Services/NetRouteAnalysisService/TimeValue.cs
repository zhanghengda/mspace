using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(TimeExtent))]
    [XmlInclude(typeof(TimeInstant))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public class TimeValue : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public TimeReference TimeReference
        {
            get
            {
                return this.timeReferenceField;
            }
            set
            {
                this.timeReferenceField = value;
                this.RaisePropertyChanged("TimeReference");
            }
        }

        // (add) Token: 0x0600083F RID: 2111 RVA: 0x00011318 File Offset: 0x0000F518
        // (remove) Token: 0x06000840 RID: 2112 RVA: 0x00011350 File Offset: 0x0000F550
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

        private TimeReference timeReferenceField;
    }
}