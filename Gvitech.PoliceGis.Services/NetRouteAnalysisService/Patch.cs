using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(LinePatch))]
    [XmlInclude(typeof(AreaPatch))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public class Patch : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("Name");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public bool PreserveAspectRatio
        {
            get
            {
                return this.preserveAspectRatioField;
            }
            set
            {
                this.preserveAspectRatioField = value;
                this.RaisePropertyChanged("PreserveAspectRatio");
            }
        }

        [XmlIgnore]
        public bool PreserveAspectRatioSpecified
        {
            get
            {
                return this.preserveAspectRatioFieldSpecified;
            }
            set
            {
                this.preserveAspectRatioFieldSpecified = value;
                this.RaisePropertyChanged("PreserveAspectRatioSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public Geometry Geometry
        {
            get
            {
                return this.geometryField;
            }
            set
            {
                this.geometryField = value;
                this.RaisePropertyChanged("Geometry");
            }
        }

        // (add) Token: 0x06000528 RID: 1320 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
        // (remove) Token: 0x06000529 RID: 1321 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
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

        private string nameField;

        private bool preserveAspectRatioField;

        private bool preserveAspectRatioFieldSpecified;

        private Geometry geometryField;
    }
}