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
    public class PeopleDetailViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
            });
        }

        public override void OnChecked()
        {
            base.OnChecked();

            //UnitId = "30755c00b2fe4079b22a8479f6cde90e";
            //http 请求测试数据
            HttpService httpService = new HttpService();
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);

            string url = string.Format(@"{0}{1}", json.useUnitServiceUrl, UnitId);
            var result = httpService.HttpRequestAsync(url);
            {
                var DynamicObject = JsonConvert.DeserializeObject<dynamic>(result);

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
                    //ImgName = "pack://siteoforigin:,,,/Resources/BuildInfo/BuildImage.png",
                };
                BuildCode = deatailObj.buildCode;
                var dutyInfos = deatailObj.dutyInfos;
                foreach (var dutyInfoObj in dutyInfos)
                {
                    if (dutyInfoObj.type == "1")
                    {
                        PeopleName = dutyInfoObj.name;
                        PeoplePhone = dutyInfoObj.phone;
                    }
                }

                string urlUnitDetail = string.Format(@"{0}/{1}", json.unitServiceUrl, UnitId);
                var unitDetailJson = httpService.HttpRequestAsync(urlUnitDetail);
                var unitDetailResult = JsonConvert.DeserializeObject<dynamic>(unitDetailJson).data;
                this.unitName = unitDetailResult.unitName;
            }
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
        private string peopleName;
        private string peoplePhone;
        private string buildCode;
        private string unitName;

        public string BuildCode
        {
            get { return this.buildCode; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.buildCode, value, "BuildCode"); }
        }

        public BuildContentViewModel BuildContent
        {
            get { return this.buildContent; }
            set { base.SetAndNotifyPropertyChanged<BuildContentViewModel>(ref this.buildContent, value, "BuildContent"); }
        }

        /// <summary>
        /// 人
        /// </summary>
        public string PeopleName
        {
            get { return this.peopleName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.peopleName, value, "PeopleName"); }
        }

        /// <summary>
        /// 电话
        /// </summary>
        public string PeoplePhone
        {
            get { return this.peoplePhone; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.peoplePhone, value, "PeoplePhone"); }
        }

        public string BuildAdrress
        {
            get { return this.unitName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.unitName, value, "BuildAdrress"); }
        }
    }
}