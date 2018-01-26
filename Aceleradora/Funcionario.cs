using System;

namespace Aceleradora
{
    public class Funcionario
    {
        /// <summary>
        /// Utilizado como costante, os valores que são utilizador para calcular os beneficios dos funcionarios
        /// </summary>
        #region Constantes da classe

        public const double TaxaJurosINSS = 0.10;
        public const double TaxaJurosSeguroVida = 0.15;
        public const double TaxaJurosValeRefeicao = 0.11;
        public const double TaxaJurosValeTransporte = 0.05;

        //utilizado para calcular o bonos de 20% do salario do funcionario a cada 1 ano de contrato com a empresa
        public const int TotalDiasAno = 365;
        public const double TaxaBonosAnual = 0.20;
        #endregion

        /// <summary>
        /// Estrutura publicas da classe
        /// </summary>
        #region Estrutura Publicas

        public string Nome { get; set; }

        public DateTime Contratacao { get; set; }

        public double Salario { get; set; }

        public double ValorInss
        {
            get
            {
                return CalcularJuros(TaxaJurosINSS);
            }
        }

        public double ValorSeguroVida
        {
            get
            {
                return CalcularJuros(TaxaJurosSeguroVida);
            }
        }

        public double ValorValeRefeicao
        {
            get
            {
                return CalcularJuros(TaxaJurosValeRefeicao);
            }
        }

        public double ValorValeTransporte
        {
            get
            {
                return CalcularJuros(TaxaJurosValeTransporte);
            }
        }

        public double ValorBonosFuncionario
        {
            get
            {
                TimeSpan tempoContratacao = DateTime.Now - Contratacao;

                var bonosFuncionario = Salario * (int)tempoContratacao.TotalDays;

                bonosFuncionario = bonosFuncionario / TotalDiasAno;
                bonosFuncionario = bonosFuncionario * TaxaBonosAnual;

                return bonosFuncionario;
            }
        }

        public double TotalImpostos()
        {
            return (ValorInss + ValorSeguroVida + ValorValeRefeicao + ValorValeTransporte);
        }

        public double TotalGastos()
        {
            return (Salario + TotalImpostos() + ValorBonosFuncionario);
        }

        #endregion

        private double CalcularJuros(double taxaJuros)
        {
            return Salario * taxaJuros;
        }
    }
}
