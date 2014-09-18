using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LanguageExtension;
namespace LanguageExtension.Tests
{
    [TestFixture]
    public class LanguageBuilderTests
    {

        [Test]
        public void PointsAddTest()
        {
            var builder = new LanguageBuilder();
            var model = new LanguageModel("cs", "Somehting.cs");            
            builder.AddLanguage(model);
            builder.AddLanguage(model);
            model = new LanguageModel("java", "Program.java");
            builder.AddLanguage(model);
            model = new LanguageModel("py", "program.py");
            builder.AddLanguage(model);
            Assert.AreEqual(2, builder.LanguageAttributes["cs"].Count);
            Assert.AreEqual(1, builder.LanguageAttributes["java"].Count);
            Assert.AreEqual(1, builder.LanguageAttributes["py"].Count);
        }

        [Test]
        public void TransformPathInLanguageModel()
        {
            var test1 = @"C:\folder\file.cs";
            var test2 = @"..\folder\file2.java";
            var test3 = @"arquivo.cs";

            var model = LanguageBuilder.TransformPathToLanguageModel(test1);
            Assert.AreEqual("cs", model.Name);
            Assert.AreEqual("file.cs", model.File);

            model = LanguageBuilder.TransformPathToLanguageModel(test2);
            Assert.AreEqual("java", model.Name);
            Assert.AreEqual("file2.java", model.File);

            model = LanguageBuilder.TransformPathToLanguageModel(test3);
            Assert.AreEqual("cs", model.Name);
            Assert.AreEqual("arquivo.cs", model.File);

        }

        [Test]
        public void TransformPathInFolder()
        {
            var test = "folder1";
            var test2 = @".\TesteFolder\Folder";

            var model = LanguageBuilder.TransformPathToLanguageModel(test);
            Assert.AreEqual("Folder", model.Name);
            Assert.AreEqual("folder1", model.File);
            model = LanguageBuilder.TransformPathToLanguageModel(test2);
            Assert.AreEqual("Folder", model.Name);
            Assert.AreEqual("Folder", model.File);
        }


        public object LanguageModel { get; set; }
    }
}
