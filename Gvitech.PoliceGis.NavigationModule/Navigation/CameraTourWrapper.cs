using System;
using System.Windows.Input;
using System.Windows.Threading;
//using Gvitech.AppPd.UrbanPlan.DAL;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Framework.Wpf.Core;
using Mmc.Mspace.Models.Navigation;
using Mmc.Mspace.Theme.Pop;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.NavigationModule.Navigation
{
    // Token: 0x02000005 RID: 5
    public class CameraTourWrapper : ExBindableBase
    {
        // Token: 0x0600000B RID: 11 RVA: 0x000021CC File Offset: 0x000003CC
        public CameraTourWrapper()
        {
            this.PlayCmd = new RelayCommand<bool>((IsChecked) =>
            {
                bool flag = this.cameraTour == null;
                if (flag)
                {
                    this.cameraTour = GviMap.MapControl.ObjectManager.CreateCameraTour();
                    this.cameraTour.FromXml(this.CameraTour.XmlRoute);
                    this.TotalTime = this.cameraTour.TotalTime;
                }
                bool flag2 = this.cameraTour != null;
                if (flag2)
                {
                    bool flag3 = !this.isChecked;
                    if (flag3)
                    {
                        this.Reset();
                        this.cameraTour.Time = 0.0;
                        this.isChecked = true;
                        this.timer = new DispatcherTimer();
                        this.timer.Interval = TimeSpan.FromMilliseconds(100.0);
                        this.timer.Tick += delegate (object s, EventArgs e)
                        {
                            bool flag4 = this.cameraTour != null;
                            if (flag4)
                            {
                                this.Time = this.cameraTour.Time;
                            }
                        };
                        this.timer.Start();
                    }
                    
                    this.cameraTour.Play();
                    this.cameraTour.Time = this.Time;
                }
            });
            this.PauseCmd = new RelayCommand<bool>((IsChecked) =>
            {
                bool flag = this.cameraTour != null;
                if (flag)
                {
                    this.cameraTour.Pause();
                }
            });
            this.StopCmd = new RelayCommand(() =>
            {
                bool flag = this.cameraTour != null;
                if (flag)
                {
                    this.cameraTour.Stop();
                }
                this.Reset();
                this.isChecked = false;
            });
            this.SaveAsVideoCmd = new RelayCommand<string>((Path) =>
            {
                bool flag = this.cameraTour == null;
                if (flag)
                {
                    this.cameraTour = GviMap.MapControl.ObjectManager.CreateCameraTour();
                    this.cameraTour.FromXml(this.CameraTour.XmlRoute);
                }
                bool flag2 = this.cameraTour != null;
                if (flag2)
                {
                    if(this.cameraTour.WaypointsNumber>2)
                    {

                        var IsExport = this.cameraTour.ExportVideo(Path, 25);
                        Messages.ShowMessage("动画导出成功");
                    }
                    else
                    {
                        Messages.ShowMessage("当前动画导航为空或者节点数小于2，不支持视频输出");
                    }
                    
                }
            });
        }

        // Token: 0x0600000C RID: 12 RVA: 0x0000222C File Offset: 0x0000042C
        private void Reset()
        {
            this.Time = 0.0;
            bool flag = this.timer != null;
            if (flag)
            {
                this.timer.Stop();
                this.timer = null;
            }
        }

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x0600000D RID: 13 RVA: 0x0000226C File Offset: 0x0000046C
        // (set) Token: 0x0600000E RID: 14 RVA: 0x00002274 File Offset: 0x00000474
        public ICommand PlayCmd { get; set; }

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x0600000F RID: 15 RVA: 0x0000227D File Offset: 0x0000047D
        // (set) Token: 0x06000010 RID: 16 RVA: 0x00002285 File Offset: 0x00000485
        public ICommand PauseCmd { get; set; }

        // Token: 0x17000004 RID: 4
        // (get) Token: 0x06000011 RID: 17 RVA: 0x0000228E File Offset: 0x0000048E
        // (set) Token: 0x06000012 RID: 18 RVA: 0x00002296 File Offset: 0x00000496
        public ICommand StopCmd { get; set; }

        public ICommand SaveAsVideoCmd { get; set; }

        // Token: 0x17000005 RID: 5
        // (get) Token: 0x06000013 RID: 19 RVA: 0x0000229F File Offset: 0x0000049F
        // (set) Token: 0x06000014 RID: 20 RVA: 0x000022A7 File Offset: 0x000004A7
        public CameraTourData CameraTour { get; set; }

        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000015 RID: 21 RVA: 0x000022B0 File Offset: 0x000004B0
        // (set) Token: 0x06000016 RID: 22 RVA: 0x000022C8 File Offset: 0x000004C8


        public double TotalTime
        {
            get
            {
                return this.totalTime;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<double>(ref this.totalTime, value, "TotalTime");
            }
        }

        // Token: 0x17000007 RID: 7
        // (get) Token: 0x06000017 RID: 23 RVA: 0x000022E0 File Offset: 0x000004E0
        // (set) Token: 0x06000018 RID: 24 RVA: 0x000022F8 File Offset: 0x000004F8
        public double Time
        {
            get
            {
                return this.time;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<double>(ref this.time, value, "Time");
            }
        }

        // Token: 0x04000004 RID: 4
        private ICameraTour cameraTour;

        // Token: 0x04000005 RID: 5
        private bool isChecked;

        // Token: 0x04000006 RID: 6
        private DispatcherTimer timer;

        // Token: 0x0400000B RID: 11
        private double totalTime;

        // Token: 0x0400000C RID: 12
        private double time=0.0;
    }
}
