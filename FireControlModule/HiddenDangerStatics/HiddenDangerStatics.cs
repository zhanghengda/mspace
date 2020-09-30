using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Utils;
using Mmc.Framework.Services;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;

namespace FireControlModule
{
    public class HiddenDangerStatics
    {
        private List<IRenderable> _rObjs = new List<IRenderable>();
        private List<string> _fids = new List<string>();

        /// <summary>
        /// 监督检查
        /// </summary>
        /// <param name="StaticsInfos">
        /// Dictionary<string,string>  buildCode,type
        /// </param>
        public void ShowSupervisoryReviewStatics()
        {
            CloseStatics();
            var layers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
            layers.ForEach(p =>
            {
                if (p.Fc.Alias == "建筑")
                {
                    Random random = new Random();

                    //先清除高亮
                    p.UnHighLightFeatures(_fids.ToArray(), GviMap.FeatureManager);
                    _fids.Clear();
                    var fc = p.Fc;
                    var filter = new QueryFilter();
                    filter.AddSubField("oid");
                    var cursor = fc.Search(filter, false);
                    IRowBuffer row = null;
                    while ((row = cursor.NextRow()) != null)
                    {
                        var fid = row.GetFid().ParseTo<string>();
                        _fids.Add(fid);
                        var type = random.Next(1, 3).ToString();

                        if (type == "1")
                        {
                            p.HighLightFeature(fid, GviMap.FeatureManager, GviColors.OrangeRed);
                        }
                        else if (type == "2")
                        {
                            p.HighLightFeature(fid, GviMap.FeatureManager, GviColors.Green);
                        }
                        row.ReleaseComObject();
                    }
                    cursor.ReleaseComObject();
                    filter.ReleaseComObject();
                }
            });
        }

        public void CloseStatics()
        {
            GviMap.ObjectManager.ReleaseRenderObject(_rObjs.ToArray());
            var layers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
            layers.ForEach(p =>
            {
                if (p.Fc.Alias == "建筑")
                {
                    //先清除高亮
                    p.UnHighLightFeatures(_fids.ToArray(), GviMap.FeatureManager);
                    _fids.Clear();
                }
            });
        }
    }
}