using System;
using System.Windows;
using System.Windows.Controls;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;

namespace Mmc.Mspace.Main.DTSelector
{
	// Token: 0x02000007 RID: 7
	public class BarDataTemplateSelector : DataTemplateSelector
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002414 File Offset: 0x00000614
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			BarModel barModel = item as BarModel;
			bool flag = barModel == null;
			DataTemplate result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DataTemplate dataTemplate = ServiceManager.GetService<IShellService>(null).ShellWindow.FindResource("BarIconBtnDataTemplate") as DataTemplate;
				result = dataTemplate;
			}
			return result;
		}
	}
}
