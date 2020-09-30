using System;
using System.Windows;
using Gvitech.CityMaker.FdeGeometry;
using Mmc.Framework.Services;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Models.Case;
using Mmc.Mspace.Services.PoliceEventService;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.PoliceResourceModule.PoliceEvent
{
    // Token: 0x02000009 RID: 9
    public class PoliceEventModel : BindableBase
    {
        // Token: 0x06000051 RID: 81 RVA: 0x0000370C File Offset: 0x0000190C
        public PoliceEventModel(CaseInfo caseInfo)
        {
            this.HttpCaseInfo = caseInfo;
            bool flag = this.HttpCaseInfo != null;
            if (flag)
            {
                bool flag2 = !string.IsNullOrEmpty(this.HttpCaseInfo.X) && !string.IsNullOrEmpty(this.HttpCaseInfo.Y);
                if (flag2)
                {
                    this.Location = IGeometryFactoryExtension.CreatePoint(GviMap.GeoFactory, StringExtension.ParseTo<double>(this.HttpCaseInfo.X, 0.0), StringExtension.ParseTo<double>(this.HttpCaseInfo.Y, 0.0), 0.0, (ISpatialCRS)GviMap.CrsFactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT));
                }
                bool flag3 = !string.IsNullOrEmpty(this.HttpCaseInfo.BJLB);
                if (flag3)
                {
                    string bjlb = this.HttpCaseInfo.BJLB;
                    if (bjlb == "举报投诉")
                    {
                        this.EventType = CaseType.Reports;
                    }

                    else if (bjlb == "交通类警情")
                    {
                        this.EventType = CaseType.TrafficAccident;
                    }

                    else if (bjlb == "其他警情")
                    {
                        this.EventType = CaseType.OtherAlarm;
                    }

                    else if (bjlb == "火灾事故")
                    {
                        this.EventType = CaseType.FireAccident;
                    }

                    else if (bjlb == "纠纷")
                    {
                        this.EventType = CaseType.Disputes;
                    }

                    else if (bjlb == "群众求助")
                    {
                        this.EventType = CaseType.MassRescue;
                    }

                    else if (bjlb == "行政（治安）案件")
                    {
                        this.EventType = CaseType.PublicSecurity;
                    }

                    else if (bjlb == "刑事案件")
                    {
                        this.EventType = CaseType.Criminal;
                    }
                }
            }
        }

        // Token: 0x1700000C RID: 12
        // (get) Token: 0x06000052 RID: 82 RVA: 0x00003959 File Offset: 0x00001B59
        // (set) Token: 0x06000053 RID: 83 RVA: 0x00003961 File Offset: 0x00001B61
        public CaseType EventType { get; set; }

        // Token: 0x1700000D RID: 13
        // (get) Token: 0x06000054 RID: 84 RVA: 0x0000396C File Offset: 0x00001B6C
        // (set) Token: 0x06000055 RID: 85 RVA: 0x00003984 File Offset: 0x00001B84
        public Visibility IsVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<Visibility>(ref this.isVisible, value, "IsVisible");
            }
        }

        // Token: 0x1700000E RID: 14
        // (get) Token: 0x06000056 RID: 86 RVA: 0x0000399A File Offset: 0x00001B9A
        // (set) Token: 0x06000057 RID: 87 RVA: 0x000039A2 File Offset: 0x00001BA2
        public CaseInfo HttpCaseInfo { get; set; }

        // Token: 0x1700000F RID: 15
        // (get) Token: 0x06000058 RID: 88 RVA: 0x000039AB File Offset: 0x00001BAB
        // (set) Token: 0x06000059 RID: 89 RVA: 0x000039B3 File Offset: 0x00001BB3
        public object Location { get; set; }

        // Token: 0x0400001C RID: 28
        private Visibility isVisible;
    }
}
