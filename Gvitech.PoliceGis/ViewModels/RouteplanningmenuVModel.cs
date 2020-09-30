using Mmc.Mspace.Common.Messenger;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.MSpace.ViewModels
{
    public class RouteplanningmenuVModel: BindableBase
    {
        public RouteplanningmenuVModel()
        {

        }

        private RelayCommand editwaypointCommand;

        public RelayCommand EditwaypointCommand
        {
            get { return editwaypointCommand??(editwaypointCommand=new RelayCommand (OnEditwaypointCommand)); }
            set { editwaypointCommand = value; }
        }

        private RelayCommand _editCommand;
        private RelayCommand _routePlanCommand;
        private RelayCommand _hangxianEditCommand;

        public RelayCommand EditCommand
        {
            get { return _editCommand??(_editCommand=new RelayCommand (OnEditCommand)); }
            set { _editCommand = value; }
        }

        public RelayCommand RoutePlanCommand
        {
            get { return _routePlanCommand ?? (_routePlanCommand = new RelayCommand(OnRoutePlanCommand)); }
            set { _routePlanCommand = value; }
        }

        public RelayCommand HangxianEditCommand
        {
            get { return _hangxianEditCommand ?? (_hangxianEditCommand = new RelayCommand(OnHangxianEditCommand)); }
            set { _hangxianEditCommand = value; }
        }

        private bool _editwaypointOpen;

        public bool EditwaypointOpen
        {
            get { return _editwaypointOpen; }
            set { _editwaypointOpen = value;base.NotifyPropertyChanged("EditwaypointOpen"); }
        }


        private void OnEditwaypointCommand()
        {
            EditwaypointOpen = true;
        }


        private void OnEditCommand()
        {
        
        }

        private void OnRoutePlanCommand()
        {
            Messenger.Messengers.Notify("ShowRoutePlanView");
        }

        private void OnHangxianEditCommand()
        {
            Messenger.Messengers.Notify("ShowHangdianEditView");
        }
    }
}
