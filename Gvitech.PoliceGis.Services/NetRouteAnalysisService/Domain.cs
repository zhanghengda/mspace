using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(CodedValueDomain))]
    [XmlInclude(typeof(BitMaskCodedValueDomain))]
    [XmlInclude(typeof(RangeDomain))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class Domain : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string DomainName
        {
            get
            {
                return this.domainNameField;
            }
            set
            {
                this.domainNameField = value;
                this.RaisePropertyChanged("DomainName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public esriFieldType FieldType
        {
            get
            {
                return this.fieldTypeField;
            }
            set
            {
                this.fieldTypeField = value;
                this.RaisePropertyChanged("FieldType");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public esriMergePolicyType MergePolicy
        {
            get
            {
                return this.mergePolicyField;
            }
            set
            {
                this.mergePolicyField = value;
                this.RaisePropertyChanged("MergePolicy");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public esriSplitPolicyType SplitPolicy
        {
            get
            {
                return this.splitPolicyField;
            }
            set
            {
                this.splitPolicyField = value;
                this.RaisePropertyChanged("SplitPolicy");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
                this.RaisePropertyChanged("Description");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public string Owner
        {
            get
            {
                return this.ownerField;
            }
            set
            {
                this.ownerField = value;
                this.RaisePropertyChanged("Owner");
            }
        }

        // (add) Token: 0x06000976 RID: 2422 RVA: 0x000134A8 File Offset: 0x000116A8
        // (remove) Token: 0x06000977 RID: 2423 RVA: 0x000134E0 File Offset: 0x000116E0
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

        private string domainNameField;

        private esriFieldType fieldTypeField;

        private esriMergePolicyType mergePolicyField;

        private esriSplitPolicyType splitPolicyField;

        private string descriptionField;

        private string ownerField;
    }
}