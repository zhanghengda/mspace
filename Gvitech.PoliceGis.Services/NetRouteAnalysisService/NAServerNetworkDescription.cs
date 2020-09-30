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
    public class NAServerNetworkDescription : INotifyPropertyChanged
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

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 1)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public NAServerNetworkAttribute[] NetworkAttributes
        {
            get
            {
                return this.networkAttributesField;
            }
            set
            {
                this.networkAttributesField = value;
                this.RaisePropertyChanged("NetworkAttributes");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 2)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public NAServerNetworkSource[] NetworkSources
        {
            get
            {
                return this.networkSourcesField;
            }
            set
            {
                this.networkSourcesField = value;
                this.RaisePropertyChanged("NetworkSources");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public NAServerNetworkDirections NetworkDirections
        {
            get
            {
                return this.networkDirectionsField;
            }
            set
            {
                this.networkDirectionsField = value;
                this.RaisePropertyChanged("NetworkDirections");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public DateTime BuildTime
        {
            get
            {
                return this.buildTimeField;
            }
            set
            {
                this.buildTimeField = value;
                this.RaisePropertyChanged("BuildTime");
            }
        }

        [XmlIgnore]
        public bool BuildTimeSpecified
        {
            get
            {
                return this.buildTimeFieldSpecified;
            }
            set
            {
                this.buildTimeFieldSpecified = value;
                this.RaisePropertyChanged("BuildTimeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public esriNetworkDatasetState State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
                this.RaisePropertyChanged("State");
            }
        }

        [XmlIgnore]
        public bool StateSpecified
        {
            get
            {
                return this.stateFieldSpecified;
            }
            set
            {
                this.stateFieldSpecified = value;
                this.RaisePropertyChanged("StateSpecified");
            }
        }

        // (add) Token: 0x06000A8D RID: 2701 RVA: 0x000150A0 File Offset: 0x000132A0
        // (remove) Token: 0x06000A8E RID: 2702 RVA: 0x000150D8 File Offset: 0x000132D8
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

        private NAServerNetworkAttribute[] networkAttributesField;

        private NAServerNetworkSource[] networkSourcesField;

        private NAServerNetworkDirections networkDirectionsField;

        private DateTime buildTimeField;

        private bool buildTimeFieldSpecified;

        private esriNetworkDatasetState stateField;

        private bool stateFieldSpecified;
    }
}