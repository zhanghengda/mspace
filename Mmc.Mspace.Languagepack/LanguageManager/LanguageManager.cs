using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mmc.Mspace.Languagepack.LanguageManager
{
    public class LanguageManager
    {
        private static IList<Languages> _languages;
        public static IEnumerable<Languages> Languages
        {
            get
            {
                if (_languages != null)
                    return _languages;

                var languages = new List<Tuple<string, string>>
                {
                    new Tuple<string, string>("zh-CN", "简体中文"),
                    new Tuple<string, string>("en-US", "English"),
                    new Tuple<string, string>("ru-RU", "Russian"),
                };

                _languages = new List<Languages>(languages.Count);

                try
                {
                    foreach (var language in languages)
                    {
                        var resourceAddress = new Uri($"pack://application:,,,/Mmc.Mspace.Languagepack;component/LanguagePackage/{language.Item1}.xaml");
                        _languages.Add(new Languages(language.Item1, language.Item2, resourceAddress));
                    }
                }
                catch (Exception e)
                {

                }
                return _languages;
            }
        }

        private static void ChangeLanguages(ResourceDictionary resources, Languages oldLanguageInfo, Languages newLanguage)
        {
            var languageChange = false;
            if (oldLanguageInfo != null)
            {
                var oldLanguage = oldLanguageInfo;
                if (oldLanguage != null && oldLanguage != newLanguage)
                {
                    resources.MergedDictionaries.Add(newLanguage.Resources);

                    var key = oldLanguage.Resources.Source.ToString().ToLower();
                    var oldLanguageResource = resources.MergedDictionaries.Where(x => x.Source != null).FirstOrDefault(d => d.Source.ToString().ToLower() == key);
                    if (oldLanguageResource != null)
                    {
                        resources.MergedDictionaries.Remove(oldLanguageResource);
                    }
                    languageChange = true;
                }
            }
            else
            {
                ChangeLanguages(resources, newLanguage);
                languageChange = true;
            }
            if (languageChange)
            {                                    
            }
        }
        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="resources"></param>
        /// <param name="newLanguage"></param>
        public static void ChangeLanguages(ResourceDictionary resources, Languages newLanguage)
        {
            if (resources == null) throw new ArgumentNullException(nameof(resources));
            if (newLanguage == null) throw new ArgumentNullException(nameof(newLanguage));


            ApplyResourceDictionary(newLanguage.Resources, resources);
        }
        private static void ApplyResourceDictionary(ResourceDictionary newRd, ResourceDictionary oldRd)
        {
            if (newRd == null) throw new ArgumentNullException(nameof(newRd));
            if (oldRd == null) throw new ArgumentNullException(nameof(oldRd));
            oldRd.MergedDictionaries.Add(newRd);
        }
        /// <summary>
        /// 通过资源获取语言
        /// </summary>
        /// <param name="resources"></param>
        /// <returns></returns>
        public static Languages GetLanguages(ResourceDictionary resources)
        {
            if (resources == null) throw new ArgumentNullException(nameof(resources));

            return Languages.FirstOrDefault(x => AreResourceDictionarySourcesEqual(x.Resources.Source, resources.Source));
        }
        /// <summary>
        /// 通过语言名称获取语言
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static Languages GetLanguages(string languageName)
        {
            if (languageName == null) throw new ArgumentNullException(nameof(languageName));

            return Languages.FirstOrDefault(x => x.Name.Equals(languageName, StringComparison.OrdinalIgnoreCase));
        }

        private static bool AreResourceDictionarySourcesEqual(Uri first, Uri second)
        {
            return Uri.Compare(first, second,
                 UriComponents.Host | UriComponents.Path, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="app"></param>
        /// <param name="newLanguage"></param>
        public static void ChangeLanguages(Application app, Languages newLanguage)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (newLanguage == null) throw new ArgumentNullException(nameof(newLanguage));

            var oldLanguage = DetectLanguages(app);
            ChangeLanguages(app.Resources, oldLanguage, newLanguage);
        }

        public static Languages DetectLanguages(Application app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            return DetectLanguages(app.Resources);
        }

        private static Languages DetectLanguages(ResourceDictionary resources)
        {
            if (resources == null) throw new ArgumentNullException(nameof(resources));

            Languages currentLanguages = null;

            if (DetectLanguagesFromResources(ref currentLanguages, resources))
            {
                return currentLanguages;
            }
            return null;
        }

        private static bool DetectLanguagesFromResources(ref Languages detectedLanguage, ResourceDictionary dict)
        {
            var enumerator = dict.MergedDictionaries.Reverse().GetEnumerator();
            while (enumerator.MoveNext())
            {
                var currentRd = enumerator.Current;

                Languages matched;
                if ((matched = GetLanguages(currentRd)) != null)
                {
                    detectedLanguage = matched;
                    enumerator.Dispose();
                    return true;
                }

                if (DetectLanguagesFromResources(ref detectedLanguage, currentRd))
                {
                    return true;
                }
            }
            enumerator.Dispose();
            return false;
        }
    }
}
