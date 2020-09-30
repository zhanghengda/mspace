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
    public class GPMessages : INotifyPropertyChanged
    {
        [XmlArray("GPMessages", Form = XmlSchemaForm.Unqualified, Order = 0)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public GPMessage[] GPMessages1
        {
            get
            {
                return this.gPMessages1Field;
            }
            set
            {
                this.gPMessages1Field = value;
                this.RaisePropertyChanged("GPMessages1");
            }
        }

        // (add) Token: 0x060008E7 RID: 2279 RVA: 0x00012580 File Offset: 0x00010780
        // (remove) Token: 0x060008E8 RID: 2280 RVA: 0x000125B8 File Offset: 0x000107B8
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

        private GPMessage[] gPMessages1Field;
    }
}