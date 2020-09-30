using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationConfig;
using Mmc.Mspace.Services.LocalConfigService;

namespace Mmc.Mspace.Services.LayerGroupService
{
    public class LocalGroupLayerCfg<T> : BaseConfigCols<T> where T : LayerGroup, new()
    {
        public LocalGroupLayerCfg(LiteDbHelper litedbHelper, string configKey, string userName) : base(litedbHelper, configKey, userName)
        {
            LiteDbHelper = litedbHelper;
            ConfigKey = configKey;
            CurUserName = userName;
        }

        public string[] GetGroupLayers(string groupName)
        {
            var layerGroup = FindOne(p => p.GroupName == groupName && p.UserName == CurUserName);
            var result = ((layerGroup != null && !string.IsNullOrEmpty(layerGroup.Layers)) ? layerGroup.Layers.ToLower().Split(new char[]
            {
                ',',
                ';'
            }, StringSplitOptions.RemoveEmptyEntries) : null);
            return result;
        }

        public string[] GetGroupNames()
        {
            var layerGroups = FindAll();
            if (!layerGroups.HasValues())
                return null;
            var result = (from @group in layerGroups
                          select @group.GroupName).ToArray<string>();
            return result;

        }



        public void AddGroupLayers(string groupName, string layerName)
        {
            var layerGroup = FindOne(p => p.GroupName == groupName && p.UserName == CurUserName);
            if (layerGroup != null)
            {
                //if (layerGroup == null)
                //{
                //    layerGroup = new T() {  GroupName = groupName };
                //    Add(layerGroup);
                //}

                if (string.IsNullOrEmpty(layerGroup.Layers))
                    layerGroup.Layers = layerName;
                else
                    layerGroup.Layers = layerGroup.Layers + "," + layerName;

                Update(layerGroup);
            }
        }

        public void DelGroupLayers(string groupName, string layerName)
        {
            var layerGroup = FindOne(p => p.GroupName == groupName && p.UserName == CurUserName);
            if (layerGroup != null&& layerGroup.Layers.HasValues())
            {
                if (!layerGroup.Layers.Contains(","))
                    layerGroup.Layers.Replace(layerName, "");
                else
                    layerGroup.Layers.Replace("," + layerName, "");

                Update(layerGroup);
            }
        }
    }
}
