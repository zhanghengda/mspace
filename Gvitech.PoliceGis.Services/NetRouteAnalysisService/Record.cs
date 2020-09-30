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
    public class Record : INotifyPropertyChanged
    {
        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 0)]
        [XmlArrayItem("Value", Form = XmlSchemaForm.Unqualified)]
        public object[] Values
        {
            get
            {
                return this.valuesField;
            }
            set
            {
                this.valuesField = value;
                this.RaisePropertyChanged("Values");
            }
        }

        // (add) Token: 0x0600099D RID: 2461 RVA: 0x000138D8 File Offset: 0x00011AD8
        // (remove) Token: 0x0600099E RID: 2462 RVA: 0x00013910 File Offset: 0x00011B10
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

        private object[] valuesField;
    }
}