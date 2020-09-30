using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Models.MovePoi;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.MovePoiService
{
    public interface IMovePoiService
    {
        IPOIFeatureClass this[string fcAliasName]
        {
            get;
        }

        void StartWork();

        void PauseWork();

        void ContinueWork();

        void StopWork();

        Dictionary<string, List<LayerItemModel>> GroupLayerItemModels();

        void TestUrl();
    }
}