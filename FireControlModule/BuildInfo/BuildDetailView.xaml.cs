using FireControlModule.BuildInfo;
using FireControlModule.InsideBuild;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Utils;
using System;
using System.Windows;
using System.Windows.Input;

namespace FireControlModule
{
    /// <summary>
    /// BuildDetailView.xaml 的交互逻辑
    /// </summary>
    public partial class BuildDetailView : Window
    {
        public BuildDetailView()
        {
            InitializeComponent();
            //  this.StateChanged += BuildDetailView_StateChanged;
            this.IsVisibleChanged += BuildDetailView_IsVisibleChanged;
            this.Activated += BuildDetailView_Activated;
        }

        private void BuildDetailView_Activated(object sender, EventArgs e)
        {
            BuildDetailView_StateChanged(null, null);
        }

        private void BuildDetailView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            BuildDetailView_StateChanged(null, null);
        }

        private void BuildDetailView_StateChanged(object sender, EventArgs e)
        {
            var viewMode = (BuildDetailViewModel)this.DataContext;
            if (archivesViewModel != null)
            {
                archivesViewModel.BuildCode = viewMode.BuildCode;
                insideBuildViewModel.BuildCode = viewMode.BuildCode;
                unitListViewModel.BuildCode = viewMode.BuildCode;

                this.insideBuildBtn.DataContext = insideBuildViewModel;
                this.buildDetialBtn.DataContext = archivesViewModel;
                this.unitDetialBtn.DataContext = unitListViewModel;
            }
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                base.DragMove();
            }
            catch (Exception)
            {
            }
        }

        private ArchivesViewModel archivesViewModel;
        private InsideBuildViewModel insideBuildViewModel;
        private UnitListViewModel unitListViewModel;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var viewMode = (BuildDetailViewModel)this.DataContext;
            archivesViewModel = new ArchivesViewModel()
            {
                Icon = "BuildInfo/档.png",
                BuildCode = viewMode.BuildCode,
            };

            insideBuildViewModel = new InsideBuildViewModel()
            {
                Icon = "BuildInfo/网格.png",
                BuildCode = viewMode.BuildCode,
            };

            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = json1.threeUnitUrl;
            unitListViewModel = new UnitListViewModel()
            {
                Icon = "BuildInfo/单位.png",
                BuildCode = viewMode.BuildCode,
                TitleName = "单位列表",
                RequestUrl = url,
            };
            this.insideBuildBtn.DataContext = insideBuildViewModel;
            this.buildDetialBtn.DataContext = archivesViewModel;
            this.unitDetialBtn.DataContext = unitListViewModel;
            //this.peopleDetialBtn.DataContext = new ToolItemModel()
            //{
            //    Icon = "BuildInfo/信息.png",
            //    MouseOverIcon = "BuildInfo/信息.png"
            // // Command = new AppClosedCmd()
            // ,
            //};
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }
    }
}