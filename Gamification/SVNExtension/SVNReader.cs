using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using LanguageExtension;
using Extension;
namespace SVNExtension
{
    public class SVNReader
    {

        public List<IUser> Read(string xmlPath)
        {
            var begin = 0;
            return Read(xmlPath, begin);
        }

        public List<IUser> Read(string xmlPath, int startRevision)
        {
            var results = new Stack<IUser>();
            if (string.IsNullOrWhiteSpace(xmlPath))
            {
                throw new ArgumentNullException("xmlPath");
            }
            LanguageBuilder languageBuilder = null;
            var svnModel = new SVNModel();
            var doc = new XmlDocument();
            var xpath = string.Format("//logentry[@revision>'{0}']/paths/path", startRevision);
            doc.Load(xmlPath);

            var userDict = new Dictionary<string, IUser>();
            IUser user = null;
            foreach (XmlNode node in doc.SelectNodes(xpath))
            {
                var currentUser = node.ParentNode.ParentNode.SelectSingleNode("author").InnerText;
                if (userDict.Keys.Count > 0)
                {
                    if (userDict.Keys.Contains(currentUser))
                    {
                        user = userDict[currentUser];
                    }
                    else
                    {
                        user = new DefaultUser(currentUser);
                        userDict.Add(currentUser, user);
                    }
                }
                else
                {
                    user = new DefaultUser(currentUser);
                    userDict.Add(currentUser, user);
                }

                

                if (!(user.ExtensionPoint.Keys.Count > 0))
                {
                    user.ExtensionPoint.Add("LanguageExtension", new LanguageBuilder());
                    user.ExtensionPoint.Add("SVNExtension", new SVNModel());                    
                }

                var action = node.Attributes["action"].Value;
                var kind = node.InnerText;
                var modelLanguage = LanguageBuilder.TransformPathToLanguageModel(kind);
                ((LanguageBuilder)user.ExtensionPoint["LanguageExtension"]).AddLanguage(modelLanguage);
                user.ExtensionPoint["SVNExtension"] = SVNBuilder.AddAction(action, ((SVNModel)user.ExtensionPoint["SVNExtension"]));                         
            }
            

            var resultado = new List<IUser>();

            foreach (var key in userDict.Keys)
            {
                resultado.Add(userDict[key]);
            }


            return resultado;
        }
    }
}

