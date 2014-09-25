using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Diagnostics;
using System.Configuration;
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
        public void GetReposLogs()
        {
            var url = "file:///C:/users/leonardo.kobus/games/gamification/SVNExtension.UnitTest/bin/Debug/RepositorioNET";
            var manager = new SVNManager();
            var repos = manager.Generate(url);

            Assert.IsTrue(false);
        }


    }
}
