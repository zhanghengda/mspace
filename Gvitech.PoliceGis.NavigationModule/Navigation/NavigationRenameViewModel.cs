using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.NavigationModule.Navigation
{
    public class NavigationRenameViewModel: BaseViewModel,IDisposable
    {
        
        public NavigationRenameViewModel()
        {
            this.DisposeCmd = new RelayCommand(OnCloseView);
            this.CancelRenameCmd = new RelayCommand(OnCancelRename);
            this.ConfirmRenameCmd = new RelayCommand(OnConfirmRename);
        }

        #region 方法
        /// <summary>
        /// 确认重命名并关闭窗口
        /// </summary>
        private void OnConfirmRename()
        {
            try
            {
                var model= NavigationCollection.FirstOrDefault(t => t.CameraTour.NodeName == NavigationNewName && t.CameraTour.CameraTourID !=NavigationCameraTourID);
                if(model!=null)
                {
                    Messages.ShowMessage("该名称已存在！");
                }
                else
                {
                    if(NavigationRename!=null)
                    {
                        NavigationRename(NavigationNewName, NavigationCameraTourID);
                    }
                    CloseView();
                }
                return;
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }  
        }

        /// <summary>
        /// 取消重命名并关闭窗口
        /// </summary>
        private void OnCancelRename()
        {
            try
            {
                CloseView();
                if (IsNewNavigation)
                {
                    if (!Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("DeleteNavigation"), Helpers.ResourceHelper.FindKey("ConfirmDeleteNavigation") + " " + NavigationNewName + "?"))
                    {
                        ShowView(NavigationCollection, NavigationOldName, NavigationCameraTourID, true); return;
                    }
                    NavigationRename("cancelCreate" + NavigationCameraTourID, NavigationCameraTourID);
                }
            }catch(Exception e)
            {
                SystemLog.Log(e);
            }
            
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        private void OnCloseView()
        {
            OnCancelRename();
        }

        /// <summary>
        /// 弹出重命名窗口
        /// </summary>
        public void ShowView(ObservableCollection<CameraTourWrapper> navigationCollection, string oldName,string cameraTourID,bool isNew)
        {
            try
            {
                this.NavigationOldName = oldName;
                this.NavigationNewName = oldName;
                this.IsNewNavigation = isNew;
                this.NavigationCameraTourID = cameraTourID;
                this.NavigationCollection = navigationCollection;
                if (navigationRenameView == null)
                {
                    navigationRenameView = new NavigationRenameView();
                }
                navigationRenameView.DataContext = this;
                navigationRenameView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                navigationRenameView.Owner = Application.Current.MainWindow;
                navigationRenameView.ShowDialog();
            }catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 关闭重命名窗口
        /// </summary>
        public void CloseView()
        {
            try
            {
                if (navigationRenameView != null)
                {
                    navigationRenameView.Hide();
                }
            }catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        #endregion

        #region 命令及属性

        public Action<string,string> NavigationRename;
        [XmlIgnore]
        public ICommand DisposeCmd { get; set; }
        [XmlIgnore]
        public ICommand CancelRenameCmd { get; set; }
        [XmlIgnore]
        public ICommand ConfirmRenameCmd { get; set; }

        private NavigationRenameView navigationRenameView;

        private string _navigationOldName;
        public string NavigationOldName
        {
            get { return _navigationOldName; }
            set { _navigationOldName = value; OnPropertyChanged("NavigationOldName"); }
        }

        private string _navigationNewName;
        public string NavigationNewName
        {
            get { return _navigationNewName; }
            set { _navigationNewName = value; OnPropertyChanged("NavigationNewName"); }
        }

        private string _navigationCameraTourID;
        public string NavigationCameraTourID
        {
            get { return _navigationCameraTourID; }
            set { _navigationCameraTourID = value; OnPropertyChanged("NavigationCameraTourID"); }
        }

        private bool _isNewNavigation;
        public bool IsNewNavigation
        {
            get { return _isNewNavigation; }
            set { _isNewNavigation = value; OnPropertyChanged("IsNewNavigation"); }
        }

        private ObservableCollection<CameraTourWrapper> _navigationCollection = new ObservableCollection<CameraTourWrapper>();
        public ObservableCollection<CameraTourWrapper> NavigationCollection
        {
            get { return _navigationCollection; }
            set { _navigationCollection = value; OnPropertyChanged("NavigationCollection"); }
        }
        #endregion

    }
}
