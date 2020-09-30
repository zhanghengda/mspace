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
    public class NAClassCandidateFieldMap : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string NAClassName
        {
            get
            {
                return this.nAClassNameField;
            }
            set
            {
                this.nAClassNameField = value;
                this.RaisePropertyChanged("NAClassName");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 1)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public NACandidateFieldMap[] CandidateFieldMaps
        {
            get
            {
                return this.candidateFieldMapsField;
            }
            set
            {
                this.candidateFieldMapsField = value;
                this.RaisePropertyChanged("CandidateFieldMaps");
            }
        }

        // (add) Token: 0x06000167 RID: 359 RVA: 0x00006D14 File Offset: 0x00004F14
        // (remove) Token: 0x06000168 RID: 360 RVA: 0x00006D4C File Offset: 0x00004F4C
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

        private string nAClassNameField;

        private NACandidateFieldMap[] candidateFieldMapsField;
    }
}