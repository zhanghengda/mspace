using Mmc.Windows.Design;
using System;

namespace Mmc.LayerSymbol
{
	public class LayerStyleFactory : Singleton<LayerStyleFactory>
	{
		public ILayerStyleConvertor CreateLayerStyle(LayerStyleType type)
		{
			ILayerStyleConvertor result;
			if (type != LayerStyleType.Style70)
			{
				if (type != LayerStyleType.StyleUnkown)
				{
					result = null;
				}
				else
				{
					result = null;
				}
			}
			else
			{
				result = new LayerStyle70();
			}
			return result;
		}
	}
}
