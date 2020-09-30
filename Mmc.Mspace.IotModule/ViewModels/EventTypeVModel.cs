using Mmc.Mspace.Common.Models;
using Mmc.Mspace.IotModule.Views;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Mmc.Mspace.IotModule.ViewModels
{
   public  class EventTypeVModel:CheckedToolItemModel
   {
        public Action OnDfeipai;
        public Action OnDshenhe;
        public Action OnDshoulie;
        public Action OnDbanjie;
        public Action OnDwanjie;
        public Action OnDYguidang;
        public Action OnShowAll;
        EventTypeView eventTypeView = new EventTypeView();
        public ICommand DfeipaiCmd { get; set; }
        public ICommand DshenheCmd { get; set; }
        public ICommand DshoulieCmd { get; set; }
        public ICommand DbanjieCmd { get; set; }
        public ICommand DwanjieCmd { get; set; }
        public ICommand DYguidangCmd { get; set; }
        public ICommand ShowAllCmd { get; set; }        
        public EventTypeVModel()
        {
            this.DfeipaiCmd = new RelayCommand(OnShowDfeipai);
            this.DshenheCmd = new RelayCommand(OnShowDshenhe);
            this.DshoulieCmd = new RelayCommand(OnShowDshoulie);
            this.DbanjieCmd = new RelayCommand(OnShowDbanjie);
            this.DwanjieCmd = new RelayCommand(OnShowDwanjie);
            this.DYguidangCmd = new RelayCommand(OnShowDYguidang);
            this.ShowAllCmd = new RelayCommand(OnShowAllEvents);
        }
        public void ShowEventTypeView()
        {
            eventTypeView.DataContext = this;
            eventTypeView.Show();
            eventTypeView.Left = Application.Current.MainWindow.Width * 0.7;
            eventTypeView.Top = Application.Current.MainWindow.Height * 0.8;
        }
        public void HideEventTypeView()
        {
            eventTypeView.Hide();
        }
        public void OnShowDfeipai()
        {
            OnDfeipai();
        }
        public void OnShowDshenhe()
        {
            OnDshenhe();
        }
        public void OnShowDshoulie()
        {
            OnDshoulie();
        }
        public void OnShowDbanjie()
        {
            OnDbanjie();
        }
        public void OnShowDwanjie()
        {
            OnDwanjie();
        }
        public void OnShowDYguidang()
        {
            OnDYguidang();
        }
        public void OnShowAllEvents()
        {
            OnShowAll();
        }
    }
}
