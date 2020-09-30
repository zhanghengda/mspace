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
    public class RasterUniqueValues : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int UniqueValuesSize
        {
            get
            {
                return this.uniqueValuesSizeField;
            }
            set
            {
                this.uniqueValuesSizeField = value;
                this.RaisePropertyChanged("UniqueValuesSize");
            }
        }

        [XmlIgnore]
        public bool UniqueValuesSizeSpecified
        {
            get
            {
                return this.uniqueValuesSizeFieldSpecified;
            }
            set
            {
                this.uniqueValuesSizeFieldSpecified = value;
                this.RaisePropertyChanged("UniqueValuesSizeSpecified");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 1)]
        [XmlArrayItem("Value", Form = XmlSchemaForm.Unqualified)]
        public object[] Values
        {
            get
            {
                return this.valuesField;
            }
            set
            {
                this.valuesField = value;
                this.RaisePropertyChanged("Values");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 2)]
        [XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public int[] Counts
        {
            get
            {
                return this.countsField;
            }
            set
            {
                this.countsField = value;
                this.RaisePropertyChanged("Counts");
            }
        }

        // (add) Token: 0x06000555 RID: 1365 RVA: 0x0000CC48 File Offset: 0x0000AE48
        // (remove) Token: 0x06000556 RID: 1366 RVA: 0x0000CC80 File Offset: 0x0000AE80
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

        private int uniqueValuesSizeField;

        private bool uniqueValuesSizeFieldSpecified;

        private object[] valuesField;

        private int[] countsField;
    }
}