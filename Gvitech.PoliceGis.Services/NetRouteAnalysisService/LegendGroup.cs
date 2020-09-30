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
    public class LegendGroup : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public bool Visible
        {
            get
            {
                return this.visibleField;
            }
            set
            {
                this.visibleField = value;
                this.RaisePropertyChanged("Visible");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public bool Editable
        {
            get
            {
                return this.editableField;
            }
            set
            {
                this.editableField = value;
                this.RaisePropertyChanged("Editable");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public string Heading
        {
            get
            {
                return this.headingField;
            }
            set
            {
                this.headingField = value;
                this.RaisePropertyChanged("Heading");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 3)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public LegendClass[] LegendClasses
        {
            get
            {
                return this.legendClassesField;
            }
            set
            {
                this.legendClassesField = value;
                this.RaisePropertyChanged("LegendClasses");
            }
        }

        // (add) Token: 0x060004FF RID: 1279 RVA: 0x0000C320 File Offset: 0x0000A520
        // (remove) Token: 0x06000500 RID: 1280 RVA: 0x0000C358 File Offset: 0x0000A558
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

        private bool visibleField;

        private bool editableField;

        private string headingField;

        private LegendClass[] legendClassesField;
    }
}