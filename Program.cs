using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
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
            Console.Write("Digite o tempo de uso diário (em hora): ");
            NovoEletro.TempoUsoDiario = double.Parse(Console.ReadLine());
            lista.Add(NovoEletro);

        }

        static void salvarEletros(List<Eletro> listaEletros)
        {
            using (StreamWriter sw = new StreamWriter("eletros.txt"))
            {
                foreach (Eletro e in listaEletros)
                {
                    sw.WriteLine(e.nome + ";" + e.potencia + ";" + e.TempoUsoDiario);
                }
            }
        }
        static void carregarEletros(List<Eletro> listaEletros)
        {
            if (File.Exists("eletros.txt"))
            {
                using (StreamReader sr = new StreamReader("eletros.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');
                        Console.WriteLine(parts.Length);
                        Eletro e = new Eletro();
                        e.nome = parts[0];
                        e.potencia = double.Parse(parts[1]);
                        e.TempoUsoDiario = double.Parse(parts[2]);
                        listaEletros.Add(e);
                    }
                }
            }
        }

        static void mostrarEletros(List<Eletro> listaEletros)
        {
            foreach (Eletro e in listaEletros)
            {
                Console.WriteLine("Nome: " + e.nome);
                Console.WriteLine("Potencia: " + e.potencia);
                Console.WriteLine("Tempo Medio de Uso Diario: " + e.TempoUsoDiario);
                Console.WriteLine("-----------------------------");
            }
        }

        static bool buscarEletro(List<Eletro> listaEletros, string nomeBusca)
        {
            bool encontrou = false;
            foreach (Eletro e in listaEletros)
            {
                if (e.nome.ToUpper().Contains(nomeBusca.ToUpper()))
                {
                    Console.WriteLine("**** Eletrodoméstico ****");
                    Console.WriteLine($"Nome: {e.nome}");
                    Console.WriteLine($"Potência: {e.potencia}");
                    Console.WriteLine($"Tempo médio de uso: {e.TempoUsoDiario}");
                    encontrou = true;
                }//fim if
            }//fim foreach
            return encontrou;
        }

        static bool buscarPotencia(List<Eletro> listaEletros, double potenciaBusca)
        {
            bool encontrou = false;
            foreach (Eletro e in listaEletros)
            {
                if (e.potencia >= potenciaBusca)
                {
                    Console.WriteLine("**** Eletrodoméstico ****");
                    Console.WriteLine($"Nome: {e.nome}");
                    Console.WriteLine($"Potência: {e.potencia}");
                    Console.WriteLine($"Tempo médio de uso: {e.TempoUsoDiario}");
                    encontrou = true;
                }//fim if
            }//fim foreach
            return encontrou;
        }

        static double consumoTotalKwDia(List<Eletro> listaEletros)
        {
            double total = 0;
            foreach (Eletro e in listaEletros)
            {
                total += (e.potencia * e.TempoUsoDiario);
            }
            return total;
        }

        static int menu()
        {
            Console.WriteLine("1 - Adicionar Eletrodomestico");
            Console.WriteLine("2 - Mostrar Eletrodomesticos");
            Console.WriteLine("3 - Buscar pelo nome");
            Console.WriteLine("4 - Buscar por potencia (kw/h) ");
            Console.WriteLine("5 - Calcular Consumo diário e mensal");
            Console.WriteLine("6 - Calcular Custo total por eletrodomestico");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opcao: ");
            return int.Parse(Console.ReadLine());
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
                    case 3: Console.Write("Eletrodomestico para busca: ");
                        string nome = Console.ReadLine();
                        bool encontrou = buscarEletro(ListaEletros, nome);
                        if(!encontrou)
                            Console.WriteLine("Eletrodomestico nao encontrado!");
                        break;
                    case 4: Console.Write("Potencia para busca (kw/h): ");
                        double potenciaBusca = double.Parse(Console.ReadLine());
                        bool encontrouPotencia = buscarPotencia(ListaEletros, potenciaBusca);
                        if (!encontrouPotencia)
                            Console.WriteLine("Eletrodomestico nao encontrado!");
                        break;
                    case 5: Console.WriteLine("Valor do Kw/h em R$");
                        double valorKw = double.Parse(Console.ReadLine());
                        double consumoKw = consumoTotalKwDia(ListaEletros);
                        Console.WriteLine($"Consumo diário em Kw:{consumoKw:F2}");
                        Console.WriteLine($"Consumo diário em R$:{(valorKw * consumoKw)}");
                        Console.WriteLine($"Consumo mensal em Kw:{(consumoKw * 30):F2}");
                        Console.WriteLine($"Consumo mensal em R$:{(valorKw * consumoKw*30):F2}");
                        break;
                    case 0:
                        Console.WriteLine("Saindo do sistema...");
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            } while (opcao != 0);
        }
    }
}

