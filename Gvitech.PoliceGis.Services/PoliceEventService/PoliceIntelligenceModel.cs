using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Windows;
using System.Windows.Input;

namespace Mmc.Mspace.Services.PoliceEventService
{
    public class PoliceIntelligenceModel : BindableBase
    {
        public PoliceIntelligenceModel()
        {
            this.EditStateCmd = new RelayCommand(delegate (object p)
            {
                ProcessingState processingState = (ProcessingState)Enum.Parse(typeof(ProcessingState), p.ToString());
                this.ProcessingState = processingState;
            });
        }

        public string CaseCotent { get; set; }

        public string Informant { get; set; }

        public string ReportingTime { get; set; }

        public string CrimePlace { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public CaseType CaseType { get; set; }

        public ProcessingState ProcessingState
        {
            get
            {
                return this._processingState;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<ProcessingState>(ref this._processingState, value, "ProcessingState");
            }
        }

        public CaseState CaseState
        {
            get
            {
                return this._caseState;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<CaseState>(ref this._caseState, value, "CaseState");
            }
        }

        public string CaseId { get; set; }

        public Visibility IsVisible
        {
            get
            {
                return this._isVisible;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<Visibility>(ref this._isVisible, value, "IsVisible");
            }
        }

        public override string ToString()
        {
            return string.Concat(new string[]
            {
                "CaseId--  ",
                this.CaseId,
                "  Informant--  ",
                this.Informant,
                "  ReportingTime--  ",
                this.ReportingTime,
                "  CrimePlace--  ",
                this.CrimePlace,
                "\n"
            });
        }

        private CaseState _caseState;

        private Visibility _isVisible;

        private ProcessingState _processingState;

        [NonSerialized]
        public ICommand EditStateCmd;
    }
}