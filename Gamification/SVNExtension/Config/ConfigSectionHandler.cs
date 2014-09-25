using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
namespace SVNExtension.Config
{
    public class ConfigSectionHandler : IConfigurationSectionHandler
    {

        public const string SECTION_NAME = "Repository";

        public object Create(object parent, object configContext, XmlNode section)
        {

            string szConfig = section.SelectSingleNode("//Repository").OuterXml;

            Repository retConf = null;

            if (szConfig != string.Empty || szConfig != null)
            {
                XmlSerializer xsw = new XmlSerializer(typeof(Repository));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szConfig));
                ms.Position = 0;
                retConf = (Repository)xsw.Deserialize(ms);
                
            }
            return retConf;
        }
    }
}
