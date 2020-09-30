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
    public class NAAttributeParameterValue : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string AttributeName
        {
            get
            {
                return this.attributeNameField;
            }
            set
            {
                this.attributeNameField = value;
                this.RaisePropertyChanged("AttributeName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string ParameterName
        {
            get
            {
                return this.parameterNameField;
            }
            set
            {
                this.parameterNameField = value;
                this.RaisePropertyChanged("ParameterName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public long VarType
        {
            get
            {
                return this.varTypeField;
            }
            set
            {
                this.varTypeField = value;
                this.RaisePropertyChanged("VarType");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, IsNullable = true, Order = 3)]
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

        // (add) Token: 0x06000A14 RID: 2580 RVA: 0x000143C8 File Offset: 0x000125C8
        // (remove) Token: 0x06000A15 RID: 2581 RVA: 0x00014400 File Offset: 0x00012600
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

        private string attributeNameField;

        private string parameterNameField;

        private long varTypeField;

        private object valueField;
    }
}