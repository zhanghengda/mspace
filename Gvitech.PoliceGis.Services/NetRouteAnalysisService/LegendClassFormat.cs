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
    public class LegendClassFormat : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public Symbol LabelSymbol
        {
            get
            {
                return this.labelSymbolField;
            }
            set
            {
                this.labelSymbolField = value;
                this.RaisePropertyChanged("LabelSymbol");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public Symbol DescriptionSymbol
        {
            get
            {
                return this.descriptionSymbolField;
            }
            set
            {
                this.descriptionSymbolField = value;
                this.RaisePropertyChanged("DescriptionSymbol");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public LinePatch LinePatch
        {
            get
            {
                return this.linePatchField;
            }
            set
            {
                this.linePatchField = value;
                this.RaisePropertyChanged("LinePatch");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public AreaPatch AreaPatch
        {
            get
            {
                return this.areaPatchField;
            }
            set
            {
                this.areaPatchField = value;
                this.RaisePropertyChanged("AreaPatch");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public double PatchWidth
        {
            get
            {
                return this.patchWidthField;
            }
            set
            {
                this.patchWidthField = value;
                this.RaisePropertyChanged("PatchWidth");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public double PatchHeight
        {
            get
            {
                return this.patchHeightField;
            }
            set
            {
                this.patchHeightField = value;
                this.RaisePropertyChanged("PatchHeight");
            }
        }

        // (add) Token: 0x0600051B RID: 1307 RVA: 0x0000C640 File Offset: 0x0000A840
        // (remove) Token: 0x0600051C RID: 1308 RVA: 0x0000C678 File Offset: 0x0000A878
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

        private Symbol labelSymbolField;

        private Symbol descriptionSymbolField;

        private LinePatch linePatchField;

        private AreaPatch areaPatchField;

        private double patchWidthField;

        private double patchHeightField;
    }
}