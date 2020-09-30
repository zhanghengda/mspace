using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(MapServerBookmark))]
    [XmlInclude(typeof(MapExtent))]
    [XmlInclude(typeof(FeatureExtent))]
    [XmlInclude(typeof(CenterAndSize))]
    [XmlInclude(typeof(CenterAndScale))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class MapArea : INotifyPropertyChanged
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public Envelope Extent
        {
            get
            {
                return this.extentField;
            }
            set
            {
                this.extentField = value;
                this.RaisePropertyChanged("Extent");
            }
        }

        // (add) Token: 0x060001A5 RID: 421 RVA: 0x000073E4 File Offset: 0x000055E4
        // (remove) Token: 0x060001A6 RID: 422 RVA: 0x0000741C File Offset: 0x0000561C
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

        private Envelope extentField;
    }
}