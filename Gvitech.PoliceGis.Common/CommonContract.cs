using Helpers;
using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Common
{
    public class CommonContract
    {
        public enum LanguageEnum
        {
            en_US=0,
            ru_RU=1,
            zh_CN=3,
        }

        /// <summary>
        /// (1:未处理  2：处理中  3：已处理)|
        /// </summary>
        public enum AccountStatus
        {
            Untreated=1,
            Processing=2,
            Processed=3
        }
        public enum RenderLayerType
        {
            RootLayer = 0,
            GroupLayer,
            TileGroupLayer,
            ImageGroupLayer,
            DataSetGroupLayer,
            ShpGroupLayer,
            FeatureLayer,
            TileLayer,
            ImageLayer,
            Poi,
        }

        public enum MessengerKey
        {
            Splitscreen,
            Openscreen,
            FlyToRederLayer,
            DeleteRederLayer,
        }
        public enum LeftMenuEnum
        {
            LeftManagementView = 1,
            RegularInspectionView = 2,
        }

        public enum RenderLayerStatus
        {
            AllHide,
            SaveStatus,
            RecoveryRenderStatus
        }

        /// <summary>
        /// 常态化巡检数据类型 Dom=正射，Picture=图片，Route=航线，Video=视频，Report=报告
        /// </summary>
        public enum InspectDataType
        {
            Region = 0,
            Unit,
            Dom,
            Picture,
            Route,
            Video,
            Report,
            Folder
        }
        /// <summary>
        /// 常态化巡检类型
        /// </summary>
        /// <returns></returns>
        public static List<TextItem> GetInspectDataType()
        {
            List<TextItem> list = new List<TextItem>()
            {
                new TextItem(){ Key= InspectDataType.Dom.ToString(),Value=ResourceHelper.FindKey("Inspection"+InspectDataType.Dom.ToString())},
                new TextItem(){ Key=InspectDataType.Picture.ToString(),Value=ResourceHelper.FindKey("Inspection"+InspectDataType.Picture.ToString())},
                new TextItem(){ Key=InspectDataType.Video.ToString(),Value=ResourceHelper.FindKey("Inspection"+InspectDataType.Video.ToString())},
                new TextItem(){ Key=InspectDataType.Route.ToString(),Value=ResourceHelper.FindKey("Inspection"+InspectDataType.Route.ToString())},
                new TextItem(){ Key=InspectDataType.Report.ToString(),Value=ResourceHelper.FindKey("Inspection"+InspectDataType.Report.ToString())}, 
            };
            return list;
        }

        public enum InspectDomTime
        {
            All = 0,
            LastMonth,
            LastThreeMonth,
            LastHalfYear,
            LastYear,
        }

        public static List<TextItem> GetInspectDomTime()
        {
            List<TextItem> list = new List<TextItem>()
            {
                new TextItem(){ Key= InspectDomTime.All.ToString(),Value=ResourceHelper.FindKey("DomTime"+InspectDomTime.All.ToString())},
                new TextItem(){ Key=InspectDomTime.LastMonth.ToString(),Value=ResourceHelper.FindKey("DomTime"+InspectDomTime.LastMonth.ToString())},
                new TextItem(){ Key=InspectDomTime.LastThreeMonth.ToString(),Value=ResourceHelper.FindKey("DomTime"+InspectDomTime.LastThreeMonth.ToString())},
                new TextItem(){ Key=InspectDomTime.LastHalfYear.ToString(),Value=ResourceHelper.FindKey("DomTime"+InspectDomTime.LastHalfYear.ToString())},
                new TextItem(){ Key=InspectDomTime.LastYear.ToString(),Value=ResourceHelper.FindKey("DomTime"+InspectDomTime.LastYear.ToString())},
            };
            return list;
        }

        public enum OperateDataStatus
        {
            NOPERMISSION=0,
            LOADFAILED,
            LOADSUCCESSED,
            DATAEXISTED, // 已打开的
            SAVEFAILED,
            SAVESUCCESSED
        }

        public enum TowerType
        {
            Straight,
            Wine,
            Safe,
            //Other
        }

        public static List<TextItem> GetTowerType()
        {
            List<TextItem> list = new List<TextItem>()
            {
                new TextItem(){ Key= TowerType.Straight.ToString(),Value=ResourceHelper.FindKey($"WT{TowerType.Straight}Tower")},
                new TextItem(){ Key= TowerType.Wine.ToString(),Value=ResourceHelper.FindKey($"WT{TowerType.Wine}Tower")},
                /*
                 * 屏蔽安全性塔 ，需求不明确 liangms 2019-7-31
                new TextItem(){ Key= TowerType.Safe.ToString(),Value=ResourceHelper.FindKey($"WT{TowerType.Safe}Tower")},
                */
                //new TextItem(){ Key= TowerType.Other.ToString(),Value=ResourceHelper.FindKey($"WT{TowerType.Other}Tower")},
            };
            return list;
        }

        public enum SignType
        {
            TopCenter,
            TopLeft,
            TopRight,
            Inner,
            Left,// one side
            //LeftUp,
            //LeftDown,
            Right,// another side
            //RightUp,
            //RightDown,
            Aided
            //Other
        }

        public static List<TextItem> GetSignType()
        {
            List<TextItem> list = new List<TextItem>()
            {
                new TextItem()
                {
                    Key = SignType.TopCenter.ToString(),
                    Value = ResourceHelper.FindKey($"WT{SignType.TopCenter}Sign")
                },
                new TextItem()
                {
                    Key = SignType.TopLeft.ToString(),
                    Value = ResourceHelper.FindKey($"WT{SignType.TopLeft}Sign")
                },
                new TextItem()
                {
                    Key = SignType.TopRight.ToString(),
                    Value = ResourceHelper.FindKey($"WT{SignType.TopRight}Sign")
                },
                new TextItem()
                {
                    Key = SignType.Inner.ToString(), Value = ResourceHelper.FindKey($"WT{SignType.Inner}Sign")
                },
                new TextItem()
                {
                    Key = SignType.Left.ToString(), Value = ResourceHelper.FindKey($"WT{SignType.Left}Sign")
                },
            
                new TextItem()
                {
                    Key = SignType.Right.ToString(), Value = ResourceHelper.FindKey($"WT{SignType.Right}Sign")
                },

                /*
                 * 屏蔽辅助点，需求不明确
                new textitem()
                    {key = SignType.Aided.ToString(), Value = ResourceHelper.FindKey($"WT{SignType.Aided}Sign")}
                */
            };
            return list;
        }

        public enum MarkRankType
        {
            First =1,
            Second =2,
            Third,
            Fourth,
            Fifth
        }

        public static List<TextItem> GetMarkRankType()
        {
            List<TextItem> list = new List<TextItem>()
            {
                new TextItem()
                {
                    Key = MarkRankType.First.ToString(),
                    Value = "1级"
                },
                new TextItem()
                {
                    Key = MarkRankType.Second.ToString(),
                    Value = "2级"
                },
                new TextItem()
                {
                    Key = MarkRankType.Third.ToString(),
                    Value = "3级"
                },
                new TextItem()
                {
                    Key = MarkRankType.Fourth.ToString(),
                    Value = "4级"
                },
                new TextItem()
                {
                    Key = MarkRankType.Fifth.ToString(),
                    Value = "5级"
                },

            };
            return list;
        }


        public enum MarkHandleStatus
        {
            WaitingForCheck = 1,
            NotInProcess,
            Handling,
            Handled,
            NoNeedHandle
        }

        public static List<TextItem> GetMarkHandleStatus()
        {
            List<TextItem> list = new List<TextItem>()
            {
                new TextItem()
                {
                    Key = MarkHandleStatus.WaitingForCheck.ToString(),
                    Value = "待审核"
                },
                new TextItem()
                {
                    Key = MarkHandleStatus.NotInProcess.ToString(),
                    Value = "未处理"
                },
                new TextItem()
                {
                    Key = MarkHandleStatus.Handling.ToString(),
                    Value = "处理中"
                },
                new TextItem()
                {
                    Key = MarkHandleStatus.Handled.ToString(),
                    Value = "已处理"
                },
                new TextItem()
                {
                    Key = MarkHandleStatus.NoNeedHandle.ToString(),
                    Value = "无需处理"
                }

            };
            return list;
        }
    }
}
