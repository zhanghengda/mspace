using Mmc.Windows.Framework.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mmc.Windows.Framework.Commands
{
	public abstract class AsyncCommandBase : NotifyObject, IAsyncCommand, ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		public abstract bool CanExecute(object parameter);

		public abstract Task ExecuteAsync(object parameter);

		public async void Execute(object parameter)
		{
			await this.ExecuteAsync(parameter);
		}

		protected void RaiseCanExecuteChanged()
		{
			CommandManager.InvalidateRequerySuggested();
		}
	}
}
