using System;
using System.Collections.Generic;
using System.Text;

namespace Aceleradora
{
    public class Program
    {
        private static Dictionary<int, Funcionario> ListaFuncionarios;

        private enum OpcaoMenu: int
        {
            SIM = 1,
            NAO = 2
        }

        static void Main(string[] args)
        {
            // Coloquei este codigo para fechar o sistema somente se o usuario quiser 
            // se acontecer alguma Exception vai começar do passo 1 com os dados de funcionario salvos
            ExecutarSistema(args, ref ListaFuncionarios);
        }

        private static void ExecutarSistema(string[] args, ref Dictionary<int, Funcionario> ListaFuncionarios)
        {
            try
            {
                if (ListaFuncionarios == null)
                {
                    ListaFuncionarios = new Dictionary<int, Funcionario>();
                }

                CadastroFuncionarios();
                ExibirRelatorioFuncionarios();
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine(" ---------------------------------------------------------");
                Console.WriteLine("| Aconteceu um erro inesperado, por favor tente novamente |");
                Console.WriteLine(" ---------------------------------------------------------");
                Console.WriteLine("");
            }
            finally
            {
                Main(args);
            }
        }

        private static void MontaRelatorio()
        {
            StringBuilder textoSaida = new StringBuilder();
            double impostos = 0;
            double salarios = 0;
            double bonosTotal = 0;

            foreach (var funcionario in ListaFuncionarios.Values)
            {
                textoSaida.AppendLine("");
                textoSaida.Append($"Nome: {funcionario.Nome}; ");
                textoSaida.Append($"Bônus: {FormataValoresMoedaBrasil(funcionario.ValorBonosFuncionario)}; ");
                textoSaida.Append($"INSS: {FormataValoresMoedaBrasil(funcionario.ValorInss)}; ");
                textoSaida.Append($"SegVida: {FormataValoresMoedaBrasil(funcionario.ValorSeguroVida)}; ");
                textoSaida.Append($"VR: {FormataValoresMoedaBrasil(funcionario.ValorValeRefeicao)}; ");
                textoSaida.Append($"VT: {FormataValoresMoedaBrasil(funcionario.ValorValeTransporte)}; ");
                textoSaida.Append($"Custo Total: {FormataValoresMoedaBrasil(funcionario.TotalGastos())}; ");
                textoSaida.AppendLine("");

                impostos += funcionario.TotalImpostos();
                salarios += funcionario.Salario;
                bonosTotal += funcionario.ValorBonosFuncionario;
            }

            textoSaida.AppendLine($"Total Impostos: {FormataValoresMoedaBrasil(impostos)}");
            textoSaida.AppendLine("");
            textoSaida.AppendLine($"Total Salários: {FormataValoresMoedaBrasil(salarios)}");
            textoSaida.AppendLine("");
            textoSaida.AppendLine($"Total Bônus:    {FormataValoresMoedaBrasil(bonosTotal)}");
            textoSaida.AppendLine("");
            textoSaida.AppendLine($"Total Geral:    {FormataValoresMoedaBrasil(impostos + salarios + bonosTotal)}");

            Console.Write(textoSaida.ToString());
        }

        private static void ExibirRelatorioFuncionarios()
        {
            MensagemSaida();
            MontaRelatorio();
            FecharSessao();
        }

        private static void FecharSessao()
        {
            OpcaoMenu respostaMenu = PerguntaOpcaoMenu("Fechar o sistema de funcionários: ");
            switch (respostaMenu)
            {
                case OpcaoMenu.SIM:
                    Console.WriteLine("Aperte enter para finalizar a sessão");
                    Console.ReadKey();
                    break;
                case OpcaoMenu.NAO:
                    CadastroFuncionarios();
                    ExibirRelatorioFuncionarios();
                    break;
                default:
                    Console.ReadKey();
                    break;
            }
        }

        private static void MensagemEntrada()
        {
            Console.WriteLine("---------------");
            Console.WriteLine("ENTRADA");
            Console.WriteLine("---------------");
            Console.WriteLine("");
        }

        private static void MensagemSaida()
        {
            Console.WriteLine("");
            Console.WriteLine("---------------");
            Console.WriteLine("SAÍDA");
            Console.WriteLine("---------------");
            Console.WriteLine("");
        }

        private static void CadastroFuncionarios()
        {
            MensagemEntrada();

            OpcaoMenu respostaMenu = OpcaoMenu.SIM;

            if (ListaFuncionarios.Count > 0)
            {
                respostaMenu = PerguntaOpcaoMenu("Cadastrar outro funcionário: ");
            }
            
            while(respostaMenu == OpcaoMenu.SIM)
            {
                var funcionario = MontaFuncionario();
                var QuantidadeFuncionarios = ListaFuncionarios.Count + 1;

                ListaFuncionarios.Add(QuantidadeFuncionarios, funcionario);

                respostaMenu = PerguntaOpcaoMenu("Cadastrar outro funcionário: ");
            }
        }

        private static OpcaoMenu PerguntaOpcaoMenu(string mensagem)
        {
            Console.WriteLine("");
            Console.WriteLine("Responda o menu da seguinte maneira");
            Console.WriteLine("1 para SIM");
            Console.WriteLine("2 para NÃO");
            Console.Write(mensagem);

            var valorDigitado = Console.ReadLine();
            var opcaoValida = Int32.TryParse(valorDigitado, out int respostaMenu);

            if (!opcaoValida)
            {
                respostaMenu = (int)RespostaInvalidaMenu(valorDigitado, mensagem);
            }

            if ((OpcaoMenu)respostaMenu != OpcaoMenu.SIM & (OpcaoMenu)respostaMenu != OpcaoMenu.NAO)
            {
                respostaMenu = (int)RespostaInvalidaMenu(valorDigitado, mensagem);
            }

            return (OpcaoMenu)respostaMenu;
        }

        private static OpcaoMenu RespostaInvalidaMenu(string valorDigitado, string mensagem)
        {
            Console.WriteLine($"O valor {valorDigitado} é uma opção inválida, por favor digite novamente ");
            return PerguntaOpcaoMenu(mensagem);
        }

        private static Funcionario MontaFuncionario()
        {
            Funcionario funcionario = new Funcionario()
            {
                Nome = BuscaNomeFuncionario(),
                Salario = BuscaSalario(),
                Contratacao = BuscaContratacao()
            };

            return funcionario;
        }

        private static string BuscaNomeFuncionario()
        {
            Console.Write("Nome do Funcionário: ");

            var nome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Informe o nome do funcionário");
                return BuscaNomeFuncionario();
            }

            return nome;
        }

        private static double BuscaSalario()
        {
            Console.Write("Salário do Funcionário: ");

            var valorDigitado = Console.ReadLine();
            var salarioValido = Double.TryParse(valorDigitado, out double salario);

            if (!salarioValido)
            {
                Console.WriteLine($"O valor {valorDigitado} é um salário inválido, por favor digite novamente ");
                return BuscaSalario();
            }

            return salario;
        }

        private static DateTime BuscaContratacao()
        {
            Console.Write("Data de contratação do Funcionário: ");

            var valorDigitado = Console.ReadLine();
            
            var dataValida = DateTime.TryParse(valorDigitado, out DateTime contratacao);

            if (!dataValida)
            {
                Console.WriteLine($"O valor {valorDigitado} é uma data inválida, por favor digite novamente ");
                return BuscaContratacao();
            }

            return contratacao;
        }

        private static string FormataValoresMoedaBrasil(double valor)
        {
            return valor.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-br"));
        }
    }
}
