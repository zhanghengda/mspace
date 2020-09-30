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
    public class RasterDef : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
                this.RaisePropertyChanged("Description");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public bool IsByRef
        {
            get
            {
                return this.isByRefField;
            }
            set
            {
                this.isByRefField = value;
                this.RaisePropertyChanged("IsByRef");
            }
        }

        [XmlIgnore]
        public bool IsByRefSpecified
        {
            get
            {
                return this.isByRefFieldSpecified;
            }
            set
            {
                this.isByRefFieldSpecified = value;
                this.RaisePropertyChanged("IsByRefSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public SpatialReference SpatialReference
        {
            get
            {
                return this.spatialReferenceField;
            }
            set
            {
                this.spatialReferenceField = value;
                this.RaisePropertyChanged("SpatialReference");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public bool IsByFunction
        {
            get
            {
                return this.isByFunctionField;
            }
            set
            {
                this.isByFunctionField = value;
                this.RaisePropertyChanged("IsByFunction");
            }
        }

        [XmlIgnore]
        public bool IsByFunctionSpecified
        {
            get
            {
                return this.isByFunctionFieldSpecified;
            }
            set
            {
                this.isByFunctionFieldSpecified = value;
                this.RaisePropertyChanged("IsByFunctionSpecified");
            }
        }

        // (add) Token: 0x06000997 RID: 2455 RVA: 0x00013808 File Offset: 0x00011A08
        // (remove) Token: 0x06000998 RID: 2456 RVA: 0x00013840 File Offset: 0x00011A40
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

        private string descriptionField;

        private bool isByRefField;

        private bool isByRefFieldSpecified;

        private SpatialReference spatialReferenceField;

        private bool isByFunctionField;

        private bool isByFunctionFieldSpecified;
    }
}