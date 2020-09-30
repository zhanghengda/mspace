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
    public class FIDSet : INotifyPropertyChanged
    {
        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 0)]
        [XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public int[] FIDArray
        {
            get
            {
                return this.fIDArrayField;
            }
            set
            {
                this.fIDArrayField = value;
                this.RaisePropertyChanged("FIDArray");
            }
        }

        // (add) Token: 0x06000A23 RID: 2595 RVA: 0x000145A0 File Offset: 0x000127A0
        // (remove) Token: 0x06000A24 RID: 2596 RVA: 0x000145D8 File Offset: 0x000127D8
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

        private int[] fIDArrayField;
    }
}