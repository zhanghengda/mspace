using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Mmc.Mspace.Theme.Pop
{
    public class Messages : Window
    {
        private static readonly object SynObject = new object();
        private static event Action<string> SendMessage;
        private static bool _start;

        private static MmcMsBox MmcMsBox;
        private static MmcConfirmationBox MmcConfirmationBox;

        private DefaultSecondWindow DefaultSecondWindow;
        /// <summary>
        /// 时间控制器
        /// </summary>
        private static System.Timers.Timer t = new System.Timers.Timer(2000);//实例化Timer类

        private static Messages _message;

        public static Messages Message
        {
            get
            {
                if (_message == null)
                    _message = new Messages();
                return _message;
            }
            set { _message = value; }
        }

        /// <summary>
        /// 时间结束执行函数
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Action add = new Action(() =>
                {
                    t.Stop();//结束显示
                    MmcMsBox.Visibility = Visibility.Collapsed;
                });
                MmcMsBox.Dispatcher.BeginInvoke(add);

            }
            catch (Exception ex)
            {
                

            }
        }

        private static void TimeStart()
        {
            _start = true;
            if (MmcMsBox == null)
                MmcMsBox = new MmcMsBox();
            SendMessage = MmcMsBox.UpdateMeg;
            t.Elapsed += new System.Timers.ElapsedEventHandler(theout);
        }

        public static void ShowMessage(string msg)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action<string>(ShowMessages2), new object[] { msg });
        }

        private static void ShowMessages2(string msg)
        {
            if (MmcMsBox == null)
            {
                MmcMsBox = new MmcMsBox();
            }
            else
            {
                MmcMsBox.Close();
                MmcMsBox = new MmcMsBox();
                t.Stop();
                t.Start();//启动定时器
            }
            if (!_start)//首次需要注册定时器
                TimeStart();
            t.Start();//启动定时器
            if (Application.Current.MainWindow!=null)
            {
                Application.Current.MainWindow.StateChanged -= MainWindow_StateChanged;
                Application.Current.MainWindow.StateChanged += MainWindow_StateChanged;
                MmcMsBox.Owner = Application.Current.MainWindow;
            }
            MmcMsBox.tbMessage.Text = msg;
            MmcMsBox.Show();
        }

        private static void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Normal) return;
            Action add = new Action(() =>
              {
                  t.Stop();//结束显示
                if (MmcMsBox != null)
                  {
                      MmcMsBox.Visibility = Visibility.Collapsed;
                  }
              });
            if (MmcMsBox != null)
                MmcMsBox.Dispatcher.BeginInvoke(add);
        }

        public static bool ShowMessageDialog(string title,string msg)
        {
            return System.Windows.Application.Current.Dispatcher.Invoke(new Func<bool>(() =>
            {
                MmcConfirmationBox = new MmcConfirmationBox();
                MmcConfirmationBox.Title = title;
                MmcConfirmationBox.Msg = msg;
                MmcConfirmationBox.Owner = Application.Current.MainWindow;
                MmcConfirmationBox.ShowDialog();
                return MmcConfirmationBox.IsOk;
            }));
        }

        public void CloseWindow()
        {
            DefaultSecondWindow.Close();
        }

        public void ShowWindow(string title, UserControl control)
        {
            SecondNotification secondNotification = new SecondNotification(title, control, control.MinWidth, control.MinHeight + 40);
            DefaultSecondWindow = new DefaultSecondWindow();
            DefaultSecondWindow.NavigateToPager(secondNotification);
        }
    }
}