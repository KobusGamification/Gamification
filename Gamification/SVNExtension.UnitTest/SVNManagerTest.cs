using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Diagnostics;
using System.Configuration;
using SVNExtension;
namespace SVNExtension.UnitTest
{
    [TestFixture]
    public class SVNManagerTest
    {

        [SetUp]
        public void SetUp()
        {
            using (var process = new Process())
            {            
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = ".\\BuildTests\\BuildSvnRepos.ps1";
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit(20000);
            }
            
        }

        [Test]
        public void GetReposLogsTest()
        {           
            var url = @"file:///C:/users/leonardo.kobus/desktop/games/gamification/SVNExtension.UnitTest/bin/Debug/RepositorioNET";
            int initialRelease = 0;
            using (var manager = new SVNManager())
            {
                manager.Generate(url, initialRelease);
                Assert.IsTrue(Directory.GetFiles("SVNReports").Length > 0);
            }
            Assert.IsTrue(!Directory.Exists("SVNReports"));
        }

        
        [Test]
        public void GetRepostLogByRevisionTest()
        {
            var url = @"file:///C:/users/leonardo.kobus/desktop/games/gamification/SVNExtension.UnitTest/bin/Debug/RepositorioNET";
            int initialRelease = 0;
            using (var manager = new SVNManager())
            {
                manager.Generate(url, initialRelease);
                var reader = new SVNReader(initialRelease);
                foreach (var file in manager.Files)
                {
                    var users = reader.Read(file);
                    Assert.AreEqual(1, users.Count);

                    foreach (var user in users)
                    {
                        Assert.AreEqual(10, ((SVNModel)user.ExtensionPoint["SVNExtension"]).Add);
                        Assert.AreEqual(2, reader.CurrentRevision);
                        Assert.AreEqual(0, ((SVNModel)user.ExtensionPoint["SVNExtension"]).Merges);
                        Assert.AreEqual(0, ((SVNModel)user.ExtensionPoint["SVNExtension"]).Deleted);
                        Assert.AreEqual(0, ((SVNModel)user.ExtensionPoint["SVNExtension"]).Merges);
                        Assert.AreEqual(0, ((SVNModel)user.ExtensionPoint["SVNExtension"]).Modified);
                    }
                }
            }
        }
        
        [Test]
        public void GetReposByOneRevisionAhed()
        {
            var url = @"file:///C:/users/leonardo.kobus/desktop/games/gamification/SVNExtension.UnitTest/bin/Debug/RepositorioNET";
            int initialRelease = 2;
            using (var manager = new SVNManager())
            {
                var reader = new SVNReader(initialRelease);
                manager.Generate(url, initialRelease);

                foreach (var file in manager.Files)
                {
                    var users = reader.Read(file);
                    Assert.AreEqual(0, users.Count);
                }

                
            }
        }

    

    }
}
