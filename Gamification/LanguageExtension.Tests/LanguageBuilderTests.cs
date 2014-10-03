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

        [Test]
        public void MergeTest()
        {
            var expectedBuilder = new LanguageBuilder();
            expectedBuilder.AddLanguage(new LanguageModel("py", "file1.py"));
            expectedBuilder.AddLanguage(new LanguageModel("py", "file2.py"));
            expectedBuilder.AddLanguage(new LanguageModel("cs", "file1.cs"));
            expectedBuilder.AddLanguage(new LanguageModel("java", "file1.java"));

            var builder1 = new LanguageBuilder();
            builder1.AddLanguage(new LanguageModel("py", "file1.py"));
            builder1.AddLanguage(new LanguageModel("py", "file2.py"));

            var builder2 = new LanguageBuilder();
            builder2.AddLanguage(new LanguageModel("cs", "file1.cs"));
            builder2.AddLanguage(new LanguageModel("java", "file1.java"));

            var resultMerge = builder1.Merge(builder2);
            var builderMerge = (LanguageBuilder)resultMerge;

            Assert.AreEqual(expectedBuilder.LanguageAttributes["py"].Count, builderMerge.LanguageAttributes["py"].Count);
            Assert.AreEqual(expectedBuilder.LanguageAttributes["java"].File[0], builderMerge.LanguageAttributes["java"].File[0]);
            Assert.AreEqual(expectedBuilder.LanguageAttributes["cs"].File[0], builderMerge.LanguageAttributes["cs"].File[0]);



        }

    }
}
