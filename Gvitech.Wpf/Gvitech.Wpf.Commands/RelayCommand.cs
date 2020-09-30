using System;
using System.Windows.Input;

namespace Mmc.Wpf.Commands
{
	public class RelayCommand<T> : ICommand
	{
		private readonly Predicate<T> _canExecute;

		private readonly Action<T> _execute;

		public event EventHandler CanExecuteChanged
		{
			add
			{
				if (this._canExecute != null)
				{
					CommandManager.RequerySuggested += value;
				}
			}
			remove
			{
				if (this._canExecute != null)
				{
					CommandManager.RequerySuggested -= value;
				}
			}
		}

		public RelayCommand(Action<T> execute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
			this._execute = execute;
		}

		public RelayCommand(Action<T> execute, Predicate<T> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
			this._execute = execute;
			this._canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return (parameter == null || parameter is T) && (this._canExecute == null || this._canExecute((T)((object)parameter)));
		}

		public void Execute(object parameter)
		{
			this._execute((T)((object)parameter));
		}
	}
	public class RelayCommand : ICommand
	{
		private readonly Func<bool> _canExecute;

		private readonly Action _execute;

		private readonly Action<object> _execute2;

		public event EventHandler CanExecuteChanged
		{
			add
			{
				if (this._canExecute != null)
				{
					CommandManager.RequerySuggested += value;
				}
			}
			remove
			{
				if (this._canExecute != null)
				{
					CommandManager.RequerySuggested -= value;
				}
			}
		}

		public RelayCommand(Action execute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
			this._execute = execute;
		}

		public RelayCommand(Action execute, Func<bool> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
			this._execute = execute;
			this._canExecute = canExecute;
		}

		public RelayCommand(Action<object> execute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
			this._execute2 = execute;
		}

		public bool CanExecute(object parameter)
		{
			return this._canExecute == null || this._canExecute();
		}

		public void Execute(object parameter)
		{
			if (parameter == null)
			{
				this._execute();
			}
			else
			{
				this._execute2(parameter);
			}
		}
	}
}
