using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aceleradora;

namespace TestesAceleradora
{
    [TestClass]
    public class TestesFuncionario
    {
        public Funcionario funcionario;

        public TestesFuncionario()
        {
            funcionario = RetornaFuncionarioPadrao();
        }

        [TestMethod]
        public void TestaCalcularValorINSS()
        {
            double valorEsperado = 150;
            Assert.AreEqual(funcionario.ValorInss, valorEsperado);
            Assert.AreEqual((funcionario.ValorInss / funcionario.Salario), Funcionario.TaxaJurosINSS);
        }

        [TestMethod]
        public void TestaCalcularValorSeguroVida()
        {
            double valorEsperado = 225;
            Assert.AreEqual(funcionario.ValorSeguroVida, valorEsperado);
            Assert.AreEqual((funcionario.ValorSeguroVida / funcionario.Salario), Funcionario.TaxaJurosSeguroVida);
        }

        [TestMethod]
        public void TestaCalcularValorValeRefeicao()
        {
            double valorEsperado = 165;
            Assert.AreEqual(funcionario.ValorValeRefeicao, valorEsperado);
            Assert.AreEqual((funcionario.ValorValeRefeicao / funcionario.Salario), Funcionario.TaxaJurosValeRefeicao);
        }

        [TestMethod]
        public void TestaCalcularValorValeTransporte()
        {
            double valorEsperado = 75;
            Assert.AreEqual(funcionario.ValorValeTransporte, valorEsperado);
            Assert.AreEqual((funcionario.ValorValeTransporte / funcionario.Salario), Funcionario.TaxaJurosValeTransporte);
        }

        [TestMethod]
        public void TestaCalcularBonosAnual()
        {
            //teste baseado no funcionario com 2 de contratação e ganhando 1500 reais o bonos dele nesses dois anos é de 600 reais
            double valorEsperado = 600;
            Assert.AreEqual(funcionario.ValorBonosFuncionario, valorEsperado);
        }

        [TestMethod]
        public void TestaCalcularTotalImpostos()
        {
            double VT = 75;
            double VR = 165;
            double SeguroVida = 225;
            double INSS = 150;
            double totalImpostos = VT + VR + SeguroVida + INSS;
            Assert.AreEqual(funcionario.TotalImpostos(), totalImpostos);
        }

        [TestMethod]
        public void TestaTotalGastos()
        {
            // valor correspondente respectivamente á Bônus, Salário, impostos 
            var valorEsperado = 600 + 1500 + 615;
            Assert.AreEqual(funcionario.TotalGastos(), valorEsperado);
        }

        private Funcionario RetornaFuncionarioPadrao()
        {
            return new Funcionario()
            {
                Contratacao = DateTime.Now.AddYears(-2).AddDays(1),
                Nome = "Rafael Eduardo Rauber",
                Salario = 1500
            };
        }
    }
}
