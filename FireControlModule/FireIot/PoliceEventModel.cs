using Gvitech.CityMaker.FdeGeometry;
using Mmc.Framework.Services;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Models.Case;
using Mmc.Mspace.Services.PoliceEventService;
using Mmc.Wpf.Mvvm;
using System.Windows;

namespace FireControlModule.FireIot
{
    public class PoliceEventExModel : BindableBase
    {
        public PoliceEventExModel(CaseInfo caseInfo)
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

        public CaseType EventType { get; set; }

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

        public CaseInfo HttpCaseInfo { get; set; }

        public object Location { get; set; }

        private Visibility isVisible;
    }
}