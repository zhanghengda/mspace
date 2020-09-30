using System;

/// <summary>
///     <see cref="System.String" />扩展类。
/// </summary>
public static partial class StringExtension
{
    /// <summary>
    ///     返回的字符串数组包含此字符串中的子字符串（由指定字符串分隔）。参数指定是否返回空数组元素。
    /// </summary>
    /// <param name="this">string对象。</param>
    /// <param name="separator">分隔此字符串中子字符串的字符串。</param>
    /// <param name="option">
    ///     要省略返回的数组中的空数组元素，则为<see cref="System.StringSplitOptions.RemoveEmptyEntries" />；要包含返回的数组中的空数组元素，则为
    ///     <see cref="System.StringSplitOptions.None" />。
    /// </param>
    /// <returns>
    ///     一个数组，其元素包含此字符串中的子字符串，这些子字符串由<paramref name="separator" /> 字符串分隔。
    /// </returns>
    /// <example>
    ///     <code>
    ///          <![CDATA[
    ///          ]]>
    ///     </code>
    /// </example>
    public static string[] Split(this string @this, string separator,
        StringSplitOptions option = StringSplitOptions.None)
    {
        return @this.Split(new[] {separator}, option);
    }
}