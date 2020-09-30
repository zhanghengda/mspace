using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Serialization;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Models.Navigation;
//using Gvitech.AppPd.UrbanPlan.DAL;
using Mmc.Mspace.NavigationModule.Core;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Application = System.Windows.Application;
using DragEventArgs = System.Windows.DragEventArgs;

namespace Mmc.Mspace.NavigationModule.Navigation
{
    public class NavigationViewModel : ScenariosViewModelBase,IDisposable
    {
        //[DllImport(@"F:\mmcode\Mspace1\binPath\GenerateMiniDump.dll")]
        //extern static int add();
        [DllImport(@"G:\Exceptiondll\GenerateMiniDump\x64\Release\GenerateMiniDump.dll", EntryPoint = "add")]

        public static extern int add();
        bool flag = false;
        bool flag2 = false;
        private ICameraTour camerat;
        public bool GotMouseCapture;
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (Mmc.Mspace.Common.Models.ViewType)1;
            _localWsCfgSrv = ServiceManager.GetService<ILocalWsConfigService>(null);

            //List <CameraTourData> cameraTours = ServiceManager.GetService<IDataBaseService>(null).GetCameraTours();
            //ObservableCollection<CameraTourWrapper> observableCollection = new ObservableCollection<CameraTourWrapper>();
            //bool flag = !IEnumerableExtension.HasValues<CameraTour>(cameraTours);
            //if (!flag)
            //{
            //    foreach (CameraTour cameraTour in cameraTours)
            //    {
            //        observableCollection.Add(new CameraTourWrapper
            //        {
            //            CameraTour = cameraTour
            //        });
            //    }
            //    this.Parameter = observableCollection;
            //}

            this.PlayCmd = new RelayCommand<bool>((IsChecked) => OnPlayAnimationNavigation(IsChecked));
            this.StopCmd = new RelayCommand(OnStopAnimationNavigation);
            this.EndEditCmd = new RelayCommand(OnEndEdit);
            this.AddAnimationPointCmd = new RelayCommand(OnAddAnimationPoint);
            this.AddAnimationNavigationCmd = new RelayCommand(OnAddAnimationNavigation);
            this.RenameAnimationNavigationCmd = new RelayCommand(OnRenameAnimationNavigation);
            this.RemoveAnimationNavigationCmd = new RelayCommand(OnRemoveAnimationNavigation);

            this.ImportNavigationCmd = new RelayCommand(OnImportNavigation);
            this.ExportNavigationCmd = new RelayCommand(OnExportNavigation);
            this.LeftDoubleClickCmd = new RelayCommand<CameraTourWrapper>((cameraTourItem) =>OnLeftDoubleClick(cameraTourItem));
            GviMap.AxMapControl.RcCameraFlyFinished += new _IRenderControlEvents_RcCameraFlyFinishedEventHandler(OnRcCameraFlyFinished);
            
        }


        private void DumpTest()
        {
            string temp = null;
            string temp2 = temp.ToString();
        }
        public NavigationViewModel()
        {
            
            RestoreNavigationData();
        }

        #region 方法


        /// <summary>
        /// 导出动画导航
        /// </summary>
        private void OnExportNavigation()
        {
            if(File.Exists(@"G:\Exceptiondll\GenerateMiniDump\x64\Release\GenerateMiniDump.dll"))
            {
                var aa = add();
            }
            
            try
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.AddExtension = true;
                sd.DefaultExt = "xml";
                sd.Filter = "XML文件|*.xml;|video(*.avi)|*.avi";
                if (SelectedItem != null)
                {
                    sd.FileName = SelectedItem.CameraTour.NodeName;
                    if (sd.ShowDialog() == DialogResult.OK)
                    {
                    string xmlFinal = sd.FileName;
                    string videoFinal = sd.FileName;
                        if (sd.FileName.LastIndexOf(".xml") == -1)
                        xmlFinal = String.Format("{0}.xml", sd.FileName);
                    else if (sd.FileName.LastIndexOf(".avi") == -1)
                            videoFinal = String.Format("{0}.avi", sd.FileName);
                        if (sd.FilterIndex == 1)
                        {
                            ConfigHelper<CameraTourData>.SaveXml(xmlFinal, ConvertToCameraTourData(SelectedItem.CameraTour));
                            Messages.ShowMessage("动画导出成功");
                        }
                        else if (sd.FilterIndex == 2)
                        {
                            SelectedItem.SaveAsVideoCmd.Execute(videoFinal);
                        }
                       
                    }
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 导入动画导航
        /// </summary>
        private void OnImportNavigation()
        {
            try
            {
                OpenFileDialog od = new OpenFileDialog();
                od.Filter = "XML文件|*.xml";
                if (od.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(od.FileName))
                    {
                        CameraTourData cameraTourData = ConfigHelper<CameraTourData>.ResovleConfigFromFile(od.FileName);
                        var model=NavigationCollection.FirstOrDefault(t => t.CameraTour.CameraTourID == cameraTourData.CameraTourID);
                        if (model==null)
                        {
                            cameraTourData.RemoveAnimationNavigationCmd = RemoveAnimationNavigationCmd;
                            cameraTourData.RenameAnimationNavigationCmd = RenameAnimationNavigationCmd;
                            NavigationCollection.Insert(0, new CameraTourWrapper() { CameraTour = cameraTourData });
                            _localWsCfgSrv.CameraTourDatas.Add(cameraTourData);
                            SelectedItem = NavigationCollection[0];
                            var modelName = NavigationCollection.FirstOrDefault(t => t.CameraTour.NodeName == cameraTourData.NodeName&& t.CameraTour.CameraTourID != cameraTourData.CameraTourID);
                            if(modelName!=null)
                            {
                                RenameAnimationNavigation(NavigationCollection, cameraTourData.NodeName, cameraTourData.CameraTourID, true);
                            }
                        }
                        else
                        {
                            
                            
                            NavigationCollection.Remove(model);
                            NavigationCollection.Insert(0, model);
                            SelectedItem = model;
                            Messages.ShowMessage("该动画已存在！");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        /// <summary>
        /// 创建动画导航的每个点
        /// </summary>
        public void OnAddAnimationPoint()
        {
            try
            {
                if(!flag)
                {
                    NavigationImage();
                }
                
                NavigationCount++;
                flag = true;
                GviMap.Camera.GetCamera(out IVector3 Position, out IEulerAngle Angle);
                camerat.AddWaypoint(Position, Angle, 2, gviCameraTourMode.gviCameraTourSmooth);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 结束动画导航创建编辑
        /// </summary>
        private void OnEndEdit()
        {
            try
            {
                if(flag)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        
                        //Thread.Sleep(500);
                         flag = false;
                        Guid guid = Guid.NewGuid();
                        CameraTourData cameraTourData = new CameraTourData()
                        {
                            NodeName = "动画" + NavigationCountIndex.NavigationNameIndexdex++,
                            CameraTourID = guid.ToString(),
                            TourGroupID = guid.ToString(),
                            LocationName = "动画" + NavigationCountIndex.NavigationNameIndexdex,
                            XmlRoute = camerat.AsXml(),
                            XmlRoad = camerat.AsXml(),
                            ImageSource = NavigationImgCompletePath,
                            RemoveAnimationNavigationCmd = RemoveAnimationNavigationCmd,
                            RenameAnimationNavigationCmd = RenameAnimationNavigationCmd
                        };
                        NavigationCount = 0;
                        cameraTourData.ImageSource = NavigationImgCompletePath;
                        NavigationCollection.Insert(0, new CameraTourWrapper() { CameraTour = cameraTourData });
                        _localWsCfgSrv.CameraTourDatas.Add(cameraTourData);
                        IsAddNavigationChecked = false;
                        AddNavigationSetBtnEnable(false);
                        RenameAnimationNavigation(NavigationCollection, cameraTourData.NodeName, cameraTourData.CameraTourID, true);
                        //var model=NavigationCollection.FirstOrDefault(t => t.CameraTour.CameraTourID == cameraTourData.CameraTourID);
                        //if(model!=null)
                        //{
                        //    model.CameraTour.ImageSource = NavigationImgCompletePath;
                        //}
                    });
                        
                    
                   
                }
                else
                {
                    IsAddNavigationChecked = false;
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private CameraTourData ConvertToCameraTourData(CameraTourData item)
        {
            try
            {
                CameraTourData cameraTourData = new CameraTourData();
                cameraTourData.CameraTourID = item.CameraTourID;
                cameraTourData.NodeName = item.NodeName;
                cameraTourData.TourGroupID = item.TourGroupID;
                cameraTourData.LocationName = item.LocationName;
                cameraTourData.XmlRoad = item.XmlRoad;
                cameraTourData.XmlRoute = item.XmlRoute;
                cameraTourData.ImageSource = item.ImageSource;

                return cameraTourData;
            }catch(Exception e)
            {
                SystemLog.Log(e);
                return new CameraTourData();
            }
        }

        /// <summary>
        /// 保存动画导航数据
        /// </summary>
        private void SaveNavigationData()
        {
            try
            {
                NavigationDataCollection1 = NavigationCollection.Select(t => t.CameraTour).ToList();
                foreach (var item in NavigationDataCollection1)
                {
                    NavigationDataCollection.Add(ConvertToCameraTourData(item));
                    
                }
                //JsonUtil.SerializeToFile(NavigationDataPath, NavigationDataCollection);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 取出保存在xml中的动画导航数据
        /// </summary>
        private void RestoreNavigationData()
        {
            try
            {
                NavigationDataCollection=_localWsCfgSrv.CameraTourDatas.FindAll();
                if(NavigationDataCollection!=null)
                {
                    foreach (var item in NavigationDataCollection)
                    {
                        
                        CameraTourData cameraTourData = new CameraTourData();
                        cameraTourData = item;
                        cameraTourData.NavigationOperateVisibility = Visibility.Collapsed;
                        cameraTourData.RemoveAnimationNavigationCmd = RemoveAnimationNavigationCmd;
                        cameraTourData.RenameAnimationNavigationCmd = RenameAnimationNavigationCmd;
                        NavigationCollection.Insert(0, new CameraTourWrapper() { CameraTour = cameraTourData });
                    }
                }
                
                //if(File.Exists(NavigationDataPath))
                //{
                //    NavigationDataCollection = JsonUtil.DeserializeFromFile<List<CameraTourData>>(NavigationDataPath);
                //    foreach (var item in NavigationDataCollection)
                //    {
                //        CameraTourData cameraTourData = new CameraTourData();
                //        cameraTourData = item;
                //        cameraTourData.RemoveAnimationNavigationCmd = RemoveAnimationNavigationCmd;
                //        cameraTourData.RenameAnimationNavigationCmd = RenameAnimationNavigationCmd;
                //        NavigationCollection.Insert(0, new CameraTourWrapper() { CameraTour = cameraTourData });
                //    }
                //}
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 双击播放
        /// </summary>
        /// <param name="cameraTourItem"></param>
        private void OnLeftDoubleClick(CameraTourWrapper cameraTourItem)
        {
            if(GotMouseCapture)
            {
                if (IsAddNavigationChecked)
                {
                    if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("NavigationTip"), Helpers.ResourceHelper.FindKey("NavigationCancelMes") + "?"))
                    {
                        IsAddNavigationChecked = false;
                        AddNavigationSetBtnEnable(false);
                        if (CurrentPlayItem != SelectedItem)
                        {
                            OnStopAnimationNavigation();
                        }
                        CurrentPlayItem = SelectedItem;
                        PlayIsChecked = true;
                        PlayAnimationNavigation(cameraTourItem);
                        return;
                    }
                    PlayIsChecked = false;
                }
                else
                {
                    if(CurrentPlayItem!=SelectedItem)
                    {
                        OnStopAnimationNavigation();
                    }
                    CurrentPlayItem = SelectedItem;
                    PlayIsChecked = true;
                    SelectedItem.PlayCmd.Execute(true);
                }
                GotMouseCapture = false;
                
                AddIsEnable = false;
                
            }
        }

        /// <summary>
        /// 播放/暂停动画导航
        /// </summary>
        /// <param name="isChecked"></param>
        private void OnPlayAnimationNavigation(bool isChecked)
        {
            try
            {
                if (isChecked)
                {
                    if (IsAddNavigationChecked)
                    {
                        if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("NavigationTip"), Helpers.ResourceHelper.FindKey("NavigationCancelMes") + "?"))
                        {
                            IsAddNavigationChecked = false;
                            AddNavigationSetBtnEnable(false);
                            if (CurrentPlayItem != SelectedItem)
                            {
                                OnStopAnimationNavigation();
                            }
                            CurrentPlayItem = SelectedItem;
                            PlayIsChecked = true;
                            SelectedItem.PlayCmd.Execute(true);
                            return;
                        }

                        PlayIsChecked = false;
                    }
                    else
                    {
                        if (CurrentPlayItem != SelectedItem)
                        {
                            OnStopAnimationNavigation();
                        }
                        CurrentPlayItem = SelectedItem;
                        PlayIsChecked = true;
                        SelectedItem.PlayCmd.Execute(true);
                    }

                    GviMap.AxMapControl.Camera.GetCamera(out IVector3 Position, out IEulerAngle Angle);
                    OriginPosition = Position;
                    OriginAngle = Angle;
                    
                    AddIsEnable = false;
                    
                }
                else
                {
                    SelectedItem.PauseCmd.Execute(true);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 双击播放动画导航
        /// </summary>
        /// <param name="SelectedItem"></param>
        private void PlayAnimationNavigation(CameraTourWrapper cameraTourItem)
        {
            try
            {
                PlayIsChecked = true;
                CurrentPlayItem = cameraTourItem;
                cameraTourItem.PlayCmd.Execute(true);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 停止播放动画导航
        /// </summary>
        private void OnStopAnimationNavigation()
        {
            try
            {
                if (NavigationCollection.Count == 0)
                    return;
                foreach (var item in NavigationCollection)
                    item.StopCmd.Execute(null);
                PlayIsChecked = false;
                AddIsEnable = true;
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 动画播放结束事件
        /// </summary>
        /// <param name="Type"></param>
        private void OnRcCameraFlyFinished(byte Type)
        {
            try
            {
                if (Type == 1)
                {
                    PlayIsChecked = false;
                    AddIsEnable = true;
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 添加动画导航
        /// </summary>
        private void OnAddAnimationNavigation()
        {
            try
            {
                OnStopAnimationNavigation();
                if (!IsAddNavigationChecked)
                {
                    if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("NavigationTip"), Helpers.ResourceHelper.FindKey("NavigationCancelMes") + "?"))
                    {
                        AddNavigationSetBtnEnable(false);
                        flag = false;
                        return;
                    }
                }
                IsAddNavigationChecked = true;
                AddNavigationSetBtnEnable(true);

                AddIsEnable = false;
                camerat = GviMap.MapControl.ObjectManager.CreateCameraTour();
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 删除选中项
        /// </summary>
        private void OnRemoveAnimationNavigation()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (!Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("DeleteNavigation"), Helpers.ResourceHelper.FindKey("ConfirmDeleteNavigation")+" "+SelectedItem.CameraTour.NodeName+"?"))
                        return;
                    SelectedItem.StopCmd.Execute(null);
                    string path = SelectedItem.CameraTour.ImageSource;
                    _localWsCfgSrv.CameraTourDatas.Delete(t => t.CameraTourID == SelectedItem.CameraTour.CameraTourID);
                    NavigationCollection.Remove(SelectedItem);
                    
                    SelectedItem = null;
                    Dispose();
                    //DeleteNavigationImage(path);
                });
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        //public bool DeleteNavigationImage(string url)
        //{
        //    try
        //    {
        //        DirectoryInfo di = new DirectoryInfo(NavigationImgPath);
        //        //var aa= di.GetFiles();
        //        FileInfo model = di.GetFiles().FirstOrDefault(t => t.FullName == url);
        //        File.Delete(model.FullName);

        //        return true;
        //    }
        //    catch(Exception e)
        //    {
        //        SystemLog.Log(e);
        //        return false;
        //    }
        //}

        /// <summary>
        /// 重命名
        /// </summary>
        private void OnRenameAnimationNavigation()
        {
            RenameAnimationNavigation(NavigationCollection, SelectedItem.CameraTour.NodeName, SelectedItem.CameraTour.CameraTourID,false);
        }

        private void RenameAnimationNavigation(ObservableCollection<CameraTourWrapper> navigationCollection,string name,string cameraTourID,bool isNew)
        {
            try
            {
                if (navigationRenameViewModel == null)
                {
                    navigationRenameViewModel = new NavigationRenameViewModel();
                }
                NavigationOldName = name;
                navigationRenameViewModel.NavigationRename += OnNavigationRename;
                navigationRenameViewModel.ShowView(navigationCollection, name, cameraTourID, isNew);
                
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 重命名事件
        /// </summary>
        /// <param name="newName"></param>
        private void OnNavigationRename(string newName,string cameraTourID)
        {
            try
            {
                if(newName== ("cancelCreate"+ cameraTourID))
                {
                    var model=NavigationCollection.FirstOrDefault(t => t.CameraTour.CameraTourID == cameraTourID);
                    if(model!=null)
                    {
                        NavigationCollection.Remove(model);
                        _localWsCfgSrv.CameraTourDatas.Delete(t=> t.CameraTourID == cameraTourID);
                    }
                }
                else
                {
                    var model = NavigationCollection.FirstOrDefault(t => t.CameraTour.CameraTourID == cameraTourID);
                    if (model != null)
                    {
                        model.CameraTour.NodeName = newName;
                        _localWsCfgSrv.CameraTourDatas.Update(model.CameraTour);
                    }
                    
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 设置按钮的可用状态
        /// </summary>
        /// <param name="isEnable"></param>
        private void SetBtnEnable(bool isEnable)
        {
            PlayIsEnable = isEnable;
            StopIsEnable = isEnable;
            //IsEndEditEnable = isEnable;
            //IsAddPointEnable = isEnable;
            IsExportEnable = isEnable;
            NavigationCollection.ForEach(t => t.CameraTour.NavigationOperateVisibility = Visibility.Collapsed);
            if (isEnable)
            {
                SelectedItem.CameraTour.NavigationOperateVisibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 添加动画导航后按钮的可用状态
        /// </summary>
        /// <param name="isEnable"></param>
        private void AddNavigationSetBtnEnable(bool isEnable)
        {
            IsAddPointEnable = isEnable;
            IsEndEditEnable = isEnable;
            if(!isEnable)
            {
                NavigationCount = 0;
            }
        }

        /// <summary>
        /// 回到动画导航起点
        /// </summary>
        private void BackToOrigin()
        {
            try
            {
                GviMap.AxMapControl.Camera.FlyTime = 1;
                GviMap.AxMapControl.Camera.SetCamera(OriginPosition, OriginAngle, gviSetCameraFlags.gviSetCameraNoFlags);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 创建动画导航截屏并保存图片
        /// </summary>
        private void NavigationImage()
        {
            NavigationImgCompletePath = NavigationImgPath + GetTimeStamp() + ".png";
            RegisterExportImgEvent();
            bool b = GviMap.MapControl.ExportManager.ExportImage(NavigationImgCompletePath, 120, 120, true);
            if (!b)
            {
                SystemLog.Log(string.Format("CityMaker错误码为：{0}", GviMap.MapControl.GetLastError().ToString()));
                UnRegisterExportImgEvent();
            } 
        }

        private void AxMapControl_RcPictureExportBegin(int NumberOfWidth, int NumberOfHeight)
        {
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            if (Application.Current.Dispatcher.Thread.ManagedThreadId != shell.Dispatcher.Thread.ManagedThreadId)
            {
                shell.Dispatcher.BeginInvoke(this._rcPictureExportBegin);
                return;
            }
            //else
            //{
            //    Application.Current.Dispatcher.Invoke(() =>
            //    {
            //        ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
            //        this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("Compositing"));
            //    });
            //}
        }

        //@param bool IsAborted : true代表取消出图成功；false表示正常出图成功。
        private void AxMapControl_RcPictureExportEnd(double Time, bool IsAborted)
        {
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            if (Application.Current.Dispatcher.Thread.ManagedThreadId != shell.Dispatcher.Thread.ManagedThreadId)
            {
                shell.Dispatcher.BeginInvoke(this._rcPictureExportEnd);
                return;
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    UnRegisterExportImgEvent();
                });
            }
        }

        private void FinishProcess()
        {
            ServiceManager.GetService<IShellService>(null).ProgressView.ClearValue(System.Windows.Controls.ContentControl.ContentProperty);
        }

        private void AxMapControl_RcPictureExporting(int Index, float Percentage)
        {
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            if (Application.Current.Dispatcher.Thread.ManagedThreadId != shell.Dispatcher.Thread.ManagedThreadId)
            {
                shell.Dispatcher.BeginInvoke(this._rcPictureExporting);
                return;
            }
        }

        private void RegisterExportImgEvent()
        {
            UnRegisterExportImgEvent();
            GviMap.AxMapControl.RcPictureExportBegin += new _IRenderControlEvents_RcPictureExportBeginEventHandler(this.AxMapControl_RcPictureExportBegin);
            GviMap.AxMapControl.RcPictureExporting += new _IRenderControlEvents_RcPictureExportingEventHandler(this.AxMapControl_RcPictureExporting);
            GviMap.AxMapControl.RcPictureExportEnd += new _IRenderControlEvents_RcPictureExportEndEventHandler(this.AxMapControl_RcPictureExportEnd);
        }

        private void UnRegisterExportImgEvent()
        {
            GviMap.AxMapControl.RcPictureExportBegin -= new _IRenderControlEvents_RcPictureExportBeginEventHandler(this.AxMapControl_RcPictureExportBegin);
            GviMap.AxMapControl.RcPictureExporting -= new _IRenderControlEvents_RcPictureExportingEventHandler(this.AxMapControl_RcPictureExporting);
            GviMap.AxMapControl.RcPictureExportEnd -= new _IRenderControlEvents_RcPictureExportEndEventHandler(this.AxMapControl_RcPictureExportEnd);
        }

        #endregion

        #region 命令 and 属性

        private NavigationRenameViewModel navigationRenameViewModel;

        private string NavigationDataPath= System.Windows.Forms.Application.LocalUserAppDataPath + "\\NavigationData\\NavigationData.json";
        private string NavigationImgPath = System.Windows.Forms.Application.LocalUserAppDataPath+"\\NavigationImage\\";

        private _IRenderControlEvents_RcPictureExportBeginEventHandler _rcPictureExportBegin;
        private _IRenderControlEvents_RcPictureExportEndEventHandler _rcPictureExportEnd;
        private _IRenderControlEvents_RcPictureExportingEventHandler _rcPictureExporting;
        private ILocalWsConfigService _localWsCfgSrv;


        [XmlIgnore]
        public ICommand PlayCmd { get; set; }
        [XmlIgnore]
        public ICommand PauseCmd { get; set; }
        [XmlIgnore]
        public ICommand StopCmd { get; set; }
        [XmlIgnore]
        public ICommand LeftDoubleClickCmd { get; set; }
        [XmlIgnore]
        public ICommand EndEditCmd { get; set; }
        [XmlIgnore]
        public ICommand AddAnimationPointCmd { get; set; }
        [XmlIgnore]
        public ICommand AddAnimationNavigationCmd { get; set; }
        [XmlIgnore]
        public ICommand RenameAnimationNavigationCmd { get; set; }
        [XmlIgnore]
        public ICommand RemoveAnimationNavigationCmd { get; set; }
        [XmlIgnore]
        public ICommand ImportNavigationCmd { get; set; }
        [XmlIgnore]
        public ICommand ExportNavigationCmd { get; set; }

        private ObservableCollection<CameraTourWrapper> _navigationCollection = new ObservableCollection<CameraTourWrapper>();
        public ObservableCollection<CameraTourWrapper> NavigationCollection
        {
            get { return _navigationCollection; }
            set { _navigationCollection = value;
                NotifyPropertyChanged("NavigationCollection");}
        }
        private List<CameraTourData> _navigationDataCollection = new List<CameraTourData>();
        public List<CameraTourData> NavigationDataCollection
        {
            get { return _navigationDataCollection; }
            set
            {
                _navigationDataCollection = value;
                NotifyPropertyChanged("NavigationDataCollection");
            }
        }

        private List<CameraTourData> _navigationDataCollection1 = new List<CameraTourData>();
        public List<CameraTourData> NavigationDataCollection1
        {
            get { return _navigationDataCollection1; }
            set
            {
                _navigationDataCollection1 = value;
                NotifyPropertyChanged("NavigationDataCollection1");
            }
        }

        private IVector3 _originPosition;
        public IVector3 OriginPosition
        {
            get { return _originPosition; }
            set { _originPosition = value; NotifyPropertyChanged("OriginPosition"); }
        }

        private bool _playIsChecked;
        public bool PlayIsChecked
        {
            get { return _playIsChecked; }
            set { _playIsChecked = value; NotifyPropertyChanged("PlayIsChecked"); }
        }

        private CameraTourWrapper _selectedItem;
        public CameraTourWrapper SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if(_selectedItem==null)
                {
                    SetBtnEnable(false);
                }else
                {
                    SetBtnEnable(true);
                }
                NotifyPropertyChanged("IsSelectedItem");
            }
        }

        private CameraTourWrapper _currentPlayItem;
        public CameraTourWrapper CurrentPlayItem
        {
            get { return _currentPlayItem; }
            set
            {
                _currentPlayItem = value;
                NotifyPropertyChanged("CurrentPlayItem");
            }
        }

        private IEulerAngle _originAngle;
        public IEulerAngle OriginAngle
        {
            get { return _originAngle; }
            set { _originAngle = value; NotifyPropertyChanged("OriginAngle"); }
        }
        
        public override FrameworkElement CreatedBottomView()
        {
            return new NavigationView();
        }

        private bool _isEndEditEnable;
        public bool IsEndEditEnable
        {
            get { return _isEndEditEnable; }
            set { _isEndEditEnable = value; NotifyPropertyChanged("IsEndEditEnable"); }
        }

        private bool _isExportEnable;
        public bool IsExportEnable
        {
            get { return _isExportEnable; }
            set { _isExportEnable = value; NotifyPropertyChanged("IsExportEnable"); }
        }


        private bool _isAddPointEnable;
        public bool IsAddPointEnable
        {
            get { return _isAddPointEnable; }
            set { _isAddPointEnable = value; NotifyPropertyChanged("IsAddPointEnable"); }
        }

        private bool _playIsEnable;
        public bool PlayIsEnable
        {
            get { return _playIsEnable; }
            set { _playIsEnable = value; NotifyPropertyChanged("PlayIsEnable"); }
        }

        private bool _stopIsEnable;
        public bool StopIsEnable
        {
            get { return _stopIsEnable; }
            set { _stopIsEnable = value; NotifyPropertyChanged("StopIsEnable"); }
        }

        private bool _addIsEnable=true;
        public bool AddIsEnable
        {
            get { return _addIsEnable; }
            set { _addIsEnable = value; NotifyPropertyChanged("AddIsEnable"); }
        }

        private bool _isAddNavigationChecked;
        public bool IsAddNavigationChecked
        {
            get { return _isAddNavigationChecked; }
            set
            {
                _isAddNavigationChecked = value;
                
                NotifyPropertyChanged("IsAddNavigationChecked");
            }
        }


        private string _navigationOldName;
        public string NavigationOldName
        {
            get { return _navigationOldName; }
            set { _navigationOldName = value; NotifyPropertyChanged("NavigationOldName"); }
        }

        private string _navigationImgCompletePath;
        public string NavigationImgCompletePath
        {
            get { return _navigationImgCompletePath; }
            set { _navigationImgCompletePath = value; NotifyPropertyChanged("NavigationImgCompletePath"); }
        }

        private  int _navigationCount;
        public  int NavigationCount
        {
            get { return _navigationCount; }
            set { _navigationCount = value; NotifyPropertyChanged("NavigationCount"); }
        }
        

        #endregion

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            Messenger.Messengers.Notify("BottomMenuEnumNavigation", IsChecked);

            if (NavigationCollection.Count == 0)
                return;

            foreach (var item in NavigationCollection)
                item.StopCmd.Execute(null);
        }

        public override void OnChecked()
        {
            Messenger.Messengers.Notify("BottomMenuEnumNavigation", IsChecked);
            base.OnChecked();
        }

        public void Dispose()
        {
            
        }
    }
}
