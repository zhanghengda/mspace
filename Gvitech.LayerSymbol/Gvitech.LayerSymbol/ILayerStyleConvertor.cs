using System;
using System.Xml;

namespace Mmc.LayerSymbol
{
	public interface ILayerStyleConvertor
	{
		bool ImportXml(string fileName, out FlyrProperty xmlFcLyr);

		bool ExportXml(string fileName, FlyrProperty layer);

		bool ExportXmlToBuilder(string fileName, FlyrProperty layer);

		bool LayerStyle2Xml(FlyrProperty layer, ref string xmlInfo);

		bool Xml2LayerStyle(XmlDocument xmlDoc, string geoFieldName, out FlyrProperty xmlFcLyr);
	}
}
