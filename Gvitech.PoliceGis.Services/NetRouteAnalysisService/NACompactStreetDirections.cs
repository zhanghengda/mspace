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
    public class NACompactStreetDirections : INotifyPropertyChanged
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
        public NACompactStreetDirectionSummary Summary
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
        public NACompactStreetDirection[] Directions
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

        // (add) Token: 0x060008A9 RID: 2217 RVA: 0x00011EB0 File Offset: 0x000100B0
        // (remove) Token: 0x060008AA RID: 2218 RVA: 0x00011EE8 File Offset: 0x000100E8
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

        private NACompactStreetDirectionSummary summaryField;

        private NACompactStreetDirection[] directionsField;
    }
}