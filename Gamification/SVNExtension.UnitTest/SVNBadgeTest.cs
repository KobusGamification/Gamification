using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NUnit.Framework;
using Extension.Badge;
using SVNExtension.Badges;
using DatabaseAccess;
using Extension;
using LanguageExtension;
namespace SVNExtension.UnitTest
{
    [TestFixture]
    public class SVNBadgeTest
    {

        [TestFixtureSetUp]
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
        public void ModifedBadgeEarnedTest()
        {
            var model = new SVNModel();
            model.AddModified(2);
            var badge = new SVNModified();
            var p = new SVNPlugin();
            p.LoadDBMaps();
            p.LoadBadges();
            badge.Compute(model);
            Assert.AreEqual(true, badge.Gained);            
        }

        [Test]
        public void SimpleBadgeEarned()
        {
            var plugin = new SVNPlugin();
            new LanguagePlugin().LoadDBMaps();
            plugin.LoadDBMaps();
            plugin.LoadBadges();
            plugin.Analyze();
            plugin.Compute();
            plugin.ComputeBadges();

            var manager = new DatabaseManager();
            var collection = manager.GetDatabase()
                .GetCollection<IUser>(typeof(IUser).Name)
                .FindAll()
                .ToList();

            Assert.AreEqual(1, collection.Count);

            foreach (var user in collection)
            {
                var badges = user.Badges;
                Assert.AreEqual(1, badges.Count);
                foreach (var badge in badges)
                {
                    Assert.AreEqual("Add!", badge.Name);
                }
            }

            
        }

    }
}
