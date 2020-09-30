using FireControlModule.BuildInfo;
using FireControlModule.UnitInfo;
using System;
using System.Windows;
using System.Windows.Input;

namespace FireControlModule
{
    /// <summary>
    /// UnitDetailView.xaml 的交互逻辑
    /// </summary>
    public partial class UnitDetailView : Window
    {
        public UnitDetailView()
        {
            InitializeComponent();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var viewMode = (UnitDetailViewModel)this.DataContext;
            this.buildDetialBtn.DataContext = new ArchivesViewModel()
            {
                Icon = "BuildInfo/档.png",
                BuildCode = viewMode.BuildCode,
            };

            //var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            //string url = json.threeUnitUrl;

            //url = string.Format("{0}{1}", json.unitBaseInfo, viewMode.UnitId);
            //this.UnitBaseInfo= new UnitListViewModel()
            //{
            //    Icon = "BuildInfo/档.png",
            //    UnitId = viewMode.UnitId,
            //    RequestUrl = url,
            //};
            //this.unitBaseBtn.DataContext = UnitBaseInfo;

            //url = string.Format("{0}{1}", json.unitProblemInfo, viewMode.UnitId);
            //this.UnitProblem = new UnitListViewModel()
            //{
            //    Icon = "BuildInfo/档.png",
            //    UnitId = viewMode.UnitId,
            //    RequestUrl = url,
            //};
            //this.UnitProblemBtn.DataContext = UnitProblem;
        }
    }
}