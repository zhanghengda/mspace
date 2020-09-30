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
    public class GeoTransformation : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string WKT
        {
            get
            {
                return this.wKTField;
            }
            set
            {
                this.wKTField = value;
                this.RaisePropertyChanged("WKT");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public int WKID
        {
            get
            {
                return this.wKIDField;
            }
            set
            {
                this.wKIDField = value;
                this.RaisePropertyChanged("WKID");
            }
        }

        [XmlIgnore]
        public bool WKIDSpecified
        {
            get
            {
                return this.wKIDFieldSpecified;
            }
            set
            {
                this.wKIDFieldSpecified = value;
                this.RaisePropertyChanged("WKIDSpecified");
            }
        }

        // (add) Token: 0x0600017B RID: 379 RVA: 0x00006F74 File Offset: 0x00005174
        // (remove) Token: 0x0600017C RID: 380 RVA: 0x00006FAC File Offset: 0x000051AC
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

        private string wKTField;

        private int wKIDField;

        private bool wKIDFieldSpecified;
    }
}