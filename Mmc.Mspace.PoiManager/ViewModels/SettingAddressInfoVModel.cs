using Gvitech.CityMaker.Math;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Models.AddressManagementModel;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class SettingAddressInfoVModel: BaseViewModel
    {
        
        public SettingAddressInfoVModel()
        {
            this.ConfirmSettingCmd = new RelayCommand(OnConfirmSetting);
            this.CancelSettingCmd = new RelayCommand(OnCancelSetting);
            this.FlyToPerspectiveCmd = new RelayCommand(OnFlyToPerspective);
            this.DisposeCmd = new RelayCommand(OnDispose);
        }

        #region 方法

        private void OnDispose()
        {
            try
            {
                if(!string.IsNullOrEmpty(FlagString))
                {
                    CancelSettingAddress(FlagString, AddressInfo);
                    CloseView();
                }
                else
                {
                    //if (!Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("AddressManagementTip"), Helpers.ResourceHelper.FindKey("AddressManagementSaveChangeMes")))
                    //{
                        CancelSettingAddress(FlagString, AddressInfo);
                        
                    //}
                    CloseView();
                }
                
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        /// <summary>
        /// 显示原视角
        /// </summary>
        private void OnFlyToPerspective()
        {
            try
            {
                GviMap.Camera.LookAt(Postion,0,Angle);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        private void OnCancelSetting()
        {
            try
            {
                CancelSettingAddress(FlagString, AddressInfo);
                CloseView();
                //if (!string.IsNullOrEmpty(FlagString))
                //{
                //    CancelSettingAddress(FlagString, AddressInfo);
                //    CloseView();
                //}
                //else
                //{

                //        //OnConfirmSetting();

                //    CloseView();
                //}
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnConfirmSetting()
        {
            try
            {
                if (string.IsNullOrEmpty(AddressNodeName))
                {
                    Messages.ShowMessage("请设置地址名称");
                    return;
                }
                if ((Postion == null || Angle == null)&& IsAddressChecked==false)
                {
                    Messages.ShowMessage("请设置视角");
                    return;
                }
                string position = null;
                string angle = null;
                if (IsAddressChecked)
                {
                    GviMap.Camera.GetCamera(out IVector3 Position, out IEulerAngle Angle);
                    position = JsonUtil.SerializeToString(Position);
                    angle = JsonUtil.SerializeToString(Angle);
                }else
                {
                    position = AddressInfo.StringPosition;
                    angle = AddressInfo.StringAngle;
                }
                SettingAddress(position, angle, AddressNodeName, AddressInfo);
                CloseView();
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 弹出重命名窗口
        /// </summary>
        public void ShowView(AddressInfoModel addressInfoModel,string str)
        {
            try
            {
                FlagString = str;
                AddressInfo = addressInfoModel;
                AddressNodeName = addressInfoModel.NodeName;
                if (string.IsNullOrEmpty(addressInfoModel.StringPosition))
                {
                    SettingLabelVisibility = Visibility.Visible;
                    SettingBtnVisibility = Visibility.Collapsed;
                }
                else
                {
                    Postion= JsonUtil.DeserializeFromString<Vector3>(addressInfoModel.StringPosition);
                    Angle = JsonUtil.DeserializeFromString<EulerAngle>(addressInfoModel.StringAngle);

                    SettingLabelVisibility = Visibility.Collapsed;
                    SettingBtnVisibility = Visibility.Visible;
                }
                if (settingAddressInfoView == null)
                {
                    settingAddressInfoView = new SettingAddressInfoView();
                }
                settingAddressInfoView.DataContext = this;
                settingAddressInfoView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                settingAddressInfoView.Owner = Application.Current.MainWindow;
                settingAddressInfoView.Show();
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        /// <summary>
        /// 关闭设置窗口
        /// </summary>
        public void CloseView()
        {
            try
            {

                if (settingAddressInfoView != null)
                {
                    settingAddressInfoView.Hide();
                    CloseWindow();
                    IsAddressChecked = false;
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        #endregion


        #region 命令及属性

        public Action<string, string,string, AddressInfoModel> SettingAddress;
        public Action<string,AddressInfoModel> CancelSettingAddress;
        public Action CloseWindow;

        [XmlIgnore]
        public ICommand ConfirmSettingCmd { get; set; }
        [XmlIgnore]
        public ICommand CancelSettingCmd { get; set; }
        [XmlIgnore]
        public ICommand FlyToPerspectiveCmd { get; set; }
        [XmlIgnore]
        public ICommand DisposeCmd { get; set; }
        

        private SettingAddressInfoView settingAddressInfoView;

        private Visibility _settingLabelVisibility;
        public Visibility SettingLabelVisibility
        {
            get { return _settingLabelVisibility; }
            set { _settingLabelVisibility = value; OnPropertyChanged("SettingLabelVisibility"); }
        }

        private Visibility _settingBtnVisibility;
        public Visibility SettingBtnVisibility
        {
            get { return _settingBtnVisibility; }
            set { _settingBtnVisibility = value; OnPropertyChanged("SettingBtnVisibility"); }
        }

        private IVector3 _postion;
        public IVector3 Postion
        {
            get { return _postion; }
            set { _postion = value; OnPropertyChanged("Postion"); }
        }

        private IEulerAngle _angle;
        public IEulerAngle Angle
        {
            get { return _angle; }
            set { _angle = value; OnPropertyChanged("Angle"); }
        }

        private bool _isAddressChecked;
        public bool IsAddressChecked
        {
            get { return _isAddressChecked; }
            set { _isAddressChecked = value; OnPropertyChanged("IsAddressChecked"); }
        }

        private string _addressNodeName;
        public string AddressNodeName
        {
            get { return _addressNodeName; }
            set { _addressNodeName = value; OnPropertyChanged("AddressNodeName"); }
        }

        private string _flagString;
        public string FlagString
        {
            get { return _flagString; }
            set { _flagString = value; OnPropertyChanged("FlagString"); }
        }

        private AddressInfoModel _addressInfo=new AddressInfoModel();
        public AddressInfoModel AddressInfo
        {
            get { return _addressInfo; }
            set { _addressInfo = value; OnPropertyChanged("AddressInfo"); }
        }
        
        #endregion
    }
}
