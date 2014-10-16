using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace LanguageExtension.Tests
{
    [TestFixture]
    public class LanguageExperienceTests
    {

        [Test]
        public void lvlUpTest()
        {
            var builder = new LanguageBuilder();
            var languageModel = new LanguageModel("cs", "file1.cs");
            var lang = new SimpleLanguage(languageModel);
            languageModel = new LanguageModel("cs", "file.cs");
            lang.Add(languageModel);
            languageModel = new LanguageModel("cs", "file23.cs");
            lang.Add(languageModel);
            builder.LanguageAttributes.Add("cs", lang);

            var expLang = new LanguageExperience("Teste", @".\Contents\Level\lvlUpTest.prop", "Language Points");
            expLang.AddModel(builder);

            Assert.AreEqual(2, expLang.Level);
            Assert.AreEqual(3, expLang.ExperiencePoints);
            Assert.AreEqual("Teste", expLang.Name);
        }

        
    }
}
