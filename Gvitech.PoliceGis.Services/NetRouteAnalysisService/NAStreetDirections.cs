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
    public class NAStreetDirections : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int RouteID
        {
            get
            {
                return this.routeIDField;
            }
            set
            {
                this.routeIDField = value;
                this.RaisePropertyChanged("RouteID");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string RouteName
        {
            get
            {
                return this.routeNameField;
            }
            set
            {
                this.routeNameField = value;
                this.RaisePropertyChanged("RouteName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public NAStreetDirection Summary
        {
            get
            {
                return this.summaryField;
            }
            set
            {
                this.summaryField = value;
                this.RaisePropertyChanged("Summary");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 3)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public NAStreetDirection[] Directions
        {
            get
            {
                return this.directionsField;
            }
            set
            {
                this.directionsField = value;
                this.RaisePropertyChanged("Directions");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 4)]
        [XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public string[] StopNames
        {
            get
            {
                return this.stopNamesField;
            }
            set
            {
                this.stopNamesField = value;
                this.RaisePropertyChanged("StopNames");
            }
        }

        // (add) Token: 0x060008D7 RID: 2263 RVA: 0x00012380 File Offset: 0x00010580
        // (remove) Token: 0x060008D8 RID: 2264 RVA: 0x000123B8 File Offset: 0x000105B8
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

        private int routeIDField;

        private string routeNameField;

        private NAStreetDirection summaryField;

        private NAStreetDirection[] directionsField;

        private string[] stopNamesField;
    }
}