using System;

namespace Aceleradora
{
    public class Funcionario
    {
        /// <summary>
        /// Utilizado como costante, os valores que são utilizador para calcular os beneficios dos funcionarios
        /// </summary>
        #region Constantes da classe

        private const double TaxaJurosINSS = 0.10;
        private const double TaxaSeguroVida = 0.15;
        private const double TaxaJurosValeRefeicao = 0.11;
        private const double TaxaJurosValeTransporte = 0.05;

        //utilizado para calcular o bonos de 20% do salario do funcionario a cada 1 ano de contrato com a empresa
        private const int TotalDiasAno = 365;
        private const double TaxaBonosAnual = 0.20;
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
            set
            {
                ValorInss = value;
            }
        }

        public double ValorSeguroVida
        {
            get
            {
                return CalcularJuros(TaxaSeguroVida);
            }
            set
            {
                ValorSeguroVida = value;
            }
        }

        public double ValorValeRefeicao
        {
            get
            {
                return CalcularJuros(TaxaJurosValeRefeicao);
            }
            set
            {
                ValorValeRefeicao = value;
            }
        }

        public double ValorValeTransporte
        {
            get
            {
                return CalcularJuros(TaxaJurosValeTransporte);
            }
            set
            {
                ValorValeTransporte = value;
            }
        }

        public double ValorBonosFuncionario
        {
            get
            {
                return CalcularBonosAnual();
            }
            set
            {
                ValorBonosFuncionario = value;
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

        private double CalcularBonosAnual()
        {
            TimeSpan tempoContratacao = DateTime.Now - Contratacao ;

            var bonosFuncionario = Salario * tempoContratacao.TotalDays;

            bonosFuncionario = bonosFuncionario / TotalDiasAno;
            bonosFuncionario = bonosFuncionario * TaxaBonosAnual;

            return bonosFuncionario;
        }
    }
}
