using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.WireTowerModule.DTO;
using Mmc.Mspace.WireTowerModule.Models;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.WireTowerModule.ViewModels
{
    public class TowerManageViewModel : BaseViewModel
    {
        #region varies
        public Action<bool> OnManagerViewShow;
        private readonly WireTowerConverter _wireTowerConverter;
        //private readonly string _towerFilePath;
        private List<TowerForClient> _curTowerList;
        private GenerateRouteViewModel _generateRouteViewModel;
        private TowerViewModel _towerViewModel;
        #endregion

        #region binging varies and command
        private LineForClient _selectedLine;
        public LineForClient SelectedLine
        {
            get => _selectedLine ?? new LineForClient();
            set { _selectedLine = value; OnPropertyChanged("SelectedLine"); }
        }
        private ObservableCollection<LineForClient> _lineSet;
        public ObservableCollection<LineForClient> LineSet
        {
            get => _lineSet ?? new ObservableCollection<LineForClient>();
            set { _lineSet = value; OnPropertyChanged("LineSet"); }
        }
        private ObservableCollection<TowerForClient> _towerSet;
        public ObservableCollection<TowerForClient> TowerSet
        {
            get => _towerSet ?? new ObservableCollection<TowerForClient>();
            set { _towerSet = value; OnPropertyChanged("TowerSet"); }
        }

        private TowerForClient _selectedDataRow;

        public TowerForClient SelectedDataRow
        {
            get => _selectedDataRow;
            set
            {
                _selectedDataRow = value;
                OnPropertyChanged("SelectedDataRow");
            }
        }
        [XmlIgnore]
        public ICommand SaveCommand { get; set; }
        [XmlIgnore]
        public ICommand GenerateRouteCommand { get; set; }
        [XmlIgnore]
        public ICommand AddTowerCommand { get; set; }
        //[XmlIgnore]
        //public ICommand DetailCommand { get; set; }
        [XmlIgnore]
        public ICommand EditCommand { get; set; }
        [XmlIgnore]
        public ICommand DeleteCommand { get; set; }
        [XmlIgnore]
        public ICommand SelectLineCommand { get; set; }
        [XmlIgnore] public ICommand MoveUpCommand { get; set; }
        [XmlIgnore] public ICommand MoveDownCommand { get; set; }
        #endregion

        public TowerManageViewModel()
        {
            this.SaveCommand = new Wpf.Commands.RelayCommand(SaveTowerSet);
            this.GenerateRouteCommand = new Wpf.Commands.RelayCommand(GenerateRoute);
            this.AddTowerCommand = new Wpf.Commands.RelayCommand(AddTower);
            //this.DetailCommand = new Mmc.Wpf.Commands.RelayCommand<object>(OnShowDetailTower);
            this.EditCommand = new Wpf.Commands.RelayCommand<object>(OnEditTower);
            this.DeleteCommand = new Wpf.Commands.RelayCommand<object>(OnDeleteTower);
            this.SelectLineCommand = new Wpf.Commands.RelayCommand<object>(OnSelectedLineChange);
            this.MoveUpCommand = new Wpf.Commands.RelayCommand(OnSelectedMoveUp);
            this.MoveDownCommand = new Wpf.Commands.RelayCommand(OnSelectedMoveDown);

            if (_curTowerList == null) _curTowerList = new List<TowerForClient>();
            if (_wireTowerConverter == null) _wireTowerConverter = new WireTowerConverter();
            //_towerFilePath = System.Windows.Forms.Application.LocalUserAppDataPath + "\\" + "TestTower.json";
            
            LineSet = new ObservableCollection<LineForClient>(WirTowRenderManagement.Instance.Lines);
            if(LineSet?.Count>0) SelectedLine = LineSet.FirstOrDefault();
            OnSelectedLineChange(SelectedLine);
            string fileName = "MmcTowersOfLine" + SelectedLine.Id + ".json";
            ReadData(fileName);
        }

        private void OnSelectedMoveUp()
        {
            if (SelectedDataRow == null)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTChooseOprObj"));
            }
            else
            {
                if (SelectedDataRow == _curTowerList.First())
                {
                    Messages.ShowMessage($"{SelectedDataRow.Serial}{Helpers.ResourceHelper.FindKey("WTFirstItemWarn")}");
                }
                else if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("WTItemHint"), $"{Helpers.ResourceHelper.FindKey("WTItemMoveUp")}{SelectedDataRow.Serial}"))
                {
                    ChangeOrder(-1);
                }
            }
        }

        private void OnSelectedMoveDown()
        {
            if (SelectedDataRow == null)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTChooseOprObj"));
            }
            else
            {
                if (SelectedDataRow == _curTowerList.Last())
                {
                    Messages.ShowMessage($"{SelectedDataRow.Serial}{Helpers.ResourceHelper.FindKey("WTLastItemWarn")}");
                }
                else if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("WTItemHint"), $"{Helpers.ResourceHelper.FindKey("WTItemMoveDown")}{SelectedDataRow.Serial}"))
                {
                    ChangeOrder(1);
                }
            }
        }

        

        private void ChangeOrder(int num)
        {
            var curItemSerial = SelectedDataRow.Serial;
            var currIndex = _curTowerList.FindIndex(p => p.Serial.Equals(curItemSerial));
            var curNum = TowerSerialToNum(curItemSerial);
            if (curNum == -1)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTSerialIllegal"));
            }
            else
            {
                var changeItemSerial = SelectedLine.LineSerial + CreateTowerSerial(curNum + num);
                var changeIndex = _curTowerList.FindIndex(p => p.Serial.Equals(changeItemSerial));
                _curTowerList[currIndex].Serial = changeItemSerial;
                if (changeIndex != -1) _curTowerList[changeIndex].Serial = curItemSerial;
                _curTowerList = TowerSet.ToList().OrderBy(p =>TowerSerialToNum(p.Serial)).ToList();
                TowerSet = new ObservableCollection<TowerForClient>(_curTowerList);
            }
        }

        private void OnSelectedLineChange(object obj)
        {
            //if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("WTItemHint"), Helpers.ResourceHelper.FindKey("WTSaveData")))
            //{
            //    this.SaveTowerSet();
            //}

            if (!(obj is LineForClient line)) return;
            RefleshLineSet(line.Id);
        }

        private void RefleshLineSet(int lineId)
        {
            string fileName = "MmcTowersOfLine" + lineId + ".json";

            _curTowerList = TowerSet.ToList().FindAll(p => p.Pid == lineId);

            SelectedLine = LineSet.ToList().Find(p => p.Id == lineId);

            string filePath = WirTowRenderManagement.Instance.GetLocalWirePath(fileName);

            var towers = JsonUtil.DeserializeFromFile<List<TowerModel>>(filePath);
            
            _curTowerList.Clear();

            if (towers == null || towers.Count <= 0)
            {
                TowerSet = null;
                return;
            }

            foreach (var tower in towers)
            {
                _curTowerList.Add(_wireTowerConverter.TowerConvert(tower));
            }

            TowerSet = new ObservableCollection<TowerForClient>(_curTowerList);
        }

        public void UpdateLineSetValue(List<LineForClient> obj)
        {
            if(obj == null ) return;
            LineSet = new ObservableCollection<LineForClient>(obj);
            if (SelectedLine != null && !LineSet.Contains(SelectedLine))
            {
                SelectedLine = LineSet.FirstOrDefault();
            }
        }

        public void SaveTowerSet()
        {
            List<TowerModel> tempList = new List<TowerModel>();
            foreach (var item in _curTowerList)
            {
                tempList.Add(_wireTowerConverter.TowerConvert(item));
            }
            string towerStr = Newtonsoft.Json.JsonConvert.SerializeObject(tempList);

            string fileName = "MmcTowersOfLine" + SelectedLine.Id + ".json";
            string filePath = WirTowRenderManagement.Instance.GetLocalWirePath(fileName);
            WirTowRenderManagement.Instance.WriteFile(filePath, towerStr);
        }

        private void ReadData(string fileName)
        {
            try
            {
                string filePath = WirTowRenderManagement.Instance.GetLocalWirePath(fileName);
                List<TowerModel> towers = JsonUtil.DeserializeFromFile<List<TowerModel>>(filePath);
                var temp = new List<TowerForClient>();
               
                if (towers == null || towers.Count <= 0) return;
                foreach (var item in towers)
                {
                    temp.Add(_wireTowerConverter.TowerConvert(item));
                }

                _curTowerList = temp.OrderBy(p => TowerSerialToNum(p.Serial)).ToList();
                TowerSet = new ObservableCollection<TowerForClient>(_curTowerList);
            }
            catch
            {
            }
        }

        private void OnDeleteTower(object obj)
        {
            var tower = obj as TowerForClient;
            if (tower == null) return;
            if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("WTItemWarn"), $"{Helpers.ResourceHelper.FindKey("WTItemDelete")}:{tower.Serial} ") )
            {
                TowerSet.Remove(tower);
                _curTowerList.Remove(tower);
            }
        }

        private void OnEditTower(object obj)
        {
            if (!(obj is TowerForClient tower)) return;

            var newTower = new TowerForClient()
            {
                Id = tower.Id,
                Pid = tower.Pid,
                Name = tower.Name,
                Serial=tower.Serial,
                X = tower.X,
                Y = tower.Y,
                Z = tower.Z,
                SafeDistance = tower.SafeDistance,
                TowerType = tower.TowerType,
                SignList = tower.SignList,
                CrossVotor = tower.CrossVotor,
                LeftLine = tower.LeftLine,
                RightLine = tower.RightLine,
                IsSelected = tower.IsSelected,
                RelativeHeight=tower.RelativeHeight,
            };

            ShowTowerView(newTower, true);
        }

        //private void OnShowDetailTower(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        private void AddTower()
        {
            if (!IsLineChoosed()) return;
            var lastTower = TowerSet.ToList().LastOrDefault();
            var maxId = TowerSet.Count>0? TowerSet.ToList().Max(p => p.Id):-1;
            int id = 0;
            int num = 1;
         
            if (lastTower != null)
            {
                id = maxId + 1;
                var temp = lastTower.Serial.Substring(lastTower.Serial.Length - 4);
                if (int.TryParse(temp, out num))
                    num += 1;
            }

            string serial = SelectedLine.LineSerial +  CreateTowerSerial(num);

            TowerForClient tower = new TowerForClient()
            {
                Id = id,
                SafeDistance = 10,
                Pid = SelectedLine.Id,
                Serial = serial,
                Name = serial,
                RelativeHeight = 0
            };

            ShowTowerView(tower, false);
        }

        private void GenerateRoute()
        {
            if (!IsLineChoosed()) return;
            var list = TowerSet.ToList().FindAll(p => p.IsSelected == true);
            if(list?.Count<=0)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTChooseLineFirst"));
                return;
            }

            if(_generateRouteViewModel==null) _generateRouteViewModel = new GenerateRouteViewModel();

            _generateRouteViewModel.CurrentTowerList = list;
            _generateRouteViewModel.ShowView();
        }

        private void ShowTowerView(TowerForClient tower, bool isEdit)
        {
            if (_towerViewModel == null) _towerViewModel = new TowerViewModel();
            _towerViewModel.SelectedLine = SelectedLine;
            _towerViewModel.LineSet = LineSet;
            
            _towerViewModel.TowerModel = tower;
            _towerViewModel.IsEdit = isEdit;

            _towerViewModel.OnUpdateTowerInfo -= UpdateTowerCallback;
            _towerViewModel.OnUpdateTowerInfo += UpdateTowerCallback;

            RenderTowerInfo(tower);
            
            _towerViewModel.ShowView();

            OnManagerViewShow(false);
        }

        private void UpdateTowerCallback(TowerForClient obj,bool isEdit, bool isUpdate)
        {
            if (!isUpdate)
            {
                OnManagerViewShow(true);
                return;
            }

            if (TowerSerialToNum(obj.Serial) == -1)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTSerialIllegal"));

                this.ShowTowerView(obj, isEdit);
                //_towerViewModel.ShowView();
                return;
            }

            if (!isEdit && _curTowerList.FindAll(p => p.Serial.Equals(obj.Serial)).Count > 0)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTSerialRepeat"));
                this.ShowTowerView(obj, isEdit);
                //_towerViewModel.ShowView();
                return;
            }

            obj.Serial = SelectedLine.LineSerial + CreateTowerSerial(TowerSerialToNum(obj.Serial));
            var index = _curTowerList.FindIndex(p => p.Id == obj.Id);
            _wireTowerConverter.CreatTowerLineOrder(ref obj);

            if (index >= 0)
            {
                _curTowerList[index] = obj;
            }
            else
            {
                _curTowerList.Add(obj);
            }

            _curTowerList.OrderBy(p => p.Serial);
            TowerSet = new ObservableCollection<TowerForClient>(_curTowerList);

            RenderTowerInfo(obj);

            OnManagerViewShow(true);
        }

        private void RenderTowerInfo(TowerForClient tower)
        {
            WirTowRenderManagement.Instance.ClearRenderObj();
            WirTowRenderManagement.Instance.RenderTowerInfo(tower);
            WirTowRenderManagement.Instance.RenderSignsOfTower(tower.SignList.ToList());
        }

        private bool IsLineChoosed()
        {
            bool isChoosed = false;
            if (SelectedLine==null || SelectedLine.LineName == null)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTChooseLineFirst"));
            }
            else
            {
                isChoosed = true;
            }
            return isChoosed;
        }

        // num`s range default as (0,10000)
        private string CreateTowerSerial(int num)
        {
            string serial;
            if (num <= 0 || num >= 10000) return null;
            if (num < 10) serial = $"000{num}";
            else if (num < 100) serial = $"00{num}";
            else if (num < 1000) serial = $"0{num}";
            else serial = num.ToString();
            return "-"+ serial;
        }

        private int TowerSerialToNum(string serial)
        {
            int num = -1;

            var temp = serial.Split('-').ToList().LastOrDefault();

            if (int.TryParse(temp, out num))
            {
                if (_curTowerList.Count > 0 && num == 0)
                    num = -1;
            }
            else
            {
                num = -1;
            }
            return num;
        }

        #region data for test
        //private void testData()
        //{
        //    List<TowerForClient> tempList = new List<TowerForClient>();

        //    double distance = 20;
        //    TowerForClient towerA = new TowerForClient()
        //    {
        //        Id = 1,
        //        Serial = "CZ01",
        //        Name = "1号塔",
        //        Pid = 1,
        //        X = 114.450914187539,
        //        Y = 30.3984391216036,
        //        Z = 40.899102821015,
        //        TowerType = Common.CommonContract.TowerType.Straight.ToString(),
        //        SignList = new ObservableCollection<SignModel>()
        //        {
        //            new SignModel()
        //            {
        //                id=1,
        //                pid = 1,
        //                name = " 左侧  a点",
        //                serial = "1",
        //                X =114.450916,
        //                Y=30.398335,
        //                Z=79.29,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.Left.ToString()
        //            },
        //            new SignModel()
        //            {
        //                id=2,
        //                pid = 1,
        //                name = " 左侧  b点",
        //                serial = "2",
        //                X =114.450922,
        //                Y=30.398289,
        //                Z=91.78,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.Left.ToString()
        //            },
        //            new SignModel()
        //            {
        //                id=3,
        //                pid = 1,
        //                name = " 左侧  c点",
        //                serial = "3",
        //                X =114.450913,
        //                Y=30.398341,
        //                Z=111.68,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.Left.ToString()
        //            },

        //            new SignModel() {
        //                id=4,
        //                pid = 1,
        //                name = " 中间  顶点",
        //                serial = "4",
        //                X =114.450914187539,
        //                Y=30.3984391216036,
        //                Z=111.899102821015,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.TopCenter.ToString()
        //            },
        //            new SignModel()
        //            {
        //                id=5,
        //                pid = 1,
        //                name = " 右侧  e点",
        //                serial = "5",
        //                X =114.450915,
        //                Y=30.398538,
        //                Z=112.12,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.Right.ToString()
        //            },
        //            new SignModel()
        //            {
        //                id=6,
        //                pid = 1,
        //                name = " 右侧  f点",
        //                serial = "6",
        //                X =114.450929,
        //                Y=30.398607,
        //                Z=91.79,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.Right.ToString()
        //            },
        //            new SignModel()
        //            {
        //                id=7,
        //                pid = 1,
        //                name = " 右侧  g点",
        //                serial = "7",
        //                X =114.450916536386,
        //                Y=30.3985644866285,
        //                Z=80.34,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.Right.ToString()
        //            }
        //        }
        //    };

        //    var Aleft = towerA.SignList.ToList().Find(p => p.id == 3);
        //    var Aright = towerA.SignList.ToList().Find(p => p.id == 5);

        //    IPoint AleftPoint = GviMap.PointManager.CreatePoint(Aleft.X, Aleft.Y, Aleft.Z);

        //    IPoint ArigthPoint = GviMap.PointManager.CreatePoint(Aright.X, Aright.Y, Aright.Z);

        //    towerA.CrossVotor = CalulateUnitVector(AleftPoint, ArigthPoint, true);

        //    TowerForClient towerB = new TowerForClient()
        //    {
        //        Id = 2,
        //        Serial = "CZ02",
        //        Name = "2号塔",
        //        Pid = 1,
        //        X = 114.454374278628,
        //        Y = 30.398447050862,
        //        Z = 38.08,
        //        TowerType = Common.CommonContract.TowerType.Straight.ToString(),
        //        SignList = new ObservableCollection<SignModel>()
        //        {
        //            new SignModel()
        //            {
        //                id=8,
        //                pid = 2,
        //                name = " 左侧  a点",
        //                serial = "8",
        //                X =114.4544045218,
        //                Y=30.3982840415648,
        //                Z=68.41,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.Left.ToString()
        //            },
        //            new SignModel()
        //            {
        //                id=7,
        //                pid = 1,
        //                name = " 左侧  b点",
        //                serial = "7",
        //                X =114.454367111122,
        //                Y=30.3982818057176,
        //                Z=79.22,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.Left.ToString()
        //            },
        //            new SignModel()
        //            {
        //                id=6,
        //                pid = 1,
        //                name = " 左侧  c点",
        //                serial = "6",
        //                X =114.454382116062,
        //                Y=30.3983110425043,
        //                Z=107.9,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.Left.ToString()
        //            },

        //            new SignModel() {
        //                id=5,
        //                pid = 1,
        //                name = " 中间  顶点",
        //                serial = "5",
        //                X =114.454374278628,
        //                Y=30.398447050862,
        //                Z=108.08,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.TopCenter.ToString()
        //            },
        //            new SignModel()
        //            {
        //                id=4,
        //                pid = 1,
        //                name = " 右侧  e点",
        //                serial = "4",
        //                X =114.454367,
        //                Y=30.3986,
        //                Z=108.34,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.Right.ToString()
        //            },
        //            new SignModel()
        //            {
        //                id=3,
        //                pid = 1,
        //                name = " 右侧  f点",
        //                serial = "3",
        //                X =114.454395170856,
        //                Y=30.3985838178919,
        //                Z=96.42,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.Right.ToString()
        //            },
        //            new SignModel()
        //            {
        //                id=2,
        //                pid = 1,
        //                name = " 右侧  g点",
        //                serial = "2",
        //                X =114.454373703696,
        //                Y=30.3985942950495,
        //                Z=79.84,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.Right.ToString()
        //            },
        //            new SignModel()
        //            {
        //                id=1,
        //                pid = 1,
        //                name = " 右侧  h点",
        //                serial = "1",
        //                X =114.454394876477,
        //                Y=30.3985967644223,
        //                Z=68.27,
        //                distance=distance,
        //                type = Common.CommonContract.SignType.Right.ToString()
        //            }
        //        }
        //    };

        //    var Bleft = towerB.SignList.ToList().Find(p => p.id == 6);
        //    var Bright = towerB.SignList.ToList().Find(p => p.id == 4);

        //    IPoint BleftPoint = GviMap.PointManager.CreatePoint(Bleft.X, Bleft.Y, Bleft.Z);

        //    IPoint BrigthPoint = GviMap.PointManager.CreatePoint(Bright.X, Bright.Y, Bright.Z);

        //    towerB.CrossVotor = CalulateUnitVector(BleftPoint, BrigthPoint, true);

        //    tempList.Add(towerA);
        //    tempList.Add(towerB);

        //    _curTowerList = tempList;
        //    TowerSet = new ObservableCollection<TowerForClient>(_curTowerList);
        //}
        #endregion
    }
}
