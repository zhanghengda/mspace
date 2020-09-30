using Gvitech.CityMaker.Math;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Models.AddressManagementModel;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class AddressManagementVModel: BaseViewModel
    {
        public AddressManagementVModel()
        {
            this.AddFirstLevelCmd = new RelayCommand(OnAddFirstLevel);
            this.AddChildNodeCmd = new RelayCommand<AddressInfoModel>((addressInfoModel)=>OnAddChildNode(addressInfoModel));
            this.DeleteChildNodeCmd = new RelayCommand<AddressInfoModel>((addressInfoModel) => OnDeleteChildNode(addressInfoModel));
            this.SetNodeInfoCmd = new RelayCommand<AddressInfoModel>((addressInfoModel) => OnSetNodeInfo(addressInfoModel));
            this.LeftDoubleClickCmd = new RelayCommand<AddressInfoModel>((addressInfoModel) => OnLeftDoubleClick(addressInfoModel));
            this.SearchCommand = new RelayCommand(OnSearch);
            Init();
        }


        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            GviMap.Camera.GetCamera(out IVector3 Position, out IEulerAngle Angle);
            string aa = JsonUtil.SerializeToString(Position);
            string bb = JsonUtil.SerializeToString(Angle);
            IVector3 cc = JsonUtil.DeserializeFromString<Vector3>(aa);
            IEulerAngle dd = JsonUtil.DeserializeFromString<EulerAngle>(bb);

            _localWsCfgSrv = ServiceManager.GetService<ILocalWsConfigService>(null);
            var AddressDatasList = _localWsCfgSrv.AddressManagementDatas.FindAll();
            if (AddressDatasList != null)
            {
                loadLocalAddressDatas(AddressDatasList);
            }
        }


        private void loadLocalAddressDatas(List<AddressInfoModel> addressDatasList)
        {
            List<AddressInfoModel> addressList = new List<AddressInfoModel>();
            addressList=addressDatasList.OrderBy(s => s.AddressLevel).ToList();
            foreach (var item in addressList)
            {
                if(string.IsNullOrEmpty(item.ParentId)&& !string.IsNullOrEmpty(item.AddressId ))
                {
                    AddressInfoCollection.Add(item);
                }
                else
                {
                    LocalDataAddressCollection(item,AddressInfoCollection);
                }
            }
        }


        private void LocalDataAddressCollection(AddressInfoModel addressInfo, ObservableCollection<AddressInfoModel> AddressInfoCollection)
        {

            AddressInfoCollection.ForEach(e =>
            {
                if (e.AddressId == addressInfo.ParentId)
                {
                    var model = e.ChlidList.FirstOrDefault(t => t.AddressId == addressInfo.AddressId);
                    if (model == null)
                    {
                        if (addressInfo.AddressParentNode == null)
                        {
                            addressInfo.AddressParentNode = new ObservableCollection<AddressInfoModel>();
                        }
                        else
                        {
                            addressInfo.AddressParentNode.Clear();
                        }
                        addressInfo.AddressParentNode.Insert(0, e);
                        e.ChlidList.Add(addressInfo);
                    }
                }
                else
                {
                    if (e.ChlidList.Count > 0)
                    {
                        LocalDataAddressCollection(addressInfo, e.ChlidList);
                    }
                }
            });
        }

        private void OnSearch()
        {
            try
            {
                if(!string.IsNullOrEmpty(SearchAddressText))
                {
                    SearchNode(AddressInfoCollection);
                    TempAddressInfoCollection = new ObservableCollection<AddressInfoModel>(AddressInfoCollection);
                    AddressInfoCollection = new ObservableCollection<AddressInfoModel>(SearchAddressInfoCollection);
                }
                else
                {
                    if(TempAddressInfoCollection.Count ==0)
                    {
                        AddressInfoCollection = new ObservableCollection<AddressInfoModel>(AddressInfoCollection);
                    }else
                    {
                        AddressInfoCollection = new ObservableCollection<AddressInfoModel>(TempAddressInfoCollection);
                    }
                    
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void SearchNode(ObservableCollection<AddressInfoModel> AddressInfoCollection)
        {
            foreach(var item in AddressInfoCollection)
            {
                if(item.NodeName.Contains(SearchAddressText))
                {
                    SearchAddressInfoCollection.Add(item);
                }
                else
                {
                    if(item.ChlidList.Count>0)
                    {
                        SearchNode(item.ChlidList);
                    }
                }
            }
        }
        private int times = 0;
        public void OnLeftDoubleClick(AddressInfoModel addressInfoModel)
        {
            times += 1;

            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = new TimeSpan(0, 0, 0, 0, 300);

            timer.Tick += (s, e1) => { timer.IsEnabled = false; times = 0; };

            timer.IsEnabled = true;

            if (times % 2 == 0)
            {

                timer.IsEnabled = false;

                times = 0;
                try
                {
                    if (addressInfoModel != null && !string.IsNullOrEmpty(addressInfoModel.StringPosition) && !string.IsNullOrEmpty(addressInfoModel.StringAngle))
                    {
                        Postion = JsonUtil.DeserializeFromString<Vector3>(addressInfoModel.StringPosition);
                        Angle = JsonUtil.DeserializeFromString<EulerAngle>(addressInfoModel.StringAngle);
                        GviMap.Camera.LookAt(Postion, 0, Angle);
                    }

                }
                catch (Exception e)
                {
                    SystemLog.Log(e);
                }
            }
        }

        /// <summary>
        /// 添加一级项
        /// </summary>
        private void OnAddFirstLevel()
        {
            try
            {
                Guid guid = Guid.NewGuid();
                AddressInfoModel addressInfoModel = new AddressInfoModel();
                addressInfoModel.AddressId  = guid.ToString();
                addressInfoModel.NodeName = "1"+ Helpers.ResourceHelper.FindKey("AddressManagementLevelNode");
                addressInfoModel.AddressLevel = 1;
                AddressInfoCollection.Add(addressInfoModel);
                CreateNewNodeInfo(addressInfoModel,"createnew");
                _localWsCfgSrv.AddressManagementDatas.Add(addressInfoModel);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnSetNodeInfo(AddressInfoModel addressInfoModel)
        {
            try
            {
                CreateNewNodeInfo(addressInfoModel, string.Empty);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }


        private void CreateNewNodeInfo(AddressInfoModel addressInfoModel,string str)
        {
            try
            {
                if (settingAddressInfoVModel == null)
                {
                    settingAddressInfoVModel = new SettingAddressInfoVModel();
                }
                settingAddressInfoVModel.SettingAddress += settingAddressInfoVModel_SettingAddress;
                settingAddressInfoVModel.CloseWindow += settingAddressInfoVModel_CloseWindow;
                settingAddressInfoVModel.CancelSettingAddress += settingAddressInfoVModel_CancelSettingAddress;
                AddressBtnEnable = false;
                settingAddressInfoVModel.ShowView(addressInfoModel, str);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void settingAddressInfoVModel_CancelSettingAddress(string str, AddressInfoModel addressInfo)
        {
            try
            {
                settingAddressInfoVModel.CancelSettingAddress -= settingAddressInfoVModel_CancelSettingAddress;
                if (!string.IsNullOrEmpty(str))
                {
                    AddressInfoModel newAddressInfo = FindDeleteAddressItemParent(addressInfo);
                    DeleteLocalDatasChildItem(addressInfo);
                    RemoveNewNode(addressInfo, AddressInfoCollection, newAddressInfo);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void DeleteLocalDatasChildItem(AddressInfoModel addressInfo)
        {
            _localWsCfgSrv.AddressManagementDatas.Delete(t => t.AddressId == addressInfo.AddressId);
            if (addressInfo.ChlidList.Count>0)
            {
                foreach (var item in addressInfo.ChlidList)
                {
                    DeleteLocalDatasChildItem(item);
                }
            }
        }

        private void settingAddressInfoVModel_CloseWindow()
        {
            settingAddressInfoVModel.CloseWindow -= settingAddressInfoVModel_CloseWindow;
            AddressBtnEnable = true;
        }

        private void settingAddressInfoVModel_SettingAddress(string position, string angle,string name, AddressInfoModel addressInfo)
        {
            try
            {
                settingAddressInfoVModel.SettingAddress -= settingAddressInfoVModel_SettingAddress;
                
                UpdateAddressInfo(position, angle, name, addressInfo, AddressInfoCollection);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        /// <summary>
        /// 更新树节点数据
        /// </summary>
        /// <param name="position"></param>
        /// <param name="angle"></param>
        /// <param name="addressInfo"></param>
        /// <param name="AddressInfoList"></param>
        public void UpdateAddressInfo(string position, string angle, string name, AddressInfoModel addressInfo, ObservableCollection<AddressInfoModel> AddressInfoList)
        {
            try
            {
                foreach (var item in AddressInfoList)
                {
                    if (item.AddressId == addressInfo.AddressId)
                    {
                        item.StringPosition = position;
                        item.StringAngle = angle;
                        item.NodeName = name;
                        _localWsCfgSrv.AddressManagementDatas.Update(item);
                        return;
                    }
                    if (item.ChlidList != null)
                    {
                        UpdateAddressInfo(position,angle,name, addressInfo, item.ChlidList);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnDeleteChildNode(AddressInfoModel addressInfoModel)
        {
            try
            {
                if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("AddressManagementTip"), Helpers.ResourceHelper.FindKey("AddressManagementCancelMes")))
                {
                    AddressInfoModel newAddressInfo = FindDeleteAddressItemParent(addressInfoModel);
                    DeleteLocalDatasChildItem(addressInfoModel);
                    RemoveNewNode(addressInfoModel, AddressInfoCollection, newAddressInfo);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        AddressInfoModel DeleteAddressItem = new AddressInfoModel();
        public AddressInfoModel FindDeleteAddressItemParent(AddressInfoModel addressInfo)
        {
            try
            {

                if(!string.IsNullOrEmpty(addressInfo.ParentId))
                {
                    FindDeleteAddressItemParent(addressInfo.AddressParentNode[0]);
                }else
                {
                    DeleteAddressItem = addressInfo;
                    return DeleteAddressItem;
                }
                return DeleteAddressItem;
            }catch(Exception e)
            {
                return null;
            }
        }


        public void RemoveNewNode(AddressInfoModel addressInfoModel, ObservableCollection<AddressInfoModel> AddressInfoList, AddressInfoModel newAddressInfo)
        {
            try
            {
                foreach (var item in AddressInfoList)
                {
                    if (item.AddressId == addressInfoModel.AddressId)
                    {
                        
                        AddressInfoList.Remove(addressInfoModel);
                        _localWsCfgSrv.AddressManagementDatas.Update(newAddressInfo);
                        return;
                    }
                    if (item.ChlidList != null)
                    {
                        RemoveNewNode(addressInfoModel, item.ChlidList,newAddressInfo);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnAddChildNode(AddressInfoModel addressInfoModel)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                AddressInfoModel addressInfo= new AddressInfoModel();
                addressInfo.AddressId = guid.ToString();
                addressInfo.ParentId = addressInfoModel.AddressId;
                addressInfo.AddressLevel = addressInfoModel.AddressLevel + 1;
                addressInfo.NodeName = addressInfoModel.AddressLevel + 1 + Helpers.ResourceHelper.FindKey("AddressManagementLevelNode");
                addressInfo.AddressParentNode.Insert(0,addressInfoModel);
                if (addressInfo.AddressLevel==10)
                {
                    addressInfo.AddBtnVisibility = System.Windows.Visibility.Hidden;
                }
                if(addressInfo.AddressLevel > 10)
                {
                    Messages.ShowMessage("最大级别为10级！");
                }
                AddNewNode(addressInfo, AddressInfoCollection);
                CreateNewNodeInfo(addressInfo, "createnew");
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public void AddNewNode(AddressInfoModel addressInfoModel, ObservableCollection<AddressInfoModel> AddressInfoList)
        {
            try
            {
                foreach (var item in AddressInfoList)
                {
                    if(item.AddressId == addressInfoModel.ParentId)
                    {
                        item.ChlidList.Add(addressInfoModel);
                        _localWsCfgSrv.AddressManagementDatas.Add(addressInfoModel);
                        return;
                    }
                    if(item.ChlidList!=null)
                    {
                        AddNewNode(addressInfoModel, item.ChlidList);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void DropNodeUpdateLevel(AddressInfoModel addressInfo,int level)
        {
            try
            {
                if (addressInfo.ChlidList.Count>0)
                {
                    addressInfo.ChlidList.ForEach(e =>
                    {
                        e.AddressLevel = level + 1;
                        if (e.ChlidList.Count > 0)
                        {
                            DropNodeUpdateLevel(e, e.AddressLevel);
                        }
                        if(e.AddressLevel==10)
                        {
                            e.AddBtnVisibility = System.Windows.Visibility.Hidden;
                        }else
                        {
                            e.AddBtnVisibility = System.Windows.Visibility.Visible;
                        }
                        _localWsCfgSrv.AddressManagementDatas.Update(e);
                    });
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }


        public void DropNode2New(AddressInfoModel sourceItem,AddressInfoModel targetItem, ObservableCollection<AddressInfoModel> AddressInfoList, AddressInfoModel targetItemAddressInfo)
        {
            try
            {
                if (targetItem == null|| string.IsNullOrEmpty(targetItem.AddressId))
                {
                    sourceItem.ParentId = string.Empty;
                    sourceItem.AddressLevel = 1;
                    sourceItem.AddressParentNode = null;
                    DropNodeUpdateLevel(sourceItem, sourceItem.AddressLevel);
                    AddressInfoList.Add(sourceItem);

                    foreach(var updateitem in AddressInfoList)
                    {
                        if(updateitem.AddressId ==sourceItem.AddressId)
                        {
                            updateitem.ParentId = string.Empty;
                            updateitem.AddressLevel = 1;
                            updateitem.AddressParentNode = null;
                            _localWsCfgSrv.AddressManagementDatas.Update(updateitem);
                            return;
                        }
                    }
                    return;
                }
                foreach (var item in AddressInfoList)
                {
                    if (item.AddressId == targetItem.AddressId)
                    {
                        sourceItem.ParentId = item.AddressId;
                        sourceItem.AddressLevel = item.AddressLevel + 1;
                        if(sourceItem.AddressParentNode==null)
                        {
                            sourceItem.AddressParentNode = new ObservableCollection<AddressInfoModel>();
                        }
                        else
                        {
                            sourceItem.AddressParentNode.Clear();
                        }

                        sourceItem.AddressParentNode.Insert(0,item);
                        DropNodeUpdateLevel(sourceItem, sourceItem.AddressLevel);
                        item.ChlidList.Add(sourceItem);
                        _localWsCfgSrv.AddressManagementDatas.Update(sourceItem);
                        _localWsCfgSrv.AddressManagementDatas.Update(item);
                        return;
                    }
                    if (item.ChlidList != null)
                    {
                        DropNode2New(sourceItem, targetItem, item.ChlidList, targetItemAddressInfo);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }


        #endregion
        #region 命令属性
        public ICommand AddFirstLevelCmd { get; set; }
        public ICommand AddChildNodeCmd { get; set; }
        public ICommand DeleteChildNodeCmd { get; set; }
        public ICommand SetNodeInfoCmd { get; set; }
        public ICommand LeftDoubleClickCmd { get; set; }

        public ICommand SearchCommand { get; set; }
        IVector3 Postion;
        IEulerAngle Angle;
        private ILocalWsConfigService _localWsCfgSrv;

        public SettingAddressInfoVModel settingAddressInfoVModel;

        private ObservableCollection<AddressInfoModel> _addressInfoCollection = new ObservableCollection<AddressInfoModel>();
        public ObservableCollection<AddressInfoModel> AddressInfoCollection
        {
            get { return _addressInfoCollection; }
            set
            {
                _addressInfoCollection = value;
                OnPropertyChanged("AddressInfoCollection");
            }
        }


        private ObservableCollection<AddressInfoModel> _tempAddressInfoCollection = new ObservableCollection<AddressInfoModel>();
        public ObservableCollection<AddressInfoModel> TempAddressInfoCollection
        {
            get { return _tempAddressInfoCollection; }
            set
            {
                _tempAddressInfoCollection = value;
                OnPropertyChanged("TempAddressInfoCollection");
            }
        }

        private ObservableCollection<AddressInfoModel> _searchAddressInfoCollection = new ObservableCollection<AddressInfoModel>();
        public ObservableCollection<AddressInfoModel> SearchAddressInfoCollection
        {
            get { return _searchAddressInfoCollection; }
            set
            {
                _searchAddressInfoCollection = value;
                OnPropertyChanged("SearchAddressInfoCollection");
            }
        }

        private AddressInfoModel _selectedItem = new AddressInfoModel();
        public AddressInfoModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private bool _addressBtnEnable = true;
        public bool AddressBtnEnable
        {
            get { return _addressBtnEnable; }
            set
            {
                _addressBtnEnable = value;
                OnPropertyChanged("AddressBtnEnable");
            }
        }
        private string _searchAddressText;
        public string SearchAddressText
        {
            get { return _searchAddressText; }
            set
            {
                _searchAddressText = value;
                OnPropertyChanged("SearchAddressText");
            }
        }

        
        #endregion
    }
}
