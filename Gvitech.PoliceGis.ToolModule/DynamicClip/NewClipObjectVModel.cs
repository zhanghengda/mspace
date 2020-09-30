using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mmc.Mspace.ToolModule.DynamicClip
{
    class NewClipObjectVModel : BaseViewModel
    {
        public Action<string> GeoSectionName;
        private NewClipObjectView newClipObjectView;
        public DynamicClipVModel ClipVModel = null;

        private string _clipName;
        public string ClipName
        {
            get { return _clipName; }
            set
            {
                _clipName = value; OnPropertyChanged("ClipName");
            }
        }

        public ICommand NewClipDataCmd { get; set; }

        public ICommand CloseClipViewCmd { get; set; }

        public ICommand DisposeCmd2 { get; set; }
        public NewClipObjectVModel(gviClipMode gviClipMode, ObservableCollection<ClipData> ClipDataColletion)
        {
            newClipObjectView = new NewClipObjectView();
            newClipObjectView.DataContext = this;
            if(gviClipMode== gviClipMode.gviClipBox)
            {
                ClipName = "裁剪" + ClipIndexCount.GviClipBoxIndex++;
            }
            else if (gviClipMode == gviClipMode.gviClipCustomePlane)
            {
                ClipName = "裁剪" + ClipIndexCount.GviClipCustomePlaneIndex++;
            }

             this.CloseClipViewCmd = new RelayCommand(() =>
            {
                CloseView();
            });
            this.NewClipDataCmd = new RelayCommand(() =>
            {
                if (ClipDataColletion != null)
                {

                    foreach (ClipData clipData in ClipDataColletion)
                    {
                        if (Convert.ToString(clipData.Name) == ClipName)
                        {

                            Messages.ShowMessage("该名称已存在！");
                            return;
                        }
                    }
                    SaveName();
                }
                if (ClipDataColletion == null)
                {
                    SaveName();
                }


            });
            this.DisposeCmd2 = new RelayCommand(() =>
            {
                CloseView();
            });

        }

    public void SaveName()
        {

            if(string.IsNullOrEmpty(ClipName))
            {
                Messages.ShowMessage("请填写剖面名称");
                return;
            }
            GeoSectionName(ClipName);

            CloseView();
        }

        public void ShowView()
        {
            if(newClipObjectView ==null)
                newClipObjectView = new NewClipObjectView();



            newClipObjectView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            newClipObjectView.Show();
        }


        public void CloseView()
        {
            if (newClipObjectView == null)
                newClipObjectView = new NewClipObjectView();

            newClipObjectView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            newClipObjectView.Close();
        }

        //public override void Initialize()
        //{
        //    base.Initialize();
        //    base.ViewType = (ViewType)1;
        //    this.PolygonClipCmd = new RelayCommand(() => {

        //        ConfirmOrNot = "0";
        //        //Messenger.Messengers.Notify("close");
        //    });
        //    this.NewClipDataCmd = new RelayCommand(() => {
        //        ConfirmOrNot = "1";
        //        //Messenger.Messengers.Notify("close");
        //    });
        //}

        //public NewClipObjectVModel(int clipMode)
        //{

        //}
        //public void GetClipVModel(DynamicClipVModel f)
        //{
        //    ClipVModel = f;
        //}
        //public override void OnChecked()
        //{

        //    base.OnChecked();
        //    if (NewClipObjectView != null)
        //    {
        //        //NewClipObjectView.ClipType.Items.Clear();
        //        //NewClipObjectView.ClipType.Items.Add("体裁剪");
        //        //NewClipObjectView.ClipType.Items.Add("面裁剪");
        //        // dynamicClip.Renametextbox.Text = "裁剪面1";
        //    }
        //    //ShowNewClipView();

        //}
        //public override void OnUnchecked()
        //{
        //    //NewClipObjectView.ClipType.Items.Clear();

        //    /* GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
        //     GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;// gviSelectFeatureLayer;
        //     GviMap.MapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
        //   */
        //    base.OnUnchecked();
        //    NewClipObjectView = (NewClipObjectView)base.View;
        //    NewClipObjectView.Hide();

        //}
        //public override void Reset()
        //{
        //    base.Reset();
        //    base.IsChecked = false;
        //}
        //public void ShowNewClipView()
        //{
        //    //NewClipObjectView = (NewClipObjectView)base.View;

        //    //NewClipObjectView.DataContext = this;

        //    NewClipObjectView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
        //    NewClipObjectView.Show();
        //}
        //private string _selectType;
        //public string SelectType
        //{
        //    get { return _selectType; }
        //    set
        //    {
        //        base.SetAndNotifyPropertyChanged<string>(ref this._selectType, value, "SelectType");
        //        if (SelectType == "体裁剪")
        //        {
        //            GviMap.MapControl.InteractMode = gviInteractMode.gviInteractClipPlane;
        //            GviMap.MapControl.ClipMode = gviClipMode.gviClipBox;
        //        }
        //        if (SelectType == "面裁剪")
        //        {
        //            GviMap.AxMapControl.InteractMode = gviInteractMode.gviInteractClipPlane;
        //            GviMap.AxMapControl.ClipMode = gviClipMode.gviClipCustomePlane;
        //        }
        //    }

        //}

        //private string _clipName;
        //public string ClipName
        //{
        //    get { return _clipName; }
        //    set
        //    {
        //        base.SetAndNotifyPropertyChanged<string>(ref this._clipName, value, "ClipName");
        //    }
        //}

        //private string _confirmOrNot;
        //public string ConfirmOrNot
        //{
        //    get { return _confirmOrNot; }
        //    set
        //    {
        //        base.SetAndNotifyPropertyChanged<string>(ref this._confirmOrNot, value, "ConfirmOrNot");
        //    }
        //}
    }
}
