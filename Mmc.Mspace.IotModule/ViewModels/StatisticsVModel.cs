using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.IotModule.Views;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.IotModule.ViewModels
{
    public class StatisticsVModel: CheckedToolItemModel
    {

        private  StatisticsView StatisticsView;
        #region Property

        public string WindowTitle { get; set; }

        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        public override void Initialize()
        {
            base.ViewType =  ViewType.CheckedIcon;
            WindowTitle = Helpers.ResourceHelper.FindKey("Statistics");
            base.Initialize();
            CloseCmd = new RelayCommand(() => {
                base.IsChecked = false;
            });
            //Command = new RelayCommand(GetSource);
        }
        //private  bool ShowState = false;
        public override void OnChecked()
        {
            base.OnChecked();
            Messenger.Messengers.Notify("HideSummery", false);
            ShowData();

        }
        public override void OnUnchecked()
        {
            Messenger.Messengers.Notify("ShowSummery", true);

            base.OnUnchecked();
            HiddenData();
        }
        //private void GetSource()
        //{
        //    if (ShowState)
        //        HiddenData();
        //    else
        //        ShowData();
        //}
        private void ShowData()
        {
            IsSelected = true;
            //ShowState = true;
            try
            {

                if (StatisticsView == null)
                    StatisticsView = new StatisticsView();
                StatisticsView.DataContext = this;
                StatisticsView.Owner = Application.Current.MainWindow;
                StatisticsView.Show();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
        }

        private void HiddenData()
        {
            IsSelected = false;
            //ShowState = false;
            StatisticsView.Hide();
        }
        #endregion
    }
}
