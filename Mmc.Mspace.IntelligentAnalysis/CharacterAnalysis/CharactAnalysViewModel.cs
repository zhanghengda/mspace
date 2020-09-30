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
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.IntelligentAnalysisModule.CharacterAnalysis
{
    public class CharactAnalysViewModel : CheckedToolItemModel
    {
        private ImgCharactAnalys imgCharactAnalys;
        private ImportImgView importImgView;
        private AnalysisView analyseImgView;
        private  ExportProgressView progressView;
        private IRenderLayer imgRender1;
        private IRenderLayer resultImgRender;
        private string _Stream_address;
        public string Stream_address
        {
            get { return this._Stream_address; }
            set { _Stream_address = value; NotifyPropertyChanged("Stream_address"); }
        }
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            imgCharactAnalys = new ImgCharactAnalys();
            progressView = new ExportProgressView();
            imgRender1 = new RenderLayer();
            resultImgRender = new RenderLayer();
            ServiceManager.GetService<IShellService>(null).OnShellLocationChanged += new Action<double[]>(OnShellLocationChanged);
            this.CloseCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                base.IsChecked = false;
            });
            this.OKCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                var view = base.View as CharactAnalysView;
                if (view.rBtnTif.IsChecked == true)//影像
                {
                    ShowImgAnalyseView();                    
                }
                else if (view.rBtnVideo.IsChecked == true)//视频
                {
                    ExitAnalysis();
                    VideoAnalys viewModel = new VideoAnalys();
                    viewModel.OnChecked();
                }
                 else if (view.stream_address.IsChecked == true)//流
                {
                    ExitAnalysis();
                    VideoAnalys viewModel = new VideoAnalys();
                    viewModel.OnChecked2(_Stream_address);
                }
                view.Hide();
            });
        }
        private void OnShellLocationChanged(double [] loctionArr)
        {
            if(importImgView!=null)
            {
                importImgView.Top = loctionArr[0]+48;
                importImgView.Left = loctionArr[1];
            }
            if (analyseImgView != null)
            {
                analyseImgView.Top = loctionArr[0]+48;
                analyseImgView.Left = loctionArr[1] + loctionArr[2] / 2;
            }
        }
        public override void OnChecked()
        {
            try
            {
                base.OnChecked();
                this.WinTitle = Helpers.ResourceHelper.FindKey("Characteristicsanalysis");
                this.AnalysModels = new ObservableCollection<CharacAnalysModel>() {
                    new CharacAnalysModel{ AnalysType= CharacAnalysType.BlueRoot,Name=Helpers.ResourceHelper.FindKey("Blueceiling")},
                     //new CharacAnalysModel{ AnalysType= CharacAnalysType.insulator,Name=Helpers.ResourceHelper.FindKey("Insulator")}
                };
                this.SelectedAnalysModel = this.AnalysModels[0];
                //显示窗体
                var view = (Window)base.View;
                view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                view.Show();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

        }

        private void ShowImgAnalyseView()
        {
            ShowView();
             var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;
            Messenger.Messengers.Notify(CommonContract.MessengerKey.Openscreen.ToString(), "2");
            Messenger.Messengers.Notify(CommonContract.RenderLayerStatus.SaveStatus.ToString());
            Messenger.Messengers.Notify(CommonContract.RenderLayerStatus.AllHide.ToString());
            if (importImgView == null)
            {
                importImgView = new ImportImgView();
                importImgView.Owner = Application.Current.MainWindow;
                importImgView.Width =mapView.Width/2;
                importImgView.WindowStartupLocation = WindowStartupLocation.Manual;
                importImgView.Top = mapView.Top+48;
                importImgView.Left = mapView.Left;
                
                importImgView.importDataBtnOnClick += new Action(ImportImgAsync);
            }
            importImgView.Show();

            if (analyseImgView == null)
            {
                analyseImgView = new AnalysisView();
                analyseImgView.Owner = Application.Current.MainWindow;
                analyseImgView.WindowStartupLocation = WindowStartupLocation.Manual;
                analyseImgView.Width = mapView.Width / 2;
                analyseImgView.Top = mapView.Top+48;
                //analyseImgView.btnImg.Content = "分析";
                analyseImgView.Left = mapView.Left+ mapView.Width / 2;
                analyseImgView.analysisBtnOnClick += new Action(AnalyseImgAsync);
            }
            analyseImgView.Show();
        }

        public void ImportImgAsync()
        {

            imgCharactAnalys.PythonProcess.FileFilter = FileFilterStrings.TIF;
            var fileName = imgCharactAnalys.PythonProcess.GetOpenFile();
            if (string.IsNullOrEmpty(fileName))  return;

            imgCharactAnalys.PythonProcess.ImputFile = fileName.Replace(@"\", "/");

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
                    imgRender1 = DataBaseService.Instance.OpenImageLayer(layerConfig,out status);
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


        public void AnalyseImgAsync()
        {
            imgCharactAnalys.PythonProcess.FileFilter = FileFilterStrings.TIF;
            var fileName = imgCharactAnalys.PythonProcess.GetSaveFile();
            imgCharactAnalys.PythonProcess.OutputFile = fileName.Replace(@"\", "/");
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
                            resultImgRender.Renderable.VisibleMask = gviViewportMask.gviView1;
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

        private void AxMapControl_RcUIWindowEvent(IUIEventArgs EventArgs, gviUIEventType EventType)
        {
            throw new NotImplementedException();
        }


        public override void OnUnchecked()
        {
            try
            {
                base.OnUnchecked();
                ((Window)base.View).Hide();
                GviMap.AxMapControl.InteractMode = gviInteractMode.gviInteractNormal;
                //if (isImageAnalyse)
                //{
                //    ExitAnalysis();
                //}
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
            finally
            {
                ExitAnalysis();
            }
        }

        private void ExitAnalysis()
        {
            RecoverSingleView();
            CloseImportViews();
        }

        private void CloseImportViews()
        {
            if (importImgView != null) { importImgView.Close(); importImgView = null; }
            if (analyseImgView != null) { analyseImgView.Close(); analyseImgView = null; }
        }

        public String WinTitle { get; set; }
        public override FrameworkElement CreatedView()
        {
            return new CharactAnalysView()
            {
                Owner = Application.Current.MainWindow
            };
        }

        private ObservableCollection<CharacAnalysModel> _analysModel;

        [XmlIgnore]
        public ObservableCollection<CharacAnalysModel> AnalysModels
        {
            get { return this._analysModel; }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<CharacAnalysModel>>(ref this._analysModel, value, "AnalysModels");
            }
        }

        private CharacAnalysModel _selectedAnalysModel;
        [XmlIgnore]
        public CharacAnalysModel SelectedAnalysModel
        {
            get { return this._selectedAnalysModel; }
            set
            {
                base.SetAndNotifyPropertyChanged<CharacAnalysModel>(ref this._selectedAnalysModel, value, "SelectedAnalysModel");
            }
        }

        [XmlIgnore]
        public ICommand OKCmd { get; set; }

        [XmlIgnore]
        public ICommand CloseCmd { get; set; }


        private void ShowView()
        {
            ServiceManager.GetService<IShellService>(null).IsCompareView = true;
            //其他组建隐藏
            var shellView = ServiceManager.GetService<IShellService>(null).ShellWindow;

            var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;
            mapView.Width = shellView.Width;
            mapView.Left = shellView.Left ;
            mapView.UpdateLayout();

        }

        private void RecoverSingleView()
        {
            var shellView = ServiceManager.GetService<IShellService>(null).ShellWindow;
            var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;
            mapView.Width = shellView.Width;
            mapView.Left = shellView.Left;
            mapView.UpdateLayout();
            //退出恢复单屏模式
            GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportSinglePerspective;
            ServiceManager.GetService<IShellService>(null).IsCompareView = false;

            Messenger.Messengers.Notify(CommonContract.RenderLayerStatus.RecoveryRenderStatus.ToString());
            
            // 清除数据
            if (!string.IsNullOrEmpty(imgRender1?.Guid))
            {
                Messenger.Messengers.Notify(CommonContract.MessengerKey.DeleteRederLayer.ToString(), imgRender1.Guid);
            }
            if (!string.IsNullOrEmpty(resultImgRender?.Guid))
            {
                Messenger.Messengers.Notify(CommonContract.MessengerKey.DeleteRederLayer.ToString(), resultImgRender.Guid);
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
                this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("ImageAnalysing"));
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
