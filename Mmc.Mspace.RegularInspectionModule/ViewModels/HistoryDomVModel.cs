using Gvitech.CityMaker.RenderControl;
using Helpers;
using LiteDB;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Models.Inspection;
using Mmc.Mspace.RegularInspectionModule.Dto;
using Mmc.Mspace.RegularInspectionModule.Views;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Mspace.Theme.Controls;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Mmc.Mspace.RegularInspectionModule.ViewModels
{
    public class HistoryDomVModel : CheckedToolItemModel
    {
        private List<InspectModel> domList;
        private int maxDisplayNum;
        private int stepNum;

        private ObservableCollection<InspectModel> _displayDomList;
        public ObservableCollection<InspectModel> DisplayDomList
        {
            get { return _displayDomList ?? (_displayDomList = new ObservableCollection<InspectModel>()); }
            set { _displayDomList = value; NotifyPropertyChanged("DisplayDomList"); }
        }

        private ObservableCollection<TextItem> _inspectRegions;

        public ObservableCollection<TextItem> InspectRegions
        {
            get { return _inspectRegions ?? (_inspectRegions = new ObservableCollection<TextItem>()); }
            set { _inspectRegions = value; NotifyPropertyChanged("InspectRegions"); }
        }

        private TextItem _selectedRegion;
        public TextItem SelectedRegion
        {
            get { return _selectedRegion; }
            set { _selectedRegion = value; NotifyPropertyChanged("SelectedRegion"); }
        }

        private ObservableCollection<TextItem> _inspectTimeRange;
        public ObservableCollection<TextItem> InspectTimeRange
        {
            get
            {
                return _inspectTimeRange ?? (_inspectTimeRange = new ObservableCollection<TextItem>(CommonContract.GetInspectDomTime()));
            }
            set { _inspectTimeRange = value; NotifyPropertyChanged("InspectTimeRange"); }
        }

        private TextItem _selectedTimeRange;

        public TextItem SelectedTimeRange
        {
            get { return _selectedTimeRange; }
            set { _selectedTimeRange = value; NotifyPropertyChanged("SelectedTimeRange"); }
        }

        private InspectModel _selectedDom;
        public InspectModel SelectedDom
        {
            get { return _selectedDom ?? (_selectedDom = new InspectModel()); }
            set { _selectedDom = value; NotifyPropertyChanged("SelectedDom"); }
        }

        private bool _leftBtnIsEnable;
        public bool LeftBtnIsEnable
        {
            get { return _leftBtnIsEnable; }
            set { _leftBtnIsEnable = value; NotifyPropertyChanged("LeftBtnIsEnable"); }
        }
        private bool _rightBtnIsEnable;
        public bool RightBtnIsEnable
        {
            get { return _rightBtnIsEnable; }
            set { _rightBtnIsEnable = value; NotifyPropertyChanged("RightBtnIsEnable"); }
        }

        private bool _isUnitCbxEnable = true;
        public bool IsUnitCbxEnable
        {
            get { return _isUnitCbxEnable; }
            set { _isUnitCbxEnable = value; NotifyPropertyChanged("IsUnitCbxEnable"); }
        }
        [XmlIgnore]
        public ICommand DragDomToOpenCmd { get; set; }

        [XmlIgnore]
        public ICommand ChoosedDomCmd { get; set; }
        [XmlIgnore]
        public ICommand LeftMoveCmd { get; set; }
        [XmlIgnore]
        public ICommand RightMoveCmd { get; set; }
        [XmlIgnore]
        public ICommand SelectRegionCommand { get; set; }

        [XmlIgnore]
        public ICommand SelectTimeRangeCommand { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;

            this.ChoosedDomCmd = new RelayCommand<InspectModel>(OpenDomData);
            this.DragDomToOpenCmd = new RelayCommand<InspectModel>(OpenDomToTarget);
            this.LeftMoveCmd = new RelayCommand(() =>
            {
                LeftMove();
            });
            this.RightMoveCmd = new RelayCommand(() =>
            {
                RightMove();
            });

            SelectRegionCommand = new RelayCommand<TextItem>(OnSelectInspectCommand);
            SelectTimeRangeCommand = new RelayCommand<TextItem>(OnSelectTimeRangeCommand);

            Messenger.Messengers.Register("HistoryDomRefresh", () =>
            {
                LoadRegionsData();
            });
        }
        private FrameworkElement _dragScope;
        private DragAdorner _adorner;
        private AdornerLayer _layer;
        private void OpenDomToTarget(InspectModel obj)
        {
            try
            {
                ListBoxItem item = new ListBoxItem();
                item.DataContext = obj;
                if (item != null)
                {
                    this._dragScope = Application.Current.MainWindow.Content as FrameworkElement;
                    this._dragScope.AllowDrop = true;
                    DragEventHandler draghandler = new DragEventHandler(DragScope_PreviewDragOver);
                    this._dragScope.PreviewDragOver += draghandler;
                    this._adorner = new DragAdorner(this._dragScope, (UIElement)item, 0.5);
                    this._layer = AdornerLayer.GetAdornerLayer(this._dragScope as Visual);
                    this._layer.Add(this._adorner);
                    DataObject data = new DataObject(typeof(InspectModel), obj);
                    DragDrop.DoDragDrop(item, data, DragDropEffects.Move);
                    AdornerLayer.GetAdornerLayer(this._dragScope).Remove(this._adorner);
                    this._adorner = null;
                    this._dragScope.PreviewDragOver -= draghandler;

                    gviViewportMask viewMask=gviViewportMask.gviViewAllNormalView;

                    var viewNum = GviMap.MapControl.Viewport.ActiveView;
                    if (GviMap.Viewport.ViewportMode == gviViewportMode.gviViewportL1R1)
                    {
                        if (viewNum == 0)
                            viewMask = gviViewportMask.gviView0;
                        else if (viewNum == 1)
                            viewMask = gviViewportMask.gviView1;
                    }

                    RegInsDataRenderManager.Instance.OpenDomData(obj, viewportMask: viewMask);
                }
            }
            catch (Exception d)
            {
                
            }
            void DragScope_PreviewDragOver(object sender, DragEventArgs args)
            {

                if (this._adorner != null)
                {
                    this._adorner.LeftOffset = args.GetPosition(this._dragScope).X;
                    this._adorner.TopOffset = args.GetPosition(this._dragScope).Y;
                }
            }
        }

        private void OnSelectInspectCommand(TextItem parameter)
        {
            if (parameter == null) return;
            DisplayDomList = null;
            SelectedTimeRange = null;

            //if (parameter.Key == "0")
            //{
            //    IsUnitCbxEnable = false;
            //}
            //else
            //{
            //    IsUnitCbxEnable = true;
            //}
        }

        public void LoadRegionsData()
        {
            domList = new List<InspectModel>();
            var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;

            double remainWidth = mapView.Width - 208 - 32 * 2; // 主屏幕宽度 去掉 左侧选择栏宽度 、两侧按钮宽度

            maxDisplayNum = (int)remainWidth / (144 + 3 * 2); // 最大个数 等于 剩余宽度 除以 单个宽度 ；取整
            stepNum = maxDisplayNum / 2;

            //IsUnitCbxEnable = false;
            var regions = InspectionService.Instance.GetAllRegion().Select(t => RegInsModelConvert.InspectRegionConvert(t)).ToList();
            List<TextItem> regionList = new List<TextItem>();
            regionList.AddRange(regions.Select(t => new TextItem() { Key = t.Id.ToString(), Value = t.Name }).ToList());
            InspectRegions = new ObservableCollection<TextItem>(regionList);


            if (InspectRegions.Count > 0)
                SelectedRegion = InspectRegions[0];

            if (InspectTimeRange.Count > 0)
            {
                SelectedTimeRange = InspectTimeRange[0];
                OnSelectTimeRangeCommand(SelectedTimeRange);
            }
        }

        private void OnSelectTimeRangeCommand(TextItem parameter)
        {
            if (parameter == null) return;
            if (SelectedRegion == null || string.IsNullOrEmpty(SelectedRegion.Key)) return;
            var tiemPoint = DateTime.Now;

            string name = string.Empty;
            List<InspectUnit> unitList = InspectionService.Instance.FindUnits(p => p.InspectRegionId.ToString() == SelectedRegion.Key.ToString());
            if (unitList == null) return;

            List<int> unitIDList = new List<int>();

            if (parameter.Key == CommonContract.InspectDomTime.All.ToString())
            {
                tiemPoint = DateTime.Now.AddYears(-20);
            }
            else if (parameter.Key == CommonContract.InspectDomTime.LastMonth.ToString())
            {
                tiemPoint = DateTime.Now.AddMonths(-1);
            }
            else if (parameter.Key == CommonContract.InspectDomTime.LastThreeMonth.ToString())
            {
                tiemPoint = DateTime.Now.AddMonths(-3);
            }
            else if (parameter.Key == CommonContract.InspectDomTime.LastHalfYear.ToString())
            {
                tiemPoint = DateTime.Now.AddMonths(-6);
            }
            else if (parameter.Key == CommonContract.InspectDomTime.LastYear.ToString())
            {
                tiemPoint = DateTime.Now.AddYears(-1);
            }

            unitIDList = unitList.FindAll(p => p.Time > tiemPoint).ConvertAll<int>(p => p.Id);

            BsonValue[] bsonArr = new BsonValue[unitIDList.Count] ;
            for(var i =0;i< unitIDList.Count;i++)
            {
                bsonArr[i] = unitIDList[i];
            }

            Query query = Query.In("InspectUnitId", bsonArr);

            DisplayDomList.Clear();
            domList.Clear();
            var doms =  InspectionService.Instance.FindDoms(query);
            foreach(var item in doms)
            {
                item.Time = unitList.Find(p => p.Id == item.InspectUnitId).Time;
                domList.Add(RegInsModelConvert.DomConvert(item));
            }

            if (domList.Count > maxDisplayNum)
            {
                LeftBtnIsEnable = false;
                RightBtnIsEnable = true;
                DisplayDomList = new ObservableCollection<InspectModel>(domList.GetRange(0, maxDisplayNum));
            }
            else
            {
                LeftBtnIsEnable = false;
                RightBtnIsEnable = false;
                DisplayDomList = new ObservableCollection<InspectModel>(domList.GetRange(0, domList.Count));
            }
            //DisplayDomList = new ObservableCollection<InspectModel>(domList.GetRange(0, domList.Count));
        }
        public void LeftMove()
        {
            int currentIndex = domList.FindIndex(p => p == DisplayDomList[0]);
            int nextIndex = currentIndex - stepNum;
            if (nextIndex < 0)
            {
                nextIndex = 0;
                LeftBtnIsEnable = false;

            }
            RightBtnIsEnable = true;
            DisplayDomList = new ObservableCollection<InspectModel>(domList.GetRange(nextIndex, maxDisplayNum));
        }
        public void RightMove()
        {
            int currentIndex = domList.FindIndex(p => p == DisplayDomList[0]);
            int nextIndex = currentIndex + stepNum;
            if (nextIndex + maxDisplayNum >= domList.Count)
            {
                nextIndex = domList.Count - maxDisplayNum;
                RightBtnIsEnable = false;
            }
            LeftBtnIsEnable = true;
            DisplayDomList = new ObservableCollection<InspectModel>(domList.GetRange(nextIndex, maxDisplayNum));
        }


        private void OpenDomData(InspectModel inspectModel)
        {
            if (inspectModel == null) return;
            string filePath = inspectModel.Path;

            RegInsDataRenderManager.Instance.OpenDomData(inspectModel);
        }

        /// <summary>
        /// test data 
        /// </summary>
        /// <returns></returns>
        private List<InspectModel> test()
        {
            return new List<InspectModel>()
            {
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320111.JPG",Name="S1320111",Time = Convert.ToDateTime(@"2018-11-01 00:00:00") },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320112.JPG",Name="S1320112",Time = Convert.ToDateTime(@"2018-11-02 00:00:00")  },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320113.JPG",Name="S1320113",Time = Convert.ToDateTime(@"2018-11-03 00:00:00") },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320114.JPG",Name="S1320114",Time = Convert.ToDateTime(@"2018-11-04 00:00:00")  },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320115.JPG",Name="S1320115",Time = Convert.ToDateTime(@"2018-11-05 00:00:00") },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320116.JPG",Name="S1320116",Time = Convert.ToDateTime(@"2018-11-06 00:00:00") },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320117.JPG",Name="S1320117",Time = Convert.ToDateTime(@"2018-11-07 00:00:00")  },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320118.JPG",Name="S1320118",Time = Convert.ToDateTime(@"2018-11-08 00:00:00") },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320119.JPG",Name="S1320119",Time = Convert.ToDateTime(@"2018-11-09 00:00:00")  },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320120.JPG",Name="S1320120",Time = Convert.ToDateTime(@"2018-11-10 00:00:00") },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320121.JPG",Name="S1320121",Time = Convert.ToDateTime(@"2018-11-11 00:00:00") },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320122.JPG",Name="S1320122",Time = Convert.ToDateTime(@"2018-11-12 00:00:00")  },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320123.JPG",Name="S1320123",Time = Convert.ToDateTime(@"2018-11-13 00:00:00") },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320124.JPG",Name="S1320124",Time = Convert.ToDateTime(@"2018-11-14 00:00:00")  },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320125.JPG",Name="S1320125",Time = Convert.ToDateTime(@"2018-11-15 00:00:00") },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320126.JPG",Name="S1320126",Time = Convert.ToDateTime(@"2018-11-16 00:00:00") },
                new InspectModel(){ DataType=Common.CommonContract.InspectDataType.Dom,InspectUnitId=1,Thumbnail=@"F:\TestData\航线轨迹的航片\S1320127.JPG",Name="S1320127",Time = Convert.ToDateTime(@"2018-11-17 00:00:00")  }
            };

        }
        
        public override void OnChecked()
        {
            base.OnChecked();
            Messenger.Messengers.Notify("BottomMenuEnum", IsChecked);
        }
        
        public override void OnUnchecked()
        {
            base.OnUnchecked();
            Messenger.Messengers.Notify("BottomMenuEnum", IsChecked);
        }

        public void ClearData()
        {
            this.DisplayDomList = null;
            this.domList = null;
            this.InspectRegions = null;
            //this.InspectTimeRange = null;
            this.SelectedDom = null;
            this.LeftBtnIsEnable = false;
            this.RightBtnIsEnable = false;
            this.IsUnitCbxEnable = false;
            this.SelectedRegion = null;
            this.SelectedTimeRange = null;
        }

        public override void Reset()
        {
            base.Reset();
        }

    }
}
