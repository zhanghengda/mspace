using Mmc.Windows.Design;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Mmc.Mspace.Common.ShellService
{
    public class ShellService : Singleton<ShellService>, IShellService
    {
        public Window ShellWindow { get; set; }
        public FrameworkElement ShellMenu { get; set; }
        public FrameworkElement ToolMenu { get; set; }
        public FrameworkElement BottomToolMenu { get; set; }
        public Canvas PopView { get; set; }
        public Window CompareView { get; set; }
        public ContentControl BottomView { get; set; }
        public ContentControl RightView { get; set; }
        public ContentControl ContentView { get; set; }
        public ContentControl ProgressView { get; set; }
        public ICommand HomeCmd { get; set; }
        public Window LeftWindow { get; set; }

        public Panel LeftPanel { get; set; }
        public FrameworkElement RightToolMenu { get; set; }

        public bool IsCompareView { get; set; }
        public Action<double[]> OnShellLocationChanged { get; set; }
        public ContentControl ToolRouteplanningMenu { get; set; }

        public static IShellService GetDefault(object args = null)
        {
            return Singleton<ShellService>.Instance;
        }

        public void HideAllView()
        {
            //this.LeftWindow.Hide();
            this.LeftPanel.Visibility = Visibility.Collapsed;
            this.ShellMenu.Visibility = Visibility.Collapsed;
            this.RightToolMenu.Visibility = Visibility.Collapsed;
            this.BottomToolMenu.Visibility = Visibility.Collapsed;
        }

        public void ShowAllView()
        {
            // this.LeftWindow.Hide();
            this.LeftWindow.Visibility = Visibility.Visible;
            this.ShellMenu.Visibility = Visibility.Visible;
            this.RightToolMenu.Visibility = Visibility.Visible;
            this.BottomToolMenu.Visibility = Visibility.Visible;
        }

        public void ShowBottomRight(int time = 5)
        {
            time = time * 1000;
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                System.Threading.Thread.Sleep(time);
                RightToolMenu.Visibility = Visibility.Visible;
                BottomToolMenu.Visibility = Visibility.Visible;
            }), DispatcherPriority.Background, new object[0]);
        }

        public void ShowMenuView(string groupname)
        {
            if (groupname == "Cityoverview")
            {
                Messenger.Messenger.Messengers.Notify("ShowHiddenMenu", true);
                Messenger.Messenger.Messengers.Notify("ShowSummery", true);
            }
            else
            {
                Messenger.Messenger.Messengers.Notify("undeviceview", false);
                Messenger.Messenger.Messengers.Notify("ShowHiddenMenu", false);
                Messenger.Messenger.Messengers.Notify("unpatrol", false);
                Messenger.Messenger.Messengers.Notify("unAdministrative", false);
                Messenger.Messenger.Messengers.Notify("HideSummery", false);


            }
            if (groupname == "RegularInspection")
            {
                Messenger.Messenger.Messengers.Notify("LeftMenuEnum", CommonContract.LeftMenuEnum.RegularInspectionView);
                //Messenger.Messenger.Messengers.Notify(CommonContract.RenderLayerStatus.SaveStatus.ToString());
                //Messenger.Messenger.Messengers.Notify(CommonContract.RenderLayerStatus.AllHide.ToString());
            }
            else
            {

                Messenger.Messenger.Messengers.Notify("LeftMenuEnum", CommonContract.LeftMenuEnum.LeftManagementView);

                //Messenger.Messenger.Messengers.Notify(CommonContract.RenderLayerStatus.RecoveryRenderStatus.ToString());
            }
        }
    }
}