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
    public class TimeReference : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string TimeZoneNameID
        {
            get
            {
                return this.timeZoneNameIDField;
            }
            set
            {
                this.timeZoneNameIDField = value;
                this.RaisePropertyChanged("TimeZoneNameID");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public bool RespectsDaylightSavingTime
        {
            get
            {
                return this.respectsDaylightSavingTimeField;
            }
            set
            {
                this.respectsDaylightSavingTimeField = value;
                this.RaisePropertyChanged("RespectsDaylightSavingTime");
            }
        }

        [XmlIgnore]
        public bool RespectsDaylightSavingTimeSpecified
        {
            get
            {
                return this.respectsDaylightSavingTimeFieldSpecified;
            }
            set
            {
                this.respectsDaylightSavingTimeFieldSpecified = value;
                this.RaisePropertyChanged("RespectsDaylightSavingTimeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public bool RespectsDynamicAdjustmentRules
        {
            get
            {
                return this.respectsDynamicAdjustmentRulesField;
            }
            set
            {
                this.respectsDynamicAdjustmentRulesField = value;
                this.RaisePropertyChanged("RespectsDynamicAdjustmentRules");
            }
        }

        [XmlIgnore]
        public bool RespectsDynamicAdjustmentRulesSpecified
        {
            get
            {
                return this.respectsDynamicAdjustmentRulesFieldSpecified;
            }
            set
            {
                this.respectsDynamicAdjustmentRulesFieldSpecified = value;
                this.RaisePropertyChanged("RespectsDynamicAdjustmentRulesSpecified");
            }
        }

        // (add) Token: 0x06000839 RID: 2105 RVA: 0x00011248 File Offset: 0x0000F448
        // (remove) Token: 0x0600083A RID: 2106 RVA: 0x00011280 File Offset: 0x0000F480
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

        private string timeZoneNameIDField;

        private bool respectsDaylightSavingTimeField;

        private bool respectsDaylightSavingTimeFieldSpecified;

        private bool respectsDynamicAdjustmentRulesField;

        private bool respectsDynamicAdjustmentRulesFieldSpecified;
    }
}