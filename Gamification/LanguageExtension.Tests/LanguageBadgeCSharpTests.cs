using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LanguageExtension.Badges.CSharp;
using Extension;
using DatabaseAccess;
namespace LanguageExtension.Tests
{
    [TestFixture]
    public class LanguageBadgeCSharpTests
    {

        [SetUp]
        public void SetUp()
        {

        }


        [Test]
        public void LevelUpBadgeTest()
        {
            var user = new DefaultUser("Language");
            var experience = new LanguageExperience("Language", ".\\Contents\\Level\\lvlUpTest.prop", "cs");
            experience.AddExperience(10);
            user.ExperiencePoints.Add("cs", experience);            
            var badge = new LanguageFirstCSharpLevel();
            badge.Compute(user);
            Assert.IsTrue(badge.Gained);
        }

    }
}
