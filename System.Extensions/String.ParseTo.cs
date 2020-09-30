using System;

/// <summary>
///     <see cref="System.String" />扩展类。
/// </summary>
public static partial class StringExtension
{
    /// <summary>
    ///     将字符串解析成指定类型。
    /// </summary>
    /// <typeparam name="T">要转换成的类型。</typeparam>
    /// <param name="this">当前对象。</param>
    /// <param name="defaultValue">默认值。</param>
    /// <returns></returns>
    /// <example>
    ///     <code>
    ///          <![CDATA[
    ///          ]]>
    ///     </code>
    /// </example>
    public static T ParseTo<T>(this object @this, T defaultValue = default(T))
    {
        if (@this == null)
        {
            return defaultValue;
        }
        var type = typeof(T);

        //@this.GetType();
        var typeCode = Type.GetTypeCode(type);
        dynamic dv = defaultValue;
        switch (typeCode)
        {
            case TypeCode.Boolean:
                return ParseToBoolean(@this, dv);
            case TypeCode.Char:
                return ParseToChar(@this, dv);
            case TypeCode.Byte:
                return ParseToByte(@this, dv);
            case TypeCode.Int16:
                return ParseToInt16(@this, dv);
            case TypeCode.UInt16:
                return ParseToUInt16(@this, dv);
            case TypeCode.Int32:
                return ParseToInt32(@this, dv);
            case TypeCode.UInt32:
                return ParseToUInt32(@this, dv);
            case TypeCode.Int64:
                return ParseToInt64(@this, dv);
            case TypeCode.UInt64:
                return ParseToUInt64(@this, dv);
            case TypeCode.Single:
                return ParseToSingle(@this, dv);
            case TypeCode.Double:
                return ParseToDouble(@this, dv);
            case TypeCode.Decimal:
                return ParseToDecimal(@this, dv);
            case TypeCode.DateTime:
                return ParseToDateTime(@this, dv);
            case TypeCode.String:
                return ParseToString(@this, dv);
            default:
                throw new Exception(string.Format("该类型:{} 未定义转换", type.FullName));
        }
    }

    private static short ParseToInt16(object obj, short defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (short.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }

    private static int ParseToInt32(object obj, int defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (int.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }

    private static long ParseToInt64(object obj, long defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (long.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }

    private static ushort ParseToUInt16(object obj, ushort defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (ushort.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }

    private static uint ParseToUInt32(object obj, uint defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (uint.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }

    private static ulong ParseToUInt64(object obj, ulong defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (ulong.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }

    private static double ParseToDouble(object obj, double defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (double.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }

    private static float ParseToSingle(object obj, float defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (float.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }

    private static string ParseToString(object obj, string defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        return obj.ToString();
    }

    private static decimal ParseToDecimal(object obj, decimal defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (decimal.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }

    private static DateTime ParseToDateTime(object obj, DateTime defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (DateTime.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }

    private static byte ParseToByte(object obj, byte defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (byte.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }

    private static sbyte ParseToSByte(object obj, sbyte defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (sbyte.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }

    private static bool ParseToBoolean(object obj, bool defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (bool.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }

    private static char ParseToChar(object obj, char defaultValue)
    {
        if (obj == null)
        {
            return defaultValue;
        }
        var outValue = defaultValue;
        if (char.TryParse(obj.ToString(), out outValue))
        {
            return outValue;
        }
        return defaultValue;
    }
}