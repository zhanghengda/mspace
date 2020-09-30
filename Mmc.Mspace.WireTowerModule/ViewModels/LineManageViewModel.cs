using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.WireTowerModule.DTO;
using Mmc.Mspace.WireTowerModule.Models;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.WireTowerModule.ViewModels
{
    public class LineManageViewModel : BaseViewModel
    {
        #region varies

        public Action<List<LineForClient>> OnLineChanged;
        private readonly WireTowerConverter _wireTowerConverter;
        private readonly string _allWireLinesFile;
        private readonly LineViewModel _lineViewModel;
        private readonly List<LineForClient> _curLineList;
        #endregion

        #region binging varies and command
        private ObservableCollection<LineForClient> _lineSet;
        public ObservableCollection<LineForClient> LineSet
        {
            get => _lineSet ?? new ObservableCollection<LineForClient>();
            set
            {
                _lineSet = value;
                OnPropertyChanged("LineSet");
            }
        }

        [XmlIgnore] public ICommand SaveCommand { get; set; }
        [XmlIgnore] public ICommand AddCommand { get; set; }
        [XmlIgnore] public ICommand EditCommand { get; set; }
        [XmlIgnore] public ICommand DeleteCommand { get; set; }
        #endregion

        public LineManageViewModel()
        {
            this.SaveCommand = new Wpf.Commands.RelayCommand(SaveData);
            this.AddCommand = new Wpf.Commands.RelayCommand(OnAddLine);
            this.EditCommand = new Wpf.Commands.RelayCommand<object>(OnEditLine);
            this.DeleteCommand = new Wpf.Commands.RelayCommand<object>(OnDeleteLine);

            _curLineList = WirTowRenderManagement.Instance.Lines;
            _wireTowerConverter = new WireTowerConverter();
            _lineViewModel = new LineViewModel();
            _allWireLinesFile = WirTowRenderManagement.Instance.GetLocalWirePath("MmcWireLine.json");
            //ReadData(_allWireLinesFile);
            LineSet = new ObservableCollection<LineForClient>(_curLineList);
        }

        public void SaveData()
        {
            List<LineModel> models = new List<LineModel>();
            foreach (var item in _curLineList)
            {
                models.Add(_wireTowerConverter.LineConvert(item));
            }
            var dataStr = JsonUtil.SerializeToString(models);
            WirTowRenderManagement.Instance.WriteFile(_allWireLinesFile, dataStr);
        }

        public void ReadData(string fileName)
        {
            try
            {
                List<LineModel> lines = JsonUtil.DeserializeFromFile<List<LineModel>>(_allWireLinesFile);

                if (lines == null || lines.Count <= 0) return;
                foreach (var model in lines)
                {
                    _curLineList.Add(_wireTowerConverter.LineConvert(model));
                }

                LineSet = new ObservableCollection<LineForClient>(_curLineList);
            }
            catch
            {
            }
        }

        private void OnDeleteLine(object obj)
        {
            if (!(obj is LineForClient line)) return;
            if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("WTItemWarn"), $"{Helpers.ResourceHelper.FindKey("WTItemDelete")}{Helpers.ResourceHelper.FindKey("WTLine")}:{line.LineSerial} {Helpers.ResourceHelper.FindKey("WTLineWarn")}"))
            {
                if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("WTItemWarn"), $"{Helpers.ResourceHelper.FindKey("WTItemDelete")}{Helpers.ResourceHelper.FindKey("WTLine")}:{line.LineSerial} {Helpers.ResourceHelper.FindKey("WTLineWarn")}"))
                {
                    LineSet.Remove(line);
                    _curLineList.Remove(line);
                    OnLineChanged(_curLineList);
                }
            }
        }

        private void OnEditLine(object obj)
        {
            if (!(obj is LineForClient tower)) return;
            ShowLineView(tower, true);
        }

        private void OnAddLine()
        {
            var id = LineSet.ToList().LastOrDefault()?.Id ?? 0;
            var tower = new LineForClient() {Id = id + 1};
            ShowLineView(tower, false);
        }

        private void ShowLineView(LineForClient line, bool isEdit)
        {
            _lineViewModel.LineModel = line;
            _lineViewModel.IsEdit = isEdit;
            if (_lineViewModel.OnAddOrUpdateLine == null) _lineViewModel.OnAddOrUpdateLine += OnAddOrUpdateLine;
            _lineViewModel.ShowView();
        }

        private void OnAddOrUpdateLine(LineForClient obj, bool isUpdate)
        {

            if (!isUpdate) return;

            if (obj == null) return;
            var item = _curLineList.Find(p => p.Id == obj.Id);

            if (item != null)
            {
                item = obj;
            }
            else
            {
                _curLineList.Add(obj);
            }

            LineSet = new ObservableCollection<LineForClient>(_curLineList);
            OnLineChanged(_curLineList);
        }

        //private void LineChangeNotify()
        //{
        //    Messenger.Messengers.Register("LineChange",);
        //}
    }
}
