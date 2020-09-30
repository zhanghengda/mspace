using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Mmc.Framework.Services;
using Mmc.MathUtil;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.WireTowerModule.Models;
using Mmc.Mspace.WireTowerModule.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.WireTowerModule.ViewModels
{
    public class TowerViewModel : BaseViewModel
    {
        #region vary

        public Action<TowerForClient,bool, bool> OnUpdateTowerInfo;
        public bool IsEdit { get; set; }

        private List<SignModel> _currentSigns;
        private SignModel _currentSign;
        private TowerView _towerView;
        private SignViewModel _signViewModel;

        #endregion

        #region binging vary and command
        private bool _isSerialEnabled;

        public bool IsSerialEnabled
        {
            get { return _isSerialEnabled; }
            set
            {
                _isSerialEnabled = value;
                OnPropertyChanged("IsSerialEnabled");
            }
        }
        private LineForClient _selectedLine;

        public LineForClient SelectedLine
        {
            get => _selectedLine ?? new LineForClient();
            set
            {
                _selectedLine = value;
                OnPropertyChanged("SelectedLine");
            }
        }

        private ObservableCollection<LineForClient> _lineSet;

        public ObservableCollection<LineForClient> LineSet
        {
            get => _lineSet ?? new ObservableCollection<LineForClient>();
            set
            {
                _lineSet = value;
                OnPropertyChanged("LineSet");
            }
        }

        private string _viewTitle;

        public string ViewTitle
        {
            get { return _viewTitle; }
            set
            {
                _viewTitle = value;
                OnPropertyChanged("ViewTitle");
            }
        }

        private SignModel _selectedDataRow;

        public SignModel SelectedDataRow
        {
            get => _selectedDataRow;
            set
            {
                _selectedDataRow = value;
                OnPropertyChanged("SelectedDataRow");
            }
        }

        private TowerForClient _towerModel;

        public TowerForClient TowerModel
        {
            get { return _towerModel ?? new TowerForClient(); }
            set
            {
                _towerModel = value;
                OnPropertyChanged("TowerModel");
            }
        }

        private TextItem _selectedType;

        public TextItem SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged("SelectedType");
            }
        }

        private ObservableCollection<TextItem> _towerTypeSet;

        public ObservableCollection<TextItem> TowerTypeSet
        {
            get
            {
                return _towerTypeSet ??
                       (_towerTypeSet = new ObservableCollection<TextItem>(CommonContract.GetTowerType()));
            }
            set
            {
                _towerTypeSet = value;
                OnPropertyChanged("TowerTypeSet");
            }
        }

        private string _hintText;

        public string HintText
        {
            get
            {
                _hintText =
                    $" {Helpers.ResourceHelper.FindKey("WTTowerHint")}{Helpers.ResourceHelper.FindKey("WTTopCenterSign")}/{Helpers.ResourceHelper.FindKey("WTTopLeftSign")}/{Helpers.ResourceHelper.FindKey("WTTopRightSign")} "
                    ;
                return _hintText;
            }
            private set
            {
                _hintText = value;
                OnPropertyChanged("HintText");
            }
        }


        [XmlIgnore] public ICommand CancelCommand { get; set; }
        [XmlIgnore] public ICommand SaveCommand { get; set; }

        [XmlIgnore] public ICommand GetPositionCommand { get; set; }

        [XmlIgnore] public ICommand AddSignCommand { get; set; }

        [XmlIgnore] public ICommand EditCommand { get; set; }
        [XmlIgnore] public ICommand DeleteCommand { get; set; }
        [XmlIgnore] public ICommand SelectLineCommand { get; set; }
        [XmlIgnore] public ICommand MoveUpCommand { get; set; }
        [XmlIgnore] public ICommand MoveDownCommand { get; set; }
        [XmlIgnore] public ICommand DetailCommand { get; set; }

        #endregion

        public TowerViewModel()
        {
            _currentSigns = new List<SignModel>();

            this.CancelCommand = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                MapControlEventManagement(false);
                this.HideView();
                OnUpdateTowerInfo(TowerModel,IsEdit, false);
            });
            this.SaveCommand = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                MapControlEventManagement(false);
                if (this.SaveTowerInfo())
                {
                    this.HideView();
                    OnUpdateTowerInfo(TowerModel, IsEdit, true);
                }
            });
            this.GetPositionCommand = new Mmc.Wpf.Commands.RelayCommand(getPosition);
            this.AddSignCommand = new Mmc.Wpf.Commands.RelayCommand(AddSignPoint);
            this.EditCommand = new Mmc.Wpf.Commands.RelayCommand<object>(OnEditSign);
            this.DeleteCommand = new Mmc.Wpf.Commands.RelayCommand<object>(OnDeleteSign);
            this.SelectLineCommand = new Wpf.Commands.RelayCommand<object>(OnSelectedLineChange);
            this.MoveUpCommand = new Wpf.Commands.RelayCommand(OnSelectedMoveUp);
            this.MoveDownCommand = new Wpf.Commands.RelayCommand(OnSelectedMoveDown);
            this.DetailCommand = new Mmc.Wpf.Commands.RelayCommand(OnShowRoute);
        }

        private void OnShowRoute()
        {
            if (this.SaveTowerInfo())
            {
                WirTowRenderManagement.Instance.ClearRenderObj();
                WirTowRenderManagement.Instance.RenderTowerInfo(TowerModel);
                WirTowRenderManagement.Instance.RenderSignsOfTower(TowerModel.SignList.ToList());

                var calculater = new CalculateAndOutputRoute();
                var lines = calculater.ShowTowerDetail(TowerModel);
                WirTowRenderManagement.Instance.RenderRouteOfTower(lines, null);
            }
        }

        private void OnSelectedMoveUp()
        {
            if (SelectedDataRow == null)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTChooseOprObj"));
                return;
            }
            else
            {
                if (SelectedDataRow == _currentSigns.First())
                {
                    Messages.ShowMessage($"{SelectedDataRow.serial}{Helpers.ResourceHelper.FindKey("WTFirstItemWarn")}");
                }
                else if(Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("WTItemHint"), $"{Helpers.ResourceHelper.FindKey("WTItemMoveUp")}{SelectedDataRow.serial}"))
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
                return;
            }
            else
            {
                if (SelectedDataRow == _currentSigns.Last())
                {
                    Messages.ShowMessage($"{SelectedDataRow.serial}{Helpers.ResourceHelper.FindKey("WTLastItemWarn")}");
                }
                else if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("WTItemHint"), $"{Helpers.ResourceHelper.FindKey("WTItemMoveDown")}{SelectedDataRow.serial}"))
                {
                    ChangeOrder(1);
                }
            }
        }

        private void ChangeOrder(int num)
        {
            var curItemSerial = SelectedDataRow.serial;
            var currIndex = _currentSigns.FindIndex(p => p.serial.Equals(SelectedDataRow.serial));
            var changeItemSerial = (Convert.ToInt32(SelectedDataRow.serial) + num).ToString();
            var changeIndex = _currentSigns.FindIndex(p => p.serial.Equals(changeItemSerial));
            _currentSigns[currIndex].serial = changeItemSerial;
            if (changeIndex != -1) _currentSigns[changeIndex].serial = curItemSerial;
            _currentSigns = TowerModel.SignList.ToList().OrderBy(p => int.Parse(p.serial)).ToList();
            TowerModel.SignList = new ObservableCollection<SignModel>(_currentSigns);
        }

        private void OnSelectedLineChange(object obj)
        {
            if (!(obj is LineForClient line)) return;
            TowerModel.Pid = line.Id;
        }

        private void AddOrUpdateSignCallback(SignModel obj,bool isEdit, bool isUpdate)
        {

            if (!isUpdate|| obj == null)
            {
                this.ShowView();
                return;
            }

            //var itemsCount = _currentSigns.FindAll(p => p.serial.Equals(obj.serial)).Count;
            //if (isEdit && (_currentSign.serial.Equals(obj.serial) || itemsCount != 0))
            //{
            //    Messages.ShowMessage("该编号已存在");
            //    ShowSignView(obj, isEdit);
            //    return;
            //}




            if (!isEdit && _currentSigns.FindAll(p => p.serial.Equals(obj.serial)).Count > 0)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTSerialRepeat"));
                this.ShowSignView(obj, isEdit);
                return;
            }


            

            var index = _currentSigns.FindIndex(p => p.id == obj.id);
            
            if (index >= 0)
            {
                _currentSigns[index] = obj;
            }
            else
            {
                _currentSigns.Add(obj);
            }

            this.RefreshViewData();

            WirTowRenderManagement.Instance.AddOrUpdateRPoi(obj.id.ToString() + obj.serial, obj);
            this.ShowView();
        }

        private void RefreshViewData()
        {
            TowerModel.SignList = new ObservableCollection<SignModel>(_currentSigns);
        }

        private void OnDeleteSign(object obj)
        {
            var sign = obj as SignModel;
            if (sign == null) return;

            if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("WTItemHint"), $"{Helpers.ResourceHelper.FindKey("WTItemDelete")}{sign.serial}")  )
            {
                _currentSigns.Remove(sign);
            }

            RefreshViewData();

            WirTowRenderManagement.Instance.DeleteRenderObj(sign.id.ToString() + sign.serial);
        }

        private void OnEditSign(object obj)
        {
            if (!(obj is SignModel sign)) return;

            _currentSign = sign;
             var   newSign = new SignModel()
            {
                serial = _currentSign.serial,
                name = _currentSign.name,
                type = _currentSign.type,
                id = _currentSign.id,
                pid = _currentSign.pid,
                speDistance = _currentSign.speDistance,
                eulerAngle = _currentSign.eulerAngle,
                X = _currentSign.X,
                Y = _currentSign.Y,
                Z = _currentSign.Z,
                pitchAngle=_currentSign.pitchAngle,
                trendAngle = _currentSign.trendAngle
            };
            ShowSignView(newSign, true);
        }

        private void AddSignPoint()
        {
            int id = 0;
            int serial = 1;
            var last = _currentSigns.LastOrDefault();
            var maxId = _currentSigns?.Count>0? _currentSigns.Max(p => p.id) : -1;
            if (last != null)
            {
                id = maxId + 1;
                if(int.TryParse(last.serial,out serial))
                    serial += 1;
            }

            SignModel sign = new SignModel()
            {
                pid = TowerModel.Id,
                id = id,
                speDistance = 0,
                serial = serial.ToString(),
                X = 0,
                Y = 0,
                Z = 0,
                pitchAngle = 15,
                trendAngle = 0
            };

            ShowSignView(sign, false);
        }

        private void ShowSignView(SignModel sign, bool isEdit)
        {
            if (_signViewModel == null) _signViewModel = new SignViewModel();
            _signViewModel.Sign = sign;
            _signViewModel.IsEdit = isEdit;
            _signViewModel.OnAddOrUpdateSign -= AddOrUpdateSignCallback;
            _signViewModel.OnAddOrUpdateSign += AddOrUpdateSignCallback;
            MapControlEventManagement(false);
            _signViewModel.ShowView();
            this.HideView();
        }

        private bool SaveTowerInfo()
        {
            bool isSuccess = false;
            var topCenter = _currentSigns.FindAll(p => p.type.Equals(CommonContract.SignType.TopCenter.ToString()));
            var leftTop = _currentSigns.FindAll(p => p.type.Equals(CommonContract.SignType.TopLeft.ToString()));
            var rightTop = _currentSigns.FindAll(p => p.type.Equals(CommonContract.SignType.TopRight.ToString()));

            if (topCenter.Count != 1 || leftTop.Count != 1 || rightTop.Count != 1)
            {
                Messages.ShowMessage(HintText);
                return isSuccess;
            }

            IPoint AleftPoint = GviMap.PointManager.CreatePoint(leftTop[0].X, leftTop[0].Y, leftTop[0].Z);

            IPoint ArigthPoint = GviMap.PointManager.CreatePoint(rightTop[0].X, rightTop[0].Y, rightTop[0].Z);

            TowerModel.CrossVotor = CalulateUnitVector(AleftPoint, ArigthPoint, true);

            TowerModel.TowerType = SelectedType?.Key ?? TowerTypeSet.FirstOrDefault()?.Key;

            isSuccess = true;
            return isSuccess;
        }

        private Vector3 CalulateUnitVector(IPoint pt1, IPoint pt2, bool isHorizon = false)
        {
            var pt1utm = Wgs84ToUtm(pt1);

            var pt2utm = Wgs84ToUtm(pt2);

            Vector3 vactor = new Vector3() {X = pt2utm.X - pt1utm.X, Y = pt2utm.Y - pt1utm.Y, Z = pt2utm.Z - pt1utm.Z};
            if (isHorizon) vactor.Z = 0;

            vactor.Normalize();
            return vactor;
        }

        private IPoint Wgs84ToUtm(IPoint point)
        {
            var prjWkt = Wgs84UtmUtil.GetWkt(point.X);
            if (!string.IsNullOrEmpty(prjWkt)) point.ProjectEx(prjWkt);
            return point;
        }

        private void getPosition()
        {
            this.HideView();
            MapControlEventManagement(true);
        }

        private void OnUpdatePosition(string action, double x, double y, double z)
        {
            switch (action)
            {
                case "Click":
                    TowerModel.X = x;
                    TowerModel.Y = y;
                    TowerModel.Z = z;
                    this.ShowView();
                    break;
                case "Move":
                    TowerModel.X = x;
                    TowerModel.Y = y;
                    TowerModel.Z = z;
                    break;
            }
        }

        private void MapControlEventManagement(bool OnEvent)
        {
            WirTowRenderManagement.Instance.MapClickSelectEventManagement(OnEvent);
            WirTowRenderManagement.Instance.MapTransformMoveEventManagement(OnEvent);
            if (OnEvent)
            {
                WirTowRenderManagement.Instance.OnUpdatePointPosition -= OnUpdatePosition;
                WirTowRenderManagement.Instance.OnUpdatePointPosition += OnUpdatePosition;
            }
            else
            {
                WirTowRenderManagement.Instance.OnUpdatePointPosition -= OnUpdatePosition;
            }
        }

        public void ShowView()
        {
            IsSerialEnabled = !IsEdit;
            MapControlEventManagement(true);

            if (IsEdit)
            {
                ViewTitle = Helpers.ResourceHelper.FindKey("WTEdit");
                SelectedType = SelectedType ??
                               TowerTypeSet.ToList().Find(p => p.Key.ToString().Equals(TowerModel.TowerType));
                WirTowRenderManagement.Instance.SetTransformPoint(TowerModel.X, TowerModel.Y, TowerModel.Z);
            }
            else
            {
                ViewTitle = Helpers.ResourceHelper.FindKey("WTAdd");
                SelectedType = SelectedType ?? TowerTypeSet[0];
            }

            ViewTitle += Helpers.ResourceHelper.FindKey("WTTower");

            _currentSigns = TowerModel.SignList.ToList().OrderBy(p => int.Parse(p.serial)).ToList();
            TowerModel.SignList = new ObservableCollection<SignModel>(_currentSigns);
            if (_towerView == null) _towerView = new TowerView();
            _towerView.DataContext = this;
            _towerView.Owner = Application.Current.MainWindow;
            _towerView.Left = 100;
            _towerView.Top = 100;
            _towerView.Show();
        }

        public void HideView()
        {
            _towerView?.Hide();
        }

        public void CloseView()
        {
            _towerView?.Close();
        }

        public void ClearData()
        {
            IsEdit = false;
            MapControlEventManagement(false);
        }
    }
}
