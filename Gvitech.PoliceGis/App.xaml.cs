using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;
using MMC.MSpace.Properties;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using MMC.MSpace.Views;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Languagepack.LanguageManager;
using System.Linq;
using Mmc.Windows.Services;
using System.Windows.Threading;
using Mmc.Mspace.Theme.Pop;
using System.Threading;
using Mmc.Mspace.Common.DumpService;

namespace MMC.MSpace
{

    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AddLanguageResources();
            
            //获取欲启动进程名
            string strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //检查进程是否已经启动，已经启动则显示报错信息退出程序。 
            if (System.Diagnostics.Process.GetProcessesByName(strProcessName).Length > 1)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("MSpacerunning"));
                Thread.Sleep(1500);
                Environment.Exit(0);
                return;
            }
            App.RunInReleaseModel();
        }

        private void AddLanguageResources()
        {
            var language = LanguageManager.Languages.SingleOrDefault(t => t.Name == CacheData.CurrentLanguage);
            LanguageManager.ChangeLanguages(Application.Current.Resources, language);
        }

        private static void RunInDebugMode()
        {
            PoliceGisBootstrapper policeGisBootstrapper = new PoliceGisBootstrapper();
            policeGisBootstrapper.Run();
        }


        private static void RunInReleaseModel()
        {

            AppDomain.CurrentDomain.UnhandledException += App.CurrentDomain_UnhandledException;
            Current.DispatcherUnhandledException += Application_DispatcherUnhandledExecption;
            try
            {
                PoliceGisBootstrapper policeGisBootstrapper = new PoliceGisBootstrapper();
                policeGisBootstrapper.Run();
            }
            catch (Exception ex)
            {
                SystemLog.WriteLog("RunInReleaseModel  ", ex);
            }
        }
        /// <summary>
        /// UI线程异常处理(程序可继续运行)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_DispatcherUnhandledExecption(object sender,
            DispatcherUnhandledExceptionEventArgs e)
        {

            //日志处理
            SystemLog.WriteLog("Application_DispatcherUnhandledExecption  ", e.Exception);
            //程序继续运行
            e.Handled = true;
        }

        /// <summary>
        /// 非UI线程异常处理(程序只能终止)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                SystemLog.WriteLog("CurrentDomain_UnhandledException1");
                //System.Environment.GetEnvironmentVariable("TEMP")
                MessageBox.Show("不可恢复的非UI线程异常，应用程序将退出！");
                string dumpFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, string.Format("crash-dump-{0}.dmp", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")));
                MiniDump.Write(dumpFile);
                SystemLog.WriteLog("CurrentDomain_UnhandledException  ", e.ExceptionObject as Exception);
                
            }
            catch
            {
                MessageBox.Show("不可恢复的非UI线程异常，应用程序将退出！");
            }
        }


    }
}
