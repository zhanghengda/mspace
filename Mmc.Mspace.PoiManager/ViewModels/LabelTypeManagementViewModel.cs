using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Gvitech.CityMaker.RenderControl;
using Microsoft.Vbe.Interop;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using Application = System.Windows.Application;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class LabelTypeManagementViewModel : BaseViewModel
    {
        private LabelTypeManagementView labelTypeManagement;
        public LabelTypeManagementViewModel()
        {
            if (labelTypeManagement == null)
                labelTypeManagement = new LabelTypeManagementView();
            labelTypeManagement.Owner = Application.Current.MainWindow;
            labelTypeManagement.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            labelTypeManagement.DataContext = this;
            labelTypeManagement.Show();

            GetData();
        }

        #region Property

        private ObservableCollection<TagTypeModel> _tagTypes;

        public ObservableCollection<TagTypeModel> TagTypes
        {
            get { return _tagTypes ?? (_tagTypes = new ObservableCollection<TagTypeModel>()); }
            set { _tagTypes = value; OnPropertyChanged("TagTypes"); }
        }

        private string _currentPageStatus;
        public string CurrentPageStatus
        {
            get { return _currentPageStatus; }
            set { _currentPageStatus = value;OnPropertyChanged("CurrentPageStatus"); }
        }

        private string _turnToPageNum;
        public string TurnToPageNum
        {
            get { return _turnToPageNum; }
            set { _turnToPageNum = value; OnPropertyChanged("TurnToPageNum");}
        } 

        private int Total;
        private int PageTotal;
        private int CurrentPage;
        #endregion

        #region Command

        private RelayCommand _closeCommand;

        public RelayCommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(OnCloseCommand)); }
            set { _closeCommand = value; }
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(OnAddCommand)); }
            set { _addCommand = value; }
        }

        private RelayCommand<TagTypeModel> _editCommand;

        public RelayCommand<TagTypeModel> EditCommand
        {
            get { return _editCommand ?? (_editCommand = new RelayCommand<TagTypeModel>(OnEditCommand)); }
            set { _editCommand = value; }
        }

        private RelayCommand<TagTypeModel> _deleteCommand;

        public RelayCommand<TagTypeModel> DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand<TagTypeModel>(OnDeleteCommand)); }
            set { _deleteCommand = value; }
        }

        private RelayCommand _pageTopCommand;

        public RelayCommand PageTopCommand
        {
            get { return _pageTopCommand ?? (_pageTopCommand = new RelayCommand(OnPageTopCommand)); }
        }

        private RelayCommand _pagePreCommand;

        public RelayCommand PagePreCommand
        {
            get { return _pagePreCommand ?? (_pagePreCommand = new RelayCommand(OnPagePreCommand)); }
        }

        private RelayCommand _pageNextCommand;
        public RelayCommand PageNextCommand
        {
            get { return _pageNextCommand ?? (_pageNextCommand = new RelayCommand(OnPageNextCommand)); }
            set { _pageNextCommand = value; }
        }

        private RelayCommand _pageEndCommand;
        public RelayCommand PageEndCommand
        {
            get { return _pageEndCommand ?? (_pageEndCommand = new RelayCommand(OnPageEndCommand)); }
            set { _pageEndCommand = value; }
        } 

        private RelayCommand _pageTurnToCommand;
        public RelayCommand PageTurnToCommand
        {
            get { return _pageTurnToCommand ?? (_pageTurnToCommand = new RelayCommand(OnPageTurnToCommand)); }
            set { _pageTurnToCommand = value; }
        }
        #endregion

        #region Method

        private void GetData(int pageSize=10, int page=1)
        {
            TagTypes.Clear();

            foreach (var item in MarkerHelper.Instance.GetTagTypesSource(pageSize,page))
            {
                TagTypeModel model = new TagTypeModel
                {
                    name = item.name,
                    id = item.id,
                    user_id = item.user_id,
                    IsChecked = false
                };
                foreach (var labelInfoModel in item.tags)
                {
                    LabelInfoModel labelModel = new LabelInfoModel
                    {
                        LabelId = labelInfoModel.id,
                        LabelName = labelInfoModel.name
                    };
                    model.tags.Add(labelModel);
                }

                TagTypes.Add(model);
            }

            #region 分页显示

            string pageApi = "/api/tag/taggrouplistcount";

            var dic = new Dictionary<string, int>();
            dic.Add("page_size", pageSize);

            string pageJson = JsonUtil.SerializeToString(dic);

            string pageData = HttpServiceHelper.Instance.PostRequestForData(pageApi, pageJson);

            var pageRes = JsonUtil.DeserializeFromString<dynamic>(pageData);

            Total = pageRes.total;
            PageTotal = pageRes.pageNum;
            CurrentPage = page;

            if (PageTotal == 0)
            {
                CurrentPageStatus = $"0/0";
            }
            else
            {
                CurrentPageStatus = $"{page}/{PageTotal}";
            }

            #endregion
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        private void OnCloseCommand()
        {
            labelTypeManagement.Close();
            labelTypeManagement = null;
        }

        /// <summary>
        /// 添加标签类型
        /// </summary>
        private void OnAddCommand()
        {
            LabelTypeAddOrEditViewModel labelTypeAdd = new LabelTypeAddOrEditViewModel(GetData);
        }
        


        /// <summary>
        /// 删除标签类型
        /// </summary>
        /// <param name="tagType"></param>
        private void OnDeleteCommand(TagTypeModel tagType)
        {
            if (Messages.ShowMessageDialog("删除标签类型", $"是否删除标签类型「{tagType?.name}」"))
            {
                string api = "/api/tag/taggroupdel";

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("taggroup_id", tagType?.id);

                string json = JsonUtil.SerializeToString(dic);

                var res = HttpServiceHelper.Instance.PostRequestForStatus(api, json);

                if (res)
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("DeleteSuccess"));
                    GetData();
                }
                else
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("DeleteFailed"));
                }
            }
        }

        /// <summary>
        /// 编辑标签类型
        /// </summary>
        /// <param name="tagType"></param>
        private void OnEditCommand(TagTypeModel tagType)
        {
            LabelTypeAddOrEditViewModel labelTypeEdit = new LabelTypeAddOrEditViewModel(GetData,tagType);
        }

        /// <summary>
        /// 分页最前页
        /// </summary>
        private void OnPageTopCommand()
        {
            GetData(page:1);
        }

        /// <summary>
        /// 分页前一页
        /// </summary>
        private void OnPagePreCommand()
        {
            if (CurrentPage - 1 > 0)
                GetData(page: CurrentPage - 1);
        }

        /// <summary>
        /// 分页下一页
        /// </summary>
        private void OnPageNextCommand()
        {
            if (CurrentPage + 1 <= PageTotal)
                GetData(page: CurrentPage + 1);
        }

        /// <summary>
        /// 分页最后一页
        /// </summary>
        private void OnPageEndCommand()
        {
            GetData(page: PageTotal);
        }

        /// <summary>
        /// 分页跳转
        /// </summary>
        private void OnPageTurnToCommand()
        {
            if (int.TryParse(TurnToPageNum, out int PageNum) && PageNum <= PageTotal && PageNum >= 1)
            {
                if (PageNum != CurrentPage)
                {
                    GetData(page: PageNum);
                }
            }
            else
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalIntNumber"));
            }
        }
        #endregion


    }
}
