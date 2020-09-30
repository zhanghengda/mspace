using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Models.HttpResult;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class LabelTypeAddOrEditViewModel : BaseViewModel
    {
        #region Property

        /// <summary>
        /// 控制新增或者修改标签
        /// 0:新增标签 1:修改标签
        /// </summary>
        private int flag;

        private Action<int,int> getDataAction;

        private LabelTypeAddOrEditView labelTypeAddOrEditView;

        private TagTypeModel editType;

        private string _titleName;
        public string TitleName
        {
            get { return _titleName; }
            set
            {
                _titleName = value;
                OnPropertyChanged("TitleName");
            }
        }

        private string _tagTypeName;
        public string TagTypeName
        {
            get { return _tagTypeName; }
            set { _tagTypeName = value; OnPropertyChanged("TagTypeName"); }
        }

        private bool _isCheckedAll;
        public bool IsCheckedAll
        {
            get { return _isCheckedAll; }
            set { _isCheckedAll = value; OnPropertyChanged("IsCheckedAll"); }
        }

        private ObservableCollection<LabelInfoModel> _tags;
        public ObservableCollection<LabelInfoModel> Tags
        {
            get { return _tags ?? (_tags = new ObservableCollection<LabelInfoModel>()); }
            set { _tags = value; OnPropertyChanged("Tags"); }
        }

        #endregion


        #region Command

        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(OnCloseCommand)); }
            set { _closeCommand = value; }
        }

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(OnSaveCommand)); }
            set { _saveCommand = value; }
        }

        private RelayCommand<LabelInfoModel> _tagItemCheckedCommand;
        public RelayCommand<LabelInfoModel> TagItemCheckedCommand
        {
            get { return _tagItemCheckedCommand ?? (_tagItemCheckedCommand = new RelayCommand<LabelInfoModel>(OnTagItemCheckedCommand)); }
            set { _tagItemCheckedCommand = value; }
        }

        private RelayCommand<bool> _isCheckedAllCommand;
        public RelayCommand<bool> IsCheckedAllCommand
        {
            get { return _isCheckedAllCommand ?? (_isCheckedAllCommand = new RelayCommand<bool>(OnIsCheckedAllCommand)); }
            set { _isCheckedAllCommand = value; }
        } 
        #endregion

        /// <summary>
        /// 新增标签
        /// </summary>
        public LabelTypeAddOrEditViewModel(Action<int,int> action)
        {
            flag = 0;

            this.getDataAction = action;

            this.TitleName = "新增标签类型";

            if (labelTypeAddOrEditView == null)
                labelTypeAddOrEditView = new LabelTypeAddOrEditView();

            Init();
        }

        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="tagType"></param>
        public LabelTypeAddOrEditViewModel(Action<int,int> action,TagTypeModel tagType)
        {
            flag = 1;

            this.getDataAction = action;

            this.TitleName = "修改标签类型";

            if (labelTypeAddOrEditView == null)
                labelTypeAddOrEditView = new LabelTypeAddOrEditView();

            this.editType = tagType;

            TagTypeName = editType.name;

            


            if (Tags.Count == Tags.Count(t => t.LabelIsSelected))
            {
                IsCheckedAll = true;
            }

            Init();
        }


        #region Methods

        /// <summary>
        /// 初始化加载数据
        /// </summary>
        private void Init()
        {
            foreach (var tagItem in MarkerHelper.Instance.GetTagsSource())
            {
                LabelInfoModel model = new LabelInfoModel
                {
                    LabelName = tagItem.name,
                    LabelId = tagItem.id.ToString(),
                    LabelIsSelected = false
                };

                Tags.Add(model);
            }

            if (flag == 1)
            {
                foreach (var item in editType.tags)
                {
                    var result = Tags.FirstOrDefault(t => t.LabelId == item.LabelId);
                    if (result != null)
                    {
                        result.LabelIsSelected = true;
                    }
                }

                if (Tags.Count == Tags.Count(t => t.LabelIsSelected))
                {
                    IsCheckedAll = true;
                }
                else
                {
                    IsCheckedAll = false;
                }
            }

            labelTypeAddOrEditView.Owner = Application.Current.MainWindow;
            labelTypeAddOrEditView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            labelTypeAddOrEditView.DataContext = this;
            labelTypeAddOrEditView.Show();
        }

        private void OnCloseCommand()
        {
            labelTypeAddOrEditView.Close();
            labelTypeAddOrEditView = null;
        }

        private void OnSaveCommand()
        {
            if (string.IsNullOrWhiteSpace(TagTypeName))
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("TileLayerPropView_SearchByName"));
                return;
            }

            if (Tags.Count(t => t.LabelIsSelected) < 1)
            {
                Messages.ShowMessage("请勾选标签后保存!");
                return;
            }

            if (flag == 0)
            {
                try
                {
                    string addTagTypeApi = "/api/tag/taggroupadd";

                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("name", TagTypeName);
                    string addTagTypeJson = JsonUtil.SerializeToString(dic);

                    var HttpModel = HttpServiceHelper.Instance.PostRequestForResultModel(addTagTypeApi, addTagTypeJson);

                    if (HttpModel?.status != "1")
                    {
                        Messages.ShowMessage(HttpModel?.message);
                        return;
                    }

                    var resModel = JsonUtil.DeserializeFromString<TagTypeModel>(JsonUtil.SerializeToString(HttpModel?.data));

                    string addApi = "/api/tag/taggrouptagadd";

                    dic = new Dictionary<string, string>();
                    dic.Add("taggroup_id", resModel.id);
                    var lst = Tags.Where(t => t.LabelIsSelected).Select(t => t.LabelId).ToList();
                    dic.Add("tags", string.Join(",", lst));

                    string json = JsonUtil.SerializeToString(dic);

                    var res = HttpServiceHelper.Instance.PostRequestForStatus(addApi, json);

                    if (res)
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Savesuccess"));
                        labelTypeAddOrEditView.Close();

                        getDataAction?.Invoke(10,1);
                    }
                    else
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Savefailed"));
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }

            }
            else if(flag ==1)
            {
                try
                {
                    if (TagTypeName != editType.name)
                    {
                        string editApi = "/api/tag/taggroupup";

                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("name", TagTypeName);
                        dic.Add("id", editType.id);

                        string json = JsonUtil.SerializeToString(dic);

                        var res = HttpServiceHelper.Instance.PostRequestForStatus(editApi, json);
                        if (!res)
                        {
                            Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Savefailed"));
                        }
                    }

                    string editTagsApi = "/api/tag/taggrouptagadd";
                    Dictionary<string,string> dicTags = new Dictionary<string, string>();
                    dicTags.Add("taggroup_id", editType.id);
                    var lst = Tags.Where(t => t.LabelIsSelected).Select(t => t.LabelId).ToList();
                    dicTags.Add("tags", string.Join(",", lst));

                    string jsonTags = JsonUtil.SerializeToString(dicTags);
                    var resTags = HttpServiceHelper.Instance.PostRequestForStatus(editTagsApi, jsonTags);

                    if (resTags)
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Savesuccess"));
                        labelTypeAddOrEditView.Close();

                        getDataAction?.Invoke(10,1);
                    }
                    else
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Savefailed"));
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
            }
        }

        private void OnTagItemCheckedCommand(LabelInfoModel tag)
        {
            if (!tag.LabelIsSelected)
            {
                IsCheckedAll = false;
                
            }
            else
            {
                if (Tags.Count == Tags.Count(t => t.LabelIsSelected))
                {
                    IsCheckedAll = true;
                }
            }
        }

        private void OnIsCheckedAllCommand(bool isCheckedAll)
        {
            Tags.ForEach(t => t.LabelIsSelected = isCheckedAll);
        }
        #endregion
    }
}
