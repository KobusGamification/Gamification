using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Extension;
namespace LanguageExtension
{
    public class LanguageBuilder : IExtension
    {
        private string LanguageConfiguration { get; set; }
        public IDictionary<string, ILanguage> LanguageAttributes { get; private set; }        

        public LanguageBuilder()
        {
            LanguageConfiguration = ".\\LanguageExtensionConfiguration\\KnownLanguages.prop";
            LanguageAttributes = new Dictionary<string, ILanguage>();
        }

        public void AddLanguage(LanguageModel model)
        {
            model = GetValidLanguage(model);
            if (model != null)
            {
                ILanguage value = null;
                if (!LanguageAttributes.TryGetValue(model.Name, out value))
                {
                    LanguageAttributes.Add(model.Name, new SimpleLanguage(model));
                }
                else
                {
                    LanguageAttributes[model.Name].Add(model);
                }
            }
        }

        private LanguageModel GetValidLanguage(LanguageModel model)
        {
            LanguageModel result = null;
            using (var reader = new StreamReader(LanguageConfiguration))
            {
                while (!reader.EndOfStream)
                {
                    var cfg = reader.ReadLine().Split('=');

                    if (model.Name.Equals(cfg[0]) || model.Name.Equals(cfg[1]))
                    {
                        result = new LanguageModel(cfg[1], model.File);
                    }

                }
            }
            return result;
        }

        public void AddBuilder(LanguageBuilder currentLang)
        {
            foreach (var key in currentLang.LanguageAttributes.Keys)
            {
                ILanguage value = null;
                if (currentLang.LanguageAttributes.TryGetValue(key, out value))
                {
                    for (int i = 0; i < currentLang.LanguageAttributes[key].File.Count; i++)
                    {
                        var langModel = new LanguageModel(key, currentLang.LanguageAttributes[key].File[i]);
                        AddLanguage(langModel);
                    }
                }
                else
                {
                    LanguageAttributes.Add(key, currentLang.LanguageAttributes[key]);                    
                }
            }
        }


        public static LanguageModel TransformPathToLanguageModel(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Caminho inserido é invalido");
            }
            var name = Path.GetExtension(path).TrimStart('.');
            var file = Path.GetFileName(path);
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Folder";
            }
            return new LanguageModel(name, file);
        }

        public IExtension Merge(IExtension extension)
        {
            AddBuilder((LanguageBuilder)extension);
            return this;
        }

    }
}
