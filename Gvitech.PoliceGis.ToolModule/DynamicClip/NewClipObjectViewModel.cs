using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.ToolModule.DynamicClip
{
    public class NewClipObjectViewModel : BaseViewModel
    {
        private ObservableCollection<ClipData> _clipDataColletion=new ObservableCollection<ClipData>();
        public ObservableCollection<ClipData> ClipDataColletion
        {
            get { return _clipDataColletion; }
            set { _clipDataColletion = value; OnPropertyChanged("ClipDataColletion"); }
        }

        public NewClipObjectViewModel()
        {
            ///ClipDataColletion=
        }

        private void Add(ClipData clipItem)
        {
            var model=ClipDataColletion.FirstOrDefault(t => t.Guid == clipItem.Guid);
            if(model==null)
            {
                ClipDataColletion.Add(clipItem);
            }
        }

        private void Remove(ClipData clipItem)
        {
            var model = ClipDataColletion.FirstOrDefault(t => t.Guid == clipItem.Guid);
            if (model != null)
            {
                ClipDataColletion.Remove(clipItem);
            }
        }


        private void Rename(ClipData clipItem,string newName)
        {
            var model = ClipDataColletion.FirstOrDefault(t => t.Guid == clipItem.Guid);
            if (model != null)
            {
                model.Name = newName;
            }
        }
    }

}
