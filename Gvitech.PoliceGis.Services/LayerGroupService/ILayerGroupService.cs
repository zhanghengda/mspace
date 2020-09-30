namespace Mmc.Mspace.Services.LayerGroupService
{
    public interface ILayerGroupService
    {
        string[] GetGroupNames();

        string[] GetGroupLayers(string groupName);
    }
}