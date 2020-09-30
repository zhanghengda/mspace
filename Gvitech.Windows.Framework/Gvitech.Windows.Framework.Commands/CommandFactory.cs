using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mmc.Windows.Framework.Commands
{
	public class CommandFactory
	{
		public static AsyncCommand<object> Create(Func<Task> command)
		{
			return new AsyncCommand<object>(async delegate (CancellationToken _)
            {
                await command();
                return null;
            });
		}

		public static AsyncCommand<TResult> Create<TResult>(Func<Task<TResult>> command)
		{
			return new AsyncCommand<TResult>((CancellationToken _) => command());
		}

		public static AsyncCommand<object> Create(Func<CancellationToken, Task> command)
		{
			return new AsyncCommand<object>(async delegate (CancellationToken token)
            {
                await command(token);
                return null;
            });
		}

		public static AsyncCommand<TResult> Create<TResult>(Func<CancellationToken, Task<TResult>> command)
		{
			return new AsyncCommand<TResult>(command);
		}
	}
}
