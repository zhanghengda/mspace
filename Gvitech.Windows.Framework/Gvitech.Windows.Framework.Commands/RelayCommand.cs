//using System;
//using System.Windows.Input;

//namespace Mmc.Windows.Framework.Commands
//{
//	public class RelayCommand<T> : ICommand
//	{
//		private readonly Predicate<T> canExecute;

//		private readonly Action<T> execute;

//		public event EventHandler CanExecuteChanged
//		{
//			add
//			{
//				if (this.canExecute != null)
//				{
//					CommandManager.RequerySuggested += value;
//				}
//			}
//			remove
//			{
//				if (this.canExecute != null)
//				{
//					CommandManager.RequerySuggested -= value;
//				}
//			}
//		}

//		public RelayCommand(Action<T> execute)
//		{
//			if (execute == null)
//			{
//				throw new ArgumentNullException("execute");
//			}
//			this.execute = execute;
//		}

//		public RelayCommand(Action<T> execute, Predicate<T> canExecute)
//		{
//			if (execute == null)
//			{
//				throw new ArgumentNullException("execute");
//			}
//			this.execute = execute;
//			this.canExecute = canExecute;
//		}

//		public bool CanExecute(object parameter)
//		{
//			return (parameter == null || parameter is T) && (this.canExecute == null || this.canExecute((T)((object)parameter)));
//		}

//		public void Execute(object parameter)
//		{
//			this.execute((T)((object)parameter));
//		}
//	}
//	public class RelayCommand : ICommand
//	{
//		private readonly Func<bool> canExecute;

//		private readonly Action execute;

//		private readonly Action<object> execute2;

//		public event EventHandler CanExecuteChanged
//		{
//			add
//			{
//				if (this.canExecute != null)
//				{
//					CommandManager.RequerySuggested += value;
//				}
//			}
//			remove
//			{
//				if (this.canExecute != null)
//				{
//					CommandManager.RequerySuggested -= value;
//				}
//			}
//		}

//		public RelayCommand(Action execute)
//		{
//			if (execute == null)
//			{
//				throw new ArgumentNullException("execute");
//			}
//			this.execute = execute;
//		}

//		public RelayCommand(Action execute, Func<bool> canExecute)
//		{
//			if (execute == null)
//			{
//				throw new ArgumentNullException("execute");
//			}
//			this.execute = execute;
//			this.canExecute = canExecute;
//		}

//		public RelayCommand(Action<object> execute)
//		{
//			if (execute == null)
//			{
//				throw new ArgumentNullException("execute");
//			}
//			this.execute2 = execute;
//		}

//		public bool CanExecute(object parameter)
//		{
//			return this.canExecute == null || this.canExecute();
//		}

//		public void Execute(object parameter)
//		{
//			if (parameter == null)
//			{
//				this.execute();
//			}
//			else
//			{
//				this.execute2(parameter);
//			}
//		}
//	}
//}

using System;
using System.Windows.Input;

namespace Mmc.Windows.Framework.Commands
{
    /// <summary>
    ///     Class RelayCommand.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : ICommand
    {
        /// <summary>
        ///     The can execute
        /// </summary>
        private readonly Predicate<T> canExecute;

        /// <summary>
        ///     The execute
        /// </summary>
        private readonly Action<T> execute;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="System.ArgumentNullException">execute</exception>
        public RelayCommand(Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <exception cref="System.ArgumentNullException">execute</exception>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        ///     当出现影响是否应执行该命令的更改时发生。
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        ///     定义用于确定此命令是否可以在其当前状态下执行的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。 如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        /// <returns>如果可以执行此命令，则为 true；否则为 false。</returns>
        public bool CanExecute(object parameter)
        {
            if (parameter != null && !(parameter is T))
                return false;
            return canExecute == null ? true : canExecute((T)parameter);
        }

        /// <summary>
        ///     定义在调用此命令时调用的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。 如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public void Execute(object parameter)
        {
            execute((T)parameter);
        }
    }

    /// <summary>
    ///     Class RelayCommand.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        ///     The can execute
        /// </summary>
        private readonly Func<bool> canExecute;

        /// <summary>
        ///     The execute
        /// </summary>
        private readonly Action execute;

        /// <summary>
        ///     The execute
        /// </summary>
        private readonly Action<object> execute2;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="System.ArgumentNullException">execute</exception>
        public RelayCommand(Action execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <exception cref="System.ArgumentNullException">execute</exception>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="System.ArgumentNullException">execute</exception>
        public RelayCommand(Action<object> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            execute2 = execute;
        }

        /// <summary>
        ///     当出现影响是否应执行该命令的更改时发生。
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        ///     定义用于确定此命令是否可以在其当前状态下执行的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。 如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        /// <returns>如果可以执行此命令，则为 true；否则为 false。</returns>
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        /// <summary>
        ///     定义在调用此命令时调用的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。 如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public void Execute(object parameter)
        {
            if (parameter == null)
                execute();
            else
                execute2(parameter);
        }
    }
}
