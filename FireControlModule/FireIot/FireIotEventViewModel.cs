using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Utils;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using System.Collections.Generic;
using System.Drawing;

namespace FireControlModule.FireIot
{
    public class FireIotEventViewModel : CheckedToolItemModel
    {
        private IDisplayLayer _dLyr;
        private List<IRenderable> _rObjs;

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            _rObjs = new List<IRenderable>();
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void OnChecked()
        {
            base.OnChecked();
            if (_dLyr == null)
            {
                var actaulLyrs = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
                actaulLyrs.ForEach(p => { if (p.Fc.Alias == "建筑") _dLyr = p; });
            }

            List<string> buildCodes = new List<string>();
            buildCodes.Add("4403050080040500010");
            //buildCodes.Add("4403050080040500071");
            //buildCodes.Add("4403050080040500072");
            //buildCodes.Add("4403050080040500017");
            var filter = new QueryFilter();
            filter.WhereClause = string.Format("Name='{0}'", "4403050080040500010");
            filter.AddSubField("geometry");
            filter.AddSubField("Name");
            IModelPointSymbol mpsym = new ModelPointSymbol();
            mpsym.Color = Color.Red;
            mpsym.EnableColor = true;
            mpsym.SetResourceDataSet(_dLyr.Fc.FeatureDataSet);
            var cr = _dLyr.Fc.Search(filter, true);
            IRowBuffer row = null;
            while ((row = cr.NextRow()) != null)
            {
                var geo = row.GetValue<IModelPoint>(0);
                //geo.SelfScale(1.05, 1.05, 1.05);
                var rMp = GviMap.ObjectManager.CreateRenderModelPoint(geo, mpsym);
                rMp.Glow(-1);
                rMp.VisibleMask = gviViewportMask.gviViewAllNormalView;
                _rObjs.Add(rMp);
            }
            cr.ReleaseComObject();
            filter.ReleaseComObject();
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            GviMap.ObjectManager.ReleaseRenderObject(_rObjs.ToArray());
            _rObjs.Clear();
        }
    }
}