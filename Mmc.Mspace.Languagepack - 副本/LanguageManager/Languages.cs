using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mmc.Mspace.Languagepack.LanguageManager
{
    /// <summary>
    /// 语言实体model
    /// </summary>
    public class Languages
    {
        /// <summary>
        /// 资源文件
        /// </summary>
        public ResourceDictionary Resources { get; }
        /// <summary>
        /// 语言简码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 语言名称
        /// </summary>
        public string Name { get; set; }
        public Languages(string name, string code, Uri resourceAddress)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (resourceAddress == null) throw new ArgumentNullException(nameof(resourceAddress));


            this.Name = name;
            this.Code = code;
            this.Resources = new ResourceDictionary { Source = resourceAddress };
        }
    }
}
