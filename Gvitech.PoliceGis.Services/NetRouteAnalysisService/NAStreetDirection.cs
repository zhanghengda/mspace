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
    public class NAStreetDirection : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public double DriveTime
        {
            get
            {
                return this.driveTimeField;
            }
            set
            {
                this.driveTimeField = value;
                this.RaisePropertyChanged("DriveTime");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public Envelope Envelope
        {
            get
            {
                return this.envelopeField;
            }
            set
            {
                this.envelopeField = value;
                this.RaisePropertyChanged("Envelope");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public Point ManeuverPoint
        {
            get
            {
                return this.maneuverPointField;
            }
            set
            {
                this.maneuverPointField = value;
                this.RaisePropertyChanged("ManeuverPoint");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
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

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 5)]
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

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 6)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public double CumulativeLength
        {
            get
            {
                return this.cumulativeLengthField;
            }
            set
            {
                this.cumulativeLengthField = value;
                this.RaisePropertyChanged("CumulativeLength");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public DateTime EstimatedArrivalTime
        {
            get
            {
                return this.estimatedArrivalTimeField;
            }
            set
            {
                this.estimatedArrivalTimeField = value;
                this.RaisePropertyChanged("EstimatedArrivalTime");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
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

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 13)]
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

        // (add) Token: 0x060008C9 RID: 2249 RVA: 0x000121F0 File Offset: 0x000103F0
        // (remove) Token: 0x060008CA RID: 2250 RVA: 0x00012228 File Offset: 0x00010428
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

        private double driveTimeField;

        private Envelope envelopeField;

        private double lengthField;

        private Point maneuverPointField;

        private double timeField;

        private string[] stringsField;

        private esriDirectionsStringType[] stringTypesField;

        private double cumulativeLengthField;

        private DateTime estimatedArrivalTimeField;

        private esriDirectionsManeuverType maneuverTypeField;

        private double turnAngleField;

        private double departBearingField;

        private double generalBearingField;

        private NAStreetDirectionEvent[] eventsField;
    }
}