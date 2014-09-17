using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageExtension
{
    public interface ILanguage
    {
        string Name { get;}
        int Count
        {
            get;
        }
        List<string> File {get;}

        void Add(LanguageModel model);
    }
}
