using Gvitech.CityMaker.RenderControl;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace Mmc.Mspace.Common.Models
{
    // Token: 0x02000009 RID: 9
    public class LayerItemModel : BindableBase
    {
        // Token: 0x0600004A RID: 74 RVA: 0x000023C5 File Offset: 0x000005C5
        public LayerItemModel()
        {
            this.Children = new ObservableCollection<LayerItemModel>();
        }

        // Token: 0x1700001C RID: 28
        // (get) Token: 0x0600004B RID: 75 RVA: 0x000023DC File Offset: 0x000005DC
        // (set) Token: 0x0600004C RID: 76 RVA: 0x000023F4 File Offset: 0x000005F4
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._name, value, "Name");
            }
        }

        // Token: 0x1700001D RID: 29
        // (get) Token: 0x0600004D RID: 77 RVA: 0x0000240C File Offset: 0x0000060C
        // (set) Token: 0x0600004E RID: 78 RVA: 0x00002424 File Offset: 0x00000624
        public string Group
        {
            get
            {
                return this._group;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._group, value, "Group");
            }
        }

        // Token: 0x1700001E RID: 30
        // (get) Token: 0x0600004F RID: 79 RVA: 0x0000243C File Offset: 0x0000063C
        // (set) Token: 0x06000050 RID: 80 RVA: 0x00002454 File Offset: 0x00000654
        public bool IsChecked
        {
            get
            {
                return this._isChecked;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<bool>(ref this._isChecked, value, "IsChecked");
            }
        }

        // Token: 0x1700001F RID: 31
        // (get) Token: 0x06000051 RID: 81 RVA: 0x0000246C File Offset: 0x0000066C
        // (set) Token: 0x06000052 RID: 82 RVA: 0x00002484 File Offset: 0x00000684
        public bool IsVisible
        {
            get
            {
                return this._isVisible;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<bool>(ref this._isVisible, value, "IsVisible");
                Application.Current.Dispatcher.Invoke(new Action(this.OnVisibleChanged), DispatcherPriority.Normal);
            }
        }

        // Token: 0x17000020 RID: 32
        // (get) Token: 0x06000053 RID: 83 RVA: 0x000024B9 File Offset: 0x000006B9
        // (set) Token: 0x06000054 RID: 84 RVA: 0x000024C1 File Offset: 0x000006C1
        public ObservableCollection<LayerItemModel> Children { get; set; }

        // Token: 0x17000021 RID: 33
        // (get) Token: 0x06000055 RID: 85 RVA: 0x000024CA File Offset: 0x000006CA
        // (set) Token: 0x06000056 RID: 86 RVA: 0x000024D2 File Offset: 0x000006D2
        public object Parameters { get; set; }

        // Token: 0x06000057 RID: 87 RVA: 0x000024DC File Offset: 0x000006DC
        public virtual void OnVisibleChanged()
        {
            IShowLayer showLayer = this.Parameters as IShowLayer;
            bool flag = showLayer != null;
            if (flag)
            {
                showLayer.SetVisibleMask(this.IsVisible, gviViewportMask.gviViewAllNormalView);
            }
        }

        // Token: 0x04000013 RID: 19
        private string _group;

        // Token: 0x04000014 RID: 20
        private bool _isChecked;

        // Token: 0x04000015 RID: 21
        private bool _isVisible;

        // Token: 0x04000016 RID: 22
        private string _name;
    }
}