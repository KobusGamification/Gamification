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
    }
}
