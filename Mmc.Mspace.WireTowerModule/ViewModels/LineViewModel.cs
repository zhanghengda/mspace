using System;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.WireTowerModule.Models;
using Mmc.Mspace.WireTowerModule.Views;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.WireTowerModule.ViewModels
{
    public class LineViewModel : BaseViewModel
    {
        public LineViewModel()
        {
            CancelCommand = new RelayCommand(() =>
            {
                OnAddOrUpdateLine(LineModel, false);
                CloseView();
            });
            SaveCommand = new RelayCommand(() =>
            {
                if (string.IsNullOrEmpty(LineModel.LineName) ||string.IsNullOrEmpty(LineModel.LineSerial))
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTDataMiss"));
                    return;
                }
                OnAddOrUpdateLine(LineModel, true);
                CloseView();
            });
        }


        public void ShowView()
        {
            ViewTitle = (IsEdit ? Helpers.ResourceHelper.FindKey("WTEdit") : Helpers.ResourceHelper.FindKey("WTAdd") )+ Helpers.ResourceHelper.FindKey("WTLine");
            if (_lineView == null) _lineView = new LineView();
            _lineView.DataContext = this;
            _lineView.Owner = Application.Current.MainWindow;
            _lineView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _lineView.Show();
        }

        public void HideView()
        {
            _lineView?.Hide();
        }

        public void CloseView()
        {
            _lineView?.Close();
            _lineView = null;
        }

        #region varies

        public Action<LineForClient, bool> OnAddOrUpdateLine;

        public bool IsEdit { get; set; }
        private LineView _lineView;

        #endregion

        #region binging varies and command

        private string _viewTitle;
        public string ViewTitle {
            get => _viewTitle;
            set
            {
                _viewTitle = value;
                OnPropertyChanged("ViewTitle");
            }
        }

        private LineForClient _lineModel;

        public LineForClient LineModel
        {
            get => _lineModel;
            set
            {
                _lineModel = value;
                OnPropertyChanged("LineModel");
            }
        }

        [XmlIgnore] public ICommand CancelCommand { get; set; }
        [XmlIgnore] public ICommand SaveCommand { get; set; }

        #endregion
    }
}