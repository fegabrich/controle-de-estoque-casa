//o projeto vai ficar aqui

using System;
using System.Collections.Generic;
using System.IO;

namespace controleestoquecasa
{
    class Program
    {
        // caminho do arquivo de valores separados por vigula onde o estoque será salvo
        static string caminhoArquivo = "estoque.csv";

        static void Main(string[] args)
        {
            // carrega o estoque do arquivo 
            List<Item> estoque = CarregarEstoque();
            int opcao;

            do
            {
                // Exibe o menu de opções
                Console.WriteLine("\n=== CONTROLE DE ESTOQUE ===");
                Console.WriteLine("1 - Adicionar item");
                Console.WriteLine("2 - Remover item");
                Console.WriteLine("3 - Atualizar quantidade");
                Console.WriteLine("4 - Listar itens");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                // valida a entrada do menu
                if(!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida! Digite um número.");
                    continue;
                }

                // executa a função correspondente a opção escolhida
                if(opcao == 1) AdicionarItem(estoque);
                else if(opcao == 2) RemoverItem(estoque);
                else if(opcao == 3) AtualizarQuantidade(estoque);
                else if(opcao == 4) ListarItens(estoque);

            } while(opcao != 0);

            // Salva o estoque no arquivo valores separados por virgula antes de sair
            SalvarEstoque(estoque);
            Console.WriteLine("Estoque salvo com sucesso!");
            Console.ReadKey();
        }

        // função que carrega o estoque dos valores separados por virgula
        static List<Item> CarregarEstoque()
        {
            List<Item> estoque = new List<Item>();

            if(File.Exists(caminhoArquivo))
            {
                string[] linhas = File.ReadAllLines(caminhoArquivo);

                for(int i = 1; i < linhas.Length; i++) // pula o cabeçalho
                {
                    string[] dados = linhas[i].Split(',');
                    Item item = new Item();
                    item.nome = dados[0];
                    item.quantidade = int.Parse(dados[1]);
                    item.categoria = dados[2];
                    item.validade = DateTime.Parse(dados[3]);
                    estoque.Add(item);
                }
            }

            return estoque;
        }

        // função para salvar o estoque
        static void SalvarEstoque(List<Item> estoque)
        {
            List<string> linhas = new List<string>();
            linhas.Add("nome,quantidade,categoria,validade"); // cabeçalho

            foreach(var item in estoque)
            {
                linhas.Add($"{item.nome},{item.quantidade},{item.categoria},{item.validade:dd/MM/yyyy}");
            }

            File.WriteAllLines(caminhoArquivo, linhas);
        }

        // função para adicionar um item novo
        static void AdicionarItem(List<Item> estoque)
        {
            Item novo = new Item();

            Console.Write("Nome do item: ");
            novo.nome = Console.ReadLine();

            // vai verificar se o item ja existe
            Item existente = estoque.Find(i => i.nome.ToLower() == novo.nome.ToLower());
            if(existente != null)
            {
                Console.WriteLine("Item já existe. Deseja atualizar a quantidade? (s/n)");
                if(Console.ReadLine().ToLower() == "s")
                {
                    Console.Write("Quantidade a adicionar: ");
                    int qtd;
                    while(!int.TryParse(Console.ReadLine(), out qtd))
                    {
                        Console.WriteLine("Digite um número válido!");
                    }
                    existente.quantidade += qtd;
                    Console.WriteLine("Quantidade atualizada!");
                    return;
                }
                else return;
            }

            // validação da quantidade
            Console.Write("Quantidade: ");
            while(!int.TryParse(Console.ReadLine(), out novo.quantidade))
            {
                Console.WriteLine("Digite um número válido!");
            }

            Console.Write("Categoria: ");
            novo.categoria = Console.ReadLine();

            // validação da validade
            Console.Write("Validade (dd/MM/yyyy): ");
            while(!DateTime.TryParse(Console.ReadLine(), out novo.validade))
            {
                Console.WriteLine("Digite uma data válida no formato dd/MM/yyyy!");
            }

            estoque.Add(novo);
            Console.WriteLine("Item adicionado com sucesso!");
        }

        // função para remover um item
        static void RemoverItem(List<Item> estoque)
        {
            Console.Write("Nome do item a remover: ");
            string nome = Console.ReadLine();

            // busca ignorando letras maiúsculas/minúsculas
            Item item = estoque.Find(i => i.nome.ToLower() == nome.ToLower());

            if(item != null)
            {
                estoque.Remove(item);
                Console.WriteLine("Item removido!");
            }
            else Console.WriteLine("Item não encontrado.");
        }

        // função que atualiza a quantidade de um item
        static void AtualizarQuantidade(List<Item> estoque)
        {
            Console.Write("Nome do item a atualizar: ");
            string nome = Console.ReadLine();
            Item item = estoque.Find(i => i.nome.ToLower() == nome.ToLower());

            if(item != null)
            {
                Console.Write("Nova quantidade: ");
                int novaQtd;
                while(!int.TryParse(Console.ReadLine(), out novaQtd))
                {
                    Console.WriteLine("Digite um número válido!");
                }
                item.quantidade = novaQtd;
                Console.WriteLine("Quantidade atualizada!");
            }
            else Console.WriteLine("Item não encontrado.");
        }

        // função para listar os itens no estoque
        static void ListarItens(List<Item> estoque)
        {
            if(estoque.Count == 0)
            {
                Console.WriteLine("O estoque está vazio.");
                return;
            }

            Console.WriteLine("\nItens no estoque:");
            // cabeçalho alinhado
            Console.WriteLine("{0,-20} {1,-10} {2,-15} {3,-12}", "Nome", "Quantidade", "Categoria", "Validade");

            foreach(var item in estoque)
            {
                Console.WriteLine("{0,-20} {1,-10} {2,-15} {3,-12}", 
                    item.nome, item.quantidade, item.categoria, item.validade.ToString("dd/MM/yyyy"));
            }
        }
    }

    // classe que representa a categoria que um item do estoque se encaixa
    class Item
    {
        public string nome;
        public int quantidade;
        public string categoria;
        public DateTime validade;
    }
}