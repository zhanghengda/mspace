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
    public class NAServerRouteParams : NAServerSolverParams
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public NAServerLocations Stops
        {
            get
            {
                return this.stopsField;
            }
            set
            {
                this.stopsField = value;
                base.RaisePropertyChanged("Stops");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public NAServerLocations Barriers
        {
            get
            {
                return this.barriersField;
            }
            set
            {
                this.barriersField = value;
                base.RaisePropertyChanged("Barriers");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public bool ReturnRouteGeometries
        {
            get
            {
                return this.returnRouteGeometriesField;
            }
            set
            {
                this.returnRouteGeometriesField = value;
                base.RaisePropertyChanged("ReturnRouteGeometries");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public bool ReturnRoutes
        {
            get
            {
                return this.returnRoutesField;
            }
            set
            {
                this.returnRoutesField = value;
                base.RaisePropertyChanged("ReturnRoutes");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public bool ReturnStops
        {
            get
            {
                return this.returnStopsField;
            }
            set
            {
                this.returnStopsField = value;
                base.RaisePropertyChanged("ReturnStops");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public bool ReturnBarriers
        {
            get
            {
                return this.returnBarriersField;
            }
            set
            {
                this.returnBarriersField = value;
                base.RaisePropertyChanged("ReturnBarriers");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public bool ReturnDirections
        {
            get
            {
                return this.returnDirectionsField;
            }
            set
            {
                this.returnDirectionsField = value;
                base.RaisePropertyChanged("ReturnDirections");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public string DirectionsLanguage
        {
            get
            {
                return this.directionsLanguageField;
            }
            set
            {
                this.directionsLanguageField = value;
                base.RaisePropertyChanged("DirectionsLanguage");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public esriNetworkAttributeUnits DirectionsLengthUnits
        {
            get
            {
                return this.directionsLengthUnitsField;
            }
            set
            {
                this.directionsLengthUnitsField = value;
                base.RaisePropertyChanged("DirectionsLengthUnits");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public string DirectionsTimeAttributeName
        {
            get
            {
                return this.directionsTimeAttributeNameField;
            }
            set
            {
                this.directionsTimeAttributeNameField = value;
                base.RaisePropertyChanged("DirectionsTimeAttributeName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public bool FindBestSequence
        {
            get
            {
                return this.findBestSequenceField;
            }
            set
            {
                this.findBestSequenceField = value;
                base.RaisePropertyChanged("FindBestSequence");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public bool PreserveFirstStop
        {
            get
            {
                return this.preserveFirstStopField;
            }
            set
            {
                this.preserveFirstStopField = value;
                base.RaisePropertyChanged("PreserveFirstStop");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public bool PreserveLastStop
        {
            get
            {
                return this.preserveLastStopField;
            }
            set
            {
                this.preserveLastStopField = value;
                base.RaisePropertyChanged("PreserveLastStop");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public bool UseTimeWindows
        {
            get
            {
                return this.useTimeWindowsField;
            }
            set
            {
                this.useTimeWindowsField = value;
                base.RaisePropertyChanged("UseTimeWindows");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
        public bool CreateTraversalResult
        {
            get
            {
                return this.createTraversalResultField;
            }
            set
            {
                this.createTraversalResultField = value;
                base.RaisePropertyChanged("CreateTraversalResult");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 15)]
        public esriNAOutputLineType OutputLines
        {
            get
            {
                return this.outputLinesField;
            }
            set
            {
                this.outputLinesField = value;
                base.RaisePropertyChanged("OutputLines");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 16)]
        public bool UseStartTime
        {
            get
            {
                return this.useStartTimeField;
            }
            set
            {
                this.useStartTimeField = value;
                base.RaisePropertyChanged("UseStartTime");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 17)]
        public DateTime StartTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
                base.RaisePropertyChanged("StartTime");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 18)]
        public bool ReturnCompactDirections
        {
            get
            {
                return this.returnCompactDirectionsField;
            }
            set
            {
                this.returnCompactDirectionsField = value;
                base.RaisePropertyChanged("ReturnCompactDirections");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 19)]
        public esriDirectionsOutputType DirectionsOutputType
        {
            get
            {
                return this.directionsOutputTypeField;
            }
            set
            {
                this.directionsOutputTypeField = value;
                base.RaisePropertyChanged("DirectionsOutputType");
            }
        }

        [XmlIgnore]
        public bool DirectionsOutputTypeSpecified
        {
            get
            {
                return this.directionsOutputTypeFieldSpecified;
            }
            set
            {
                this.directionsOutputTypeFieldSpecified = value;
                base.RaisePropertyChanged("DirectionsOutputTypeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 20)]
        public string DirectionsStyleName
        {
            get
            {
                return this.directionsStyleNameField;
            }
            set
            {
                this.directionsStyleNameField = value;
                base.RaisePropertyChanged("DirectionsStyleName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 21)]
        public NAServerLocations PolygonBarriers
        {
            get
            {
                return this.polygonBarriersField;
            }
            set
            {
                this.polygonBarriersField = value;
                base.RaisePropertyChanged("PolygonBarriers");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 22)]
        public NAServerLocations PolylineBarriers
        {
            get
            {
                return this.polylineBarriersField;
            }
            set
            {
                this.polylineBarriersField = value;
                base.RaisePropertyChanged("PolylineBarriers");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 23)]
        public bool ReturnPolygonBarriers
        {
            get
            {
                return this.returnPolygonBarriersField;
            }
            set
            {
                this.returnPolygonBarriersField = value;
                base.RaisePropertyChanged("ReturnPolygonBarriers");
            }
        }

        [XmlIgnore]
        public bool ReturnPolygonBarriersSpecified
        {
            get
            {
                return this.returnPolygonBarriersFieldSpecified;
            }
            set
            {
                this.returnPolygonBarriersFieldSpecified = value;
                base.RaisePropertyChanged("ReturnPolygonBarriersSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 24)]
        public bool ReturnPolylineBarriers
        {
            get
            {
                return this.returnPolylineBarriersField;
            }
            set
            {
                this.returnPolylineBarriersField = value;
                base.RaisePropertyChanged("ReturnPolylineBarriers");
            }
        }

        [XmlIgnore]
        public bool ReturnPolylineBarriersSpecified
        {
            get
            {
                return this.returnPolylineBarriersFieldSpecified;
            }
            set
            {
                this.returnPolylineBarriersFieldSpecified = value;
                base.RaisePropertyChanged("ReturnPolylineBarriersSpecified");
            }
        }

        private NAServerLocations stopsField;

        private NAServerLocations barriersField;

        private bool returnRouteGeometriesField;

        private bool returnRoutesField;

        private bool returnStopsField;

        private bool returnBarriersField;

        private bool returnDirectionsField;

        private string directionsLanguageField;

        private esriNetworkAttributeUnits directionsLengthUnitsField;

        private string directionsTimeAttributeNameField;

        private bool findBestSequenceField;

        private bool preserveFirstStopField;

        private bool preserveLastStopField;

        private bool useTimeWindowsField;

        private bool createTraversalResultField;

        private esriNAOutputLineType outputLinesField;

        private bool useStartTimeField;

        private DateTime startTimeField;

        private bool returnCompactDirectionsField;

        private esriDirectionsOutputType directionsOutputTypeField;

        private bool directionsOutputTypeFieldSpecified;

        private string directionsStyleNameField;

        private NAServerLocations polygonBarriersField;

        private NAServerLocations polylineBarriersField;

        private bool returnPolygonBarriersField;

        private bool returnPolygonBarriersFieldSpecified;

        private bool returnPolylineBarriersField;

        private bool returnPolylineBarriersFieldSpecified;
    }
}