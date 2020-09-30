using Gvitech.CityMaker.FdeCore;
using System;

public static class IFieldInfoExtension
{
	public static Type GetSystemType(this IFieldInfo @this)
	{
		bool flag = @this == null;
		if (flag)
		{
			throw new NullReferenceException("@this");
		}
		Type result = null;
		gviFieldType fieldType = @this.FieldType;
		switch (fieldType)
		{
		case gviFieldType.gviFieldUnknown:
		case gviFieldType.gviFieldString:
		case gviFieldType.gviFieldUUID:
			result = typeof(string);
			break;
		case (gviFieldType)1:
		case gviFieldType.gviFieldDate:
			break;
		case gviFieldType.gviFieldInt16:
			result = typeof(short);
			break;
		case gviFieldType.gviFieldInt32:
		case gviFieldType.gviFieldFID:
			result = typeof(int);
			break;
		case gviFieldType.gviFieldInt64:
			result = typeof(long);
			break;
		case gviFieldType.gviFieldFloat:
			result = typeof(float);
			break;
		case gviFieldType.gviFieldDouble:
			result = typeof(double);
			break;
		case gviFieldType.gviFieldBlob:
			result = typeof(byte[]);
			break;
		default:
			if (fieldType != gviFieldType.gviFieldGeometry)
			{
			}
			break;
		}
		return result;
	}
}
