using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Xml.Serialization;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ResourceServices;
using Mmc.Mspace.Main.Commands;
using Mmc.Windows.Design;
using Mmc.Wpf.Mvvm;


namespace Mmc.Mspace.Main.Models
{
    // Token: 0x02000005 RID: 5
    public class ShellModel : BindableBase
    {
        // Token: 0x17000006 RID: 6
        // (get) Token: 0x0600000E RID: 14 RVA: 0x00002158 File Offset: 0x00000358RowCount
        // (set) Token: 0x0600000F RID: 15 RVA: 0x00002170 File Offset: 0x00000370
        public ObservableCollection<ToolItemModel> BarMenuItems
        {
            get
            {
                return this.barMenuItems;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<ToolItemModel>>(ref this.barMenuItems, value, "BarMenuItems");
            }
        }

        // Token: 0x17000007 RID: 7
        // (get) Token: 0x06000010 RID: 16 RVA: 0x00002188 File Offset: 0x00000388
        // (set) Token: 0x06000011 RID: 17 RVA: 0x000021A0 File Offset: 0x000003A0
        public ObservableCollection<ToolItemModel> ToolMenuItems
        {
            get
            {
                return this.toolMenuItems;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<ToolItemModel>>(ref this.toolMenuItems, value, "ToolMenuItems");
            }
        }


        public ObservableCollection<ToolItemModel> BottomToolMenuItems
        {
            get
            {
                return this.bottomToolMenuItems;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<ToolItemModel>>(ref this.bottomToolMenuItems, value, "BottomToolMenuItems");
            }
        }

        public ObservableCollection<ToolItemModel> RightToolMenuItems
        {
            get
            {
                return this.rightToolMenuItems;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<ToolItemModel>>(ref this.rightToolMenuItems, value, "RightToolMenuItems");
            }
        }

        // Token: 0x17000009 RID: 9
        // (get) Token: 0x06000014 RID: 20 RVA: 0x000021E8 File Offset: 0x000003E8
        // (set) Token: 0x06000015 RID: 21 RVA: 0x00002200 File Offset: 0x00000400
        public ToolItemModel ScaleViewModel
        {
            get
            {
                return this.scaleViewModel;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<ToolItemModel>(ref this.scaleViewModel, value, "ScaleViewModel");
            }
        }

        // Token: 0x1700000A RID: 10
        // (get) Token: 0x06000016 RID: 22 RVA: 0x00002218 File Offset: 0x00000418
        public ICommand FullScreenCmd
        {
            get
            {
                return new Commands.FullScreenCmd();
            }
        }

        // Token: 0x1700000B RID: 11
        // (get) Token: 0x06000017 RID: 23 RVA: 0x00002230 File Offset: 0x00000430
        public ICommand MinimizedCmd
        {
            get
            {
                return new AppMinimizedCmd();
            }
        }

        // Token: 0x1700000C RID: 12
        // (get) Token: 0x06000018 RID: 24 RVA: 0x00002248 File Offset: 0x00000448
        public ICommand CloseCmd
        {
            get
            {
                return new AppClosedCmd();
            }
        }

        // Token: 0x1700000D RID: 13
        // (get) Token: 0x06000019 RID: 25 RVA: 0x00002260 File Offset: 0x00000460
        // (set) Token: 0x0600001A RID: 26 RVA: 0x00002278 File Offset: 0x00000478
        [XmlAttribute]
        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<int>(ref this.rowCount, value, "RowCount");
            }
        }

        // Token: 0x1700000E RID: 14
        // (get) Token: 0x0600001B RID: 27 RVA: 0x00002290 File Offset: 0x00000490
        // (set) Token: 0x0600001C RID: 28 RVA: 0x000022A8 File Offset: 0x000004A8
        [XmlAttribute]
        public int ColumnCount
        {
            get
            {
                return this.columnCount;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<int>(ref this.columnCount, value, "ColumnCount");
            }
        }

        // Token: 0x1700000F RID: 15
        // (get) Token: 0x0600001D RID: 29 RVA: 0x000022C0 File Offset: 0x000004C0
        // (set) Token: 0x0600001E RID: 30 RVA: 0x000022E7 File Offset: 0x000004E7
        [XmlAttribute]
        public string BackImage
        {
            get
            {
                return Singleton<ResourceService>.Instance.GetImagePath() + this.backImage;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this.backImage, value, "BackImage");
            }
        }

        // Token: 0x04000006 RID: 6
        private ObservableCollection<ToolItemModel> barMenuItems = new ObservableCollection<ToolItemModel>();

        // Token: 0x04000007 RID: 7
        private ObservableCollection<ToolItemModel> toolMenuItems = new ObservableCollection<ToolItemModel>();

        // Token: 0x04000008 RID: 8
        private ObservableCollection<ToolItemModel> bottomToolMenuItems = new ObservableCollection<ToolItemModel>();
        private ObservableCollection<ToolItemModel> rightToolMenuItems = new ObservableCollection<ToolItemModel>();

        // Token: 0x04000009 RID: 9
        private ToolItemModel scaleViewModel;

        // Token: 0x0400000A RID: 10
        private int rowCount;

        // Token: 0x0400000B RID: 11
        private int columnCount;

        // Token: 0x0400000C RID: 12
        public string backImage;

    }
}
