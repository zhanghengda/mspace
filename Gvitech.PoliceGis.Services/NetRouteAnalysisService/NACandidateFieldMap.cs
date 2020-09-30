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
    public class NACandidateFieldMap : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string FieldName
        {
            get
            {
                return this.fieldNameField;
            }
            set
            {
                this.fieldNameField = value;
                this.RaisePropertyChanged("FieldName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public object DefaultValue
        {
            get
            {
                return this.defaultValueField;
            }
            set
            {
                this.defaultValueField = value;
                this.RaisePropertyChanged("DefaultValue");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 2)]
        [XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public string[] CandidateFieldNames
        {
            get
            {
                return this.candidateFieldNamesField;
            }
            set
            {
                this.candidateFieldNamesField = value;
                this.RaisePropertyChanged("CandidateFieldNames");
            }
        }

        // (add) Token: 0x06000171 RID: 369 RVA: 0x00006E44 File Offset: 0x00005044
        // (remove) Token: 0x06000172 RID: 370 RVA: 0x00006E7C File Offset: 0x0000507C
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

        private string fieldNameField;

        private object defaultValueField;

        private string[] candidateFieldNamesField;
    }
}