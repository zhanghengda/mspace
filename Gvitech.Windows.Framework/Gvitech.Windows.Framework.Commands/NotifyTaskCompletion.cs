using Mmc.Windows.Framework.Core;
using System;
using System.Threading.Tasks;

namespace Mmc.Windows.Framework.Commands
{
	public sealed class NotifyTaskCompletion<TResult> : NotifyObject
	{
		public Task<TResult> Task
		{
			get;
			private set;
		}

		public Task TaskCompletion
		{
			get;
			private set;
		}

		public TResult Result
		{
			get
			{
				return (this.Task.Status == TaskStatus.RanToCompletion) ? this.Task.Result : default(TResult);
			}
		}

		public TaskStatus Status
		{
			get
			{
				return this.Task.Status;
			}
		}

		public bool IsCompleted
		{
			get
			{
				return this.Task.IsCompleted;
			}
		}

		public bool IsNotCompleted
		{
			get
			{
				return !this.Task.IsCompleted;
			}
		}

		public bool IsSuccessfullyCompleted
		{
			get
			{
				return this.Task.Status == TaskStatus.RanToCompletion;
			}
		}

		public bool IsCanceled
		{
			get
			{
				return this.Task.IsCanceled;
			}
		}

		public bool IsFaulted
		{
			get
			{
				return this.Task.IsFaulted;
			}
		}

		public AggregateException Exception
		{
			get
			{
				return this.Task.Exception;
			}
		}

		public Exception InnerException
		{
			get
			{
				return (this.Exception == null) ? null : this.Exception.InnerException;
			}
		}

		public string ErrorMessage
		{
			get
			{
				return (this.InnerException == null) ? null : this.InnerException.Message;
			}
		}

		public NotifyTaskCompletion(Task<TResult> task)
		{
			this.Task = task;
			if (!task.IsCompleted)
			{
				this.TaskCompletion = this.WatchTaskAsync(task);
			}
		}

		private async Task WatchTaskAsync(Task task)
		{
			try
			{
				await task;
			}
			catch
			{
			}
			base.NotifyPropertyChanged("Status");
			base.NotifyPropertyChanged("IsCompleted");
			base.NotifyPropertyChanged("IsNotCompleted");
			if (task.IsCanceled)
			{
				base.NotifyPropertyChanged("IsCanceled");
			}
			else if (task.IsFaulted)
			{
				base.NotifyPropertyChanged("IsFaulted");
				base.NotifyPropertyChanged("Exception");
				base.NotifyPropertyChanged("InnerException");
				base.NotifyPropertyChanged("ErrorMessage");
			}
			else
			{
				base.NotifyPropertyChanged("IsSuccessfullyCompleted");
				base.NotifyPropertyChanged("Result");
			}
		}
	}
}
