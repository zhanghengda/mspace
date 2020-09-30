using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.CoreModule;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireControlModule.UnitInfo
{
    /// <summary>
    /// 全景视图
    /// </summary>
    public class Full3dViewModel : WebViewModel
    {
        private ArchivesView _full3DView;

        public Full3dViewModel()
        {
            _full3DView = new ArchivesView() { Owner = ServiceManager.GetService<IShellService>().ShellWindow };
        }

        public override void OnChecked()
        {
            base.OnChecked();
          
        }



    }
}
