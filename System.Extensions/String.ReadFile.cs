using System;
using System.IO;

/// <summary>
///     <see cref="System.String" />扩展类。
/// </summary>
public static partial class StringExtension
{
    /// <summary>
    ///     读取文件内容为字符串。
    /// </summary>
    /// <param name="this">文件路径。</param>
    /// <param name="mode">文件打开模式。</param>
    /// <returns>文件的内容字符串。</returns>
    /// <example>
    ///     <code>
    ///          <![CDATA[
    ///          ]]>
    ///     </code>
    /// </example>
    public static string ReadFile(this string @this, System.Text.Encoding encoding, FileMode mode = FileMode.Open)
    {
        if (@this == null) throw new ArgumentNullException("@this");
        if (!File.Exists(@this)) throw new Exception(@this + " not exists");
        string readStr = null;
        using (var fs = File.Open(@this, mode))
        {
            using (var reader = new StreamReader(fs,encoding))
            {
                readStr = reader.ReadToEnd();
                reader.Close();
            }
            fs.Close();
        }
        return readStr;
    }
}