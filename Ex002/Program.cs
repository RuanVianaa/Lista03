using System;
using System.Collections;
using cadastrolivros;
namespace cadastroEletro
{
    class ex002
    {
        static void salvarEletros(List<Livros> listaEletros)
        {
            using (StreamWriter sw = new StreamWriter("livros.txt"))
            {
                foreach (Livros e in listaEletros)
                {
                    sw.WriteLine(e.Titulo + ";" + e.Autor + ";" + e.Ano + ";" + e.Prateleira);
                }
            }
        }

        static void carregarEletros(List<Livros> listaEletros)
        {
            if (File.Exists("livros.txt"))
            {
                using (StreamReader sr = new StreamReader("livros.txt"))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        string[] dados = linha.Split(';');
                        Livros e = new Livros();
                        e.Titulo = dados[0];
                        e.Autor = dados[1];
                        e.Ano = int.Parse(dados[2]);
                        e.Prateleira = dados[3];
                        listaEletros.Add(e);
                    }
                }
            }
        }
        //b. Permita cadastrar livros.
        static void addLivro(List<Livros> lista, Livros novoLivro)
        {
            Livros NovoLivro = new Livros();
            Console.Write("Digite o título do livro: ");
            NovoLivro.Titulo = Console.ReadLine();
            Console.Write("Digite o autor: ");
            NovoLivro.Autor = Console.ReadLine();
            Console.Write("Digite o ano: ");
            NovoLivro.Ano = int.Parse(Console.ReadLine());
            Console.Write("Digite a prateleira: ");
            NovoLivro.Prateleira = Console.ReadLine();
            lista.Add(NovoLivro);

        }
        /*c. Procure um livro por título, perguntando ao usuário qual tıtulo deseja buscar,
apresente o nome e em qual prateleira o mesmo se encontra.*/
        static bool buscarLivro(List<Livros> lista, Livros novoLivro)
        {
            bool encontrou = false;
            Console.Write("Digite o título do livro que você deseja buscar: ");
            string tituloBusca = Console.ReadLine();
            foreach (Livros l in lista)
            {
                 if (l.Titulo.ToUpper().Contains(tituloBusca.ToUpper()))
                {
                    Console.WriteLine("Livro encontrado!");
                    Console.WriteLine("Título: " + l.Titulo);
                    Console.WriteLine("Prateleira: " + l.Prateleira);
                    encontrou = true;
                }
            }
            return encontrou;
        }
        //d. Mostre os dados de todos os livros cadastrados.
        static void mostrarLivros(List<Livros> lista)
        {
            Console.WriteLine("Livros cadastrados:");
            foreach (Livros l in lista)
            {
                Console.WriteLine("Título: " + l.Titulo);
                Console.WriteLine("Autor: " + l.Autor);
                Console.WriteLine("Ano: " + l.Ano);
                Console.WriteLine("Prateleira: " + l.Prateleira);
                Console.WriteLine("-----------------------");
            }
        }

        //e. Leia um ano e apresente todos os livros mais novos que o ano lido.
        static int anoMaisNovo(List<Livros> lista, int ano)
        {
            Console.Write("Digite um ano para buscar livros mais novos: ");
            ano = int.Parse(Console.ReadLine());
            Console.WriteLine("Livros mais novos que " + ano + ":");
            foreach (Livros l in lista)
            {
                if (l.Ano > ano)
                {
                    Console.WriteLine("Título: " + l.Titulo);
                    Console.WriteLine("Autor: " + l.Autor);
                    Console.WriteLine("Ano: " + l.Ano);
                    Console.WriteLine("Prateleira: " + l.Prateleira);
                    Console.WriteLine("-----------------------");
                }
            }
            return ano;
        }
        static int menu()
        {
            Console.WriteLine("1 - Adicionar livro");
            Console.WriteLine("2 - Buscar livro");
            Console.WriteLine("3 - Mostrar todos os livros");
            Console.WriteLine("4 - Mostrar livros mais novos que um ano");
            Console.WriteLine("0 - Sair do programa");
            Console.Write("Escolha uma opcao: ");
            return int.Parse(Console.ReadLine());
        }
        static void Main()
        {
            List<Livros> listaLivros = new List<Livros>();
            int opcao = 0;
            do
            {
                opcao = menu();
                switch (opcao)
                {
                    case 1:
                        Livros novoLivro = new Livros();
                        addLivro(listaLivros, novoLivro);
                        break;
                    case 2:
                        Livros buscar = new Livros();
                        buscarLivro(listaLivros, buscar);
                        break;
                    case 3:
                        mostrarLivros(listaLivros);
                        break;
                    case 4:
                        int ano = 0;
                        anoMaisNovo(listaLivros, ano);
                        break;
                    case 0:
                        Console.WriteLine("Saindo do programa...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }while (opcao != 0);
        }
    }
}
