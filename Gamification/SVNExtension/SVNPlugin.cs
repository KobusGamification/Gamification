using Extension;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace SVNExtension
{
    [Export(typeof(IPlugin))]
    public class SVNPlugin : IPlugin
    {
        public List<IUser> Analyze()
        {
            var list = new List<IUser>();
            var logs = GetLogs();
            foreach (var log in logs)
            {
                var reader = new SVNReader(0);
                list = reader.Read(log);
            }
            return list;
        }

        private List<string> GetLogs()
        {
            SVNManager manager = new SVNManager();

            var configs = (SVNConfiguration) ConfigurationManager.GetSection("SVN");

            foreach(SVNConfiguration.Repository repo in configs.Repos)
            {
                manager.Generate(repo.Url, 0);
            }            
            return manager.Files;
        }
    }
}
