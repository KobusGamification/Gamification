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
            var svnPoints = reader.Read(xml)[0];
            Assert.AreEqual(2, ((SVNModel)svnPoints.ExtensionPoint["SVNExtension"]).Modified);
            Assert.AreEqual(0, ((SVNModel)svnPoints.ExtensionPoint["SVNExtension"]).Merges);
            Assert.AreEqual(0, ((SVNModel)svnPoints.ExtensionPoint["SVNExtension"]).Add);
            Assert.AreEqual(0, ((SVNModel)svnPoints.ExtensionPoint["SVNExtension"]).Deleted);
        }
        
        [Test]
        public void ReadEmptyRevision()
        {
            var xml = @".\SVN_Logs_Examples\csprojeditorLog.xml";
            var reader = new SVNReader();
            var startRevision = 133;
            var svnPoints = reader.Read(xml, startRevision);
            Assert.AreEqual(0, svnPoints.Count);
        }

        [Test]
        public void ReadValidRevision()
        {
            var xml = @".\SVN_Logs_Examples\csprojeditorLog.xml";
            var reader = new SVNReader();
            var startRevision = 132;
            var svnPoints = reader.Read(xml, startRevision)[0];
            Assert.AreEqual(0, ((SVNModel)svnPoints.ExtensionPoint["SVNExtension"]).Merges);
            Assert.AreEqual(4, ((SVNModel)svnPoints.ExtensionPoint["SVNExtension"]).Modified);
            Assert.AreEqual(0, ((SVNModel)svnPoints.ExtensionPoint["SVNExtension"]).Add);
            Assert.AreEqual(0, ((SVNModel)svnPoints.ExtensionPoint["SVNExtension"]).Deleted);            
        }

        [Test]
        public void VariousModels()
        {
            var xml = @".\SVN_Logs_Examples\csprojeditorLog.xml";
            var reader = new SVNReader();
            var svnPoints = reader.Read(xml);
            var buttler = svnPoints.First(p => p.Name.Equals("jenkins.the.buttler"));
            Assert.AreEqual(0, ((SVNModel)buttler.ExtensionPoint["SVNExtension"]).Add);
            Assert.AreEqual(0, ((SVNModel)buttler.ExtensionPoint["SVNExtension"]).Merges);
            Assert.AreEqual(0, ((SVNModel)buttler.ExtensionPoint["SVNExtension"]).Modified);
            Assert.AreEqual(1, ((SVNModel)buttler.ExtensionPoint["SVNExtension"]).Deleted);
            Assert.AreEqual("jenkins.the.buttler", buttler.Name);
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
