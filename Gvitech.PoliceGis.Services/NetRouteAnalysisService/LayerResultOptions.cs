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
    public class LayerResultOptions : INotifyPropertyChanged
    {
        public LayerResultOptions()
        {
            this.includeGeometryField = true;
            this.returnFieldNamesInResultsField = false;
            this.formatValuesInResultsField = true;
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        [DefaultValue(true)]
        public bool IncludeGeometry
        {
            get
            {
                return this.includeGeometryField;
            }
            set
            {
                this.includeGeometryField = value;
                this.RaisePropertyChanged("IncludeGeometry");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public GeometryResultOptions GeometryResultOptions
        {
            get
            {
                return this.geometryResultOptionsField;
            }
            set
            {
                this.geometryResultOptionsField = value;
                this.RaisePropertyChanged("GeometryResultOptions");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        [DefaultValue(false)]
        public bool ReturnFieldNamesInResults
        {
            get
            {
                return this.returnFieldNamesInResultsField;
            }
            set
            {
                this.returnFieldNamesInResultsField = value;
                this.RaisePropertyChanged("ReturnFieldNamesInResults");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        [DefaultValue(true)]
        public bool FormatValuesInResults
        {
            get
            {
                return this.formatValuesInResultsField;
            }
            set
            {
                this.formatValuesInResultsField = value;
                this.RaisePropertyChanged("FormatValuesInResults");
            }
        }

        // (add) Token: 0x0600047A RID: 1146 RVA: 0x0000B560 File Offset: 0x00009760
        // (remove) Token: 0x0600047B RID: 1147 RVA: 0x0000B598 File Offset: 0x00009798
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

        private bool includeGeometryField;

        private GeometryResultOptions geometryResultOptionsField;

        private bool returnFieldNamesInResultsField;

        private bool formatValuesInResultsField;
    }
}