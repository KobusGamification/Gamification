using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LanguageExtension
{
    public class LanguageBuilder
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


        public static LanguageModel TransformPathToLanguageModel(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Caminho inserido é invalido");
            }
            var name = Path.GetExtension(path);
            var file = Path.GetFileName(path);
            return new LanguageModel(name, file);
        }
    }
}
