using Gvitech.CityMaker.FdeCore;
using System;

namespace Gvitech.CityMaker.FdeDataInterop
{
	public static class ILayerInfoExtension
	{
		public static gviGeometryColumnType GetGeometryColumnType(this ILayerInfo layerInfo)
		{
			IFieldInfoCollection fieldInfos = layerInfo.FieldInfos;
			int num;
			for (int i = 0; i < fieldInfos.Count; i = num + 1)
			{
				IFieldInfo fieldInfo = fieldInfos.Get(i);
				bool flag = fieldInfo.FieldType != gviFieldType.gviFieldGeometry;
				if (!flag)
				{
					return fieldInfo.GeometryDef.GeometryColumnType;
				}
				num = i;
			}
			throw new InvalidOperationException(string.Format("图层{0}没有几何空间列", layerInfo.Name));
		}
	}
}
