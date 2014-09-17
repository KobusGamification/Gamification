using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVNExtension;
using NUnit.Framework;
namespace SVNExtension.UnitTest
{
    [TestFixture]
    public class SVNToolsTest
    {

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void ReadNullLog()
        {
            var reader = new SVNReader();
            reader.Read(null);
        }

        [Test]
        public void ReadSimpleLog()
        {
            var xml = @".\SVN_Logs_Examples\SimpleLog.xml";
            var reader = new SVNReader();
            var svnPoints = reader.Read(xml);
            Assert.AreEqual(2, svnPoints.Modified);
            Assert.AreEqual(0, svnPoints.Merges);
            Assert.AreEqual(0, svnPoints.Add);
            Assert.AreEqual(0, svnPoints.Deleted);
        }
        
        //VERIFICAR sistema de revision (parametrizar de onde ele começa)
        //preparar objetos de calculos (provavelmente modelos que vão para o banco)
        //sistema de configuração para outros repositorios.
        //badeges? talvez dps de pronto pelo jeito :| 
        //TODO: PEGAR ESSES PONTOS E SEPARAR POR USUARIOS =)
    }
}
