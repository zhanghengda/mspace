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
    public class PropertySetProperty : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string Key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
                this.RaisePropertyChanged("Key");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, IsNullable = true, Order = 1)]
        public object Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
                this.RaisePropertyChanged("Value");
            }
        }

        // (add) Token: 0x06000A58 RID: 2648 RVA: 0x00014AE8 File Offset: 0x00012CE8
        // (remove) Token: 0x06000A59 RID: 2649 RVA: 0x00014B20 File Offset: 0x00012D20
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

        private string keyField;

        private object valueField;
    }
}