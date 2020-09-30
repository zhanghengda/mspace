using System;
using System.Drawing;
using Gvitech.CityMaker.RenderControl;
using Gvitech.Windows.Utils;
using Mmc.Framework.Services;

namespace Mmc.Mspace.ToolModule.DynamicClip
{
    // Token: 0x02000008 RID: 8
    public class ClipManagerCmd
    {
        // Token: 0x06000029 RID: 41 RVA: 0x00002340 File Offset: 0x00000540
        public void Run(object sender, EventArgs e)
        {

            GviMap.MapControl.SetRenderParam(gviRenderControlParameters.gviRenderParamClipPlaneLineColor, Color.Red);
            //GviMap.MapControl.SetRenderParam(gviRenderControlParameters.gviRenderParamClipPlaneLineColor, Color.Red);

        }

        // Token: 0x0600002A RID: 42 RVA: 0x0000239C File Offset: 0x0000059C
        /*public override ModelViewBase CreateUserControl()
		{
			ModelViewBase result;
			if ((result = this.ModelView) == null)
			{
				result = (this.ModelView = new UcClipManager());
			}
			return result;

		// Token: 0x0600002B RID: 43 RVA: 0x000023C1 File Offset: 0x000005C1
		public override DockPanel GetDockPanel()
		{
			return ServiceManager.GetService<IPanelEditService>(null).Panel;
		}
	}*/
    }
}
