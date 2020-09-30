using CityMakerBuilder.AddIn.Core;
using System;
using System.Collections.Generic;

namespace Gvitech.Framework.Winform.Commands
{
	// Token: 0x02000003 RID: 3
	public static class CommandsManager
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020C4 File Offset: 0x000002C4
		public static void RegisterCmd(string key, ICommand cmd)
		{
			bool flag = cmd != null && !CommandsManager.Cmds.ContainsKey(key);
			if (flag)
			{
				CommandsManager.Cmds.Add(key, cmd);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020FC File Offset: 0x000002FC
		public static void RegisterCmd(SimpleCommand cmd)
		{
			bool flag = cmd != null && !CommandsManager.Cmds.ContainsKey(cmd.CmdName);
			if (flag)
			{
				CommandsManager.Cmds.Add(cmd.CmdName, (ICommand)cmd);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000213C File Offset: 0x0000033C
		public static ICommand GetCmd(string key)
		{
			bool flag = CommandsManager.Cmds.ContainsKey(key);
			ICommand result;
			if (flag)
			{
				result = CommandsManager.Cmds[key];
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000002 RID: 2
		private static readonly Dictionary<string, ICommand> Cmds = new Dictionary<string, ICommand>();
	}
}
