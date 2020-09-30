using Mmc.Mspace.Common.Models;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using MMC.MSpace.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml.Serialization;

namespace MMC.MSpace.ViewModels
{
    public class NoticeVModel : BindableBase
    {
        private NoticeView _noticeView;
        public Action<bool> IsNoticeClear;
        private List<TextItem> _noticeList;

        private ObservableCollection<TextItem> _noticeDataSet;
        [XmlIgnore]
        public ObservableCollection<TextItem> NoticeDataSet
        {
            get { return _noticeDataSet ?? (_noticeDataSet = new ObservableCollection<TextItem>()); }
            set { _noticeDataSet = value; NotifyPropertyChanged("NoticeDataSet"); }
        }

        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(CloseView)); }
            set { _closeCommand = value; }
        }

        private RelayCommand<object> _deleteItemCommand;
        public RelayCommand<object> DeleteItemCommand
        {
            get { return _deleteItemCommand ?? (_deleteItemCommand = new RelayCommand<object>(DeleteNoticeItem)); }
            set { _deleteItemCommand = value; }
        }

        public void ShowView(List<string> list)
        {
            if (list == null || list.Count <= 0)
            {
                IsNoticeClear(true);
                return;
            }
            if (_noticeList == null) _noticeList = new List<TextItem>();
            _noticeList.Clear();
            //if (list==null || list.Count <= 0)
            //{
            //    _noticeList.Add(new TextItem() { Key = "0", Value = "没有通知" });
            //    _noticeList.Add(new TextItem() { Key = "1", Value = "hello" });
            //    _noticeList.Add(new TextItem() { Key = "2", Value = "kitty" });
            //    _noticeList.Add(new TextItem() { Key = "3", Value = "today" });
            //    _noticeList.Add(new TextItem() { Key = "4", Value = "is" });
            //    _noticeList.Add(new TextItem() { Key = "5", Value = "friday" });
            //}
            //else
            //{
            for (int i = 0; i < list.Count; i++)
            {
                _noticeList.Add(new TextItem() { Key = i.ToString(), Value = list[i] + "图层需要更新" });
            }
            //}
            NoticeDataSet = new ObservableCollection<TextItem>(_noticeList);
            if (_noticeView == null)
            {
                _noticeView = new NoticeView();
            }
            else
            {
                _noticeView.Activate();
            }

            _noticeView.DataContext = this;
            _noticeView.Owner = Application.Current.MainWindow;
            _noticeView.Left = Application.Current.MainWindow.Width - _noticeView.Width - 60;
            _noticeView.Top = 80;
            _noticeView.Show();
        }

        private void CloseView()
        {
            IsNoticeClear(IsNoticeFinished());
            if (_noticeView == null) _noticeView = new NoticeView();
            _noticeView.Hide();
        }

        private void DeleteNoticeItem(object obj)
        {
            var item = obj as TextItem;
            if (item == null) return;
            if (_noticeList != null)
            {
                _noticeList.Remove(item);
            }
            if (IsNoticeFinished()) CloseView();
            NoticeDataSet = new ObservableCollection<TextItem>(_noticeList);
        }

        private  bool  IsNoticeFinished()
        {
            if (_noticeList == null) return true;
            return  !_noticeList.HasValues();
        }
    }
}
