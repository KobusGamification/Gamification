﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using LanguageExtension;
using Extension;
using SVNExtension.Model;
using System.Globalization;
namespace SVNExtension
{
    public class SVNReader
    {

        public int CurrentRevision { get; private set; }
        public List<SVNInfo> Infos { get; private set; }
        static log4net.ILog log = log4net.LogManager.GetLogger(typeof(SVNReader));

        public SVNReader(int startRevision)
        {
            CurrentRevision = startRevision;
            Infos = new List<SVNInfo>();
        }

        public List<IUser> Read(string xmlPath)
        {
            log.InfoFormat("Reading svn : {0}", xmlPath);
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
                    var currentDate = node.ParentNode.ParentNode.SelectSingleNode("date").InnerText;
                    log.DebugFormat("current user : {0}", currentUser);
                    log.DebugFormat("currnet date : {0}", currentDate);
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

                    Infos.Add(SVNBuilder.AddInfo(action, currentUser,
                        DateTime.ParseExact(currentDate, "yyyy-MM-ddTHH:mm:ss.ffffffZ", CultureInfo.InstalledUICulture)));
                    user.ExtensionPoint["SVNExtension"] = SVNBuilder.AddAction(action, ((SVNModel)user.ExtensionPoint["SVNExtension"]));
                }

                
                foreach (var key in userDict.Keys)
                {
                    result.Add(userDict[key]);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Message : {0}", ex.Message);
                log.ErrorFormat("StackTrace : {0}", ex.StackTrace);                
                return result;
            }
            return result;
        }
    }
}

