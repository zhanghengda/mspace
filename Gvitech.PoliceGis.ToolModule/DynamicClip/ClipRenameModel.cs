using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mmc.Mspace.ToolModule.DynamicClip
{
    class ClipRenameModel : CheckedToolItemModel
    {
        public string newname = "";
        [XmlIgnore]
        public System.Windows.Input.ICommand GetName { get; set; }
        public override void Initialize()
        {
            ClipRename clipRename = new ClipRename();
            clipRename.Show();
       this.GetName = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                if (_clipname != "")
                {
                    newname = _clipname;
                }
                

                if (clipRename != null)
                {
                    clipRename.Hide();
                }
                
            });
           
        }
        private string _clipname;

        public string Clipname
        {
            get { return _clipname; }
            set {
                _clipname = value; NotifyPropertyChanged("Clipname");
                _clipname = newname;
            }
        }
    }
}
