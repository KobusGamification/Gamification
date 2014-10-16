using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
namespace SVNExtension
{
    public class SVNExperience : Experience
    {
        public SVNExperience(string name, string lvlFileProp, string alias)
            : base(name, lvlFileProp, alias)
        {
                       
        }

        public void AddModel(SVNModel model)
        {
            AddExperience(model.Add);
            AddExperience(model.Deleted);
            AddExperience(model.Modified);
        }
    }
}
