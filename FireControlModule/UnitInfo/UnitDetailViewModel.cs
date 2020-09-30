using FireControlModule.BuildInfo;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace FireControlModule.UnitInfo
{
    public class UnitDetailViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
            });

            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = json.threeUnitUrl;

            url = string.Format("{0}{1}", json.unitBaseInfo, UnitId);
            this.UnitBaseInfo = new UnitListViewModel()
            {
                Icon = "BuildInfo/单位.png",
                UnitId = UnitId,
                RequestUrl = url,
            };

            url = string.Format("{0}{1}", json.unitProblemInfo, UnitId);
            this.UnitProblem = new UnitListViewModel()
            {
                Icon = "BuildInfo/问题.png",
                UnitId = UnitId,
                RequestUrl = url,
            };

            this.UnitVideo = new UnitVideoViewModel()
            {
                Icon = "BuildInfo/视频.png",
                UnitId = UnitId,
            };
        }

        public override void OnChecked()
        {
            base.OnChecked();

            //UnitId = "30755c00b2fe4079b22a8479f6cde90e";
            //http 请求测试数据
            HttpService httpService = new HttpService();
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);

            string url = string.Format(@"{0}{1}", json.useUnitServiceUrl, UnitId);
            try
            {
                var result = httpService.HttpRequestAsync(url);
                {
                    var DynamicObject = JsonConvert.DeserializeObject<dynamic>(result);

                    if (DynamicObject.data == null || DynamicObject.data.Count == 0)
                    {
                    }
                    else
                    {
                        var buildId = DynamicObject.data[0].buildId;
                        string urlDetail = string.Format(@"{0}/{1}", json.buildServiceUrl, buildId);
                        var detailJson = httpService.HttpRequestAsync(urlDetail);
                        var detailResult = JsonConvert.DeserializeObject<dynamic>(detailJson);
                        var deatailObj = detailResult.data;
                        string imgName = deatailObj.buildPicture;
                        imgName = string.IsNullOrEmpty(imgName) ? "pack://siteoforigin:,,,/Resources/BuildInfo/BuildImage.png" : imgName;
                        BuildContent = new BuildContentViewModel
                        {
                            BuildCode = deatailObj.buildCode,
                            BuildName = deatailObj.buildName,
                            BuildAdrress = deatailObj.buildAddress,
                            ImgName = imgName,
                            BuildAdrressTitle = "单位地址",
                            BuildNameTitle = "单位名称",
                            BuildCodeTitle = "单位编号",
                            //ImgName = "pack://siteoforigin:,,,/Resources/BuildInfo/BuildImage.png",
                        };
                        BuildCode = deatailObj.buildCode;
                        var dutyInfos = deatailObj.dutyInfos;
                        foreach (var dutyInfoObj in dutyInfos)
                        {
                            if (dutyInfoObj.type == "1")
                            {
                                OwnerName = dutyInfoObj.name;
                                OwnerPhone = dutyInfoObj.phone;
                            }
                        }

                        string urlUnitDetail = string.Format(@"{0}/{1}", json.unitServiceUrl, UnitId);
                        var unitDetailJson = httpService.HttpRequestAsync(urlUnitDetail);
                        var unitDetailResult = JsonConvert.DeserializeObject<dynamic>(unitDetailJson).data;
                        this.unitName = unitDetailResult.unitName;
                    }
                }
            }
            catch (Exception ex)
            {
                Mmc.Windows.Services.SystemLog.Log(ex);
            }

            url = string.Format("{0}{1}", json.unitBaseInfo, UnitId);
            this.UnitBaseInfo.RequestUrl = url;
            url = string.Format("{0}{1}", json.unitProblemInfo, UnitId);
            this.UnitProblem.RequestUrl = url;

            this.UnitVideo.UnitId = UnitId;
            //UnitDetailView unitView = (UnitDetailView)base.View;

            //显示窗体
            ((Window)base.View).Show();
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            ((Window)base.View).Hide();
        }

        public override FrameworkElement CreatedView()
        {
            return new UnitDetailView()
            {
                Owner = Application.Current.MainWindow
            };
        }

        //企业单位 类型  三
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        private BuildContentViewModel buildContent;

        public string UnitId { get; set; }
        private string ownerName;
        private string ownerPhone;
        private string buildCode;
        private string unitName;

        public string BuildCode
        {
            get { return this.buildCode; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.buildCode, value, "BuildCode"); }
        }

        [XmlIgnore]
        public BuildContentViewModel BuildContent
        {
            get { return this.buildContent; }
            set { base.SetAndNotifyPropertyChanged<BuildContentViewModel>(ref this.buildContent, value, "BuildContent"); }
        }

        private UnitVideoViewModel unitVideo;

        [XmlIgnore]
        public UnitVideoViewModel UnitVideo
        {
            get { return this.unitVideo; }
            set { base.SetAndNotifyPropertyChanged<UnitVideoViewModel>(ref this.unitVideo, value, "UnitVideo"); }
        }

        private UnitListViewModel unitProblem;

        [XmlIgnore]
        public UnitListViewModel UnitProblem
        {
            get { return this.unitProblem; }
            set { base.SetAndNotifyPropertyChanged<UnitListViewModel>(ref this.unitProblem, value, "UnitProblem"); }
        }

        private UnitListViewModel unitBaseInfo;

        [XmlIgnore]
        public UnitListViewModel UnitBaseInfo
        {
            get { return this.unitBaseInfo; }
            set { base.SetAndNotifyPropertyChanged<UnitListViewModel>(ref this.unitBaseInfo, value, "UnitBaseInfo"); }
        }

        /// <summary>
        /// 法人
        /// </summary>
        public string OwnerName
        {
            get { return this.ownerName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.ownerName, value, "OwnerName"); }
        }

        /// <summary>
        /// 电话
        /// </summary>
        public string OwnerPhone
        {
            get { return this.ownerPhone; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.ownerPhone, value, "OwnerPhone"); }
        }

        public string UnitName
        {
            get { return this.unitName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.unitName, value, "UnitName"); }
        }
    }
}