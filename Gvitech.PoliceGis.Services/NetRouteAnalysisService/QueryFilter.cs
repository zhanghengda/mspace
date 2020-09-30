using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(SpatialFilter))]
    [XmlInclude(typeof(TimeQueryFilter))]
    [XmlInclude(typeof(ImageQueryFilter))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public class QueryFilter : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string SubFields
        {
            get
            {
                return this.subFieldsField;
            }
            set
            {
                this.subFieldsField = value;
                this.RaisePropertyChanged("SubFields");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string WhereClause
        {
            get
            {
                return this.whereClauseField;
            }
            set
            {
                this.whereClauseField = value;
                this.RaisePropertyChanged("WhereClause");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public string SpatialReferenceFieldName
        {
            get
            {
                return this.spatialReferenceFieldNameField;
            }
            set
            {
                this.spatialReferenceFieldNameField = value;
                this.RaisePropertyChanged("SpatialReferenceFieldName");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public double Resolution
        {
            get
            {
                return this.resolutionField;
            }
            set
            {
                this.resolutionField = value;
                this.RaisePropertyChanged("Resolution");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public SpatialReference OutputSpatialReference
        {
            get
            {
                return this.outputSpatialReferenceField;
            }
            set
            {
                this.outputSpatialReferenceField = value;
                this.RaisePropertyChanged("OutputSpatialReference");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public FIDSet FIDSet
        {
            get
            {
                return this.fIDSetField;
            }
            set
            {
                this.fIDSetField = value;
                this.RaisePropertyChanged("FIDSet");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public string PostfixClause
        {
            get
            {
                return this.postfixClauseField;
            }
            set
            {
                this.postfixClauseField = value;
                this.RaisePropertyChanged("PostfixClause");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 7)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public FilterDef[] FilterDefs
        {
            get
            {
                return this.filterDefsField;
            }
            set
            {
                this.filterDefsField = value;
                this.RaisePropertyChanged("FilterDefs");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public string PrefixClause
        {
            get
            {
                return this.prefixClauseField;
            }
            set
            {
                this.prefixClauseField = value;
                this.RaisePropertyChanged("PrefixClause");
            }
        }

        // (add) Token: 0x06000A39 RID: 2617 RVA: 0x000147F0 File Offset: 0x000129F0
        // (remove) Token: 0x06000A3A RID: 2618 RVA: 0x00014828 File Offset: 0x00012A28
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

        private string subFieldsField;

        private string whereClauseField;

        private string spatialReferenceFieldNameField;

        private double resolutionField;

        private SpatialReference outputSpatialReferenceField;

        private FIDSet fIDSetField;

        private string postfixClauseField;

        private FilterDef[] filterDefsField;

        private string prefixClauseField;
    }
}