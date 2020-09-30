using System;
using System.Windows;
using System.Xml.Serialization;
using Mmc.Mspace.Common.ResourceServices;
using Mmc.Windows.Design;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.Common.Models
{
	// Token: 0x0200000A RID: 10
	public class ToolItemModel : BindableBase    {
		// Token: 0x06000058 RID: 88 RVA: 0x0000250F File Offset: 0x0000070F
		public ToolItemModel()
		{
			this.Initialize();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002530 File Offset: 0x00000730
		public object GetDefault(object args = null)
		{
			return this;
		}
        private bool _isSelected;
        [XmlAttribute]

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; base.NotifyPropertyChanged("IsSelected"); }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; base.NotifyPropertyChanged("Type"); }
        }

        public string MenuName
        {
            get { return _menuName; }
            set { _menuName = value; base.NotifyPropertyChanged("MenuName"); }
        }

        // Token: 0x17000022 RID: 34
        // (get) Token: 0x0600005A RID: 90 RVA: 0x00002543 File Offset: 0x00000743
        // (set) Token: 0x0600005B RID: 91 RVA: 0x0000254B File Offset: 0x0000074B
        [XmlAttribute]
		public string Content { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002554 File Offset: 0x00000754
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000255C File Offset: 0x0000075C
		[XmlAttribute]
		public string ToolTip { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002568 File Offset: 0x00000768
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000258F File Offset: 0x0000078F
		[XmlAttribute]
		public string Icon
		{
			get
			{
				return Singleton<ResourceService>.Instance.GetImagePath() + this._icon;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<string>(ref this._icon, value, "Icon");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000025A8 File Offset: 0x000007A8
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000025CF File Offset: 0x000007CF
		[XmlAttribute]
		public string MouseOverIcon
		{
			get
			{
				return Singleton<ResourceService>.Instance.GetImagePath() + this._mouseOverIcon;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<string>(ref this._mouseOverIcon, value, "MouseOverIcon");
			}
		}
        [XmlAttribute]
        public string PressedOverIcon
        {
            get
            {
                return Singleton<ResourceService>.Instance.GetImagePath() + this._pressedOverIcon;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._pressedOverIcon, value, "PressedOverIcon");
            }
        }
        
        // Token: 0x17000026 RID: 38
        // (get) Token: 0x06000062 RID: 98 RVA: 0x000025E8 File Offset: 0x000007E8
        // (set) Token: 0x06000063 RID: 99 RVA: 0x0000260F File Offset: 0x0000080F
        [XmlAttribute]
		public string IconText
		{
			get
			{
				return Singleton<ResourceService>.Instance.GetImagePath() + this._iconText;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<string>(ref this._iconText, value, "IconText");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002628 File Offset: 0x00000828
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002640 File Offset: 0x00000840
		[XmlAttribute]
		public string Background
		{
			get
			{
				return this._background;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<string>(ref this._background, value, "Background");
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002658 File Offset: 0x00000858
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002670 File Offset: 0x00000870
		[XmlAttribute]
		public string CmdName
		{
			get
			{
				return this._cmdName;
			}
			set
			{
				this._cmdName = value;
				base.NotifyPropertyChanged("Command");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002688 File Offset: 0x00000888
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000026A0 File Offset: 0x000008A0
		[XmlAttribute]
		public bool Visible
		{
			get
			{
				return this._visible;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<bool>(ref this._visible, value, "Visible");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000026B8 File Offset: 0x000008B8
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000026D0 File Offset: 0x000008D0
		[XmlAttribute]
		public string GroupName
		{
			get
			{
				return this._groupName;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<string>(ref this._groupName, value, "GroupName");
			}
		}

        [XmlAttribute]
        public string IsConflictFlag
        {
            get
            {
                return this._isConflictFlag;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._isConflictFlag, value, "IsConflictFlag");
            }
        }

        // Token: 0x1700002B RID: 43
        // (get) Token: 0x0600006C RID: 108 RVA: 0x000026E8 File Offset: 0x000008E8
        // (set) Token: 0x0600006D RID: 109 RVA: 0x0000270F File Offset: 0x0000090F
        public object Command
		{
			get
			{
				bool flag = this._cmdName != null;
				if (flag)
				{
				}
				return this._command;
			}
			set
			{
				this._command = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000271C File Offset: 0x0000091C
		public FrameworkElement View
		{
			get
			{
				bool flag = this._view == null;
				if (flag)
				{
					this._view = this.CreatedView();
				}
				bool flag2 = this._view != null && this._view.DataContext == null;
				if (flag2)
				{
					this._view.DataContext = this;
				}
				return this._view;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000277C File Offset: 0x0000097C
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002794 File Offset: 0x00000994
		[XmlAttribute]
		public ViewType ViewType
		{
			get
			{
				return this._viewType;
			}
			set
			{
				this._viewType = value;
				base.SetAndNotifyPropertyChanged<ViewType>(ref this._viewType, value, "ViewType");
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000027B4 File Offset: 0x000009B4
		public FrameworkElement CenterView
		{
			get
			{
				bool flag = this._centerView == null;
				if (flag)
				{
					this._centerView = this.CreatedCenterView();
				}
				bool flag2 = this._centerView != null && this._centerView.DataContext == null;
				if (flag2)
				{
					this._centerView.DataContext = this;
				}
				return this._centerView;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002814 File Offset: 0x00000A14
		public FrameworkElement BottomView
		{
			get
			{
				bool flag = this._bottomView == null;
				if (flag)
				{
					this._bottomView = this.CreatedBottomView();
				}
				bool flag2 = this._bottomView != null && this._bottomView.DataContext == null;
				if (flag2)
				{
					this._bottomView.DataContext = this;
				}
				return this._bottomView;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002874 File Offset: 0x00000A74
		public FrameworkElement RightView
		{
			get
			{
				bool flag = this._rightView == null;
				if (flag)
				{
					this._rightView = this.CreatedRightView();
				}
				bool flag2 = this._rightView != null && this._rightView.DataContext == null;
				if (flag2)
				{
					this._rightView.DataContext = this;
				}
				return this._rightView;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000023AD File Offset: 0x000005AD
		public virtual void Initialize()
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000028D4 File Offset: 0x00000AD4
		public virtual FrameworkElement CreatedView()
		{
			return null;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000028E8 File Offset: 0x00000AE8
		public virtual FrameworkElement CreatedCenterView()
		{
			return null;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000028FC File Offset: 0x00000AFC
		public virtual FrameworkElement CreatedBottomView()
		{
			return null;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002910 File Offset: 0x00000B10
		public virtual FrameworkElement CreatedRightView()
		{
			return null;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000023AD File Offset: 0x000005AD
		public virtual void Reset()
		{
		}
        private string _type;

        private string _menuName;
        // Token: 0x0400001B RID: 27
        private string _icon;

		// Token: 0x0400001C RID: 28
		private string _mouseOverIcon;

        // Token: 0x0400001C RID: 28
        private string _pressedOverIcon;

        // Token: 0x0400001D RID: 29
        private string _iconText;

		// Token: 0x0400001E RID: 30
		private string _background;

		// Token: 0x0400001F RID: 31
		private string _cmdName;

		// Token: 0x04000020 RID: 32
		private bool _visible = true;

		// Token: 0x04000021 RID: 33
		private string _groupName;

		// Token: 0x04000022 RID: 34
		private object _command;

		// Token: 0x04000023 RID: 35
		private FrameworkElement _view;

		// Token: 0x04000024 RID: 36
		private ViewType _viewType = ViewType.Icon;

		// Token: 0x04000025 RID: 37
		private FrameworkElement _centerView;

		// Token: 0x04000026 RID: 38
		private FrameworkElement _bottomView;

		// Token: 0x04000027 RID: 39
		private FrameworkElement _rightView;

        private string _isConflictFlag;
    }
}
