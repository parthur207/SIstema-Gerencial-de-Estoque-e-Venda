using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    /*CASE 1*/
    public static void Importacao(ref string conteudo, ref Dictionary<string, int> Estoque, ref Dictionary<string, Dictionary<string, int>> Vendas, ref string ano, ref int venda_total_anual, ref Dictionary<string, int> Venda_produto, ref Dictionary<string, int> vendas_mensais, ref List<string>Produtos)
    {

        string prod = "";
        string mes = "";

        int qunt_estoque = 0;
        int qunt_venda = 0;
        int ii = 0;
        int iii = 0;
        int n = 0;
        int valor = 0;

        Produtos = new List<string>();


            Console.WriteLine("\nDigite o caminho do arquivo:");
            string path = Console.ReadLine();

            while (!File.Exists(path))
            {

                Console.WriteLine("\nArquivo não encontrado. Informe o caminho novamente:");
                path = Console.ReadLine();
            }

            using (StreamReader sr = new StreamReader(path))
            {
                conteudo = sr.ReadToEnd().ToLower();
            }


            for (int i = 0; i < conteudo.Length; i++)
            {

                if (conteudo[i] == 'a' && conteudo[i + 1] == 'n' && conteudo[i + 2] == 'o' && conteudo[i + 3] == ':' && conteudo[i + 4] == ' ')
                {
                    int aux1 = i + 4;
                    int index_quebra = conteudo.IndexOf('\r', aux1);

                    ano = conteudo.Substring(aux1, index_quebra - aux1).Trim();

                }
                if (conteudo[i] == '*' && conteudo[i + 1] == ' ')
                {
                    int aux2 = (i + 1);
                    int dados_estq = conteudo.IndexOf('\r', aux2);
                    string linha1 = conteudo.Substring(aux2, dados_estq - aux2).Trim();
                    string[] elementos1 = linha1.Split(':');
                    prod = elementos1[0].Trim();

                    qunt_estoque = int.Parse(elementos1[1]);
                    Estoque.Add(prod, qunt_estoque);
                    Produtos.Add(prod);
                }

                if (conteudo[i] == '(' && conteudo[i + 1] == 'm' && conteudo[i + 2] == 'ê' && conteudo[i + 3] == 's' && conteudo[i + 4] == ')' && conteudo[i + 5] == ' ')
                {
                    int aux3 = (i + 5);
                    int index_quebra2 = conteudo.IndexOf('\r', aux3);
                    string linha2 = conteudo.Substring(aux3, index_quebra2 - aux3);
                    string[] elementos2 = linha2.Split(':');
                    mes = elementos2[0].Trim();
                    qunt_venda = int.Parse(elementos2[1].Trim());
                    vendas_mensais.Add(mes, qunt_venda);
                    venda_total_anual += qunt_venda;
                    if (Produtos.Contains(prod) && n <= 12)
                    {
                        valor += qunt_venda;
                        n++;
                        if (n == 12)
                        {
                            Venda_produto.Add(Produtos[iii], valor);
                            iii++;
                            valor = 0;
                            n = 0;

                        }

                    }
                    mes = ""; qunt_venda = 0;
                }

                if (vendas_mensais.Count == 12)
                {
                    Vendas.Add(Produtos[ii], new Dictionary<string, int>(vendas_mensais));
                    ii++;
                    vendas_mensais.Clear();
                }
            }
            Console.WriteLine("\nImportação realizada com sucesso."); 
        }

        /*CASE 2*/
        public static void ImportacaoManual(ref Dictionary<string, int> Estoque, ref Dictionary<string, Dictionary<string, int>> Vendas, ref string ano, ref int venda_total_anual, ref Dictionary<string, int> Venda_produto, ref List<string> Produtos)
        {
            int n = 0;
            int iii = 0;
            int valor = 0;

            Produtos = new List<string>();

           List <string >meses = new List<string> { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };

            Console.WriteLine("\nDigite o ano:");
            ano = Console.ReadLine().Trim();

            while (ano.Length != 4 || !int.TryParse(ano, out _))
            {
                Console.WriteLine("\nDigite um ano válido. Deve conter somente 4 caracteres numéricos.");
                ano = Console.ReadLine().Trim();
            }

            Console.WriteLine("\nDigite a quantidade de produtos a serem cadastrados:");
            if (int.TryParse(Console.ReadLine(), out int qunt) && qunt > 0)
            {
                for (int i = 0; i < qunt; i++)
                {
                    Console.WriteLine($"\nDigite o nome do produto {i + 1}: ");
                    string prod = Console.ReadLine().Trim();

                    Console.WriteLine($"\nInforme o valor em estoque de {prod}:");
                    if (int.TryParse(Console.ReadLine(), out int qunt_estoque))
                    {
                        Estoque.Add(prod, qunt_estoque);
                        Produtos.Add(prod);
                        Dictionary<string, int> vendas_mensais = new Dictionary<string, int>();
                        foreach (string mes in meses)
                        {
                            Console.WriteLine($"\nDigite a quantidade de vendas de {mes} para {prod}: ");
                            if (int.TryParse(Console.ReadLine(), out int qunt_venda))
                            {
                                vendas_mensais.Add(mes, qunt_venda);

                                venda_total_anual += qunt_venda;

                                if (Produtos.Contains(prod) && n <= 12)
                                {
                                   valor += qunt_venda;
                                   n++;
                                    if (n == 12)
                                    {
                                       Venda_produto.Add(Produtos[iii], valor);
                                       iii++;
                                       valor = 0;
                                       n = 0;
                                    }
                                }
                            }
                        }
                        Vendas[prod] = vendas_mensais;

                    }
                }

                Console.WriteLine("\nImportação realizada com sucesso.");
            }
            else
            {
                Console.WriteLine("Número inválido de produtos.");
            }
    }

    /*CASE 3*/
    public static void Addprod(ref Dictionary<string, int> Estoque, ref Dictionary<string, Dictionary<string, int>> Vendas, ref string ano, ref int venda_total_anual, ref Dictionary<string, int> Venda_produto, ref List <string>Produtos)
    {

        int valor = 0, n=0, iii=0;
        List<string> meses = new List<string> { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };

        Console.WriteLine("\nDigite o nome do produto a ser adicionado:");
        string novo_prod = Console.ReadLine().ToLower();

        Console.WriteLine($"\nInforme o valor em estoque de {novo_prod}:");
        if (int.TryParse(Console.ReadLine(), out int qunt_estoque))
        {
            Estoque.Add(novo_prod, qunt_estoque);
            Produtos.Add(novo_prod);
            Dictionary<string, int> vendas_mensais = new Dictionary<string, int>();
            foreach (string mes in meses)
            {
                Console.WriteLine($"\nDigite a quantidade de vendas de {mes} para {novo_prod}: ");
                if (int.TryParse(Console.ReadLine(), out int qunt_venda))
                {
                    vendas_mensais.Add(mes, qunt_venda);

                    venda_total_anual += qunt_venda;

                    if (Produtos.Contains(novo_prod) && n <= 12)
                    {
                        valor += qunt_venda;
                        n++;
                        if (n == 12)
                        {
                            Venda_produto.Add(novo_prod, valor);
                            iii++;
                            valor = 0;

                        }
                    }
                }
            }
            Vendas[novo_prod] = vendas_mensais;

        }

    }

    /*CASE 4*/

    public static void Removeprod(ref Dictionary<string, int> Estoque, ref Dictionary<string, Dictionary<string, int>> Vendas, ref int venda_total_anual, ref Dictionary<string, int> Venda_produto, ref List<string> Produtos)
    {

        Console.WriteLine("\nDigite o produto em que deseja realizar a remoção:");
        string produto = Console.ReadLine();

        while (produto== null || !Produtos.Contains(produto))
        {
            Console.WriteLine("\nProduto não encontrado. Digite novamente:");
            produto = Console.ReadLine();
        }

        Estoque.Remove(produto);
        Vendas.Remove(produto);
        venda_total_anual -= Venda_produto[produto];
        Venda_produto.Remove(produto);
        Produtos.Remove(produto);

        Console.WriteLine("\nRemoção realizada com sucesso.");
    }
    /*CASE 5*/
    public static void RelatorioEstoqueVenda(ref Dictionary<string, int> Estoque, ref Dictionary<string, Dictionary<string, int>> Vendas, ref string ano, ref int venda_total_anual, ref Dictionary<string, int> Venda_produto)
    {
        Console.WriteLine("\nDigite '1' para verificar o estoque e venda de um produto específico ou '2' para ser exibido o estoque e venda total dos produtos:");
        string op = Console.ReadLine();

        if (op == "1")
        {
            Console.WriteLine("\nDigite o nome do produto especificado:");
            string prod = Console.ReadLine(); ;
            if (Estoque.ContainsKey(prod) && Vendas.ContainsKey(prod))
            {
                Console.WriteLine("________________");
                Console.WriteLine($"\nEstoque atual de {prod.ToUpper()}: {Estoque[prod]}");
                Console.WriteLine($"Vendas totais de {prod.ToUpper()}: {Venda_produto[prod]}");
                Console.WriteLine($"\nRelatório de vendas anual ({ano}) do produto ({prod.ToUpper()}):\n");
                foreach (var mesVenda in Vendas[prod])
                {
                    Console.WriteLine($"{mesVenda.Key}: {mesVenda.Value}");
                }
                Console.WriteLine("________________");
            }
            else
            {
                Console.WriteLine("\nProduto não encontrado.");
            }
        }
        else if (op == "2")
        {
            Console.WriteLine("________________");
            Console.WriteLine("\nAno: " + ano);
            Console.WriteLine("________________\n");

            Console.WriteLine("PRODUTOS:\n");
            foreach (var produto in Estoque)
            {
                Console.WriteLine(produto.Key);
            }
            Console.WriteLine("________________\n");

            Console.WriteLine("ESTOQUE ATUAL:\n");
            foreach (var produto in Estoque)
            {
                Console.WriteLine(produto.Key + ": " + produto.Value);
            }
            Console.WriteLine("________________\n");

            Console.WriteLine("VENDAS:");

            Console.WriteLine($"\nTotal de vendas realizadas no ano de {ano}: {venda_total_anual}");
            foreach (var produto in Vendas)
            {
                Console.WriteLine("");
                Console.WriteLine(produto.Key + ":\n");
                foreach (var mesVenda in produto.Value)
                {
                    Console.WriteLine("(Mês) " + mesVenda.Key + ": " + mesVenda.Value);
                }
                Console.WriteLine("________________");
            }
        }
        else
        {
            Console.WriteLine("\nOpção inválida.");
        }
    }

    /*CASE 6*/
    public static void RegistrarVenda(ref Dictionary<string, int> Estoque, ref Dictionary<string, Dictionary<string, int>> Vendas, ref int venda_total_anual, ref Dictionary<string, int> Venda_produto)
    {
        Console.WriteLine("\nInforme o produto:");
        string prod = Console.ReadLine();
        if (Vendas.ContainsKey(prod))
        {
            Console.WriteLine("\nDigite o número de vendas realizadas pelo produto:");
            if (int.TryParse(Console.ReadLine(), out int vendas) && vendas > 0)
            {
                Console.WriteLine("\nDigite o mês relativo à venda:");
                string mes = Console.ReadLine().ToLower();
               
                
                if (Vendas[prod].ContainsKey(mes))
                {
                    Vendas[prod][mes] += vendas;
                    Estoque[prod] -= vendas;
                    venda_total_anual += vendas;
                    Venda_produto[prod] += vendas;

                    Console.WriteLine($"\nVenda registrada com sucesso (Produto: {prod}| N°: {vendas} | Mês: {mes})");
                }
                else
                {
                    Console.WriteLine("\nMês não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("\nNúmero de vendas inválido.");
            }
        }
        else
        {
            Console.WriteLine("\nProduto não encontrado.");
        }
    }

    /*CASE 7*/
    public static void AdicaoEstoque(ref Dictionary<string, int> Estoque)
    {
        Console.WriteLine("\nInforme o produto:");
        string prod = Console.ReadLine();
        if (Estoque.ContainsKey(prod))
        {
            Console.WriteLine($"\nInforme o valor a ser incluso no estoque do produto {prod}:");
            if (int.TryParse(Console.ReadLine(), out int adicao) && adicao > 0)
            {
                Estoque[prod] += adicao;
                Console.WriteLine($"\nAdição de {adicao} unidades ao estoque do produto ({prod}) realizada com sucesso.");
            }
            else
            {
                Console.WriteLine("\nO valor a ser incluso deve ser maior do que '0'.");
            }
        }
        else
        {
            Console.WriteLine("\nProduto não encontrado.");
        }
    }

    //'CLEAR' 1
    public static void Clear1(ref string conteudo, ref Dictionary<string, int> Estoque, ref Dictionary<string, Dictionary<string, int>> Vendas, ref string ano, ref int venda_total_anual, ref Dictionary<string, int> Venda_produto, ref Dictionary<string, int> vendas_mensais, ref List<string> Produtos)
    {
        conteudo = "";
        Estoque.Clear();
        Vendas.Clear();
        ano = "";
        venda_total_anual=0;
        Venda_produto.Clear();
        vendas_mensais.Clear();
        Produtos.Clear();
    }

    //'CLEAR' 2

    public static void Clear2(ref Dictionary<string, int> Estoque, ref Dictionary<string, Dictionary<string, int>> Vendas, ref string ano, ref int venda_total_anual, ref Dictionary<string, int> Venda_produto, ref List<string>Produtos)
    {
        Estoque.Clear();
        Vendas.Clear();
        ano = "";
        venda_total_anual=0;
        Venda_produto.Clear();
        Produtos.Clear();
    }

    //MAIN
    public static void Main(string[] args)
    {
        string resp = "";
        string op = "";
        string conteudo = ""; 
        string ano = "";
        
        int N = 0;
        int venda_total_anual = 0;
        int aux = 0;

        Dictionary<string, int> Estoque = new Dictionary<string, int>();
        Dictionary<string, Dictionary<string, int>> Vendas = new Dictionary<string, Dictionary<string, int>>();
        Dictionary<string, int> Venda_produto = new Dictionary<string, int>();
        Dictionary<string, int> vendas_mensais = new Dictionary<string, int>();
        
        List<string> Produtos = new List<string>();

        Console.WriteLine("SISTEMA GERENCIAL DE ESTOQUE E VENDA");
       
        do
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("\n1 - Importar arquivo de produtos.");
            Console.WriteLine("2 - Realizar importação manual.");
            Console.WriteLine("3 - Adicionar um novo produto manualmente.");
            Console.WriteLine("4 - Remover um produto manualmente.");
            Console.WriteLine("5 - Exibir relatório de estoque e vendas.");
            Console.WriteLine("6 - Registrar venda.");
            Console.WriteLine("7 - Registrar adição ao estoque.");
            Console.WriteLine("8 - Sair.");
            op = Console.ReadLine();



            if (op == "1" || op == "2" || op == "8")
            {
                N++;
            }

            if (N < 1 && (op != "1" && op != "2"))
            {
                Console.WriteLine("\nPrimeiro, você deve importar o caminho do arquivo, ou realizar a importação manual (1 ou 2).");
                continue;
            }

            if (!int.TryParse(op, out int op_number) || op_number < 1 || op_number > 8)
            {
                Console.WriteLine("\nOpção inválida. É necessário digitar um número entre 1 e 8.");
                continue;
            }

            switch (op_number)
            {
                case 1:
                    aux++;
                    resp = "";
                    if (aux > 1)
                    {
                        do
                        {
                            Console.WriteLine("\nA importação anterior será perdida, deseja prosseguir? ");
                            resp = Console.ReadLine().ToLower();
                            if (resp == "sim")
                            {
                                Clear1(ref conteudo, ref Estoque, ref Vendas, ref ano, ref venda_total_anual, ref Venda_produto, ref vendas_mensais, ref Produtos);
                                break;
                            }
                            else if (resp != "sim" && resp != "não" && resp != "nao")
                            {
                                Console.WriteLine("\nDigite 'sim', ou 'não': ");
                            }
                        }
                        while (resp != "sim" && resp != "não" && resp != "nao");
                    }
                    if (resp == "não" || resp == "nao")
                    {
                        break;
                    }

                    else
                    {
                        resp = "";
                        Importacao(ref conteudo, ref Estoque, ref Vendas, ref ano, ref venda_total_anual, ref Venda_produto, ref vendas_mensais, ref Produtos);
                        break;
                    }

                case 2:
                    aux++;
                    resp = "";
                    if (aux > 1)
                    {

                        do
                        {
                            Console.WriteLine("\nA importação anterior será perdida perdida, deseja prosseguir?");
                            resp = Console.ReadLine().ToLower();
                            if (resp == "sim")
                            {

                                Clear2(ref Estoque, ref Vendas, ref ano, ref venda_total_anual, ref Venda_produto, ref Produtos);
                                break;
                            }

                            else if (resp != "sim" && resp != "não" && resp != "nao")
                            {
                                Console.WriteLine("Digite 'sim', ou 'não': ");
                            }
                        }
                        while (resp != "sim" && resp != "não" && resp != "nao");
                    }
                    if (resp == "não" || resp == "nao")
                    {
                        break;
                    }

                    else
                    {
                        resp = "";
                        ImportacaoManual(ref Estoque, ref Vendas, ref ano, ref venda_total_anual, ref Venda_produto, ref Produtos);
                        break;
                    }

                case 3:
                    Addprod(ref  Estoque, ref Vendas, ref ano, ref venda_total_anual, ref Venda_produto, ref Produtos);
                break;

                case 4:
                    Removeprod(ref Estoque, ref Vendas, ref venda_total_anual, ref Venda_produto, ref Produtos);
                break;

                case 5:
                    RelatorioEstoqueVenda(ref Estoque, ref Vendas, ref ano, ref venda_total_anual, ref Venda_produto);
                break;

                case 6:
                     RegistrarVenda(ref Estoque, ref Vendas, ref venda_total_anual, ref Venda_produto);
                break;

                case 7:
                    AdicaoEstoque(ref Estoque);
                break;

                case 8:
                    Console.WriteLine("\nPrograma encerrado.");
                return;
            }
        } while (true);
    }
}
