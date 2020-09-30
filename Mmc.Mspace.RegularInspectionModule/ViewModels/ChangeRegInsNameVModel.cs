using Mmc.Mspace.Common.Models;
using Mmc.Mspace.RegularInspectionModule.Dto;
using Mmc.Mspace.RegularInspectionModule.Views;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mmc.Mspace.RegularInspectionModule.ViewModels
{
    public class ChangeRegInsNameVModel : CheckedToolItemModel
    {
       
        public Action<InspectModel,string> UpdateName;
        public ChangeRegInsNameView changeRegInsNameView = new ChangeRegInsNameView();
        public ICommand CloseCmd { get; set; }
        public ICommand UpdateCmd { get; set; }
        private string _newName;            
        public ChangeRegInsNameVModel(InspectModel _inspectModel, string _name)
        {
            NewName = _name;
            changeRegInsNameView.DataContext = this;
            this.CloseCmd = new RelayCommand(() =>
            {
                CloseEditName();
            });
            this.UpdateCmd = new RelayCommand(() =>
            {
                UpdateName(_inspectModel, NewName);
                
            });

        }
   
        public void CloseEditName()
        {
            changeRegInsNameView.Hide();
        }
        public void OpenEditName()
        {
            changeRegInsNameView.Show();
        }
        public string NewName
        {
            get { return _newName; }
            set { _newName = value; NotifyPropertyChanged("NewName"); }
        }     
    }
}