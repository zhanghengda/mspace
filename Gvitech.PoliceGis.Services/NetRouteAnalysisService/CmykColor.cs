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
    public class CmykColor : Color
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public byte Cyan
        {
            get
            {
                return this.cyanField;
            }
            set
            {
                this.cyanField = value;
                base.RaisePropertyChanged("Cyan");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public byte Magenta
        {
            get
            {
                return this.magentaField;
            }
            set
            {
                this.magentaField = value;
                base.RaisePropertyChanged("Magenta");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public byte Yellow
        {
            get
            {
                return this.yellowField;
            }
            set
            {
                this.yellowField = value;
                base.RaisePropertyChanged("Yellow");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public byte Black
        {
            get
            {
                return this.blackField;
            }
            set
            {
                this.blackField = value;
                base.RaisePropertyChanged("Black");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public bool Overprint
        {
            get
            {
                return this.overprintField;
            }
            set
            {
                this.overprintField = value;
                base.RaisePropertyChanged("Overprint");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public bool IsSpot
        {
            get
            {
                return this.isSpotField;
            }
            set
            {
                this.isSpotField = value;
                base.RaisePropertyChanged("IsSpot");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public string SpotDescription
        {
            get
            {
                return this.spotDescriptionField;
            }
            set
            {
                this.spotDescriptionField = value;
                base.RaisePropertyChanged("SpotDescription");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public short SpotPercent
        {
            get
            {
                return this.spotPercentField;
            }
            set
            {
                this.spotPercentField = value;
                base.RaisePropertyChanged("SpotPercent");
            }
        }

        private byte cyanField;

        private byte magentaField;

        private byte yellowField;

        private byte blackField;

        private bool overprintField;

        private bool isSpotField;

        private string spotDescriptionField;

        private short spotPercentField;
    }
}