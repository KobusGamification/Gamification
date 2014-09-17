using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageExtension
{
    public class LanguageModel
    {

        public string Name { get; private set; }
        public string File { get; private set; }

        public LanguageModel(string name, string file )
        {
            Name = name;

            File = file;
        }

    }
}
