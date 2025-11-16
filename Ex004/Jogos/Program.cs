using System;

namespace ControleJogos
{
    class Program
    {
        static Jogo[] colecao;

        static void Main(string[] args)
        {
            Console.WriteLine("--- Sistema de Catálogo e Controle de Jogos ---");
            Console.Write("Quantos jogos deseja cadastrar inicialmente? ");
            
            int n = int.Parse(Console.ReadLine());
            colecao = new Jogo[n];
            CadastrarJogos(n);
            string opcao = "";
            while (opcao != "0")
            {
                Console.Clear(); 
                Console.WriteLine("--- Menu Principal ---");
                Console.WriteLine("1: Procurar jogo por Título");
                Console.WriteLine("2: Listar jogos por Console");
                Console.WriteLine("3: Realizar Empréstimo");
                Console.WriteLine("4: Devolver Jogo");
                Console.WriteLine("5: Listar Jogos Emprestados");
                Console.WriteLine("0: Sair");
                Console.Write("\nEscolha uma opção: ");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        BuscarPorTitulo();
                        break;
                    case "2":
                        ListarPorConsole();
                        break;
                    case "3":
                        RealizarEmprestimo();
                        break;
                    case "4":
                        DevolverJogo();
                        break;
                    case "5":
                        ListarEmprestados();
                        break;
                    case "0":
                        Console.WriteLine("Obrigado por usar o sistema!");
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

                if (opcao != "0")
                {
                    Console.WriteLine("\nPressione Enter para continuar...");
                    Console.ReadLine();
                }
            }
        }

        
        static void CadastrarJogos(int n)
        {
            Console.Clear();
            Console.WriteLine($"--- Cadastro de {n} Jogos ---");
            for (int i = 0; i < n; i++)
            {
                colecao[i] = new Jogo(); 

                Console.WriteLine($"\n--- Jogo {i + 1}/{n} ---");
                
                Console.Write("Título (max 30): ");
                colecao[i].Titulo = Console.ReadLine(); 

                Console.Write("Console (max 15): ");
                colecao[i].Console = Console.ReadLine(); 

                Console.Write("Ano de lançamento: ");
                colecao[i].Ano = int.Parse(Console.ReadLine());

                Console.Write("Ranking (1-5): ");
                colecao[i].Ranking = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("\nCadastro inicial concluído!");
        }
        static void BuscarPorTitulo()
        {
            Console.Write("Digite o título do jogo a procurar: ");
            string tituloBusca = Console.ReadLine();
            bool encontrado = false;

            foreach (Jogo jogo in colecao)
            {
                if (jogo.Titulo.Equals(tituloBusca, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\n--- Jogo Encontrado ---");
                    MostrarDetalhesJogo(jogo);
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Jogo não encontrado na coleção.");
            }
        }

        static void ListarPorConsole()
        {
            Console.Write("Digite o nome do console: ");
            string consoleBusca = Console.ReadLine();
            bool encontrado = false;
            
            Console.WriteLine($"\n--- Jogos para {consoleBusca} ---");

            foreach (Jogo jogo in colecao)
            {
                if (jogo.Console.Equals(consoleBusca, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"- {jogo.Titulo} (Ano: {jogo.Ano}, Ranking: {jogo.Ranking})");
                    encontrado = true;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Nenhum jogo encontrado para este console.");
            }
        }
        static void RealizarEmprestimo()
        {
            Console.Write("Digite o título do jogo a emprestar: ");
            string tituloBusca = Console.ReadLine();
            Jogo jogoEncontrado = null; 
            foreach (Jogo jogo in colecao)
            {
                if (jogo.Titulo.Equals(tituloBusca, StringComparison.OrdinalIgnoreCase))
                {
                    jogoEncontrado = jogo;
                    break;
                }
            }
            if (jogoEncontrado == null)
            {
                Console.WriteLine("Erro: Jogo não encontrado.");
                return;
            }
            if (jogoEncontrado.InfoEmprestimo.emprestado == 'S')
            {
                Console.WriteLine($"Erro: Jogo já está emprestado para {jogoEncontrado.InfoEmprestimo.nomePessoa}.");
            }
            else
            {
                Console.Write("Digite o nome da pessoa que está pegando o jogo: ");
                string nome = Console.ReadLine();

                jogoEncontrado.InfoEmprestimo.nomePessoa = nome;
                jogoEncontrado.InfoEmprestimo.data = DateTime.Now;
                jogoEncontrado.InfoEmprestimo.emprestado = 'S';

                Console.WriteLine($"Sucesso! Jogo '{jogoEncontrado.Titulo}' emprestado para {nome} em {jogoEncontrado.InfoEmprestimo.data.ToShortDateString()}.");
            }
        }

        static void DevolverJogo()
        {
            Console.Write("Digite o título do jogo a devolver: ");
            string tituloBusca = Console.ReadLine();
            Jogo jogoEncontrado = null;

            foreach (Jogo jogo in colecao)
            {
                if (jogo.Titulo.Equals(tituloBusca, StringComparison.OrdinalIgnoreCase))
                {
                    jogoEncontrado = jogo;
                    break;
                }
            }

            if (jogoEncontrado == null)
            {
                Console.WriteLine("Erro: Jogo não encontrado.");
                return;
            }

            if (jogoEncontrado.InfoEmprestimo.emprestado == 'N')
            {
                Console.WriteLine("Erro: Este jogo já está na coleção (não estava emprestado).");
            }
            else
            {
                string pessoaQueDevolveu = jogoEncontrado.InfoEmprestimo.nomePessoa;
                jogoEncontrado.InfoEmprestimo.emprestado = 'N';
                jogoEncontrado.InfoEmprestimo.nomePessoa = "";
                jogoEncontrado.InfoEmprestimo.data = DateTime.MinValue;
                Console.WriteLine($"Sucesso! Jogo '{jogoEncontrado.Titulo}' devolvido por {pessoaQueDevolveu}.");
            }
        }

        static void ListarEmprestados()
        {
            Console.WriteLine("--- Relatório de Jogos Emprestados ---");
            bool algumEmprestado = false;

            foreach (Jogo jogo in colecao)
            {
                if (jogo.InfoEmprestimo.emprestado == 'S')
                {
                    Console.WriteLine($"\nTítulo: {jogo.Titulo} ({jogo.Console})");
                    Console.WriteLine($"  Emprestado para: {jogo.InfoEmprestimo.nomePessoa}");
                    Console.WriteLine($"  Data: {jogo.InfoEmprestimo.data.ToShortDateString()}"); 
                    algumEmprestado = true;
                }
            }

            if (!algumEmprestado)
            {
                Console.WriteLine("Nenhum jogo está emprestado no momento.");
            }
        }
        static void MostrarDetalhesJogo(Jogo jogo)
        {
            Console.WriteLine($"Título: {jogo.Titulo}");
            Console.WriteLine($"Console: {jogo.Console}");
            Console.WriteLine($"Ano: {jogo.Ano}");
            Console.WriteLine($"Ranking: {jogo.Ranking} estrelas");

            if (jogo.InfoEmprestimo.emprestado == 'S')
            {
                Console.WriteLine($"Status: Emprestado para {jogo.InfoEmprestimo.nomePessoa} em {jogo.InfoEmprestimo.data.ToShortDateString()}");
            }
            else
            {
                Console.WriteLine("Status: Disponível na coleção");
            }
        }
    }
}