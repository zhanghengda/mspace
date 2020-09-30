using System;
using Gvitech.CityMaker.FdeCore;

// Token: 0x02000005 RID: 5
public static class IFieldInfoExtension
{
	// Token: 0x06000009 RID: 9 RVA: 0x0000288C File Offset: 0x00000A8C
	public static bool CanBeShow(this IFieldInfo field)
	{
		bool tempFC = field == null;
		if (tempFC)
		{
			throw new ArgumentNullException("field");
		}
		gviFieldType tempFeatureLayer = field.FieldType;
		if (tempFeatureLayer <= gviFieldType.gviFieldBlob)
		{
			if (tempFeatureLayer != gviFieldType.gviFieldUnknown && tempFeatureLayer != gviFieldType.gviFieldBlob)
			{
				goto IL_40;
			}
		}
		else if (tempFeatureLayer != gviFieldType.gviFieldUUID && tempFeatureLayer != gviFieldType.gviFieldGeometry)
		{
			goto IL_40;
		}
		return false;
		IL_40:
		return true;
	}
}
