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
    public class SVNDBTest
    {



        [Test]
        public void MergeTest()
        {
            SVNModel expected = new SVNModel();
            expected.AddAdd(1);
            expected.AddAdd(1);
            expected.AddAdd(1);
            expected.AddDeleted(2);

            SVNModel model1 = new SVNModel();
            model1.AddAdd(3);
            model1.AddDeleted(1);
            SVNModel model2 = new SVNModel();
            model2.AddDeleted(1);

            var mergeResult = model1.Merge(model2);
            var svnMergeResult = (SVNModel)mergeResult;

            Assert.AreEqual(expected.Add, svnMergeResult.Add);
            Assert.AreEqual(expected.Deleted, svnMergeResult.Deleted);
        }
    }
}
