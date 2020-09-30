using Mmc.Wpf.Mvvm;
using System;
using System.Windows.Input;

namespace Mmc.Wpf.Commands
{
	public class ParameterCommand : BindableBase, ICommand
	{
		private object _parameter;

		public event EventHandler CanExecuteChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		public object Parameter
		{
			get
			{
				return this._parameter;
			}
			set
			{
				this._parameter = value;
				base.NotifyPropertyChanged("Parameter");
			}
		}

		public virtual bool CanExecute(object parameter)
		{
			return true;
		}

		public virtual void Execute(object parameter)
		{
		}
	}
}
