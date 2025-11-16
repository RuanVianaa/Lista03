using System;
using System.IO; // Necessário para Salvar/Carregar (f)

namespace FazendaGado
{
    class Program
    {
        // O vetor (array) de gado, acessível por todos os métodos
        static Gado[] rebanho;
        
        // Nome do arquivo para salvar/carregar
        const string NOME_ARQUIVO = "dados_fazenda.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("--- Sistema de Controle de Gado ---");
            Console.Write("Digite o total de cabeças de gado (N): ");
            int n = int.Parse(Console.ReadLine());

            rebanho = new Gado[n];

            LerDados(n); 

            ProcessarAbate();

            Console.WriteLine("\nLeitura e processamento de abate concluídos.");
            Console.WriteLine("Pressione Enter para acessar o menu principal...");
            Console.ReadLine();

            string opcao = "";
            while (opcao != "0")
            {
                Console.Clear();
                Console.WriteLine("--- Menu Principal ---");
                Console.WriteLine("1:  Total de leite por semana");
                Console.WriteLine("2:  Total de alimento por semana");
                Console.WriteLine("3:  Listar animais para abate");
                Console.WriteLine("4:  Salvar dados em arquivo");
                Console.WriteLine("5:  Carregar dados do arquivo");
                Console.WriteLine("0:  Sair");
                Console.Write("\nEscolha uma opção: ");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        TotalLeite();
                        break;
                    case "2":
                        TotalAlimento();
                        break;
                    case "3":
                        ListarAbate();
                        break;
                    case "4":
                        SalvarDados();
                        break;
                    case "5":
                        CarregarDados();
                        break;
                    case "0":
                        Console.WriteLine("Saindo do sistema...");
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

        static void LerDados(int n)
        {
            Console.Clear();
            Console.WriteLine($"--- Leitura de {n} Cabeças de Gado ---");
            for (int i = 0; i < n; i++)
            {
                rebanho[i] = new Gado(); 

                Console.WriteLine($"\n--- Gado {i + 1}/{n} ---");
                
                Console.Write("Código: ");
                rebanho[i].codigo = int.Parse(Console.ReadLine());
                
                Console.Write("Leite (litros/semana): ");
                rebanho[i].leite = int.Parse(Console.ReadLine());
                
                Console.Write("Alimento (kg/semana): ");
                rebanho[i].alim = int.Parse(Console.ReadLine());
                
                Console.Write("Mês de nascimento (1-12): ");
                rebanho[i].nasc.mes = int.Parse(Console.ReadLine());
                
                Console.Write("Ano de nascimento (ex: 2020): ");
                rebanho[i].nasc.ano = int.Parse(Console.ReadLine());
            }
        }

        static void ProcessarAbate()
        {
            int anoAtual = DateTime.Now.Year;
            
            foreach (Gado gado in rebanho)
            {
                bool maisDe5Anos = (anoAtual - gado.nasc.ano) > 5;
                
                bool poucoLeite = gado.leite < 40;

                if (maisDe5Anos || poucoLeite)
                {
                    gado.abate = 'S';
                }
                else
                {
                    gado.abate = 'N';
                }
            }
        }

        static void TotalLeite()
        {
            int total = 0;
            foreach (Gado gado in rebanho)
            {
                total += gado.leite;
            }
            Console.WriteLine($"\nProdução total de leite: {total} litros/semana.");
        }

        static void TotalAlimento()
        {
            int total = 0;
            foreach (Gado gado in rebanho)
            {
                total += gado.alim;
            }
            Console.WriteLine($"\nConsumo total de alimento: {total} kg/semana.");
        }

        static void ListarAbate()
        {
            Console.WriteLine("\n--- Relatório de Animais para Abate ---");
            bool encontrouAlguem = false;
            
            foreach (Gado gado in rebanho)
            {
                if (gado.abate == 'S')
                {
                    Console.WriteLine($"- Código: {gado.codigo} (Produção: {gado.leite}L, Ano Nasc: {gado.nasc.ano})");
                    encontrouAlguem = true;
                }
            }

            if (!encontrouAlguem)
            {
                Console.WriteLine("Nenhum animal está marcado para abate.");
            }
        }

        static void SalvarDados()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(NOME_ARQUIVO))
                {
                    sw.WriteLine(rebanho.Length);
                    foreach (Gado gado in rebanho)
                    {
                        string linha = $"{gado.codigo},{gado.leite},{gado.alim},{gado.nasc.mes},{gado.nasc.ano},{gado.abate}";
                        sw.WriteLine(linha);
                    }
                }
                Console.WriteLine($"\nDados salvos com sucesso em {NOME_ARQUIVO}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nErro ao salvar arquivo: {e.Message}");
            }
        }
        static void CarregarDados()
        {
            if (!File.Exists(NOME_ARQUIVO))
            {
                Console.WriteLine("\nArquivo de dados não encontrado. Nada para carregar.");
                return;
            }
            try
            {
                using (StreamReader sr = new StreamReader(NOME_ARQUIVO))
                {
                    int nDoArquivo = int.Parse(sr.ReadLine());
                    if (nDoArquivo != rebanho.Length)
                    {
                        Console.WriteLine($"\nErro: O arquivo ({nDoArquivo} cabeças) é incompatível com a memória ({rebanho.Length} cabeças).");
                        return; 
                    }
                    for (int i = 0; i < rebanho.Length; i++)
                    {
                        string linha = sr.ReadLine();
                        if (linha == null) break;

                        string[] campos = linha.Split(',');

                        rebanho[i].codigo = int.Parse(campos[0]);
                        rebanho[i].leite = int.Parse(campos[1]);
                        rebanho[i].alim = int.Parse(campos[2]);
                        rebanho[i].nasc.mes = int.Parse(campos[3]);
                        rebanho[i].nasc.ano = int.Parse(campos[4]);
                        rebanho[i].abate = char.Parse(campos[5]);
                    }
                }
                Console.WriteLine($"\nDados carregados com sucesso de {NOME_ARQUIVO}");                ProcessarAbate();
                Console.WriteLine("Dados de abate reprocessados com a data atual.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nErro ao carregar arquivo: {e.Message}");
            }
        }
    }
}