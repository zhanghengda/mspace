using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Models.Case;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mmc.Mspace.Services.StatisticService
{
    public class StatisticLayerService : Singleton<StatisticLayerService>, IStatisticLayerService
    {
        protected Dictionary<string, IDisplayLayer> Layers { get; set; }

        public void InitService()
        {
            this.ReSolveAlarmData();
        }

        public void SetPoliceStationXQLayerVisible(bool visible)
        {
            IDisplayLayer disPlayLayerByFCAliasName = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCAliasName("派出所辖区");
            gviViewportMask vmask = visible ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
            bool flag = disPlayLayerByFCAliasName != null && IEnumerableExtension.HasValues<IFeatureLayer>(disPlayLayerByFCAliasName.FLyers);
            if (flag)
            {
                disPlayLayerByFCAliasName.FLyers.ForEach(delegate (IFeatureLayer fly)
                {
                    fly.VisibleMask = vmask;
                });
            }
        }

        public List<LayerItemModel> GetStatisticLayers()
        {
            List<LayerItemModel> result;
            if (!IDictionaryExtension.HasValues<string, IDisplayLayer>(this.Layers))
            {
                result = new List<LayerItemModel>();
            }
            else
            {
                result = (from dly in this.Layers
                          select new LayerItemModel
                          {
                              Name = dly.Key,
                              Group = "综合统计",
                              IsChecked = false,
                              Parameters = dly.Value
                          }).ToList<LayerItemModel>();
            }
            return result;
        }

        public List<StatisticalChartItem> GetAlarmCharData()
        {
            bool flag = this.alarmsd == null || !IEnumerableExtension.HasValues<AlarmStatisticItem>(this.alarmsd.AlarmStatisticItems);
            List<StatisticalChartItem> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                AlarmStatisticItem total = new AlarmStatisticItem();
                this.alarmsd.AlarmStatisticItems.ForEach(delegate (AlarmStatisticItem item)
                {
                    total.TotalAlarm += item.TotalAlarm;
                    total.EffectiveAlarm += item.EffectiveAlarm;
                    total.InvalidAlarm += item.InvalidAlarm;
                    total.CriminalAlarm += item.CriminalAlarm;
                    total.RescueAlarm += item.RescueAlarm;
                    total.SecurityAlarm += item.SecurityAlarm;
                    total.FireAlarm += item.FireAlarm;
                    total.OthorAlarm += item.OthorAlarm;
                    total.TrafficAlarm += item.TrafficAlarm;
                    total.DisputeAlarm += item.DisputeAlarm;
                    total.ReportAlarm += item.ReportAlarm;
                    total.IncidentAlarm += item.IncidentAlarm;
                    total.DisasterAlarm += item.DisasterAlarm;
                    total.OtherAdministrativeAlarm += item.OtherAdministrativeAlarm;
                });
                List<StatisticalChartItem> list = new List<StatisticalChartItem>();
                CollectionExtension.AddEx<StatisticalChartItem>(list, new StatisticalChartItem
                {
                    Item = "刑事案件",
                    Color = 4294901760u,
                    Value = (float)total.CriminalAlarm
                });
                CollectionExtension.AddEx<StatisticalChartItem>(list, new StatisticalChartItem
                {
                    Item = "行政（治安）案件",
                    Color = 4294901760u,
                    Value = (float)total.SecurityAlarm
                });
                CollectionExtension.AddEx<StatisticalChartItem>(list, new StatisticalChartItem
                {
                    Item = "群众救助",
                    Color = 4294901760u,
                    Value = (float)total.RescueAlarm
                });
                CollectionExtension.AddEx<StatisticalChartItem>(list, new StatisticalChartItem
                {
                    Item = "火灾事件",
                    Color = 4294901760u,
                    Value = (float)total.FireAlarm
                });
                CollectionExtension.AddEx<StatisticalChartItem>(list, new StatisticalChartItem
                {
                    Item = "交通事故",
                    Color = 4294901760u,
                    Value = (float)total.TrafficAlarm
                });
                result = list;
            }
            return result;
        }

        public Chromatography GetLengend(string alarmType)
        {
            bool flag = string.IsNullOrEmpty(alarmType);
            Chromatography result;
            if (flag)
            {
                result = null;
            }
            else
            {
                result = this.alarmchpy.Chromatographies.FirstOrDefault((Chromatography item) => item.Kind.Equals(alarmType));
            }
            return result;
        }

        public AlarmStatisticalChart GetChartData(string policement)
        {
            bool flag = string.IsNullOrEmpty(policement);
            AlarmStatisticalChart result;
            if (flag)
            {
                result = null;
            }
            else
            {
                AlarmStatisticItem alarmStatisticItem = this.alarmsd.AlarmStatisticItems.Find((AlarmStatisticItem ast) => ast.PoliceStationName.Equals(policement));
                bool flag2 = alarmStatisticItem == null;
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    result = new AlarmStatisticalChart
                    {
                        PoliceStation = policement,
                        Tittle = string.Format("{0}-{1}", policement, "统计结果"),
                        ChartItems = new List<StatisticalChartItem>
                        {
                            new StatisticalChartItem
                            {
                                Item = "刑事案件",
                                Color = 4294901760u,
                                Value = (float)alarmStatisticItem.CriminalAlarm
                            },
                            new StatisticalChartItem
                            {
                                Item = "行政（治安）案件",
                                Color = 4278255360u,
                                Value = (float)alarmStatisticItem.SecurityAlarm
                            },
                            new StatisticalChartItem
                            {
                                Item = "群众求助",
                                Color = 4278190335u,
                                Value = (float)alarmStatisticItem.RescueAlarm
                            },
                            new StatisticalChartItem
                            {
                                Item = "火灾事件",
                                Color = 4294967040u,
                                Value = (float)alarmStatisticItem.FireAlarm
                            },
                            new StatisticalChartItem
                            {
                                Item = "交通事故",
                                Color = 4294902015u,
                                Value = (float)alarmStatisticItem.TrafficAlarm
                            },
                            new StatisticalChartItem
                            {
                                Item = "其他警情",
                                Color = 4278255615u,
                                Value = (float)alarmStatisticItem.OthorAlarm
                            },
                            new StatisticalChartItem
                            {
                                Item = "纠纷",
                                Color = 4294962688u,
                                Value = (float)alarmStatisticItem.DisputeAlarm
                            },
                            new StatisticalChartItem
                            {
                                Item = "举报投诉",
                                Color = 4294958336u,
                                Value = (float)alarmStatisticItem.ReportAlarm
                            }
                        }
                    };
                }
            }
            return result;
        }

        public static IStatisticLayerService GetDefault(object obj)
        {
            return Singleton<StatisticLayerService>.Instance;
        }

        private void ReSolveAlarmData()
        {
            bool flag = StatisticLayerService.isResolved;
            if (!flag)
            {
                StatisticLayerService.isResolved = true;
                try
                {
                    this.alarmchpy = ConfigHelper<AlarmChromatography>.ResovleConfigFromFile(string.Format("{0}\\data\\xml\\Chromatography.xml", AppDomain.CurrentDomain.BaseDirectory));
                    this.alarmsd = ConfigHelper<AlarmStatisticData>.ResovleConfigFromFile(string.Format("{0}\\data\\xml\\AlarmStatisticData.xml", AppDomain.CurrentDomain.BaseDirectory));
                    this.alarmsd = this.GetHttpAlarmData();
                    this.alarmaks = this.AnalysisAlarm(this.alarmchpy, this.alarmsd);
                    bool flag2 = this.alarmaks == null;
                    if (!flag2)
                    {
                        IDisplayLayer disPlayLayerByFCAliasName = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCAliasName("派出所辖区");
                        bool flag3 = disPlayLayerByFCAliasName == null;
                        if (!flag3)
                        {
                            List<IFeatureLayer> flyers = disPlayLayerByFCAliasName.FLyers;
                            bool flag4 = !IEnumerableExtension.HasValues<IFeatureLayer>(flyers);
                            if (!flag4)
                            {
                                IFeatureLayer featureLayer = flyers.First<IFeatureLayer>();
                                this.CreateStatisticLayers(disPlayLayerByFCAliasName.Fc, featureLayer.GetTextRender(), this.alarmaks);
                            }
                        }
                    }
                }
                catch (Exception value)
                {
                    StatisticLayerService.isResolved = false;
                    Console.WriteLine(value);
                }
            }
        }

        private AlarmStatisticData GetHttpAlarmData()
        {
            List<SubjectCaseInfo> subjectCaseInfos = ServiceManager.GetService<ISubjectCaseHttpService>(null).GetSubjectCaseInfos(DateTime.Now.AddDays(-30.0), DateTime.Now, 1, 100);
            bool flag = !IEnumerableExtension.HasValues<SubjectCaseInfo>(subjectCaseInfos);
            AlarmStatisticData result;
            if (flag)
            {
                result = null;
            }
            else
            {
                AlarmStatisticData ad = new AlarmStatisticData
                {
                    AlarmStatisticItems = new List<AlarmStatisticItem>()
                };
                AreaSubjectCaseInfo asc = null;
                subjectCaseInfos.ForEach(delegate (SubjectCaseInfo item)
                {
                    List<AlarmStatisticItem> alarmStatisticItems = ad.AlarmStatisticItems;
                    AlarmStatisticItem alarmStatisticItem = new AlarmStatisticItem();
                    alarmStatisticItem.PoliceStationName = item.FirstFieldValue;
                    alarmStatisticItem.TotalAlarm = item.FirstCount;
                    AlarmStatisticItem alarmStatisticItem2 = alarmStatisticItem;
                    int trafficAlarm;
                    if (IEnumerableExtension.HasValues<AreaSubjectCaseInfo>(item.PivotList))
                    {
                        if ((asc = item.PivotList.Find((AreaSubjectCaseInfo c) => c.SecFieldValue.Equals("交通类警情"))) != null)
                        {
                            trafficAlarm = asc.SecCount;
                            goto IL_7D;
                        }
                    }
                    trafficAlarm = 0;
                    IL_7D:
                    alarmStatisticItem2.TrafficAlarm = trafficAlarm;
                    AlarmStatisticItem alarmStatisticItem3 = alarmStatisticItem;
                    int criminalAlarm;
                    if (IEnumerableExtension.HasValues<AreaSubjectCaseInfo>(item.PivotList))
                    {
                        if ((asc = item.PivotList.Find((AreaSubjectCaseInfo c) => c.SecFieldValue.Equals("刑事案件"))) != null)
                        {
                            criminalAlarm = asc.SecCount;
                            goto IL_D4;
                        }
                    }
                    criminalAlarm = 0;
                    IL_D4:
                    alarmStatisticItem3.CriminalAlarm = criminalAlarm;
                    AlarmStatisticItem alarmStatisticItem4 = alarmStatisticItem;
                    int fireAlarm;
                    if (IEnumerableExtension.HasValues<AreaSubjectCaseInfo>(item.PivotList))
                    {
                        if ((asc = item.PivotList.Find((AreaSubjectCaseInfo c) => c.SecFieldValue.Equals("火灾事故"))) != null)
                        {
                            fireAlarm = asc.SecCount;
                            goto IL_12B;
                        }
                    }
                    fireAlarm = 0;
                    IL_12B:
                    alarmStatisticItem4.FireAlarm = fireAlarm;
                    AlarmStatisticItem alarmStatisticItem5 = alarmStatisticItem;
                    int rescueAlarm;
                    if (IEnumerableExtension.HasValues<AreaSubjectCaseInfo>(item.PivotList))
                    {
                        if ((asc = item.PivotList.Find((AreaSubjectCaseInfo c) => c.SecFieldValue.Equals("群众求助"))) != null)
                        {
                            rescueAlarm = asc.SecCount;
                            goto IL_182;
                        }
                    }
                    rescueAlarm = 0;
                    IL_182:
                    alarmStatisticItem5.RescueAlarm = rescueAlarm;
                    AlarmStatisticItem alarmStatisticItem6 = alarmStatisticItem;
                    int securityAlarm;
                    if (IEnumerableExtension.HasValues<AreaSubjectCaseInfo>(item.PivotList))
                    {
                        if ((asc = item.PivotList.Find((AreaSubjectCaseInfo c) => c.SecFieldValue.Equals("行政（治安）案件"))) != null)
                        {
                            securityAlarm = asc.SecCount;
                            goto IL_1D9;
                        }
                    }
                    securityAlarm = 0;
                    IL_1D9:
                    alarmStatisticItem6.SecurityAlarm = securityAlarm;
                    AlarmStatisticItem alarmStatisticItem7 = alarmStatisticItem;
                    int othorAlarm;
                    if (IEnumerableExtension.HasValues<AreaSubjectCaseInfo>(item.PivotList))
                    {
                        if ((asc = item.PivotList.Find((AreaSubjectCaseInfo c) => c.SecFieldValue.Equals("其他警情"))) != null)
                        {
                            othorAlarm = asc.SecCount;
                            goto IL_230;
                        }
                    }
                    othorAlarm = 0;
                    IL_230:
                    alarmStatisticItem7.OthorAlarm = othorAlarm;
                    AlarmStatisticItem alarmStatisticItem8 = alarmStatisticItem;
                    int disputeAlarm;
                    if (IEnumerableExtension.HasValues<AreaSubjectCaseInfo>(item.PivotList))
                    {
                        if ((asc = item.PivotList.Find((AreaSubjectCaseInfo c) => c.SecFieldValue.Equals("纠纷"))) != null)
                        {
                            disputeAlarm = asc.SecCount;
                            goto IL_287;
                        }
                    }
                    disputeAlarm = 0;
                    IL_287:
                    alarmStatisticItem8.DisputeAlarm = disputeAlarm;
                    AlarmStatisticItem alarmStatisticItem9 = alarmStatisticItem;
                    int reportAlarm;
                    if (IEnumerableExtension.HasValues<AreaSubjectCaseInfo>(item.PivotList))
                    {
                        if ((asc = item.PivotList.Find((AreaSubjectCaseInfo c) => c.SecFieldValue.Equals("举报投诉"))) != null)
                        {
                            reportAlarm = asc.SecCount;
                            goto IL_2DE;
                        }
                    }
                    reportAlarm = 0;
                    IL_2DE:
                    alarmStatisticItem9.ReportAlarm = reportAlarm;
                    AlarmStatisticItem alarmStatisticItem10 = alarmStatisticItem;
                    int incidentAlarm;
                    if (IEnumerableExtension.HasValues<AreaSubjectCaseInfo>(item.PivotList))
                    {
                        if ((asc = item.PivotList.Find((AreaSubjectCaseInfo c) => c.SecFieldValue.Equals("事件"))) != null)
                        {
                            incidentAlarm = asc.SecCount;
                            goto IL_335;
                        }
                    }
                    incidentAlarm = 0;
                    IL_335:
                    alarmStatisticItem10.IncidentAlarm = incidentAlarm;
                    AlarmStatisticItem alarmStatisticItem11 = alarmStatisticItem;
                    int disasterAlarm;
                    if (IEnumerableExtension.HasValues<AreaSubjectCaseInfo>(item.PivotList))
                    {
                        if ((asc = item.PivotList.Find((AreaSubjectCaseInfo c) => c.SecFieldValue.Equals("灾害事故"))) != null)
                        {
                            disasterAlarm = asc.SecCount;
                            goto IL_38C;
                        }
                    }
                    disasterAlarm = 0;
                    IL_38C:
                    alarmStatisticItem11.DisasterAlarm = disasterAlarm;
                    AlarmStatisticItem alarmStatisticItem12 = alarmStatisticItem;
                    int otherAdministrativeAlarm;
                    if (IEnumerableExtension.HasValues<AreaSubjectCaseInfo>(item.PivotList))
                    {
                        if ((asc = item.PivotList.Find((AreaSubjectCaseInfo c) => c.SecFieldValue.Equals("其他行政违法"))) != null)
                        {
                            otherAdministrativeAlarm = asc.SecCount;
                            goto IL_3E3;
                        }
                    }
                    otherAdministrativeAlarm = 0;
                    IL_3E3:
                    alarmStatisticItem12.OtherAdministrativeAlarm = otherAdministrativeAlarm;
                    CollectionExtension.AddEx<AlarmStatisticItem>(alarmStatisticItems, alarmStatisticItem);
                });
                result = ad;
            }
            return result;
        }

        private AlarmAllKindStatistics AnalysisAlarm(AlarmChromatography alarmchpy, AlarmStatisticData alarmsd)
        {
            bool flag = this.alarmchpy == null || !IEnumerableExtension.HasValues<Chromatography>(this.alarmchpy.Chromatographies);
            AlarmAllKindStatistics result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = this.alarmsd == null || !IEnumerableExtension.HasValues<AlarmStatisticItem>(this.alarmsd.AlarmStatisticItems);
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    AlarmAllKindStatistics alarmaks = new AlarmAllKindStatistics();
                    alarmaks.AlarmKindStatistics = new List<AlarmKindStatistic>();
                    AlarmKindStatistic aks = null;
                    List<AlarmKindStatisticItem> lst = null;
                    AlarmKindStatisticItem aksItem = null;
                    this.alarmchpy.Chromatographies.ForEach(delegate (Chromatography graphy)
                    {
                        aks = new AlarmKindStatistic();
                        CollectionExtension.AddEx<AlarmKindStatistic>(alarmaks.AlarmKindStatistics, aks);
                        aks.Kind = graphy.Kind;
                        lst = new List<AlarmKindStatisticItem>();
                        aks.AlarmKindStatisticItems = lst;
                        this.alarmsd.AlarmStatisticItems.ForEach(delegate (AlarmStatisticItem item)
                        {
                            aksItem = new AlarmKindStatisticItem();
                            aksItem.PoliceStationName = item.PoliceStationName;
                            string kind = aks.Kind;

                            //uint num = <PrivateImplementationDetails>.ComputeStringHash(kind);
                            //if (num <= 2332207408u)
                            //{
                            //	if (num <= 1586853236u)
                            //	{
                            //		if (num != 1093334470u)
                            //		{
                            //			if (num != 1274959673u)
                            //			{
                            //				if (num == 1586853236u)
                            //				{
                            //					if (kind == "事件")
                            //					{
                            //						aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.IncidentAlarm >= cgitem.MinValue && item.IncidentAlarm <= cgitem.MaxValue).Color;
                            //					}
                            //				}
                            //			}
                            //			else if (kind == "有效警情")
                            //			{
                            //				aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.EffectiveAlarm >= cgitem.MinValue && item.EffectiveAlarm <= cgitem.MaxValue).Color;
                            //			}
                            //		}
                            //		else if (kind == "灾害事故")
                            //		{
                            //			aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.DisasterAlarm >= cgitem.MinValue && item.DisasterAlarm <= cgitem.MaxValue).Color;
                            //		}
                            //	}
                            //	else if (num <= 1720221765u)
                            //	{
                            //		if (num != 1634892554u)
                            //		{
                            //			if (num == 1720221765u)
                            //			{
                            //				if (kind == "交通类警情")
                            //				{
                            //					aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.TrafficAlarm >= cgitem.MinValue && item.TrafficAlarm <= cgitem.MaxValue).Color;
                            //				}
                            //			}
                            //		}
                            //		else if (kind == "其他行政违法")
                            //		{
                            //			aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.OtherAdministrativeAlarm >= cgitem.MinValue && item.OtherAdministrativeAlarm <= cgitem.MaxValue).Color;
                            //		}
                            //	}
                            //	else if (num != 2024465558u)
                            //	{
                            //		if (num == 2332207408u)
                            //		{
                            //			if (kind == "火灾事故")
                            //			{
                            //				aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.FireAlarm >= cgitem.MinValue && item.FireAlarm <= cgitem.MaxValue).Color;
                            //			}
                            //		}
                            //	}
                            //	else if (kind == "举报投诉")
                            //	{
                            //		aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.ReportAlarm >= cgitem.MinValue && item.ReportAlarm <= cgitem.MaxValue).Color;
                            //	}
                            //}
                            //else if (num <= 3026857017u)
                            //{
                            //	if (num != 2399312156u)
                            //	{
                            //		if (num != 2484088676u)
                            //		{
                            //			if (num == 3026857017u)
                            //			{
                            //				if (kind == "群众求助")
                            //				{
                            //					aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.RescueAlarm >= cgitem.MinValue && item.RescueAlarm <= cgitem.MaxValue).Color;
                            //				}
                            //			}
                            //		}
                            //		else if (kind == "无效警情")
                            //		{
                            //			aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.InvalidAlarm >= cgitem.MinValue && item.InvalidAlarm <= cgitem.MaxValue).Color;
                            //		}
                            //	}
                            //	else if (kind == "其他警情")
                            //	{
                            //		aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.OthorAlarm >= cgitem.MinValue && item.OthorAlarm <= cgitem.MaxValue).Color;
                            //	}
                            //}
                            //else if (num <= 3625070531u)
                            //{
                            //	if (num != 3327335512u)
                            //	{
                            //		if (num == 3625070531u)
                            //		{
                            //			if (kind == "刑事案件")
                            //			{
                            //				aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.SecurityAlarm >= cgitem.MinValue && item.SecurityAlarm <= cgitem.MaxValue).Color;
                            //			}
                            //		}
                            //	}
                            //	else if (kind == "纠纷")
                            //	{
                            //		aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.DisputeAlarm >= cgitem.MinValue && item.DisputeAlarm <= cgitem.MaxValue).Color;
                            //	}
                            //}
                            //else if (num != 3997020140u)
                            //{
                            //	if (num == 4030234561u)
                            //	{
                            //		if (kind == "行政（治安）案件")
                            //		{
                            //			aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.SecurityAlarm >= cgitem.MinValue && item.SecurityAlarm <= cgitem.MaxValue).Color;
                            //		}
                            //	}
                            //}
                            //else if (kind == "接警总量")
                            //{
                            //	aksItem.Color = graphy.ChromatographyItems.First((ChromatographyItem cgitem) => item.TotalAlarm >= cgitem.MinValue && item.TotalAlarm <= cgitem.MaxValue).Color;
                            //}
                            CollectionExtension.AddEx<AlarmKindStatisticItem>(lst, aksItem);
                        });
                    });
                    result = alarmaks;
                }
            }
            return result;
        }

        private bool CreateStatisticLayers(IFeatureClass fc, ITextRender txtRender, AlarmAllKindStatistics alarmaks)
        {
            bool flag = fc == null;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = alarmaks == null || !IEnumerableExtension.HasValues<AlarmKindStatistic>(alarmaks.AlarmKindStatistics);
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    bool flag3 = this.Layers == null;
                    if (flag3)
                    {
                        this.Layers = new Dictionary<string, IDisplayLayer>();
                    }
                    IFeatureLayer fly = null;
                    alarmaks.AlarmKindStatistics.ForEach(delegate (AlarmKindStatistic ast)
                    {
                        fly = ObjectManagerExtension.CreateFeatureLayer(GviMap.MapControl.ObjectManager, fc, fc.GeometryFields().FirstOrDefault<string>(), txtRender, this.CreateGeometryRender(ast));
                        bool flag4 = fly != null;
                        if (flag4)
                        {
                            fly.VisibleMask = gviViewportMask.gviViewNone;
                            fly.MaxVisibleDistance = 500000.0;
                            fly.MinVisiblePixels = 15f;
                        }
                        CollectionExtension.AddEx<string, IDisplayLayer>(this.Layers, ast.Kind, new DisplayLayer(fc, fly));
                    });
                    result = true;
                }
            }
            return result;
        }

        private IGeometryRender CreateGeometryRender(AlarmKindStatistic akst)
        {
            bool flag = akst == null || !IEnumerableExtension.HasValues<AlarmKindStatisticItem>(akst.AlarmKindStatisticItems);
            IGeometryRender result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IValueMapGeometryRender geoRender = null;
                IGeometryRenderScheme geoScheme = null;
                UniqueValuesRenderRule uniqueRule = null;
                SurfaceSymbol geoSymbol = null;
                CurveSymbol boundarySymbol = null;
                geoRender = new ValueMapGeometryRender();
                geoRender.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                geoRender.HeightOffset = 0.0;
                boundarySymbol = new CurveSymbol
                {
                    Color = ColorConvert.UintToColor(4278190080u),
                    RepeatLength = 1f,
                    Width = 0f,
                    Pattern = gviDashStyle.gviDashSolid
                };
                Action<AlarmKindStatisticItem> actions = null;
                IEnumerableExtension.ForEach<IGrouping<uint, AlarmKindStatisticItem>>(from item in akst.AlarmKindStatisticItems
                                                                                      group item by item.Color, delegate (IGrouping<uint, AlarmKindStatisticItem> group)
                                                                                      {
                                                                                          geoScheme = new GeometryRenderScheme();
                                                                                          uniqueRule = new UniqueValuesRenderRule();
                                                                                          uniqueRule.LookUpField = "MC";
                                                                                          uniqueRule.Otherwise = false;
                                                                                          Action<AlarmKindStatisticItem> action;
                                                                                          if ((action = actions) == null)
                                                                                          {
                                                                                              action = (actions = delegate (AlarmKindStatisticItem value)
                                                                                              {
                                                                                                  uniqueRule.AddValue(value.PoliceStationName);
                                                                                              });
                                                                                          }
                                                                                          IEnumerableExtension.ForEach<AlarmKindStatisticItem>(group, action);
                                                                                          geoScheme.AddRule(uniqueRule);
                                                                                          geoSymbol = new SurfaceSymbol();
                                                                                          geoSymbol.Color = ColorConvert.UintToColor(group.Key);
                                                                                          geoSymbol.EnableLight = false;
                                                                                          geoSymbol.BoundarySymbol = boundarySymbol;
                                                                                          geoScheme.Symbol = geoSymbol;
                                                                                          geoRender.AddScheme(geoScheme);
                                                                                      });
                IGeometryRenderScheme scheme = new GeometryRenderScheme();
                geoRender.AddScheme(scheme);
                result = geoRender;
            }
            return result;
        }

        private void InitXml()
        {
            this.InitChromatographyXml();
            this.ReadAlarmStatisticData();
        }

        private void InitChromatographyXml()
        {
            AlarmChromatography alarmChromatography = new AlarmChromatography
            {
                Chromatographies = new List<Chromatography>
                {
                    new Chromatography
                    {
                        ChromatographyItems = new List<ChromatographyItem>
                        {
                            new ChromatographyItem
                            {
                                MinValue = 0,
                                MaxValue = 150,
                                Color = 4278190335u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 151,
                                MaxValue = 300,
                                Color = 4294967040u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 301,
                                MaxValue = 450,
                                Color = 4294953984u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 451,
                                MaxValue = int.MaxValue,
                                Color = 4294901760u
                            }
                        },
                        Kind = "接警总量"
                    },
                    new Chromatography
                    {
                        ChromatographyItems = new List<ChromatographyItem>
                        {
                            new ChromatographyItem
                            {
                                MinValue = 0,
                                MaxValue = 80,
                                Color = 4278190335u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 81,
                                MaxValue = 150,
                                Color = 4294967040u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 151,
                                MaxValue = 200,
                                Color = 4294953984u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 21,
                                MaxValue = int.MaxValue,
                                Color = 4294901760u
                            }
                        },
                        Kind = "有效警情"
                    },
                    new Chromatography
                    {
                        ChromatographyItems = new List<ChromatographyItem>
                        {
                            new ChromatographyItem
                            {
                                MinValue = 0,
                                MaxValue = 100,
                                Color = 4278190335u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 101,
                                MaxValue = 200,
                                Color = 4294967040u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 201,
                                MaxValue = 300,
                                Color = 4294953984u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 301,
                                MaxValue = int.MaxValue,
                                Color = 4294901760u
                            }
                        },
                        Kind = "无效警情"
                    },
                    new Chromatography
                    {
                        ChromatographyItems = new List<ChromatographyItem>
                        {
                            new ChromatographyItem
                            {
                                MinValue = 0,
                                MaxValue = 8,
                                Color = 4278190335u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 9,
                                MaxValue = 16,
                                Color = 4294967040u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 17,
                                MaxValue = 24,
                                Color = 4294953984u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 25,
                                MaxValue = int.MaxValue,
                                Color = 4294901760u
                            }
                        },
                        Kind = "治安事件"
                    },
                    new Chromatography
                    {
                        ChromatographyItems = new List<ChromatographyItem>
                        {
                            new ChromatographyItem
                            {
                                MinValue = 0,
                                MaxValue = 6,
                                Color = 4278190335u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 7,
                                MaxValue = 12,
                                Color = 4294967040u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 13,
                                MaxValue = 18,
                                Color = 4294953984u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 19,
                                MaxValue = int.MaxValue,
                                Color = 4294901760u
                            }
                        },
                        Kind = "刑事案件"
                    },
                    new Chromatography
                    {
                        ChromatographyItems = new List<ChromatographyItem>
                        {
                            new ChromatographyItem
                            {
                                MinValue = 0,
                                MaxValue = 20,
                                Color = 4278190335u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 21,
                                MaxValue = 40,
                                Color = 4294967040u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 41,
                                MaxValue = 60,
                                Color = 4294953984u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 61,
                                MaxValue = int.MaxValue,
                                Color = 4294901760u
                            }
                        },
                        Kind = "群众救助"
                    },
                    new Chromatography
                    {
                        ChromatographyItems = new List<ChromatographyItem>
                        {
                            new ChromatographyItem
                            {
                                MinValue = 0,
                                MaxValue = 20,
                                Color = 4278190335u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 21,
                                MaxValue = 40,
                                Color = 4294967040u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 41,
                                MaxValue = 60,
                                Color = 4294953984u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 61,
                                MaxValue = int.MaxValue,
                                Color = 4294901760u
                            }
                        },
                        Kind = "交通事故"
                    },
                    new Chromatography
                    {
                        ChromatographyItems = new List<ChromatographyItem>
                        {
                            new ChromatographyItem
                            {
                                MinValue = 0,
                                MaxValue = 2,
                                Color = 4278190335u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 3,
                                MaxValue = 4,
                                Color = 4294967040u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 5,
                                MaxValue = 6,
                                Color = 4294953984u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 7,
                                MaxValue = int.MaxValue,
                                Color = 4294901760u
                            }
                        },
                        Kind = "火灾事故"
                    },
                    new Chromatography
                    {
                        ChromatographyItems = new List<ChromatographyItem>
                        {
                            new ChromatographyItem
                            {
                                MinValue = 0,
                                MaxValue = 4,
                                Color = 4278190335u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 5,
                                MaxValue = 8,
                                Color = 4294967040u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 9,
                                MaxValue = 12,
                                Color = 4294953984u
                            },
                            new ChromatographyItem
                            {
                                MinValue = 13,
                                MaxValue = int.MaxValue,
                                Color = 4294901760u
                            }
                        },
                        Kind = "其他警情"
                    }
                }
            };
            ConfigHelper<AlarmChromatography>.SaveXml("E:\\Gvitech\\04-coding\\2015PoliceGIS3D\\Mmc.Mspace.StatisticsModule\\Chromatography.xml", alarmChromatography);
        }

        private void InitAlarmStatisticDataXml()
        {
            AlarmStatisticData alarmStatisticData = new AlarmStatisticData
            {
                AlarmStatisticItems = new List<AlarmStatisticItem>
                {
                    new AlarmStatisticItem
                    {
                        PoliceStationName = "中山公园派出所",
                        TotalAlarm = 60,
                        EffectiveAlarm = 24,
                        InvalidAlarm = 36,
                        SecurityAlarm = 3,
                        CriminalAlarm = 2,
                        RescueAlarm = 7,
                        TrafficAlarm = 9,
                        FireAlarm = 1,
                        OthorAlarm = 2
                    },
                    new AlarmStatisticItem
                    {
                        PoliceStationName = "火车站派出所",
                        TotalAlarm = 90,
                        EffectiveAlarm = 36,
                        InvalidAlarm = 54,
                        SecurityAlarm = 5,
                        CriminalAlarm = 3,
                        RescueAlarm = 10,
                        TrafficAlarm = 13,
                        FireAlarm = 2,
                        OthorAlarm = 3
                    }
                }
            };
            ConfigHelper<AlarmStatisticData>.SaveXml("E:\\Gvitech\\04-coding\\2015PoliceGIS3D\\Mmc.Mspace.StatisticsModule\\AlarmStatisticData.xml", alarmStatisticData);
        }

        private void ReadAlarmStatisticData()
        {
            AlarmStatisticData alarmStatisticData = new AlarmStatisticData();
            List<AlarmStatisticItem> list = new List<AlarmStatisticItem>();
            object missing = Type.Missing;
            Application application = null;
            Workbooks workbooks = null;
            Workbook workbook = null;
            try
            {
                application = new ApplicationClass();
                workbooks = application.Workbooks;
                workbook = workbooks.Add("C:\\Users\\xiangjian\\Desktop\\专题统计--案件数量测试数据.xlsx");
                Sheets worksheets = workbook.Worksheets;
                Worksheet worksheet = worksheets[1] as Worksheet;
                int count = worksheet.UsedRange.Rows.Count;
                int num;
                for (int i = 7; i <= count; i = num + 1)
                {
                    //CollectionExtension.AddEx<AlarmStatisticItem>(list, new AlarmStatisticItem
                    //{
                    //	PoliceStationName = ((Range)worksheet.Cells.get__Default(i, "A")).get_Value(missing).ToString(),
                    //	TotalAlarm = StringExtension.ParseTo<int>(((Range)worksheet.Cells.get__Default(i, "B")).get_Value(missing), 0),
                    //	EffectiveAlarm = StringExtension.ParseTo<int>(((Range)worksheet.Cells.get__Default(i, "C")).get_Value(missing), 0),
                    //	InvalidAlarm = StringExtension.ParseTo<int>(((Range)worksheet.Cells.get__Default(i, "D")).get_Value(missing), 0),
                    //	SecurityAlarm = StringExtension.ParseTo<int>(((Range)worksheet.Cells.get__Default(i, "E")).get_Value(missing), 0),
                    //	CriminalAlarm = StringExtension.ParseTo<int>(((Range)worksheet.Cells.get__Default(i, "F")).get_Value(missing), 0),
                    //	RescueAlarm = StringExtension.ParseTo<int>(((Range)worksheet.Cells.get__Default(i, "G")).get_Value(missing), 0),
                    //	TrafficAlarm = StringExtension.ParseTo<int>(((Range)worksheet.Cells.get__Default(i, "H")).get_Value(missing), 0),
                    //	FireAlarm = StringExtension.ParseTo<int>(((Range)worksheet.Cells.get__Default(i, "I")).get_Value(missing), 0),
                    //	OthorAlarm = StringExtension.ParseTo<int>(((Range)worksheet.Cells.get__Default(i, "J")).get_Value(missing), 0)
                    //});
                    num = i;
                }
            }
            catch (Exception value)
            {
                Console.WriteLine(value);
            }
            finally
            {
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                workbooks.Close();
                application.Quit();
            }
            alarmStatisticData.AlarmStatisticItems = list;
            ConfigHelper<AlarmStatisticData>.SaveXml("E:\\Gvitech\\04-coding\\2015PoliceGIS3D\\Mmc.Mspace.StatisticsModule\\AlarmStatisticData.xml", alarmStatisticData);
        }

        private static bool isResolved;

        private AlarmAllKindStatistics alarmaks;

        private AlarmChromatography alarmchpy;

        private AlarmStatisticData alarmsd;
    }
}