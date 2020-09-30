using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mmc.Windows.Framework.Commands
{
	public sealed class AsyncCommand<TResult> : AsyncCommandBase, IDisposable
	{
		private sealed class CancelAsyncCommand : ICommand, IDisposable
		{
			private bool _commandExecuting;

			private CancellationTokenSource _cts = new CancellationTokenSource();

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

			public CancellationToken Token
			{
				get
				{
					return this._cts.Token;
				}
			}

			bool ICommand.CanExecute(object parameter)
			{
				return this._commandExecuting && !this._cts.IsCancellationRequested;
			}

			void ICommand.Execute(object parameter)
			{
				this._cts.Cancel();
				this.RaiseCanExecuteChanged();
			}

			public void Dispose()
			{
				GC.SuppressFinalize(this);
			}

			public void NotifyCommandStarting()
			{
				this._commandExecuting = true;
				if (this._cts.IsCancellationRequested)
				{
					this._cts = new CancellationTokenSource();
					this.RaiseCanExecuteChanged();
				}
			}

			public void NotifyCommandFinished()
			{
				this._commandExecuting = false;
				this.RaiseCanExecuteChanged();
			}

			private void RaiseCanExecuteChanged()
			{
				CommandManager.InvalidateRequerySuggested();
			}
		}

		private readonly AsyncCommand<TResult>.CancelAsyncCommand _cancelCommand;

		private readonly Func<CancellationToken, Task<TResult>> _command;

		private NotifyTaskCompletion<TResult> _execution;

		public ICommand CancelCommand
		{
			get
			{
				return this._cancelCommand;
			}
		}

		public NotifyTaskCompletion<TResult> Execution
		{
			get
			{
				return this._execution;
			}
			private set
			{
				base.SetAndNotifyPropertyChanged<NotifyTaskCompletion<TResult>>(ref this._execution, value, "Execution");
			}
		}

		public AsyncCommand(Func<CancellationToken, Task<TResult>> command)
		{
			this._command = command;
			this._cancelCommand = new AsyncCommand<TResult>.CancelAsyncCommand();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public override bool CanExecute(object parameter)
		{
			return this.Execution == null || this.Execution.IsCompleted;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			this._cancelCommand.NotifyCommandStarting();
			this.Execution = new NotifyTaskCompletion<TResult>(this._command(this._cancelCommand.Token));
			base.RaiseCanExecuteChanged();
			await this.Execution.TaskCompletion;
			this._cancelCommand.NotifyCommandFinished();
			base.RaiseCanExecuteChanged();
		}
	}
}
