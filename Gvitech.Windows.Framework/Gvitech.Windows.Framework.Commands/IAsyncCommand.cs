using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mmc.Windows.Framework.Commands
{
	public interface IAsyncCommand : ICommand
	{
		Task ExecuteAsync(object parameter);
	}
}
