using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using Mmc.Windows.Design;
using System;
using System.Xml;

namespace Mmc.LayerSymbol
{
	public class FlyrProperty
	{
		private gviViewportMask _visibleMask;

		private ITextRender _txtRender = null;

		private double _dMaxVisibleDistance = 150000.0;

		private float _fMinVisiblePixels = 0f;

		private bool _bForceCullMode = false;

		private gviCullFaceMode _cullFaceMode = gviCullFaceMode.gviCullNone;

		private string _geometryFiledName = string.Empty;

		private IGeometryRender _geoRender = null;

		private LayerStyleType _layerType = LayerStyleType.Style70;

		private string _layerName = null;

		public IGeometryRender GeoRender
		{
			get
			{
				return this._geoRender;
			}
			set
			{
				this._geoRender = value;
			}
		}

		public gviViewportMask VisibleMask
		{
			get
			{
				return this._visibleMask;
			}
			set
			{
				this._visibleMask = value;
			}
		}

		public LayerStyleType LayerType
		{
			get
			{
				return this._layerType;
			}
			set
			{
				this._layerType = value;
			}
		}

		public ITextRender TextRender
		{
			get
			{
				return this._txtRender;
			}
			set
			{
				this._txtRender = value;
			}
		}

		public double MaxVisibleDistance
		{
			get
			{
				return this._dMaxVisibleDistance;
			}
			set
			{
				this._dMaxVisibleDistance = value;
			}
		}

		public float MinVisiblePixels
		{
			get
			{
				return this._fMinVisiblePixels;
			}
			set
			{
				this._fMinVisiblePixels = value;
			}
		}

		public bool ForceCullMode
		{
			get
			{
				return this._bForceCullMode;
			}
			set
			{
				this._bForceCullMode = value;
			}
		}

		public gviCullFaceMode CullFaceMode
		{
			get
			{
				return this._cullFaceMode;
			}
			set
			{
				this._cullFaceMode = value;
			}
		}

		public string GeometryFiledName
		{
			get
			{
				return this._geometryFiledName;
			}
			set
			{
				this._geometryFiledName = value;
			}
		}

		public string LayerName
		{
			get
			{
				return this._layerName;
			}
			set
			{
				this._layerName = value;
			}
		}

		public static FlyrProperty GetLayerRenderFromXml(string fileName)
		{
			FlyrProperty result;
			try
			{
				FlyrProperty flyrProperty = null;
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(fileName);
				bool flag = xmlDocument == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					LayerStyleType type = Singleton<RenderXmlParser>.Instance.GetType(xmlDocument);
					ILayerStyleConvertor layerStyleConvertor = Singleton<LayerStyleFactory>.Instance.CreateLayerStyle(type);
					xmlDocument.Save(fileName);
					bool flag2 = layerStyleConvertor == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						bool flag3 = true;
						string empty = string.Empty;
						flag3 &= layerStyleConvertor.ImportXml(fileName, out flyrProperty);
						bool flag4 = !flag3 || flyrProperty == null;
						if (flag4)
						{
							flyrProperty = null;
							result = null;
						}
						else
						{
							result = flyrProperty;
						}
					}
				}
			}
			catch (Exception innerException)
			{
				throw new Exception("GetLayerRenderFromXml Function Error", innerException);
			}
			return result;
		}

		public static FlyrProperty CreateFlyrProperty(IFeatureLayer fcLyr, string fcAalisName)
		{
			bool flag = fcLyr == null;
			FlyrProperty result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					result = new FlyrProperty
					{
						ForceCullMode = fcLyr.ForceCullMode,
						CullFaceMode = fcLyr.CullMode,
						GeoRender = fcLyr.GetGeometryRender(),
						GeometryFiledName = fcLyr.GeometryFieldName,
						MaxVisibleDistance = fcLyr.MaxVisibleDistance,
						MinVisiblePixels = fcLyr.MinVisiblePixels,
						TextRender = fcLyr.GetTextRender(),
						LayerName = fcAalisName,
						VisibleMask = fcLyr.VisibleMask
					};
				}
				catch (Exception innerException)
				{
					throw new Exception("CreateFlyrProperty Function Error", innerException);
				}
			}
			return result;
		}

		public static int GetGeometrySymbolType(IGeometryRender geoRender)
		{
			bool flag = geoRender == null;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				gviRenderType renderType = geoRender.RenderType;
				if (renderType != gviRenderType.gviRenderSimple)
				{
					if (renderType != gviRenderType.gviRenderValueMap)
					{
						result = -1;
					}
					else
					{
						IValueMapGeometryRender valueMapGeometryRender = geoRender as IValueMapGeometryRender;
						bool flag2 = valueMapGeometryRender.SchemeCount == 0;
						if (flag2)
						{
							result = -1;
						}
						else
						{
							IGeometryRenderScheme scheme = valueMapGeometryRender.GetScheme(0);
							bool flag3 = scheme == null;
							if (flag3)
							{
								result = -1;
							}
							else
							{
								IGeometrySymbol symbol = scheme.Symbol;
								bool flag4 = symbol == null;
								if (flag4)
								{
									result = -1;
								}
								else
								{
									result = (int)symbol.SymbolType;
								}
							}
						}
					}
				}
				else
				{
					ISimpleGeometryRender simpleGeometryRender = geoRender as ISimpleGeometryRender;
					IGeometrySymbol symbol = simpleGeometryRender.Symbol;
					bool flag5 = symbol == null;
					if (flag5)
					{
						result = -1;
					}
					else
					{
						result = (int)symbol.SymbolType;
					}
				}
			}
			return result;
		}

		public static gviGeometryColumnType GetGeometryColumnType(IGeometryRender geoRender)
		{
			int geometrySymbolType = FlyrProperty.GetGeometrySymbolType(geoRender);
			bool flag = geometrySymbolType == -1;
			gviGeometryColumnType result;
			if (flag)
			{
				result = gviGeometryColumnType.gviGeometryColumnUnknown;
			}
			else
			{
				switch (geometrySymbolType)
				{
				case 0:
					result = gviGeometryColumnType.gviGeometryColumnPoint;
					return result;
				case 1:
					result = gviGeometryColumnType.gviGeometryColumnPoint;
					return result;
				case 2:
					result = gviGeometryColumnType.gviGeometryColumnModelPoint;
					return result;
				case 3:
					result = gviGeometryColumnType.gviGeometryColumnPolyline;
					return result;
				case 4:
					result = gviGeometryColumnType.gviGeometryColumnPolygon;
					return result;
				case 6:
					result = gviGeometryColumnType.gviGeometryColumnUnknown;
					return result;
				}
				result = gviGeometryColumnType.gviGeometryColumnUnknown;
			}
			return result;
		}

		public static bool LayerStyle2Xml(FlyrProperty flyProp, ref string xmlInfo)
		{
			xmlInfo = null;
			ILayerStyleConvertor layerStyleConvertor = Singleton<LayerStyleFactory>.Instance.CreateLayerStyle(LayerStyleType.Style70);
			return layerStyleConvertor.LayerStyle2Xml(flyProp, ref xmlInfo);
		}

		public static bool Xml2LayerStyle(string xmlInfo, string geoName, out FlyrProperty flyProp)
		{
			flyProp = null;
			bool result;
			try
			{
				XmlDocument xmlDocument = Singleton<RenderXmlParser>.Instance.LoadXmlDocument(xmlInfo);
				bool flag = xmlDocument == null;
				if (flag)
				{
					result = false;
				}
				else
				{
					ILayerStyleConvertor layerStyleConvertor = Singleton<LayerStyleFactory>.Instance.CreateLayerStyle(LayerStyleType.Style70);
					result = layerStyleConvertor.Xml2LayerStyle(xmlDocument, geoName, out flyProp);
				}
			}
			catch (Exception innerException)
			{
				throw new Exception("Xml2LayerStyle Function Error", innerException);
			}
			return result;
		}

		public void SetFeaturelayer(IFeatureLayer fcLyr)
		{
			fcLyr.ForceCullMode = this.ForceCullMode;
			fcLyr.CullMode = this.CullFaceMode;
			fcLyr.MaxVisibleDistance = this.MaxVisibleDistance;
			fcLyr.MinVisiblePixels = this.MinVisiblePixels;
			bool flag = this.GeoRender != null;
			if (flag)
			{
				fcLyr.SetGeometryRender(this.GeoRender);
			}
			bool flag2 = this.TextRender != null;
			if (flag2)
			{
				fcLyr.SetTextRender(this.TextRender);
			}
		}
	}
}
