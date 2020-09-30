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
    public class ClassBreakInfo : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public double ClassMaximumValue
        {
            get
            {
                return this.classMaximumValueField;
            }
            set
            {
                this.classMaximumValueField = value;
                this.RaisePropertyChanged("ClassMaximumValue");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string Label
        {
            get
            {
                return this.labelField;
            }
            set
            {
                this.labelField = value;
                this.RaisePropertyChanged("Label");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
                this.RaisePropertyChanged("Description");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public Symbol Symbol
        {
            get
            {
                return this.symbolField;
            }
            set
            {
                this.symbolField = value;
                this.RaisePropertyChanged("Symbol");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public double ClassMinimumValue
        {
            get
            {
                return this.classMinimumValueField;
            }
            set
            {
                this.classMinimumValueField = value;
                this.RaisePropertyChanged("ClassMinimumValue");
            }
        }

        // (add) Token: 0x0600065A RID: 1626 RVA: 0x0000E4B8 File Offset: 0x0000C6B8
        // (remove) Token: 0x0600065B RID: 1627 RVA: 0x0000E4F0 File Offset: 0x0000C6F0
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

        private double classMaximumValueField;

        private string labelField;

        private string descriptionField;

        private Symbol symbolField;

        private double classMinimumValueField;
    }
}