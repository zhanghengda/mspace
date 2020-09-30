using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mmc.Mspace.Common.ShellService
{
    public interface IShellService
    {
        FrameworkElement BottomToolMenu { get; set; }
        ContentControl BottomView { get; set; }
        Window CompareView { get; set; }

        ContentControl ContentView { get; set; }
        ICommand HomeCmd { get; set; }
        Window LeftWindow { get; set; }

        Panel LeftPanel { get; set; }
        Canvas PopView { get; set; }
        ContentControl ProgressView { get; set; }
        FrameworkElement RightToolMenu { get; set; }
        ContentControl RightView { get; set; }
        FrameworkElement ShellMenu { get; set; }
        Window ShellWindow { get; set; }
        FrameworkElement ToolMenu { get; set; }

        ContentControl ToolRouteplanningMenu { get; set; }
        void ShowMenuView(string groupname);
        void HideAllView();

        void ShowAllView();

        void ShowBottomRight(int time = 5);

        /// <summary>
        /// 是否开启多屏对比
        /// </summary>
        bool IsCompareView { get; set; }
        Action<double[]> OnShellLocationChanged { get; set; }
    }
}