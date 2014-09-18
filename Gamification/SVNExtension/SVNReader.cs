using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using LanguageExtension;
namespace SVNExtension
{
    public class SVNReader
    {

        public SVNModel Read(string xmlPath)
        {
            var begin = 0;
            return Read(xmlPath, begin);
        }

        public SVNModel Read(string xmlPath, int startRevision)
        {

            if (string.IsNullOrWhiteSpace(xmlPath))
            {
                throw new ArgumentNullException("xmlPath");
            }
            var result = new SVNModel();
            var languageBuilder = new LanguageBuilder();

            var doc = new XmlDocument();
            doc.Load(xmlPath);
            var xpath = string.Format("//logentry[@revision>'{0}']/paths/path", startRevision);
            foreach (XmlNode node in doc.SelectNodes(xpath))
            {                            
                var action = node.Attributes["action"].Value;
                var kind = node.InnerText;
                result = SVNBuilder.AddAction(action, result);
                var model = LanguageBuilder.TransformPathToLanguageModel(kind);
                languageBuilder.AddLanguage(model);                             
            }
            result.CurrentRevision = Convert.ToInt32(doc.SelectSingleNode("//logentry").Attributes["revision"].Value);
            return result;
        }
    }
}
