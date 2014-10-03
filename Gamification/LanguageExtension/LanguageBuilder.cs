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
        public IDictionary<string, ILanguage> LanguageAttributes { get; private set; }        

        public LanguageBuilder()
        {
            LanguageAttributes = new Dictionary<string, ILanguage>();
        }

        public void AddLanguage(LanguageModel model)
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
