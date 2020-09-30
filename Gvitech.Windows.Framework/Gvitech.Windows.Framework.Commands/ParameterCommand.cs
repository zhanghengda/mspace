using Mmc.Windows.Framework.Core;
using System;
using System.Windows.Input;

namespace Mmc.Windows.Framework.Commands
{
	public class ParameterCommand : NotifyObject, ICommand
	{
		private object parameter;

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
				return this.parameter;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<object>(ref this.parameter, value, "Parameter");
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
