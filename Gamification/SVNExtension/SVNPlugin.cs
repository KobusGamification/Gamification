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
        public List<IUser> Analyze()
        {
            var list = new List<IUser>();
            
            //TODO: Criar a classe SVN Revision
            // Combinar o arquivo de configurações com dict
            // associar cada revisão com sua ultima revisão vista.


            //Refatorar svn reader diminuir complexidade...

            var logFile = @"SVN_Logs_Examples\csprojeditorLog.xml";
            
            var reader = new SVNReader();
            list = reader.Read(logFile);
            return list;
        }
    }
}
