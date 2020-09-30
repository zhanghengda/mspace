using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace FireControlModule
{
    public class BuildDetailViewModel : CheckedToolItemModel
    {
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        public string BuildOid { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
                var layers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
                layers.ForEach(p =>
                {
                    if (p.Fc.Alias == "建筑")
                    {
                        if (string.IsNullOrEmpty(BuildOid))
                            GviMap.FeatureManager.UnhighlightFeatureClass(p.Fc);
                        else
                            GviMap.FeatureManager.UnhighlightFeature(p.Fc, BuildOid.ParseTo<int>());
                    }
                });
            });

            BuildContent = new BuildContentViewModel
            {
                BuildCode = "xxx",
                BuildName = "xxx",
                BuildAdrress = "xxx",
                ImgName = "pack://siteoforigin:,,,/Resources/BuildInfo/BuildImage.png",
            };
            OwnerName = "xxx";
            Manager = "xxx";
            OwnerPhone = "xxx";
            ManagerPhone = "xxx";
        }

        public override void Reset()
        {
            base.Reset();
            base.IsChecked = false;
        }

        public override FrameworkElement CreatedView()
        {
            return new BuildDetailView()
            {
                Owner = Application.Current.MainWindow
            };
        }

        public override void OnChecked()
        {
            base.OnChecked();

            try
            {
                //http 请求测试数据
                HttpService httpService = new HttpService();
                var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                string url = string.Format(@"{0}?buildCode={1}", json.buildServiceUrl, BuildCode);
                var result = httpService.HttpRequestAsync(url);
                {
                    string imgName = string.Empty;
                    var DynamicObject = JsonConvert.DeserializeObject<dynamic>(result);
                    if (DynamicObject.data == null || DynamicObject.data.Count == 0)
                    {
                        BuildContent = new BuildContentViewModel
                        {
                            BuildCode = BuildCode,
                            BuildName = "xxx",
                            BuildAdrress = "xxx",
                            ImgName = "pack://siteoforigin:,,,/Resources/BuildInfo/BuildImage.png",
                        };
                        OwnerName = "xxx";
                        Manager = "xxx";
                        OwnerPhone = "xxx";
                        ManagerPhone = "xxx";
                    }
                    else
                    {
                        var buildId = DynamicObject.data[0].buildId;
                        string urlDetail = string.Format(@"{0}/{1}", json.buildServiceUrl, buildId);
                        var detailJson = httpService.HttpRequestAsync(urlDetail);
                        var detailResult = JsonConvert.DeserializeObject<dynamic>(detailJson);
                        var deatailObj = detailResult.data;
                        imgName = deatailObj.buildPicture;
                        imgName = string.IsNullOrEmpty(imgName) ? "pack://siteoforigin:,,,/Resources/BuildInfo/BuildImage.png" : imgName;
                        BuildContent = new BuildContentViewModel
                        {
                            BuildCode = this.BuildCode,
                            BuildName = deatailObj.buildName,
                            BuildAdrress = deatailObj.buildAddress,
                            ImgName = imgName,
                            //ImgName = "pack://siteoforigin:,,,/Resources/BuildInfo/BuildImage.png",
                        };

                        var dutyInfos = deatailObj.dutyInfos;
                        foreach (var dutyInfoObj in dutyInfos)
                        {
                            if (dutyInfoObj.type == "1")
                            {
                                OwnerName = dutyInfoObj.name;
                                OwnerPhone = dutyInfoObj.phone;
                            }
                            else if (dutyInfoObj.type == "2")
                            {
                                Manager = dutyInfoObj.name;
                                ManagerPhone = dutyInfoObj.phone;
                            }
                        }
                    }
                }
             //显示窗体
             ((Window)base.View).Show();
            }
            catch (Exception ex)
            {
                Mmc.Windows.Services.SystemLog.Log(ex);
            }
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            ((Window)base.View).Hide();
        }

        private BuildContentViewModel buildContent;
        private string ownerName;
        private string manager;
        private string ownerPhone;
        private string managerPhone;
        private string buildCode;

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

        public string OwnerName
        {
            get { return this.ownerName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.ownerName, value, "OwnerName"); }
        }

        public string Manager
        {
            get { return this.manager; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.manager, value, "Manager"); }
        }

        public string OwnerPhone
        {
            get { return this.ownerPhone; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.ownerPhone, value, "OwnerPhone"); }
        }

        public string ManagerPhone
        {
            get { return this.managerPhone; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.managerPhone, value, "ManagerPhone"); }
        }

        public static BuildDetailViewModel GetTestData()
        {
            return new BuildDetailViewModel()
            {
                BuildContent = new BuildContentViewModel
                {
                    BuildCode = "xxx",
                    BuildName = "xxx",
                    BuildAdrress = "xxx",
                    ImgName = "pack://siteoforigin:,,,/Resources/BuildInfo/BuildImage.png",
                },
                OwnerName = "xxx",
                Manager = "xxx",
                OwnerPhone = "xxx",
                ManagerPhone = "xxx",
            };
        }
    }
}