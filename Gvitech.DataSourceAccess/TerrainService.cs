using System;
using Gvitech.CityMaker.Controls;

namespace Mmc.DataSourceAccess
{
	
	public class TerrainService : ITerrainService
	{
		
		public TerrainService(AxRenderControl rc)
		{
			//bool flag = rc == null;
			//if (flag)
			//{
			//	throw new ArgumentNullException("rc");
			//}
			this._rc = rc;
		}

		
		public bool RegisterTerrain(string terrainPath, string pswd)
		{
			return !string.IsNullOrEmpty(terrainPath) && this._rc.Terrain.RegisterTerrain(terrainPath, pswd);
		}

		
		public bool UnregisterTerrain()
		{
			this._rc.Terrain.UnregisterTerrain();
			return true;
		}

		
		public bool FlyToTerrain()
		{
			ITerrainExtension.FlyTo(this._rc.Terrain);
			return true;
		}

		
		private readonly AxRenderControl _rc;
	}
}
