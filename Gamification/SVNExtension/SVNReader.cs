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

        public int CurrentRevision { get; private set; }


        public SVNReader(int startRevision)
        {
            CurrentRevision = startRevision;
        }

        public List<IUser> Read(string xmlPath)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
            {
                throw new ArgumentNullException("xmlPath");
            }
            var result = new List<IUser>();
            try
            {
                var results = new Stack<IUser>();                
                var svnModel = new SVNModel();
                var doc = new XmlDocument();
                var xpath = string.Format("//logentry[@revision>'{0}']/paths/path", CurrentRevision);
                var xpathMaximumCurrentRevision = "/log/logentry/@revision[not(. < ../../logentry/@revision)][1]";
                var userDict = new Dictionary<string, IUser>();
                IUser user = null;
                doc.Load(xmlPath);
                CurrentRevision = Convert.ToInt32(doc.SelectNodes(xpathMaximumCurrentRevision)[0].Value) + 1;
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

                
                foreach (var key in userDict.Keys)
                {
                    result.Add(userDict[key]);
                }
            }
            catch (Exception)
            {
                //log msg
                return result;
            }

            return result;
        }
    }
}

