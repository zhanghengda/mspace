using Mmc.Mspace.Common.Models;
using Mmc.Mspace.IotModule.Models;
using Mmc.Mspace.IotModule.Views;
using Mmc.Mspace.Theme.Converter;
using Mmc.Mspace.Theme.Pop;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.IotModule.ViewModels
{
    public class PatrolRecordVModel : BaseViewModel
    {
        public Action StartThreadOfPatrol;
        public Func<string, string, List<DateTime>> GetPersonalMonthRecord;
        public Action<PatrolmanForClient, string> DrawingPersonalHistoryTrace;
        public PatrolmanForClient currentPerson;
        private string selectedDate;
        private string prevMonth;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value;OnPropertyChanged("Title"); }
        }
        
        private bool _isShowTraceBtnEnabled;
        public bool IsShowTraceBtnEnabled
        {
            get { return _isShowTraceBtnEnabled; }
            set { _isShowTraceBtnEnabled = value; OnPropertyChanged("IsShowTraceBtnEnabled"); }
        }
        private ObservableCollection<DateTime> _recordDate;
        public ObservableCollection<DateTime> RecordDate
        {
            get { return _recordDate ?? (_recordDate = new ObservableCollection<DateTime>()); }
            set
            {
                _recordDate = value;
                OnPropertyChanged("RecordDate");
            }
        }

        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        public ICommand ShowPatroledTraceCmd { get; set; }
        //public override FrameworkElement CreatedView()
        //{
        //    return new PatrolRecordView() { Owner = Application.Current.MainWindow };
        //}

        private PatrolRecordView _patrolRecordView;

        public PatrolRecordVModel()
        {
            if (_patrolRecordView == null)
                _patrolRecordView = new PatrolRecordView();
            this.CloseCmd = new RelayCommand(() =>
            {
                CloseView();
                StartThreadOfPatrol();
            });
            this.ShowPatroledTraceCmd = new RelayCommand(() =>
            {
                onShowPatroledTrace();
            });
            prevMonth = DateTime.Now.ToString("yyyy-MM");
            selectedDate = DateTime.Now.ToString("yyyy-MM-dd");
            IsShowTraceBtnEnabled = true;
        }

        private void onShowPatroledTrace()
        {
            Task.Run(() =>
            {
                if (selectedDate != null)
                {
                    IsShowTraceBtnEnabled = false;
                    var list = CalerdarSelectedDateCoverter.GetRecordDateStrList();
                    var item = list.Find(p => p == selectedDate);
                    if (item != null)
                    {
                        DrawingPersonalHistoryTrace(currentPerson, selectedDate);
                        Thread.Sleep(1500);
                    }
                    else
                        Messages.ShowMessage(string.Format("{0}在{1}没有记录。", currentPerson?.Name, selectedDate));

                    IsShowTraceBtnEnabled = true;
                }
            });
        }

        public void ShowView()
        {
            if (_patrolRecordView == null)
                _patrolRecordView = new PatrolRecordView();
            _patrolRecordView.DataContext = this;
            _patrolRecordView.Owner = Application.Current.MainWindow;
            _patrolRecordView.Left = Application.Current.MainWindow.Width - _patrolRecordView.Width - 60;
            _patrolRecordView.Top = Application.Current.MainWindow.Height * 0.1;
            
            Title = currentPerson?.Name + "巡检记录";
            _patrolRecordView.SelectedDateTimeChange -= onSelectedDateTimeChange;
            _patrolRecordView.SelectedDateTimeChange += onSelectedDateTimeChange;
            _patrolRecordView.DisplayDateTimeChange -= onDisplayTimeChange;
            _patrolRecordView.DisplayDateTimeChange += onDisplayTimeChange;
            _patrolRecordView.Show();
        }

        private void onDisplayTimeChange(string dateStr)
        {
            if (string.IsNullOrEmpty(dateStr)) return;

            if (prevMonth != dateStr)
            {
                prevMonth = dateStr;
                var list = GetPersonalMonthRecord(currentPerson.Phone, prevMonth);
                if (list.Count <= 0)
                    Messages.ShowMessage(string.Format("{0}在{1}月份没有记录。", currentPerson.Name, prevMonth));
                CalerdarSelectedDateCoverter.Update(list);
            }
        }

        private void onSelectedDateTimeChange(string dateStr)
        {
            DateTime dt = Convert.ToDateTime(dateStr);
            if (dt == null || currentPerson == null) return;
            selectedDate = dt.ToString("yyyy-MM-dd");
        }

        public void HideView()
        {
            if (_patrolRecordView == null)
                _patrolRecordView = new PatrolRecordView();
            _patrolRecordView.Hide();
            _patrolRecordView.SelectedDateTimeChange -= onDisplayTimeChange;
            _patrolRecordView.DisplayDateTimeChange -= onDisplayTimeChange;
        }

        public void CloseView()
        {
            if (_patrolRecordView == null)
                _patrolRecordView = new PatrolRecordView();
            _patrolRecordView.SelectedDateTimeChange -= onDisplayTimeChange;
            _patrolRecordView.DisplayDateTimeChange -= onDisplayTimeChange;
            _patrolRecordView.Close();
        }
    }
}
