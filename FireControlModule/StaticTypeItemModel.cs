using Mmc.Wpf.Mvvm;
using System;
using System.Windows;
using System.Windows.Threading;

namespace FireControlModule
{
    public class StaticTypeItemModel : BindableBase
    {
        private HiddenDangerStatics statics;

        public StaticTypeItemModel()
        {
            //this.Children = new ObservableCollection<LayerItemModel>();
            statics = new HiddenDangerStatics();
        }

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

        public object Parameters { get; set; }

        public virtual void OnVisibleChanged()
        {
            if (this.IsVisible)
            {
                statics.ShowSupervisoryReviewStatics();
            }
            else
            {
                statics.CloseStatics();
            }
            //IShowLayer showLayer = this.Parameters as IShowLayer;
            //bool flag = showLayer != null;
            //if (flag)
            //{
            //    showLayer.SetVisibleMask(this.IsVisible, gviViewportMask.gviViewAllNormalView);
            //}
        }

        private string _group;

        private bool _isChecked;

        private bool _isVisible;

        private string _name;
    }
}