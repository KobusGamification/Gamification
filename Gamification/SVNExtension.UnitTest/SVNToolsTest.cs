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
        
        [Test]
        public void ReadEmptyRevision()
        {
            var xml = @".\SVN_Logs_Examples\csprojeditorLog.xml";
            var reader = new SVNReader();
            var startRevision = 133;
            var svnPoints = reader.Read(xml, startRevision);
            Assert.AreEqual(0, svnPoints.Merges);
            Assert.AreEqual(0, svnPoints.Modified);
            Assert.AreEqual(0, svnPoints.Add);
            Assert.AreEqual(0, svnPoints.Deleted);
            Assert.AreEqual(133, svnPoints.CurrentRevision);
        }

        [Test]
        public void ReadValidRevision()
        {

            var xml = @".\SVN_Logs_Examples\csprojeditorLog.xml";
            var reader = new SVNReader();
            var startRevision = 132;
            var svnPoints = reader.Read(xml, startRevision);
            Assert.AreEqual(0, svnPoints.Merges);
            Assert.AreEqual(4, svnPoints.Modified);
            Assert.AreEqual(0, svnPoints.Add);
            Assert.AreEqual(0, svnPoints.Deleted);
            Assert.AreEqual(133, svnPoints.CurrentRevision);
        }

        //preparar objetos de calculos (provavelmente modelos que vão para o banco)
        //sistema de configuração para outros repositorios.
        //badeges? talvez dps de pronto pelo jeito :| 
        //TODO: PEGAR ESSES PONTOS E SEPARAR POR USUARIOS =)



        /*
         * 
         * chave de usuario ...
         * defini o usuario 
         * o usuario tem um hash map <string nomeObjeto, object value>
         *  
         *  void analyze deve retornar um objeto Prop que é o objeto acima
         *  
         * 
         * badge vai ser um objeto separado (você diz o nome do objeto 
         * do hash map ele instancia e faz as regras que você quer)
         * */
    }
}
