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
            if (xmlPath == null)
            {
                throw new ArgumentNullException("xmlPath");
            }
            var result = new SVNModel();
            var languageBuilder = new LanguageBuilder();
            
            var doc = new XmlDocument();
            doc.Load(xmlPath);
            foreach (XmlNode node in doc.SelectNodes("//path"))
            {
                var action = node.Attributes["action"].Value;
                result = SVNBuilder.AddAction(action, result);
                var kind = node.Attributes["kind"].Value;
                var model = LanguageBuilder.TransformPathToLanguageModel(kind);
                languageBuilder.AddLanguage(model);    
            }
            
            return result;
        }
    }
}
