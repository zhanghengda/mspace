using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Design;
using Mmc.Windows.Utils;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Mmc.Mspace.Services.LayerGroupService
{
    public class LayerGroupService : Singleton<LayerGroupService>, ILayerGroupService
    {
        public LayerGroupService()
        {
            this.ResolveLayersConfig(this._fieldsFilterConfig);
        }

        protected LayersConfig LayersConfig { get; private set; }

        public string[] GetGroupNames()
        {
            bool flag = this.LayersConfig == null || !IEnumerableExtension.HasValues<LayerGroup>(this.LayersConfig.LayerGroups);
            string[] result;
            if (flag)
            {
                result = null;
            }
            else
            {
                result = (from @group in this.LayersConfig.LayerGroups
                          select @group.GroupName.ToLower()).ToArray<string>();
            }
            return result;
        }

        public string[] GetGroupLayers(string groupName)
        {
            bool flag = this.LayersConfig == null;
            string[] result;
            if (flag)
            {
                result = null;
            }
            else
            {
                LayerGroup layerGroup = this.LayersConfig.LayerGroups.FirstOrDefault((LayerGroup ff) => ff.GroupName.ToLower().Equals(groupName.ToLower()));
                result = ((layerGroup != null && !string.IsNullOrEmpty(layerGroup.Layers)) ? layerGroup.Layers.ToLower().Split(new char[]
                {
                    ',',
                    ';'
                }, StringSplitOptions.RemoveEmptyEntries) : null);
            }
            return result;
        }

        public void SetGroupLayers(string groupName, string layerName, bool isAdd)
        {
            var layerGroup = this.LayersConfig.LayerGroups.FirstOrDefault((ff) => ff.GroupName.ToLower().Equals(groupName.ToLower()));
            if (isAdd)
            {
                if (string.IsNullOrEmpty(layerGroup.Layers))
                    layerGroup.Layers = layerName;
                else
                    layerGroup.Layers = layerGroup.Layers + "," + layerName;
            }
            else
            {
                if (!layerGroup.Layers.Contains(","))
                    layerGroup.Layers.Replace(layerName, "");
                else
                    layerGroup.Layers.Replace("," + layerName, "");
            }
            SaveLayersConfig();
        }

        public static LayerGroupService GetDefault(object args = null)
        {
            return Singleton<LayerGroupService>.Instance;
        }

        private void ResolveLayersConfig(string configPath)
        {
            if (!string.IsNullOrEmpty(configPath))
            {
                if (File.Exists(configPath))
                    this.LayersConfig = ConfigHelper<LayersConfig>.ResovleConfigFromFile(configPath);
            }
        }

        private void SaveLayersConfig()
        {
            var configPath = this._fieldsFilterConfig;
            if (File.Exists(configPath))
                ConfigHelper<LayersConfig>.SaveWsConfig(configPath, this.LayersConfig);
        }

        private readonly string _fieldsFilterConfig = Application.StartupPath + "\\" + ConfigPath.LayersConfig;
    }
}