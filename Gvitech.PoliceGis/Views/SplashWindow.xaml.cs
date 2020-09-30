using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using DevExpress.Xpf.Core;

namespace MMC.MSpace.Views
{

    public partial class SplashWindow : Window, ISplashScreen
    {

        // (get) Token: 0x0600001B RID: 27 RVA: 0x000028BE File Offset: 0x00000ABE
        // (set) Token: 0x0600001C RID: 28 RVA: 0x000028C6 File Offset: 0x00000AC6
        public bool IsLoadCompleted { get; set; }


        public SplashWindow()
        {
            this.InitializeComponent();
            //base.Topmost = true;
            base.MaxHeight = (base.Height = SystemParameters.WorkArea.Height);
            base.MaxWidth = (base.Width = SystemParameters.WorkArea.Width);
            this.Infos = new List<string>();
            this.Infos.Add(Helpers.ResourceHelper.FindKey("Startloading"));
            this.Infos.Add(Helpers.ResourceHelper.FindKey("loadingdata"));
            this.Infos.Add(Helpers.ResourceHelper.FindKey("loaded"));
            this.tittle.Text = StringExtension.ParseTo<string>(ConfigurationManager.AppSettings["SplashWindowTittle"], null);
        }


        // (get) Token: 0x0600001E RID: 30 RVA: 0x00002990 File Offset: 0x00000B90
        // (set) Token: 0x0600001F RID: 31 RVA: 0x00002998 File Offset: 0x00000B98
        public List<string> Infos { get; set; }


        public void CloseSplashScreen()
        {
            base.Close();
        }


        public void Progress(double value)
        {
            bool flag = (int)value == this.Infos.Count - 1;
            if (flag)
            {
                this.IsLoadCompleted = true;
                Thread.Sleep(500);
                this.CloseSplashScreen();
            }
            else
            {
                Thread.Sleep(500);
            }
            this.loadinfo.Text = this.Infos[(int)value];
        }


        public void SetProgressState(bool isIndeterminate)
        {
        }
    }
}
