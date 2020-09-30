using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mmc.Mspace.ToolModule.DynamicClip
{
    class RenameObjectVModel : BaseViewModel
    {
        public Action<string,string> ReSectionName;
        private RenameObjectView renameObjectView;
        public DynamicClipVModel ClipVModel = null;
        private string _reNameObject;
        public string ReNameObject
        {
            get { return _reNameObject; }
            set
            {
                _reNameObject = value; OnPropertyChanged("ReNameObject");
            }
        }

        private string _reNameOldObject;
        public string ReNameOldObject
        {
            get { return _reNameOldObject; }
            set
            {
                _reNameOldObject = value; OnPropertyChanged("ReNameOldObject");
            }
        }

        private ObservableCollection<ClipData> _renameclipDataColletion = new ObservableCollection<ClipData>();
        public ObservableCollection<ClipData> RenameClipDataColletion
        {
            get { return _renameclipDataColletion; }
            set
            {
                _renameclipDataColletion = value; 
                OnPropertyChanged("RenameClipDataColletion");
            }
        }


        private string _reNameOldObjectGuid;
        public string ReNameOldObjectGuid
        {
            get { return _reNameOldObjectGuid; }
            set
            {
                _reNameOldObjectGuid = value; OnPropertyChanged("ReNameOldObjectGuid");
            }
        }
        public ICommand RenameClipDataCmd { get; set; }

        public ICommand CloseRenameClipCmd { get; set; }

        public ICommand DisposeCmd2 { get; set; }

        public RenameObjectVModel(string oldName, ObservableCollection<ClipData> clipDataColletion,string clipDataItemGuid)//gviClipMode gviClipMode, DataTable _dtClipData
        {
            ReNameObject = oldName;
            ReNameOldObject = oldName;
            ReNameOldObjectGuid = clipDataItemGuid;
            RenameClipDataColletion = clipDataColletion;
            renameObjectView = new RenameObjectView();
            renameObjectView.DataContext = this;
            this.CloseRenameClipCmd = new RelayCommand(() =>
            {
                CloseView();
            });
            this.DisposeCmd2 = new RelayCommand(() =>
            {
                CloseView();
            });
            this.RenameClipDataCmd  = new RelayCommand(() =>
            {
                //foreach (Cl row in _dtClipData.Rows)
                //{
                //    if (Convert.ToString(row["ColName"]) == ReNameObject)
                //    {
                //        Messages.ShowMessage("该名称已存在！");
                //        return;
                //    }
                //}
                RenameClipData();
               // CloseView();
            });
        }

        private void RenameClipData()
        {
            var model=RenameClipDataColletion.FirstOrDefault(t =>t.Name== ReNameObject&& t.Guid != ReNameOldObjectGuid);
            if(model==null)
            {
                ReSectionName(ReNameObject, ReNameOldObjectGuid);
                CloseView();
            }
            else
            {
                Messages.ShowMessage("已存在该名称！");
            }
        }

        public void ReName()
        {

            if (string.IsNullOrEmpty(ReNameObject))
            {
                Messages.ShowMessage("请填写重命名");
                return;
            }
            ReSectionName(ReNameObject, ReNameOldObject);

            CloseView();
        }
        public void ShowView()
        {
            if (renameObjectView == null)
                renameObjectView = new RenameObjectView();



            renameObjectView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            renameObjectView.ShowDialog();
        }


        public void CloseView()
        {
            //if (renameObjectView == null)
            //    renameObjectView = new RenameObjectView();
            if (renameObjectView != null)
            {
                renameObjectView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                renameObjectView.Close();
            }
            
        }
    }
}
