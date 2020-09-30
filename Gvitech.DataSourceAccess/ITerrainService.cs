using System;

namespace Mmc.DataSourceAccess
{
	
	public interface ITerrainService
	{
		
		bool RegisterTerrain(string terrainPath, string pswd);

		
		bool UnregisterTerrain();

		
		bool FlyToTerrain();
	}
}
