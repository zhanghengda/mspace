using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Gvitech.CityMaker.Math;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.PoliceDeployedModule.PoliceDeployed
{
	public class Police
	{
		public Police(ObservableCollection<Police> polices)
		{
			this.Polices = polices;
			this.DeleteFromDeployCmd = new RelayCommand(() =>
            {
				this.Polices.Remove(this);
			});
		}

		public ICommand DeleteFromDeployCmd { get; set; }
		public string Name { get; set; }

		public string ID { get; set; }

		public IVector3 Position { get; set; }

		public double Distance { get; set; }

        /// <summary>
        /// 花费时间
        /// </summary>
        public string UseTime { get; set; }

        private readonly ObservableCollection<Police> Polices;
	}
}
