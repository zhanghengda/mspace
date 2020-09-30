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
    public class PropertySet : INotifyPropertyChanged
    {
        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 0)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public PropertySetProperty[] PropertyArray
        {
            get
            {
                return this.propertyArrayField;
            }
            set
            {
                this.propertyArrayField = value;
                this.RaisePropertyChanged("PropertyArray");
            }
        }

        // (add) Token: 0x06000A5E RID: 2654 RVA: 0x00014BB8 File Offset: 0x00012DB8
        // (remove) Token: 0x06000A5F RID: 2655 RVA: 0x00014BF0 File Offset: 0x00012DF0
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

        private PropertySetProperty[] propertyArrayField;
    }
}