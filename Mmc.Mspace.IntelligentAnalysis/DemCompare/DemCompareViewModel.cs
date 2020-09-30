using ApplicationConfig;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Mspace.ToolModule;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.IntelligentAnalysisModule.DemCompare
{
    public class DemCompareViewModel : CheckedToolItemModel
    {
        private ImportImgView importview1;
        private ImportImgView importview2;
        private AnalysisView analyseView;
        private DsmSubStarct imgCharactAnalys;
        private ExportProgressView progressView;
        private IRenderLayer imgRender1;
        private IRenderLayer imgRender2;
        private IRenderLayer resultImgRender;

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            progressView = new ExportProgressView();
            imgRender1 = new RenderLayer();
            imgRender2 = new RenderLayer();
            resultImgRender = new RenderLayer();
            imgCharactAnalys = new DsmSubStarct();
            ServiceManager.GetService<IShellService>(null).OnShellLocationChanged += new Action<double[]>(OnShellLocationChanged);
        }
        public override void OnChecked()
        {
            try
            {
                base.OnChecked();

                ShowImgAnalyseView();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        public override void OnUnchecked()
        {
            try
            {
                base.OnUnchecked();
                if (importview1 != null)
                {
                    importview1.Close();
                    importview1 = null;
                }

                if (importview2 != null)
                {
                    importview2.Close();
                    importview2 = null;
                }

                if (analyseView != null)
                {
                    analyseView.Close();
                    analyseView = null;
                }

            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
            finally
            {
                HideView();
                GviMap.AxMapControl.InteractMode = gviInteractMode.gviInteractNormal;
            }
        }

        private void ShowImgAnalyseView()
        {
            ShowView();
            var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;

            Messenger.Messengers.Notify(CommonContract.MessengerKey.Openscreen.ToString(), "3");
            Messenger.Messengers.Notify(CommonContract.RenderLayerStatus.SaveStatus.ToString());
            Messenger.Messengers.Notify(CommonContract.RenderLayerStatus.AllHide.ToString());
            Messenger.Messengers.Notify("ShowHiddenMenu", true);
            if (importview1 == null)
            {
                importview1 = new ImportImgView();
                importview1.WindowStartupLocation = WindowStartupLocation.Manual;
                importview1.Owner = Application.Current.MainWindow;
                importview1.Width = mapView.Width / 3;
                importview1.Top = mapView.Top+48;                
                importview1.Left = mapView.Left;
                importview1.importDataBtnOnClick += new Action(ImportImgAsync);
            }
            importview1.Show();
            var width = mapView.Width / 3;

            if (importview2 == null)
            {
                importview2 = new ImportImgView();
                importview2.Owner = Application.Current.MainWindow;
                importview2.WindowStartupLocation = WindowStartupLocation.Manual;
                importview2.Width = mapView.Width / 3;
                importview2.Top = mapView.Top+48;                
                importview2.Left = width;
                importview2.importDataBtnOnClick += new Action(ImportImg2Async);
            }
            importview2.Show();

            if (analyseView == null)
            {
                analyseView = new AnalysisView();
                analyseView.Owner = Application.Current.MainWindow;
                analyseView.WindowStartupLocation = WindowStartupLocation.Manual;
                analyseView.Width = mapView.Width / 3;                
                analyseView.Top = mapView.Top+48;
                analyseView.Left =width * 2;
                analyseView.analysisBtnOnClick += new Action(analyseAction);
            }
            analyseView.Show();
        }

        private void ShowView()
        {
            ServiceManager.GetService<IShellService>(null).IsCompareView = true;
            //其他组建隐藏
            var shellView = ServiceManager.GetService<IShellService>(null).ShellWindow;

            var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;
            mapView.Width = shellView.Width;
            mapView.Left = shellView.Left;
            mapView.UpdateLayout();

        }

        public void ImportImgAsync()
        {
            imgCharactAnalys.FileFilter = FileFilterStrings.TIF;
            var fileName = imgCharactAnalys.GetOpenFile();
            imgCharactAnalys.ImputFile1 = fileName;
            if (string.IsNullOrEmpty(fileName))
                return;

            try
            {

                if (!string.IsNullOrEmpty(imgRender1.Guid))
                {
                    Messenger.Messengers.Notify(CommonContract.MessengerKey.DeleteRederLayer.ToString(), imgRender1.Guid);
                }

                Task.Run(() =>
                {
                    BeginLoadDsProcess();

                    var name = Path.GetFileNameWithoutExtension(fileName);
                    ImageLayerConfig layerConfig = new ImageLayerConfig()
                    {
                        AliasName = name,
                        ConnInfoString = fileName,
                        AlphaEnabled = "false",
                        ConType = "File",
                    };
                    OperateDataStatus status = OperateDataStatus.LOADFAILED;
                    imgRender1 = DataBaseService.Instance.OpenImageLayer(layerConfig, out status);

                    if (imgRender1 != null)
                    {
                        imgRender1.Renderable.VisibleMask = gviViewportMask.gviView0;
                        Messenger.Messengers.Notify(CommonContract.MessengerKey.FlyToRederLayer.ToString(), imgRender1);
                    }
                    FinishLoadProcess();
                });
            }
            catch
            {
                FinishLoadProcess();
            }
        }

        public void ImportImg2Async()
        {
            imgCharactAnalys.FileFilter = FileFilterStrings.TIF;
            var fileName = imgCharactAnalys.GetOpenFile();
            imgCharactAnalys.ImputFile2 = fileName;
            if (string.IsNullOrEmpty(fileName))  return;

            try
            {

                if (!string.IsNullOrEmpty(imgRender2.Guid))
                {
                    Messenger.Messengers.Notify(CommonContract.MessengerKey.DeleteRederLayer.ToString(), imgRender2.Guid);
                }

                Task.Run(() =>
                {
                    BeginLoadDsProcess();

                    var name = Path.GetFileNameWithoutExtension(fileName);
                    ImageLayerConfig layerConfig = new ImageLayerConfig()
                    {
                        AliasName = name,
                        ConnInfoString = fileName,
                        AlphaEnabled = "false",
                        ConType = "File",
                    };
                    OperateDataStatus status = OperateDataStatus.LOADFAILED;
                    imgRender2 = DataBaseService.Instance.OpenImageLayer(layerConfig, out status);

                    if (imgRender2 != null)
                    {
                        imgRender2.Renderable.VisibleMask = gviViewportMask.gviView1;
                        Messenger.Messengers.Notify(CommonContract.MessengerKey.FlyToRederLayer.ToString(), imgRender2);
                    }
                    FinishLoadProcess();
                });
            }
            catch
            {
                FinishLoadProcess();
            }
        }

        public void analyseAction()
        {
            imgCharactAnalys.FileFilter = FileFilterStrings.TIF;
            var fileName = imgCharactAnalys.GetSaveFile();
            imgCharactAnalys.OutputFile = fileName;
            if (string.IsNullOrEmpty(fileName))
                return;

            try
            {
                if (!string.IsNullOrEmpty(resultImgRender?.Guid))
                {
                    Messenger.Messengers.Notify(CommonContract.MessengerKey.DeleteRederLayer.ToString(), resultImgRender.Guid);
                }

                Task.Run(() =>
                {
                    AnalysingProcess();
                    if (imgCharactAnalys.Analys())
                    {
                        var name = Path.GetFileNameWithoutExtension(fileName);
                        ImageLayerConfig layerConfig = new ImageLayerConfig()
                        {
                            AliasName = name,
                            ConnInfoString = fileName,
                            AlphaEnabled = "false",
                            ConType = "File",
                        };
                        OperateDataStatus status = OperateDataStatus.LOADFAILED;
                        resultImgRender = DataBaseService.Instance.OpenImageLayer(layerConfig, out status);
                        if (resultImgRender != null)
                        {
                            resultImgRender.Renderable.VisibleMask = gviViewportMask.gviView2;
                            Messenger.Messengers.Notify(CommonContract.MessengerKey.FlyToRederLayer.ToString(), resultImgRender);
                        }
                    }
                    FinishLoadProcess();
                });
            }
            catch
            {
                FinishLoadProcess();
            }
        }

        private void HideView()
        {
            try
            {
                var shellView = ServiceManager.GetService<IShellService>(null).ShellWindow;
                //var width = ServiceManager.GetService<IShellService>(null).LeftWindow.Width;
                var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;
                mapView.Width = shellView.Width;
                mapView.Left = shellView.Left;
                mapView.UpdateLayout();
                //退出恢复单屏模式
                GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportSinglePerspective;
                ServiceManager.GetService<IShellService>(null).IsCompareView = false;
                Messenger.Messengers.Notify(CommonContract.RenderLayerStatus.RecoveryRenderStatus.ToString());
                Messenger.Messengers.Notify("ShowHiddenMenu", false);
                if (!string.IsNullOrEmpty(imgRender1?.Guid))
                {
                    Messenger.Messengers.Notify(CommonContract.MessengerKey.DeleteRederLayer.ToString(), imgRender1.Guid);
                }
                if (!string.IsNullOrEmpty(imgRender2?.Guid))
                {
                    Messenger.Messengers.Notify(CommonContract.MessengerKey.DeleteRederLayer.ToString(), imgRender2.Guid);
                }
                if (resultImgRender!=null&&!string.IsNullOrEmpty(resultImgRender.Guid))
                {
                    Messenger.Messengers.Notify(CommonContract.MessengerKey.DeleteRederLayer.ToString(), resultImgRender.Guid);
                }
            }catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnShellLocationChanged(double[] loctionArr)
        {
            if (importview1 != null)
            {
                importview1.Top = loctionArr[0]+48;
                importview1.Left = loctionArr[1];
            }
            if (importview2 != null)
            {
                importview2.Top = loctionArr[0]+48;
                importview2.Left = loctionArr[1] + loctionArr[2] / 3;
            }
            if (analyseView != null)
            {
                analyseView.Top = loctionArr[0]+48;
                analyseView.Left = loctionArr[1] + loctionArr[2] / 3 *2;
            }
        }

        private void BeginLoadDsProcess()
        {
            ServiceManager.GetService<IShellService>(null).ProgressView.Dispatcher.Invoke(() =>
            {
                ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
                this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("StartLoading"));
            });
        }

        private void AnalysingProcess()
        {
            ServiceManager.GetService<IShellService>(null).ProgressView.Dispatcher.Invoke(() =>
            {
                ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
                this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("Analysing"));
            });
        }

        private void FinishLoadProcess()
        {
            ServiceManager.GetService<IShellService>(null).ProgressView.Dispatcher.Invoke(() =>
            {
                this.progressView.ViewModel.ProgressValue = string.Empty;
                ServiceManager.GetService<IShellService>(null).ProgressView.ClearValue(System.Windows.Controls.ContentControl.ContentProperty);
            });
        }
    }
}
