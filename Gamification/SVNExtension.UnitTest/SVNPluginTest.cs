using System.Diagnostics;
using NUnit.Framework;
using DatabaseAccess;
using Extension;
using SVNExtension.Model;
using System;
namespace SVNExtension.UnitTest
{
    [TestFixture]
    public class SVNPluginTest
    {

        [SetUp]
        public void SetUp()
        {
            CleanDatabase();
            using (var process = new Process())
            {
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = ".\\BuildTests\\BuildSvnRepos.ps1";
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit(20000);
            }
        }

        private void CleanDatabase()
        {
            var dbManager = new DatabaseManager();
            var testDatabase = dbManager.GetDatabase();
            testDatabase.DropCollection(typeof(IUser).Name);
            testDatabase.DropCollection(typeof(SVNRepository).Name);
        }

        [Test]
        public void TestSimplyAnalyze()
        {            
            var dbUsers = new DatabaseUsers();            
            var plugin = new SVNPlugin();
            new LanguageExtension.LanguagePlugin().LoadDBMaps();            
            plugin.LoadDBMaps();
            plugin.Analyze();
            var manager = new DatabaseManager();
            var db = manager.GetDatabase();
            var collection = db.GetCollection<IUser>(typeof(IUser).Name);
            foreach (var user in collection.FindAll())
            {
                Assert.AreEqual(Environment.UserName, user.Name);
                Assert.AreEqual(20, ((SVNModel)user.ExtensionPoint["SVNExtension"]).Add);
                Assert.AreEqual(0, ((SVNModel)user.ExtensionPoint["SVNExtension"]).Modified);
                Assert.AreEqual(0, ((SVNModel)user.ExtensionPoint["SVNExtension"]).Deleted);
            }

            var repos = db.GetCollection<SVNRepository>(typeof(SVNRepository).Name);
            foreach (var repo in repos.FindAll())
            {
                Assert.AreEqual(2, repo.CurrentVersion);
            }
        }
    }
}
