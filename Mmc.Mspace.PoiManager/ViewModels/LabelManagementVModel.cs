using Helpers;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.Theme.Pop;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Mmc.Mspace.PoiManagerModule.Views;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class LabelManagementVModel : BaseViewModel
    {

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; OnPropertyChanged("SearchText"); }
        }

        private bool fireWatermarkAnimation;

        public bool FireWatermarkAnimation
        {
            get { return fireWatermarkAnimation; }
            set
            {
                fireWatermarkAnimation = value;
                OnPropertyChanged("FireWatermarkAnimation");
            }
        }

        private string _editingTagName;


        private RelayCommand _searchCommand;

        public RelayCommand SearchCommand
        {
            get { return _searchCommand ?? (_searchCommand = new RelayCommand(OnSearchCommand)); }
            set { _searchCommand = value; }
        }


        private RelayCommand _createCommand;

        public RelayCommand CreateCommand
        {
            get { return _createCommand ?? (_createCommand = new RelayCommand(OnCreateCommand)); }
            set { _createCommand = value; }
        }

        private RelayCommand<TagItem> _editCommand;

        public RelayCommand<TagItem> EditCommand
        {
            get { return _editCommand ?? (_editCommand = new RelayCommand<TagItem>(OnEditCommand)); }
            set { _editCommand = value; }
        }

        private RelayCommand<TagItem> _deleteCommand;

        public RelayCommand<TagItem> DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand<TagItem>(OnDeleteCommand)); }
            set { _deleteCommand = value; }
        }

        private RelayCommand<TagItem> _saveCommand;
        public RelayCommand<TagItem> SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand<TagItem>(OnSaveCommand)); }
            set { _saveCommand = value; }
        }


        private RelayCommand<TagItem> _cancelCommand;

        public RelayCommand<TagItem> CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new RelayCommand<TagItem>(OnCancelCommand)); }
            set { _cancelCommand = value; }
        }


        private RelayCommand<TagItem> _selectTagCommand;

        public RelayCommand<TagItem> SelectTagCommand
        {
            get { return _selectTagCommand ?? (_selectTagCommand = new RelayCommand<TagItem>(OnSelectTagCommand)); }
            set { _selectTagCommand = value; }
        }

        private RelayCommand _labelTypeMgtCommand;

        public RelayCommand LabelTypeMgtCommand
        {
            get { return _labelTypeMgtCommand ?? (_labelTypeMgtCommand = new RelayCommand(OnLabelTypeMgtCommand));}
            set { _labelTypeMgtCommand = value; }
        }

        private ObservableCollection<TagItem> _tagSource;

        public ObservableCollection<TagItem> TagSource
        {
            get { return _tagSource ?? (_tagSource = new ObservableCollection<TagItem>()); }
            set { _tagSource = value; OnPropertyChanged("TagSource"); }
        }

        protected override void Loaded()
        {
            base.Loaded();
        }
        public void LoadData()
        {
            Task.Run(() => {
                TagSource = new ObservableCollection<TagItem>(MarkerHelper.Instance.GetTagsSource());
            });
        }
        public LabelManagementVModel()
        {

        }

        /// <summary>
        /// 搜索
        /// </summary>
        private void OnSearchCommand()
        {
            Task.Run(() =>
            {
                TagSource = new ObservableCollection<TagItem>(MarkerHelper.Instance.GetTagsSource(SearchText));
            });
        }
        /// <summary>
        /// 增加标签
        /// </summary>
        private void OnCreateCommand()
        {
            if (TagSource.Any(t => t.IsEdit))
                return;
            TagSource.ForEach(t => t.IsSelected = false);
            TagSource.ForEach(t => t.IsEdit = false);
            TagItem newtag = new TagItem();
            newtag.IsSelected = true;
            newtag.IsEdit = true;
            TagSource.Insert(0, newtag);
            OnPropertyChanged("TagSource");
        }


        private void OnSelectTagCommand(TagItem tag)
        {
            if (tag == null) return;
            TagSource.ForEach(t => t.IsEdit = false);
            TagSource.ForEach(t => t.IsSelected = false);
            tag.IsSelected = true;
        }
        private void OnDeleteCommand(TagItem tag)
        {
            if (tag == null) return;
            var result = MarkerHelper.Instance.DeleteTag(tag.id.ToString());
            if (result)
            {
                Messages.ShowMessage(ResourceHelper.FindKey("DeleteSuccess"));
                LoadData();
            }
            else
                Messages.ShowMessage(ResourceHelper.FindKey("DeleteFailed"));
        }

        private void OnSaveCommand(TagItem tag)
        {
            if (tag == null) return;
            if (tag.id == 0)
            {
                // 输入tag名称为空
                if (string.IsNullOrEmpty(tag.name))
                {
                    FireWatermarkAnimation = true;
                    FireWatermarkAnimation = false;

                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("TileLayerPropView_SearchByName"));
                    return;
                }

                var addresult = MarkerHelper.Instance.AddTag(tag.name);
                if (addresult)
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Savesuccess"));
                    OnSearchCommand();
                }
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("SaveLabelfailed"));
                return;
            }
            var result = MarkerHelper.Instance.EditTag(tag.id.ToString(), tag.name);
            if (result)
            {
                tag.IsEdit = false;
                tag.IsSelected = true;
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Savesuccess"));
            }
            else
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("SaveLabelfailed"));
        }

        private void OnCancelCommand(TagItem tag)
        {
            if (tag == null) return;
            if (tag.id == 0)
            {
                TagSource.Remove(tag);
                OnPropertyChanged("TagSource");
                return;
            }
            tag.IsEdit = false;
            tag.IsSelected = true;
            tag.name = _editingTagName;
        }
        private void OnEditCommand(TagItem tag)
        {
            if (tag == null) return;
            TagSource.ForEach(t => t.IsSelected = false);
            TagSource.ForEach(t => t.IsEdit = false);
            tag.IsEdit = true;
            tag.IsSelected = true;

            _editingTagName = tag.name;
        }

        protected override void Unloaded()
        {
            TagSource = null;
            base.Unloaded();
        }

        /// <summary>
        /// 标签类型管理
        /// </summary>
        private void OnLabelTypeMgtCommand()
        {
            LabelTypeManagementViewModel labelTypeManagementViewModel =new LabelTypeManagementViewModel();
        }
    }
}
