using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(JoinTableSourceDescription))]
    [XmlInclude(typeof(MapTableSourceDescription))]
    [XmlInclude(typeof(DataSourceDescription))]
    [XmlInclude(typeof(RasterDataSourceDescription))]
    [XmlInclude(typeof(QueryTableDataSourceDescription))]
    [XmlInclude(typeof(TableDataSourceDescription))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class MapServerSourceDescription : INotifyPropertyChanged
    {
        // (add) Token: 0x060006BF RID: 1727 RVA: 0x0000EF00 File Offset: 0x0000D100
        // (remove) Token: 0x060006C0 RID: 1728 RVA: 0x0000EF38 File Offset: 0x0000D138
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
    }
}