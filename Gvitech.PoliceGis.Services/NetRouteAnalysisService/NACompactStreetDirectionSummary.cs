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
    public class NACompactStreetDirectionSummary : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public double TotalLength
        {
            get
            {
                return this.totalLengthField;
            }
            set
            {
                this.totalLengthField = value;
                this.RaisePropertyChanged("TotalLength");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public double TotalTime
        {
            get
            {
                return this.totalTimeField;
            }
            set
            {
                this.totalTimeField = value;
                this.RaisePropertyChanged("TotalTime");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public double TotalDriveTime
        {
            get
            {
                return this.totalDriveTimeField;
            }
            set
            {
                this.totalDriveTimeField = value;
                this.RaisePropertyChanged("TotalDriveTime");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
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

        // (add) Token: 0x0600089D RID: 2205 RVA: 0x00011D50 File Offset: 0x0000FF50
        // (remove) Token: 0x0600089E RID: 2206 RVA: 0x00011D88 File Offset: 0x0000FF88
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

        private double totalLengthField;

        private double totalTimeField;

        private double totalDriveTimeField;

        private Envelope envelopeField;
    }
}