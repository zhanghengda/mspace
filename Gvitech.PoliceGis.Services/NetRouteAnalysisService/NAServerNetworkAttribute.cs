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
    public class NAServerNetworkAttribute : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("Name");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public esriNetworkAttributeUnits Units
        {
            get
            {
                return this.unitsField;
            }
            set
            {
                this.unitsField = value;
                this.RaisePropertyChanged("Units");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public esriNetworkAttributeDataType DataType
        {
            get
            {
                return this.dataTypeField;
            }
            set
            {
                this.dataTypeField = value;
                this.RaisePropertyChanged("DataType");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public esriNetworkAttributeUsageType UsageType
        {
            get
            {
                return this.usageTypeField;
            }
            set
            {
                this.usageTypeField = value;
                this.RaisePropertyChanged("UsageType");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 4)]
        [XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public string[] ParameterNames
        {
            get
            {
                return this.parameterNamesField;
            }
            set
            {
                this.parameterNamesField = value;
                this.RaisePropertyChanged("ParameterNames");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public string RestrictionUsageParameterName
        {
            get
            {
                return this.restrictionUsageParameterNameField;
            }
            set
            {
                this.restrictionUsageParameterNameField = value;
                this.RaisePropertyChanged("RestrictionUsageParameterName");
            }
        }

        // (add) Token: 0x06000A9D RID: 2717 RVA: 0x00015260 File Offset: 0x00013460
        // (remove) Token: 0x06000A9E RID: 2718 RVA: 0x00015298 File Offset: 0x00013498
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

        private string nameField;

        private esriNetworkAttributeUnits unitsField;

        private esriNetworkAttributeDataType dataTypeField;

        private esriNetworkAttributeUsageType usageTypeField;

        private string[] parameterNamesField;

        private string restrictionUsageParameterNameField;
    }
}