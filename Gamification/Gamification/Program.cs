using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
namespace Gamification
{
    class Program
    {
        static void Main(string[] args)
        {
            var catalog = new AggregateCatalog(
            new AssemblyCatalog(Assembly.GetExecutingAssembly()), new DirectoryCatalog("extensions"));
            var container = new CompositionContainer(catalog);
            var plugins = new Plugins();
            container.ComposeParts(plugins);
            plugins.Analyze();
            Console.ReadLine();
        }
    }
}
