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

namespace Mmc.Mspace.IotModule.ViewModels
{
   public class EventTimeVModel: CheckedToolItemModel
    {
        
        public ICommand RecordCmd { get; set; }
        EventTimeView  eventTimeView = new EventTimeView();
        public EventRecordVModel eventRecordVModel = new EventRecordVModel();
        public EventTimeVModel()
        {
            eventTimeView.DataContext = this;
            this.RecordCmd = new RelayCommand(GetRecord);
        }
        private string _dataTimeToday;
        public string DataTimeToday
        {
            get { return _dataTimeToday; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._dataTimeToday, value, "DataTimeToday");
            }
        }
        void getToday()
        {
            DataTimeToday = DateTime.Now.ToString("yyyy-MM-dd");
        }
        public void ShowEventTimeView()
        {
            getToday();
            eventTimeView.Show();
            eventTimeView.Left = Application.Current.MainWindow.Width * 0.6;
            eventTimeView.Top = Application.Current.MainWindow.Height * 0.1;
        }

        public void HideEventTimeView()
        {
            eventTimeView.Hide();
            eventRecordVModel.HideEventRecordView();
            eventRecordVModel.ClearMapEvent();
        }

        private void GetRecord()
        {           
            eventRecordVModel.ShowEventRecordView();
            eventRecordVModel.DateTimeChange -= OnDateTimeChange;
            eventRecordVModel.DateTimeChange += OnDateTimeChange;

        }
        private void OnDateTimeChange(string time)
        {
            DataTimeToday = time;
        }
    }
}
