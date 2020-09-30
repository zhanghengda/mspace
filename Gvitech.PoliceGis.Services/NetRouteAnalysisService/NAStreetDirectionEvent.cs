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
    public class NAStreetDirectionEvent : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public DateTime ETA
        {
            get
            {
                return this.eTAField;
            }
            set
            {
                this.eTAField = value;
                this.RaisePropertyChanged("ETA");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public Point Point
        {
            get
            {
                return this.pointField;
            }
            set
            {
                this.pointField = value;
                this.RaisePropertyChanged("Point");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 2)]
        [XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public string[] Strings
        {
            get
            {
                return this.stringsField;
            }
            set
            {
                this.stringsField = value;
                this.RaisePropertyChanged("Strings");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 3)]
        [XmlArrayItem("DirectionsStringType", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public esriDirectionsStringType[] StringTypes
        {
            get
            {
                return this.stringTypesField;
            }
            set
            {
                this.stringTypesField = value;
                this.RaisePropertyChanged("StringTypes");
            }
        }

        // (add) Token: 0x06000891 RID: 2193 RVA: 0x00011BF0 File Offset: 0x0000FDF0
        // (remove) Token: 0x06000892 RID: 2194 RVA: 0x00011C28 File Offset: 0x0000FE28
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

        private DateTime eTAField;

        private Point pointField;

        private string[] stringsField;

        private esriDirectionsStringType[] stringTypesField;
    }
}