
using Mmc.Mspace.Models.AddressManagementModel;
using Mmc.Mspace.PoiManagerModule.ViewModels;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mmc.Mspace.PoiManagerModule.Views
{
    /// <summary>
    /// AddressManagementView.xaml 的交互逻辑
    /// </summary>
    public partial class AddressManagementView : UserControl
    {
        public AddressManagementVModel addressManagementVModel;
        AddressInfoModel draggedItem, _target;
        public AddressManagementView()
        {
            InitializeComponent();
            addressManagementVModel = this.DataContext as AddressManagementVModel;
        }

        private void TreeViewItem_DragOver(object sender, DragEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
             {
                 this.Dispatcher.Invoke((Action)(() =>
                 {
                     
                 }));
             };
        }

        private void TreeViewItem_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.OriginalSource.ToString() == "System.Windows.Controls.Border")
                {

                    e.Effects = DragDropEffects.None;
                    e.Handled = true;
                    TreeViewItem TargetItem = GetNearestContainer(e.OriginalSource as UIElement);
                    if (draggedItem != null)
                    {
                        if (TargetItem == null)
                        {
                            _target = new AddressInfoModel();
                        }
                        else
                        {
                            _target = TargetItem.DataContext as AddressInfoModel;
                        }
                        e.Effects = DragDropEffects.Move;
                    }
                }


            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        private TreeViewItem GetNearestContainer(UIElement element)
        {
            // Walk up the element tree to the nearest tree view item.
            TreeViewItem container = element as TreeViewItem;
            while ((container == null) && (element != null))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as TreeViewItem;
            }
            return container;
        }

        private void TreeViewItem_MouseMove(object sender, MouseEventArgs e)
        {
            try
            { 
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    addressManagementVModel = this.DataContext as AddressManagementVModel;
                    if (addressManagementVModel != null)
                    {
                        DataObject dataObject = new DataObject(addressManagementVModel.SelectedItem);
                        draggedItem = addressManagementVModel.SelectedItem;
                        DragDropEffects finalDropEffect =DragDrop.DoDragDrop(departmentTree, dataObject, DragDropEffects.Move);
                        if (finalDropEffect == DragDropEffects.Move)
                        {
                            if (draggedItem!=_target)
                            {
                                CopyItem(draggedItem, _target);
                                _target = null;
                                draggedItem = null;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        bool flag = false;
        private bool IsSameTree(AddressInfoModel _sourceItem, AddressInfoModel _targetItem)
        {
            try
            {
                if(_sourceItem.ChlidList.Count>0)
                {
                    foreach (var item in _sourceItem.ChlidList)
                    {
                        if(item.AddressId == _targetItem.AddressId)
                        {
                            flag = true;
                            return flag;
                        }
                        else if(item.ChlidList.Count > 0&& !flag)
                        {
                            IsSameTree(item, _targetItem);
                        }
                    }
                }
                return flag;
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
                return false;
            }
        }

        
        private int SearchTreeSon(AddressInfoModel addressItem)
        {
            int depthcount = 0;
            if (addressItem.ChlidList.Count>0)
            {
                depthcount++;
                foreach (var item in addressItem.ChlidList)
                {
                    if(SearchTreeSon(item)>depthcount)
                    {
                        depthcount = SearchTreeSon(item);
                    }
                }
                return depthcount + 1;
            }
            else
            {
                return 0;
            }
        }

        private AddressInfoModel DropNodeUpdateSourceLevel(AddressInfoModel addressInfo, int level,int sourceCount)
        {
            try
            {
                if(addressInfo.AddressLevel - level + 1 == sourceCount)
                {
                    addressInfo.AddBtnVisibility = Visibility.Hidden;
                    
                }
                if (addressInfo.ChlidList.Count > 0)
                {
                    addressInfo.ChlidList.ForEach(e =>
                    {
                        e.AddressLevel = e.AddressLevel - level + 1;
                        if(e.AddressLevel== sourceCount)
                        {
                            e.AddBtnVisibility = Visibility.Hidden;
                        }
                        if (e.ChlidList.Count > 0)
                        {
                            DropNodeUpdateSourceLevel(e, level, sourceCount);
                        }
                    });
                }
                return addressInfo;
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
                return addressInfo;
            }
        }

        private void DropNodeUpdateSourceVisibility(AddressInfoModel addressInfo)
        {
            try
            {
                addressInfo.AddBtnVisibility = Visibility.Visible;
                if (addressInfo.ChlidList.Count > 0)
                {
                    addressInfo.ChlidList.ForEach(e =>
                    {
                        e.AddBtnVisibility = Visibility.Visible;
                        if (e.ChlidList.Count > 0)
                        {
                            DropNodeUpdateSourceVisibility(e);
                        }
                    });
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void CopyItem(AddressInfoModel _sourceItem, AddressInfoModel _targetItem)
        {
            if(_sourceItem!=null&& _targetItem!=null)
            {
                try
                {
                    var sourceCount = SearchTreeSon(_sourceItem);
                    if (sourceCount == 0)
                    {
                        sourceCount = 1;
                    }
                    var targetCount = _targetItem?.AddressLevel;
                    var sum = sourceCount + targetCount;
                    if (sum > 10)
                    {
                        Messages.ShowMessage("最大级别为10级！");
                    }
                    else
                    {
                        if (sum == 10)
                        {
                            _sourceItem = DropNodeUpdateSourceLevel(_sourceItem, _sourceItem.AddressLevel, sourceCount);
                            _sourceItem.AddressLevel = 1;
                        }
                        else
                        {
                            DropNodeUpdateSourceVisibility(_sourceItem);
                        }
                        addressManagementVModel = this.DataContext as AddressManagementVModel;
                        flag = false;
                        if (!IsSameTree(_sourceItem, _targetItem))
                        {
                            AddressInfoModel newAddressInfo = addressManagementVModel.FindDeleteAddressItemParent(_sourceItem);
                            AddressInfoModel targetItemAddressInfo = addressManagementVModel.FindDeleteAddressItemParent(_targetItem);
                            addressManagementVModel.RemoveNewNode(_sourceItem, addressManagementVModel.AddressInfoCollection, newAddressInfo);
                            addressManagementVModel.DropNode2New(_sourceItem, _targetItem, addressManagementVModel.AddressInfoCollection, targetItemAddressInfo);
                        }
                    }
                }
                catch (Exception e)
                {
                    SystemLog.Log(e);
                }
            }
           
        }


        private void DepartmentTree_Selected(object sender, RoutedEventArgs e)
        {
            addressManagementVModel = this.DataContext as AddressManagementVModel;
            addressManagementVModel.SelectedItem = (e.OriginalSource as TreeViewItem).DataContext as AddressInfoModel;
        }
    }
}
