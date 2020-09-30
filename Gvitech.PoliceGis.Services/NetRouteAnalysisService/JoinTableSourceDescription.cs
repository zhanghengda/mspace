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
    public class JoinTableSourceDescription : MapServerSourceDescription
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public MapServerSourceDescription LeftSourceDescription
        {
            get
            {
                return this.leftSourceDescriptionField;
            }
            set
            {
                this.leftSourceDescriptionField = value;
                base.RaisePropertyChanged("LeftSourceDescription");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public MapServerSourceDescription RightSourceDescription
        {
            get
            {
                return this.rightSourceDescriptionField;
            }
            set
            {
                this.rightSourceDescriptionField = value;
                base.RaisePropertyChanged("RightSourceDescription");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public string LeftTableKey
        {
            get
            {
                return this.leftTableKeyField;
            }
            set
            {
                this.leftTableKeyField = value;
                base.RaisePropertyChanged("LeftTableKey");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public string RightTableKey
        {
            get
            {
                return this.rightTableKeyField;
            }
            set
            {
                this.rightTableKeyField = value;
                base.RaisePropertyChanged("RightTableKey");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public esriJoinType JoinType
        {
            get
            {
                return this.joinTypeField;
            }
            set
            {
                this.joinTypeField = value;
                base.RaisePropertyChanged("JoinType");
            }
        }

        [XmlIgnore]
        public bool JoinTypeSpecified
        {
            get
            {
                return this.joinTypeFieldSpecified;
            }
            set
            {
                this.joinTypeFieldSpecified = value;
                base.RaisePropertyChanged("JoinTypeSpecified");
            }
        }

        private MapServerSourceDescription leftSourceDescriptionField;

        private MapServerSourceDescription rightSourceDescriptionField;

        private string leftTableKeyField;

        private string rightTableKeyField;

        private esriJoinType joinTypeField;

        private bool joinTypeFieldSpecified;
    }
}