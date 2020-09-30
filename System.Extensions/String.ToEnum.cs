using System;

/// <summary>
///     <see cref="System.String" />扩展类。
/// </summary>
public static partial class StringExtension
{
    /// <summary>
    ///     将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。一个参数指定该操作是否区分大小写。
    /// </summary>
    /// <typeparam name="TEnum"> 要将 @this 转换为的枚举类型。</typeparam>
    /// <param name="this">要转换的枚举名称或基础值的字符串表示形式。</param>
    /// <param name="ignoreCase"> true 表示不区分大小写；false 表示区分大小写。</param>
    /// <returns>一个类型为 TEnum 的对象。</returns>
    /// <example>
    ///     <code>
    ///          <![CDATA[
    ///          ]]>
    ///     </code>
    /// </example>
    public static TEnum ToEnum<TEnum>(this string @this, bool ignoreCase = true) where TEnum : struct
    {
        TEnum rusult;
        Enum.TryParse(@this, ignoreCase, out rusult);
        return rusult;
    }
}