using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NUnit.Framework;
using System.IO;
namespace SVNExtension.UnitTest
{
    [TestFixture]
    public class SVNPluginTest
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
        public void TestSimplyAnalyze()
        {
            var plugin = new SVNPlugin();
            var users = plugin.Analyze();
            var builder = new StringBuilder();
            foreach (var user in users)
            {
                var model = (SVNModel)user.ExtensionPoint["SVNExtension"];
                Assert.AreEqual("leonardo.kobus", user.Name);                
                Assert.AreEqual(20, model.Add);
                Assert.AreEqual(0, model.Deleted);
                Assert.AreEqual(0, model.Merges);
                Assert.AreEqual(0, model.Modified);               
            }        

        }

    }
}
