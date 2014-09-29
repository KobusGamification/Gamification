using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Extension;
namespace SVNExtension.UnitTest
{
    
    [TestFixture]
    public class SVNResultUserTest
    {

        [Test]
        public void TestUserName()
        {
            string xml = @"SVN_Logs_Examples\SimpleLog.xml";
            var p = new SVNPlugin();
            var reader = new SVNReader(0);
            var users = reader.Read(xml);
            Assert.AreEqual(1, users.Count);
            foreach (var user in users)
            {
                Assert.AreEqual("hbsis.leonardo.kobus", user.Name);
           }                     
        }

        [Test]
        public void TestMultipliesUser()
        {
            string xml = @"SVN_Logs_Examples\csprojeditorLog.xml";
            var plugin = new SVNPlugin();
            var user = plugin.Analyze();
            var result = user.First(p => p.Name.Equals("hbsis.leonardo.kobus"));
            Assert.AreEqual("hbsis.leonardo.kobus", result.Name);
            result = user.First(p => p.Name.Equals("jenkins.the.buttler"));
            Assert.AreEqual("jenkins.the.buttler", result.Name);
            Assert.AreEqual(2, user.Count);
        }


        
    }
}
