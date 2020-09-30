using System;
using System.Windows.Input;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using Gvitech.Framework.Services;

namespace Gvitech.Framework.Winform.Commands
{
	// Token: 0x02000002 RID: 2
	public abstract class SimpleCommand : ICommand
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public SimpleCommand()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205A File Offset: 0x0000025A
		public SimpleCommand(string cmdName)
		{
			this.CmdName = cmdName;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000206C File Offset: 0x0000026C
		public virtual RenderControl MapControl
		{
			get
			{
				return GviMap.MapControl;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002084 File Offset: 0x00000284
		public virtual AxRenderControl AxMapControl
		{
			get
			{
				return GviMap.AxMapControl;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000209B File Offset: 0x0000029B
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020A3 File Offset: 0x000002A3
		public string CmdName { get; set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x000020AC File Offset: 0x000002AC
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x000020AC File Offset: 0x000002AC
		public virtual event EventHandler CanExecuteChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020B0 File Offset: 0x000002B0
		public virtual bool CanExecute(object parameter)
		{
			return true;
		}

		// Token: 0x0600000A RID: 10
		public abstract void Execute(object parameter);
	}
}
