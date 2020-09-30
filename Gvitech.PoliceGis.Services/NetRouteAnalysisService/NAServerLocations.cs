using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(NAServerDataLayer))]
    [XmlInclude(typeof(NAServerRecordSet))]
    [XmlInclude(typeof(NAServerPropertySets))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class NAServerLocations : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public bool DoNotLocateOnRestrictedElements
        {
            get
            {
                return this.doNotLocateOnRestrictedElementsField;
            }
            set
            {
                this.doNotLocateOnRestrictedElementsField = value;
                this.RaisePropertyChanged("DoNotLocateOnRestrictedElements");
            }
        }

        [XmlIgnore]
        public bool DoNotLocateOnRestrictedElementsSpecified
        {
            get
            {
                return this.doNotLocateOnRestrictedElementsFieldSpecified;
            }
            set
            {
                this.doNotLocateOnRestrictedElementsFieldSpecified = value;
                this.RaisePropertyChanged("DoNotLocateOnRestrictedElementsSpecified");
            }
        }

        // (add) Token: 0x06000A66 RID: 2662 RVA: 0x00014CB8 File Offset: 0x00012EB8
        // (remove) Token: 0x06000A67 RID: 2663 RVA: 0x00014CF0 File Offset: 0x00012EF0
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

        private bool doNotLocateOnRestrictedElementsField;

        private bool doNotLocateOnRestrictedElementsFieldSpecified;
    }
}