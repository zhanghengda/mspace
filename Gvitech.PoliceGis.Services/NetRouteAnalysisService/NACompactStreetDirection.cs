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
    public class NACompactStreetDirection : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public double Length
        {
            get
            {
                return this.lengthField;
            }
            set
            {
                this.lengthField = value;
                this.RaisePropertyChanged("Length");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public double Time
        {
            get
            {
                return this.timeField;
            }
            set
            {
                this.timeField = value;
                this.RaisePropertyChanged("Time");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public string Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
                this.RaisePropertyChanged("Text");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public string CompressedGeometry
        {
            get
            {
                return this.compressedGeometryField;
            }
            set
            {
                this.compressedGeometryField = value;
                this.RaisePropertyChanged("CompressedGeometry");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public esriDirectionsManeuverType ManeuverType
        {
            get
            {
                return this.maneuverTypeField;
            }
            set
            {
                this.maneuverTypeField = value;
                this.RaisePropertyChanged("ManeuverType");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public double TurnAngle
        {
            get
            {
                return this.turnAngleField;
            }
            set
            {
                this.turnAngleField = value;
                this.RaisePropertyChanged("TurnAngle");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public double DepartBearing
        {
            get
            {
                return this.departBearingField;
            }
            set
            {
                this.departBearingField = value;
                this.RaisePropertyChanged("DepartBearing");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public double GeneralBearing
        {
            get
            {
                return this.generalBearingField;
            }
            set
            {
                this.generalBearingField = value;
                this.RaisePropertyChanged("GeneralBearing");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 9)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public NAStreetDirectionEvent[] Events
        {
            get
            {
                return this.eventsField;
            }
            set
            {
                this.eventsField = value;
                this.RaisePropertyChanged("Events");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 10)]
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

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 11)]
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

        // (add) Token: 0x06000885 RID: 2181 RVA: 0x00011A90 File Offset: 0x0000FC90
        // (remove) Token: 0x06000886 RID: 2182 RVA: 0x00011AC8 File Offset: 0x0000FCC8
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

        private double lengthField;

        private double timeField;

        private string textField;

        private string compressedGeometryField;

        private DateTime eTAField;

        private esriDirectionsManeuverType maneuverTypeField;

        private double turnAngleField;

        private double departBearingField;

        private double generalBearingField;

        private NAStreetDirectionEvent[] eventsField;

        private string[] stringsField;

        private esriDirectionsStringType[] stringTypesField;
    }
}