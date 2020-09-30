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
    public class Fields : INotifyPropertyChanged
    {
        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 0)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public Field[] FieldArray
        {
            get
            {
                return this.fieldArrayField;
            }
            set
            {
                this.fieldArrayField = value;
                this.RaisePropertyChanged("FieldArray");
            }
        }

        // (add) Token: 0x06000926 RID: 2342 RVA: 0x00012C58 File Offset: 0x00010E58
        // (remove) Token: 0x06000927 RID: 2343 RVA: 0x00012C90 File Offset: 0x00010E90
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

        private Field[] fieldArrayField;
    }
}