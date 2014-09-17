using Extension;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVNExtension
{
    [Export(typeof(IPlugin))]
    public class SVNPlugin : IPlugin
    {
        public void Analyze()
        {

            Console.WriteLine("Ppl inserted");
            Console.ReadKey();
        }
    }
}
