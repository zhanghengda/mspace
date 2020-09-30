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
    public class LabelingDescription : INotifyPropertyChanged
    {
        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 0)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public LabelClassDescription[] LabelClassDescriptions
        {
            get
            {
                return this.labelClassDescriptionsField;
            }
            set
            {
                this.labelClassDescriptionsField = value;
                this.RaisePropertyChanged("LabelClassDescriptions");
            }
        }

        // (add) Token: 0x06000692 RID: 1682 RVA: 0x0000EA48 File Offset: 0x0000CC48
        // (remove) Token: 0x06000693 RID: 1683 RVA: 0x0000EA80 File Offset: 0x0000CC80
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

        private LabelClassDescription[] labelClassDescriptionsField;
    }
}