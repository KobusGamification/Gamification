using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageExtension
{
    public class SimpleLanguage : ILanguage
    {

        public string Name { get; private set; }
        public int Count
        {
            get { return File.Count; }            
            private set { }
        }
        public List<string> File { get; private set; }

        public SimpleLanguage(LanguageModel model)
        {
            Name = model.Name;
            File = new List<string>();
            Add(model);
        }

        public void Add(LanguageModel model)
        {
            File.Add(model.File);
        }
    }
}
