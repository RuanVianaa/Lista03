using System;
using System.ComponentModel.Design;
namespace cadastroEletro
{
    class Program
    {
        static void addEletro(List<Eletro> lista, Eletro novoEletro)
        {
            Eletro NovoEletro = new Eletro();
            Console.Write("Digite o nome do eletrodoméstico: ");
            NovoEletro.nome = Console.ReadLine();
            Console.Write("Digite a potência: ");
            NovoEletro.potencia = double.Parse(Console.ReadLine());
            Console.Write("Digite o tempo de uso diário: ");
            NovoEletro.TempoUsoDiario = double.Parse(Console.ReadLine());
            lista.Add(NovoEletro);

        }
        static void mostrarEletros(List<Eletro> lista)
        {
            Console.WriteLine("Eletrodomésticos cadastrados:");
            foreach (Eletro eletro in lista)
            {
                Console.WriteLine($"Nome: {eletro.nome}, Potência: {eletro.potencia}W, Tempo de Uso Diário: {eletro.TempoUsoDiario} horas");
            }
        }
        static int menu()
        {
            int opcao;
            Console.WriteLine("*** Sistema de Cadastro de Eletrodomésticos***");
            Console.WriteLine("1- Adicionar Eletro");
            Console.WriteLine("2- Mostrar Eletros");
            Console.WriteLine("0- Sair do Sistema");
            opcao = int.Parse(Console.ReadLine());
            return opcao;
        }
        static void Main()
        {
            List<Eletro> ListaEletros = new List<Eletro>();
            int opcao = 0;
            do
            {
                opcao = menu();
                switch (opcao)
                {
                    case 1:
                    addEletro(ListaEletros, new Eletro());
                        break;
                    case 2:
                        mostrarEletros(ListaEletros);
                        break;
                    case 0:
                        Console.WriteLine("Saindo do sistema...");
                        break;
                }
            } while (opcao != 0);
        }
    }
}

