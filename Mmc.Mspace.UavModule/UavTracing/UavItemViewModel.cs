using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.UavModule.UavTracing
{
    public class UavItemViewModel : BindableBase
    {
        private UavTracingModel tracingModel;
        //  private UavVideoViewModel videoViewModel;
        private VideoControllerVModel _videoControllerVM;
        private UavHeatMapViewModel heatMapViewModel;
        private UavMountControlViewModel mountControlViewModel;
        private MGasViewModel gasViewModel;
        private ObservableCollection<VideoScreenVModel> _videoScreenModels;
        private VideoScreenVModel _selectedScreenModel;

        private bool isExpanded;

        //private bool isFollow = false;

        private bool _isMultiScreen;//是否是多屏模式
        [XmlIgnore]
        public bool IsMultiScreen
        {
            get { return this._isMultiScreen; }
            set { base.SetAndNotifyPropertyChanged<bool>(ref this._isMultiScreen, value, "IsMultiScreen"); }
        }


        private bool _isComboxEnabled;//当该项屏幕工作时，禁用下拉框不让选择
        [XmlIgnore]
        public bool IsComboxEnabled
        {
            get { return this._isComboxEnabled; }
            set { base.SetAndNotifyPropertyChanged<bool>(ref this._isComboxEnabled, value, "IsComboxEnabled"); }
        }

        [XmlIgnore]
        public VideoScreenVModel SelectedScreenModel
        {
            get { return this._selectedScreenModel; }
            set
            {
                base.SetAndNotifyPropertyChanged<VideoScreenVModel>(ref this._selectedScreenModel, value, "SelectedScreenModel");
                this.VideoControllerVM.ScreenVModel = value;
                _videoControllerVM.IsChecked = value.IsUsed;
            }
        }



        [XmlIgnore]
        public ObservableCollection<VideoScreenVModel> VideoScreenModels
        {
            get { return this._videoScreenModels; }
            set { base.SetAndNotifyPropertyChanged<ObservableCollection<VideoScreenVModel>>(ref this._videoScreenModels, value, "VedioScreenModels"); }
        }

        public UavItemViewModel()
        {
            _videoControllerVM = new VideoControllerVModel();

            //_videoScreenModels = new ObservableCollection<VideoScreenVModel>{
            //    new VideoScreenVModel { ScreenIndex = "A1", UavId =  "-1", IsUsed=false },
            //    new VideoScreenVModel { ScreenIndex = "A2", UavId =  "-1", IsUsed=false },
            //    new VideoScreenVModel { ScreenIndex = "A3", UavId =  "-1", IsUsed=false },
            //    new VideoScreenVModel { ScreenIndex = "B1", UavId =  "-1", IsUsed=false },
            //    new VideoScreenVModel { ScreenIndex = "B2", UavId =  "-1", IsUsed=false },
            //    new VideoScreenVModel { ScreenIndex = "B3", UavId =  "-1", IsUsed=false },
            //    new VideoScreenVModel { ScreenIndex = "C1", UavId =  "-1", IsUsed=false },
            //    new VideoScreenVModel { ScreenIndex = "C2", UavId =  "-1", IsUsed=false },
            //    new VideoScreenVModel { ScreenIndex = "C3", UavId =  "-1", IsUsed=false },
            //};
            //_videoScreenModels.ForEach(p => { p.ScreenItem = string.Format("{0}-空闲", p.ScreenIndex); });
        }

        [XmlIgnore]
        public VideoControllerVModel VideoControllerVM
        {
            get { return this._videoControllerVM; }
            set { base.SetAndNotifyPropertyChanged<VideoControllerVModel>(ref this._videoControllerVM, value, "VideoControllerVM"); }
        }

        [XmlIgnore]
        public UavHeatMapViewModel HeatMapViewModel
        {
            get { return this.heatMapViewModel; }
            set { base.SetAndNotifyPropertyChanged<UavHeatMapViewModel>(ref this.heatMapViewModel, value, "HeatMapViewModel"); }
        }

        [XmlIgnore]
        public UavTracingModel UavTracingModel
        {
            get { return this.tracingModel; }
            set { base.SetAndNotifyPropertyChanged<UavTracingModel>(ref this.tracingModel, value, "UavTracingModel"); }
        }

        [XmlIgnore]
        public UavMountControlViewModel MountControlViewModel
        {
            get { return this.mountControlViewModel; }
            set { base.SetAndNotifyPropertyChanged<UavMountControlViewModel>(ref this.mountControlViewModel, value, "MountControlViewModel"); }
        }

        [XmlIgnore]
        public MGasViewModel GasViewModel
        {
            get { return this.gasViewModel; }

            set { base.SetAndNotifyPropertyChanged<MGasViewModel>(ref this.gasViewModel, value, "GasViewModel"); }
        }

        //origin
        public string ground_serial { get; set; }

        public string device_name { get; set; }

        public string device_hard_id { get; set; }

        public string mount_type { get; set; }

        public string ground_state { get; set; }

        public int goods_id { get; set; }

        //view
        public string CameraState { get; set; }

        public string MountInfo { get; set; }

        public string UavTitle { get; set; }

        public string GroundState { get; set; }

        [XmlIgnore]
        public ICommand FlowUavCmd { get; set; }

        [XmlIgnore]
        public ICommand LocateCmd { get; set; }

        [XmlIgnore]
        public ICommand ShowVideoCmd { get; set; }

        [XmlIgnore]
        public bool IsExpanded
        {
            get { return this.isExpanded; }
            set { base.SetAndNotifyPropertyChanged<bool>(ref this.isExpanded, value, "IsExpanded"); }
        }

        public string uav_type { get; set; }

        public void RestoreEnv()
        {
            tracingModel?.OnUnchecked();
            MountControlViewModel?.OnUnchecked();
            GasViewModel?.OnUnchecked();
            VideoControllerVM?.OnUnchecked();
        }

        public void SetVideoScreenModels(ObservableCollection<VideoScreenVModel> videoScreenVModels)
        {
            this._videoScreenModels = videoScreenVModels;
        }

    }

    public class VideoScreenVModel : BindableBase
    {
        public string Title { get; set; }
        public string UavId { get; set; }
        public string UavName { get; set; }

        public bool IsUsed { get; set; }

        public string ScreenIndex { get; set; }

        public override string ToString()
        {
            //return base.ToString();
            return IsUsed ? string.Format("{0}-工作中", ScreenIndex) : string.Format("{0}-空闲", ScreenIndex);
        }

        private string _screenItem;
        [XmlIgnore]
        public string ScreenItem
        {
            get { return this._screenItem; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._screenItem, value, "ScreenItem"); }
        }

    }
}
