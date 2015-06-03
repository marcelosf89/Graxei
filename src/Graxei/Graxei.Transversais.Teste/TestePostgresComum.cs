using Graxei.Transversais.Comum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Teste
{
    [TestClass]
    public class TestePostgresComum
    {
        [TestMethod]
        public void DeveRetornarStringDeDataPropriaParaPostgres()
        {
            // Arrange
            string esperaResultado = "1984-03-15 21:12:26.825";
            DateTime data = new DateTime(1984, 3, 15, 21, 12, 26, 825);
            
            // Act
            string resultadoReal = PostgresComum.DataValida(data);

            // Assert
            Assert.AreEqual(esperaResultado, resultadoReal);
        }

        [TestMethod]
        public void DeveRetornarStringDeDataPropriaParaPostgresMaisUmaData()
        {
            // Arrange
            string esperaResultado = "1931-11-29 07:54:41.160";
            DateTime data = new DateTime(1931, 11, 29, 7, 54, 41, 160);

            // Act
            string resultadoReal = PostgresComum.DataValida(data);

            // Assert
            Assert.AreEqual(esperaResultado, resultadoReal);
        }

        [TestMethod]
        public void DeveRetornarStringDeDataPropriaParaPostgresZeroHora()
        {
            // Arrange
            string esperaResultado = "2003-06-01 00:11:19.972";
            DateTime data = new DateTime(2003, 6, 1, 0, 11, 19, 972);

            // Act
            string resultadoReal = PostgresComum.DataValida(data);

            // Assert
            Assert.AreEqual(esperaResultado, resultadoReal);
        }
    }
}
