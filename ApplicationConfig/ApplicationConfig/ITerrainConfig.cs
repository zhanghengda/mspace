using Mmc.Mspace.Models.User;
using System;

namespace ApplicationConfig
{
	public interface ITerrainConfig:IUserInfo
	{
		string Server { get; set; }

		string Password { get; set; }

		bool DemAvailable { get; set; }

		float DemOpacity { get; set; }

		string AliasName { get; set; }
	}
}
