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
            Assert.AreEqual(2, builder.LanguageAttributes["C#"].Count);
            Assert.AreEqual(1, builder.LanguageAttributes["Java"].Count);
            Assert.AreEqual(1, builder.LanguageAttributes["Phyton"].Count);
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

            Assert.AreEqual(expectedBuilder.LanguageAttributes["Phyton"].Count, builderMerge.LanguageAttributes["Phyton"].Count);
            Assert.AreEqual(expectedBuilder.LanguageAttributes["Java"].File[0], builderMerge.LanguageAttributes["Java"].File[0]);
            Assert.AreEqual(expectedBuilder.LanguageAttributes["C#"].File[0], builderMerge.LanguageAttributes["C#"].File[0]);



        }

        [Test]
        public void ValidLanguageTests()
        {            
            var builder = new LanguageBuilder();
            var valid = new LanguageModel("cs", "file1.cs");
            builder.AddLanguage(valid);
            valid = new LanguageModel("java", "file1.java");
            builder.AddLanguage(valid);
            valid = new LanguageModel("js", "file1.js");
            builder.AddLanguage(valid);


            Assert.AreEqual(3, builder.LanguageAttributes.Count);
            Assert.IsTrue(builder.LanguageAttributes.ContainsKey("JavaScript"));
            Assert.IsTrue(builder.LanguageAttributes.ContainsKey("C#"));
            Assert.IsTrue(builder.LanguageAttributes.ContainsKey("Java"));
        }

        [Test]
        public void InvalidLanguageTest()
        {
            var builder = new LanguageBuilder();
            var valid = new LanguageModel("rb", "file1.rb");
            builder.AddLanguage(valid);
            valid = new LanguageModel("html", "file1.html");
            builder.AddLanguage(valid);            

            Assert.AreEqual(0, builder.LanguageAttributes.Count);
            Assert.IsFalse(builder.LanguageAttributes.ContainsKey("Ruby"));
            Assert.IsFalse(builder.LanguageAttributes.ContainsKey("HTML"));            
        }

        [Test]
        public void ValidAndInvalidLanguageTest()
        {
            var builder = new LanguageBuilder();
            var valid = new LanguageModel("rb", "file1.rb");
            builder.AddLanguage(valid);
            valid = new LanguageModel("html", "file1.html");
            builder.AddLanguage(valid);
            valid = new LanguageModel("java", "file1.java");
            builder.AddLanguage(valid);
            valid = new LanguageModel("js", "file1.js");
            builder.AddLanguage(valid);

            Assert.AreEqual(2, builder.LanguageAttributes.Count);
            Assert.IsTrue(builder.LanguageAttributes.ContainsKey("JavaScript"));
            Assert.IsTrue(builder.LanguageAttributes.ContainsKey("Java"));            
            Assert.IsFalse(builder.LanguageAttributes.ContainsKey("Ruby"));
            Assert.IsFalse(builder.LanguageAttributes.ContainsKey("HTML"));            
        }

    }
}
