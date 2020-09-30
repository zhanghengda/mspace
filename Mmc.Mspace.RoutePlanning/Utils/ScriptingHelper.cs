using Mmc.Mspace.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmc.DataSourceAccess;

namespace Mmc.Mspace.RoutePlanning.Utils
{
    public delegate void Convey(string[] missionJson);
    public class ScriptingHelper : IJsScriptInvokerService
    {
        public Convey convey;
        public Action<string, string> ColorVisibleEvent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void ReadMissionJson(string name, string strJson)
        {
            string[] missionJson = { name, strJson };
            convey(missionJson);
        }
        public IRenderLayer AddImageLayer(string fileName, bool issafe)
        {
            throw new NotImplementedException();
        }

        public void collopsePanel(bool isCollopse)
        {
            throw new NotImplementedException();
        }

        public void colorVisible(string color, string visible)
        {
            throw new NotImplementedException();
        }

        public void deleteImgRenderLayer(string LayerGuid)
        {
            throw new NotImplementedException();
        }

        public void deleteMarkerPoi(string poiId)
        {
            throw new NotImplementedException();
        }

        public void flyToBuild(string buildCode)
        {
            throw new NotImplementedException();
        }

        public void flyToBuild(string layerName, string buildCode)
        {
            throw new NotImplementedException();
        }

        public void flyToRederLayer(string LayerGuid)
        {
            throw new NotImplementedException();
        }

        public void flyToRegion(string regionCode, string regionType)
        {
            throw new NotImplementedException();
        }

        public void flyToUnit(string unitCode)
        {
            throw new NotImplementedException();
        }

        public void openUnitVideo(string unitCode)
        {
            throw new NotImplementedException();
        }

        public void saveCameraParam(string unitCode)
        {
            throw new NotImplementedException();
        }

        public void SetAllLayerUnvisible()
        {
            throw new NotImplementedException();
        }

        public void setCompareViewState(int compareViewState)
        {
            throw new NotImplementedException();
        }

        public void setLayersVisible(string layerNames, bool isVisilbe)
        {
            throw new NotImplementedException();
        }

        public void setLayerVisible(string layerName, bool isVisilbe)
        {
            throw new NotImplementedException();
        }

        public void showBuildDetail(string buildCode)
        {
            throw new NotImplementedException();
        }

        public void showPeopleDetial(string peopleCode)
        {
            throw new NotImplementedException();
        }

        public void showUnitDetail(string unitCode)
        {
            throw new NotImplementedException();
        }
    }
}
