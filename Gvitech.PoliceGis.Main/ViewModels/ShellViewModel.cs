using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Common.Enum;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Main.Models;
using Mmc.Mspace.RegularInspectionModule.Dto;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Mmc.Mspace.Main.ViewModels
{
	// Token: 0x02000002 RID: 2
	public class ShellViewModel : BindableBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ShellViewModel()
		{
            try
            {

                //this.ShellModel = SerializationUtil.DeserializeFromXml<ShellModel>(ConfigPath.ShellConfig);

                ShellModel shellModel = new ShellModel()
                {
                    RowCount = 4,
                    ColumnCount = 6,
                    BackImage = "back.png"

                };
                var configsList = CacheData.UserInfo.mspace_config?.configs;
                var shellModelList = configsList.FindAll(p => p.config_name.Contains("ShellMenu"));
                List<ToolItemModel> modelArr = new List<ToolItemModel>();
                foreach (var item in shellModelList)
                {
                    try
                    {
                        var model = JsonConvert.DeserializeObject<object>(item.config_value, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Auto
                        });
                        if (model != null && model is ToolItemModel)
                        {
                            var tempModel = model as ToolItemModel;
                            modelArr.Add(tempModel);
                        }
                    }
                    catch (Exception ex)
                    {
                        SystemLog.Log(string.Format("界面解析异常：{0}", ex));
                    }
                }

                shellModel.BarMenuItems = new ObservableCollection<ToolItemModel>(modelArr.FindAll(p => p.MenuName == ShellMenuEnum.BarMenuItems.ToString()));
                shellModel.BottomToolMenuItems = new ObservableCollection<ToolItemModel>(modelArr.FindAll(p => p.MenuName == ShellMenuEnum.BottomToolMenuItems.ToString()));
                shellModel.RightToolMenuItems = new ObservableCollection<ToolItemModel>(modelArr.FindAll(p => p.MenuName == ShellMenuEnum.RightToolMenuItems.ToString()));
                 shellModel.ScaleViewModel = modelArr.Find(p => p.MenuName == ShellMenuEnum.ScaleViewModelMenu.ToString());
             
                this.ShellModel = shellModel;
                this.RigisterCommand();
                this.ShellModel.BarMenuItems[0].IsSelected = true;
            }
            catch (Exception ex)
            {
                SystemLog.WriteLog("ShellViewModel+", ex);
            }

        }


        private RelayCommand<DragEventArgs> _dragDomToOpenCmd;
        [XmlIgnore]
        public RelayCommand<DragEventArgs> DragDomToOpenCmd
        {
            get { return _dragDomToOpenCmd??(_dragDomToOpenCmd=new RelayCommand<DragEventArgs>(OpenDomToTarget)); }
            set { _dragDomToOpenCmd = value; }
        }
        private void OpenDomToTarget(DragEventArgs obj)
        {
            //throw new NotImplementedException();
            if (!obj.Data.GetDataPresent(typeof(InspectModel)))
            {
                obj.Effects = DragDropEffects.None;//不接受当前数据
                obj.Handled = true;
            }
        }
        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000002 RID: 2 RVA: 0x00002074 File Offset: 0x00000274
        public static ShellViewModel Instance
		{
			get
			{
				return ShellViewModel.instance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000208B File Offset: 0x0000028B
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002093 File Offset: 0x00000293
		public ShellModel ShellModel { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x0000209C File Offset: 0x0000029C
		public void RigisterCommand()
		{

		}

		// Token: 0x04000001 RID: 1
		private static readonly ShellViewModel instance = new ShellViewModel();
	}
}
