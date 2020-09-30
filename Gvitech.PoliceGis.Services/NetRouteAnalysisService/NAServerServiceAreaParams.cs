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
    public class NAServerServiceAreaParams : NAServerSolverParams
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public NAServerLocations Facilities
        {
            get
            {
                return this.facilitiesField;
            }
            set
            {
                this.facilitiesField = value;
                base.RaisePropertyChanged("Facilities");
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
        public bool ReturnSALineGeometries
        {
            get
            {
                return this.returnSALineGeometriesField;
            }
            set
            {
                this.returnSALineGeometriesField = value;
                base.RaisePropertyChanged("ReturnSALineGeometries");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public bool ReturnSALines
        {
            get
            {
                return this.returnSALinesField;
            }
            set
            {
                this.returnSALinesField = value;
                base.RaisePropertyChanged("ReturnSALines");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public bool ReturnSAPolygonGeometries
        {
            get
            {
                return this.returnSAPolygonGeometriesField;
            }
            set
            {
                this.returnSAPolygonGeometriesField = value;
                base.RaisePropertyChanged("ReturnSAPolygonGeometries");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public bool ReturnSAPolygons
        {
            get
            {
                return this.returnSAPolygonsField;
            }
            set
            {
                this.returnSAPolygonsField = value;
                base.RaisePropertyChanged("ReturnSAPolygons");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public bool ReturnFacilities
        {
            get
            {
                return this.returnFacilitiesField;
            }
            set
            {
                this.returnFacilitiesField = value;
                base.RaisePropertyChanged("ReturnFacilities");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public esriNATravelDirection TravelDirection
        {
            get
            {
                return this.travelDirectionField;
            }
            set
            {
                this.travelDirectionField = value;
                base.RaisePropertyChanged("TravelDirection");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 9)]
        [XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public double[] DefaultBreaks
        {
            get
            {
                return this.defaultBreaksField;
            }
            set
            {
                this.defaultBreaksField = value;
                base.RaisePropertyChanged("DefaultBreaks");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public bool SplitPolygonsAtBreaks
        {
            get
            {
                return this.splitPolygonsAtBreaksField;
            }
            set
            {
                this.splitPolygonsAtBreaksField = value;
                base.RaisePropertyChanged("SplitPolygonsAtBreaks");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public bool MergeSimilarPolygonRanges
        {
            get
            {
                return this.mergeSimilarPolygonRangesField;
            }
            set
            {
                this.mergeSimilarPolygonRangesField = value;
                base.RaisePropertyChanged("MergeSimilarPolygonRanges");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 12)]
        [XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public string[] ExcludeSourcesFromPolygons
        {
            get
            {
                return this.excludeSourcesFromPolygonsField;
            }
            set
            {
                this.excludeSourcesFromPolygonsField = value;
                base.RaisePropertyChanged("ExcludeSourcesFromPolygons");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public bool SplitLinesAtBreaks
        {
            get
            {
                return this.splitLinesAtBreaksField;
            }
            set
            {
                this.splitLinesAtBreaksField = value;
                base.RaisePropertyChanged("SplitLinesAtBreaks");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
        public bool OverlapLines
        {
            get
            {
                return this.overlapLinesField;
            }
            set
            {
                this.overlapLinesField = value;
                base.RaisePropertyChanged("OverlapLines");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 15)]
        public esriNAOutputPolygonType OutputPolygons
        {
            get
            {
                return this.outputPolygonsField;
            }
            set
            {
                this.outputPolygonsField = value;
                base.RaisePropertyChanged("OutputPolygons");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 16)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 17)]
        public bool IncludeSourceInformationOnLines
        {
            get
            {
                return this.includeSourceInformationOnLinesField;
            }
            set
            {
                this.includeSourceInformationOnLinesField = value;
                base.RaisePropertyChanged("IncludeSourceInformationOnLines");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 18)]
        public bool OverlapPolygons
        {
            get
            {
                return this.overlapPolygonsField;
            }
            set
            {
                this.overlapPolygonsField = value;
                base.RaisePropertyChanged("OverlapPolygons");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 19)]
        public bool TrimOuterPolygon
        {
            get
            {
                return this.trimOuterPolygonField;
            }
            set
            {
                this.trimOuterPolygonField = value;
                base.RaisePropertyChanged("TrimOuterPolygon");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 20)]
        public double TrimPolygonDistance
        {
            get
            {
                return this.trimPolygonDistanceField;
            }
            set
            {
                this.trimPolygonDistanceField = value;
                base.RaisePropertyChanged("TrimPolygonDistance");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 21)]
        public esriUnits TrimPolygonDistanceUnits
        {
            get
            {
                return this.trimPolygonDistanceUnitsField;
            }
            set
            {
                this.trimPolygonDistanceUnitsField = value;
                base.RaisePropertyChanged("TrimPolygonDistanceUnits");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 22)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 23)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 24)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 25)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 26)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 27)]
        public DateTime TimeOfDay
        {
            get
            {
                return this.timeOfDayField;
            }
            set
            {
                this.timeOfDayField = value;
                base.RaisePropertyChanged("TimeOfDay");
            }
        }

        [XmlIgnore]
        public bool TimeOfDaySpecified
        {
            get
            {
                return this.timeOfDayFieldSpecified;
            }
            set
            {
                this.timeOfDayFieldSpecified = value;
                base.RaisePropertyChanged("TimeOfDaySpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 28)]
        public esriNATimeOfDayUsage TimeOfDayUsage
        {
            get
            {
                return this.timeOfDayUsageField;
            }
            set
            {
                this.timeOfDayUsageField = value;
                base.RaisePropertyChanged("TimeOfDayUsage");
            }
        }

        [XmlIgnore]
        public bool TimeOfDayUsageSpecified
        {
            get
            {
                return this.timeOfDayUsageFieldSpecified;
            }
            set
            {
                this.timeOfDayUsageFieldSpecified = value;
                base.RaisePropertyChanged("TimeOfDayUsageSpecified");
            }
        }

        private NAServerLocations facilitiesField;

        private NAServerLocations barriersField;

        private bool returnSALineGeometriesField;

        private bool returnSALinesField;

        private bool returnSAPolygonGeometriesField;

        private bool returnSAPolygonsField;

        private bool returnFacilitiesField;

        private bool returnBarriersField;

        private esriNATravelDirection travelDirectionField;

        private double[] defaultBreaksField;

        private bool splitPolygonsAtBreaksField;

        private bool mergeSimilarPolygonRangesField;

        private string[] excludeSourcesFromPolygonsField;

        private bool splitLinesAtBreaksField;

        private bool overlapLinesField;

        private esriNAOutputPolygonType outputPolygonsField;

        private esriNAOutputLineType outputLinesField;

        private bool includeSourceInformationOnLinesField;

        private bool overlapPolygonsField;

        private bool trimOuterPolygonField;

        private double trimPolygonDistanceField;

        private esriUnits trimPolygonDistanceUnitsField;

        private bool createTraversalResultField;

        private NAServerLocations polygonBarriersField;

        private NAServerLocations polylineBarriersField;

        private bool returnPolygonBarriersField;

        private bool returnPolygonBarriersFieldSpecified;

        private bool returnPolylineBarriersField;

        private bool returnPolylineBarriersFieldSpecified;

        private DateTime timeOfDayField;

        private bool timeOfDayFieldSpecified;

        private esriNATimeOfDayUsage timeOfDayUsageField;

        private bool timeOfDayUsageFieldSpecified;
    }
}