using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
namespace LanguageExtension
{
    public class LanguageExperience : Experience
    {
        public LanguageExperience(string name, string lvlFileProp, string alias)
            : base(name, lvlFileProp, alias)
        {
                       
        }

        public void AddModel(LanguageBuilder builder)
        {
            foreach (var language in builder.LanguageAttributes.Keys)
            {
                AddExperience(builder.LanguageAttributes[language].Count);                
            }
        }
    }
}
