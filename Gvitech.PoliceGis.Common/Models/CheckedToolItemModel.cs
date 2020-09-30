using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using Mmc.Mspace.Common.ResourceServices;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.Common.Models
{
	public class CheckedToolItemModel : ToolItemModel
	{
        private Action onCheckEvent;
        private Action onUnCheckEvent;

        public override void Initialize()
        {
            base.Initialize();
            onCheckEvent = new Action(this.OnChecked);
            onUnCheckEvent= new Action(this.OnUnchecked);
        }
        public bool IsChecked
        {
            get
            {
                return this._isChecked;
            }
            set
            {
                SetAndNotifyPropertyChanged<bool>(ref this._isChecked, value, "IsChecked");
                if (_isChecked)
                {
                    
                    //  暂时屏蔽互斥行为，影响性能
                    string groupName = base.GroupName;
                    if (!string.IsNullOrEmpty(groupName))
                    {
                            List<CheckedToolItemModel> list = CheckedToolItemModel.AllCheckedCmds.FindAll((CheckedToolItemModel p) => p.GroupName == groupName);
                        
                            if (string.IsNullOrEmpty(base.IsConflictFlag))
                            {
                                if (list.Count > 0)
                                {
                                    foreach (CheckedToolItemModel checkedToolItemModel in list)
                                    {
                                        checkedToolItemModel.IsChecked = false;
                                       
                                    }
                                }
                            }
                            this._isChecked = true;
                            list.Clear();
                        if (!AllCheckedCmds.Contains(this))
                            AllCheckedCmds.Add(this);
                    }
                    //暂时屏蔽互斥行为，影响性能

                    Application.Current.Dispatcher.Invoke(onCheckEvent, DispatcherPriority.Normal);
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(onUnCheckEvent, DispatcherPriority.Normal);
                }
            }
        }
     

        public string CheckedIcon
		{
			get
			{
				return Singleton<ResourceService>.Instance.GetImagePath() + this._checkedIcon;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<string>(ref this._checkedIcon, value, "CheckedIcon");
			}
		}

		public virtual void OnChecked()
		{
		}

		public virtual void OnUnchecked()
		{
		}

		public static List<CheckedToolItemModel> AllCheckedCmds = new List<CheckedToolItemModel>();


		private bool _isChecked;

		private string _checkedIcon;
  
    }
}
