using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(NAServerLocationAllocationResults))]
    [XmlInclude(typeof(NAServerVRPResults))]
    [XmlInclude(typeof(NAServerODCostMatrixResults))]
    [XmlInclude(typeof(NAServerServiceAreaResults))]
    [XmlInclude(typeof(NAServerClosestFacilityResults))]
    [XmlInclude(typeof(NAServerRouteResults))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class NAServerSolverResults : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public MapImage MapImage
        {
            get
            {
                return this.mapImageField;
            }
            set
            {
                this.mapImageField = value;
                this.RaisePropertyChanged("MapImage");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public GPMessages SolveMessages
        {
            get
            {
                return this.solveMessagesField;
            }
            set
            {
                this.solveMessagesField = value;
                this.RaisePropertyChanged("SolveMessages");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public int SolveErrorCode
        {
            get
            {
                return this.solveErrorCodeField;
            }
            set
            {
                this.solveErrorCodeField = value;
                this.RaisePropertyChanged("SolveErrorCode");
            }
        }

        [XmlIgnore]
        public bool SolveErrorCodeSpecified
        {
            get
            {
                return this.solveErrorCodeFieldSpecified;
            }
            set
            {
                this.solveErrorCodeFieldSpecified = value;
                this.RaisePropertyChanged("SolveErrorCodeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public string LayerToken
        {
            get
            {
                return this.layerTokenField;
            }
            set
            {
                this.layerTokenField = value;
                this.RaisePropertyChanged("LayerToken");
            }
        }

        // (add) Token: 0x0600090B RID: 2315 RVA: 0x00012960 File Offset: 0x00010B60
        // (remove) Token: 0x0600090C RID: 2316 RVA: 0x00012998 File Offset: 0x00010B98
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

        private MapImage mapImageField;

        private GPMessages solveMessagesField;

        private int solveErrorCodeField;

        private bool solveErrorCodeFieldSpecified;

        private string layerTokenField;
    }
}