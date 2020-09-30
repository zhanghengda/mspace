using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ResourceServices;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Main.Commands;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using MMC.MSpace.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MMC.MSpace.Views
{
    /// <summary>
    /// SysMenuView.xaml 的交互逻辑
    /// </summary>
    public partial class SysMenuView : UserControl
    {
        public SysMenuView()
        {
            InitializeComponent();
            string path = Singleton<ResourceService>.Instance.GetImagePath();
            //var exitBtnData = new CheckedToolItemModel()
            //{
            //    Content = Mmc.Mspace.Common.Cache.CacheData.UserInfo.username,
            //    ViewType = ViewType.CheckedIcon,
            //};
            //exitBtnData.Command = new RelayCommand(() =>
            //{
            //    this.popuView.IsOpen = !this.popuView.IsOpen;
            //});
            var leftview = new SysMenuViewModel()
            {
                UserName = Mmc.Mspace.Common.Cache.CacheData.UserInfo.username    ,
            };
            this.Tool.DataContext = leftview;
            this.TopMenu.DataContext = leftview;
            //this.exitBtn.DataContext = exitBtnData;
            //this.closeBtn.DataContext = new ToolItemModel()
            //{
            //    Icon = "winclose.png",
            //    MouseOverIcon = "winclose.png",
            //    Command = new AppClosedCmd()
            //  ,
            //};

            //this.minBtn.DataContext = new ToolItemModel()
            //{
            //    Icon = "win-min.png",
            //    MouseOverIcon = "win-min.png",
            //    Command = new AppMinimizedCmd()
            // ,
            //};
            //this.sysBtn.DataContext = new ToolItemModel()
            //{
            //    Icon = "setting_resize.png",
            //    MouseOverIcon = "setting_resize.png",
            //    Command=new SystemCommand()
            // ,
            //};


            var leftViewWidth = int.Parse(ConfigurationManager.AppSettings["LeftViewWidth"]);
       
            //this.collapseView.DataContext = new CheckedToolItemModel()
            //{
            //    Icon = "收起.png",
            //    MouseOverIcon = "收起H.png",
            //    ViewType = ViewType.CheckedIcon,
            //    Command =new CollapseViewCommand(leftView),
            //    //MouseOverIcon = "收起H.png",
            //};
            this.Loaded += SysMenuView_Loaded;
            this.MouseEnter += SysMenuView_MouseEnter;
           // this.popuView.MouseLeave += PopuView_MouseLeave;
        }

        private void SysMenuView_Loaded(object sender, RoutedEventArgs e)
        {
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            shell.MouseEnter += Shell_MouseEnter;
        }

        private void Shell_MouseEnter(object sender, MouseEventArgs e)
        {
           // this.popuView.IsOpen = false;
        }

        private void SysMenuView_MouseEnter(object sender, MouseEventArgs e)
        {
           // this.popuView.IsOpen = false;
        }


        private void ExitBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            //this.popuView.IsOpen = true;
        }

        private void PopuView_MouseLeave(object sender, MouseEventArgs e)
        {
            //this.popuView.IsOpen = false;
        }
           

        public class CollapseViewCommand : SimpleCommand
        {

            public CollapseViewCommand(FrameworkElement view)
            {
                this.view = view;
            }
            FrameworkElement view;
            public override void Execute(object parameter)
            {
                bool position = parameter.ParseTo<bool>();
                if (position)
                    this.view.Visibility = Visibility.Visible;
                else
                    this.view.Visibility = Visibility.Hidden;

            }
        }

        public class SystemCommand : SimpleCommand
        {
            public override void Execute(object parameter)
            {
                var view =new  SettingView();
                view.ShowDialog();
            }
        }
    }

}
