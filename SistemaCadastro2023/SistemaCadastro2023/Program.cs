using System;
using System.IO;
class Program
{
    struct TipoBanda
    {
        public string nome;
        public string genero;
        public int integrantes;
        public int ranking;
    }// fim struct

    static void addBanda(List<TipoBanda> lista)
    {
        TipoBanda novaBanda = new TipoBanda();// declarando uma variavel do TipoBanda
        Console.Write("Nome da banda:");
        novaBanda.nome = Console.ReadLine();
        Console.Write("Genero da banda:");
        novaBanda.genero = Console.ReadLine();
        Console.Write("Integrantes:");
        novaBanda.integrantes = Convert.ToInt32(Console.ReadLine());
        Console.Write("Ranking:");
        novaBanda.ranking = Convert.ToInt32(Console.ReadLine());
        lista.Add(novaBanda);
    }// fim funcao 

    static void listaBandas(List<TipoBanda> lista)
    {
        int qtd = lista.Count();
        for (int i = 0; i < qtd; i++)
        {
            Console.WriteLine("\t*** Dados da banda ***");
            Console.WriteLine("Nome:" + lista[i].nome);
            Console.WriteLine("Genero:" + lista[i].genero);
            Console.WriteLine("Integrantes:" + lista[i].integrantes);
            Console.WriteLine("Ranking:" + lista[i].ranking);
        }// fim for
    }// fim lista

    //-----------------------------
    static void listarRanking(List<TipoBanda> lista, int idRanking)
    {
        int qtd = lista.Count();
        for (int i = 0; i < qtd; i++)
        {
            if(lista[i].ranking == idRanking) { 
                Console.WriteLine("\t*** Dados da banda ***");
                Console.WriteLine("Nome:" + lista[i].nome);
                Console.WriteLine("Genero:" + lista[i].genero);
                Console.WriteLine("Integrantes:" + lista[i].integrantes);
                Console.WriteLine("Ranking:" + lista[i].ranking);
            }// fim else
        }// fim for
    }// fim lista
     //----------------------------
    static void buscarNome(List<TipoBanda> lista, string nomeBusca)
    {
        int qtd = lista.Count();
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].nome.ToUpper().Contains(nomeBusca.ToUpper()))
            {
                Console.WriteLine("\t*** Dados da banda ***");
                Console.WriteLine("Nome:" + lista[i].nome);
                Console.WriteLine("Genero:" + lista[i].genero);
                Console.WriteLine("Integrantes:" + lista[i].integrantes);
                Console.WriteLine("Ranking:" + lista[i].ranking);
                // break;
            }// fim 
            
        }// fim for
    }// fim funcao
     //----------------------
    static void removerBanda(List<TipoBanda> lista, string nomeBusca)
    {
        int qtd = lista.Count();
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].nome.ToUpper().Equals(nomeBusca.ToUpper()) )
            {
                Console.Write($"Tem certeza que deseja remover a banda {nomeBusca}?[S/N]: ");
                char resp;
                resp =Convert.ToChar( Console.ReadLine() );
                if (resp == 'S') { 
                    lista.RemoveAt(i);
                    Console.WriteLine("Banda removida com sucesso");
                    break;
                }
                else
                    Console.WriteLine("Operação Cancelada");
            }// fim 

        }// fim for
    }// fim funcao
    //----------------------
    static void alterarBanda(List<TipoBanda> lista, string nomeBusca)
    {
        int qtd = lista.Count();
        bool flag = false;
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].nome.ToUpper().Equals(nomeBusca.ToUpper()))
            {
                flag = true;
                TipoBanda novaBanda = new TipoBanda();// declarando uma variavel do TipoBanda
                Console.Write("Novo nome da banda:");
                novaBanda.nome = Console.ReadLine();
                Console.Write("Novo genero da banda:");
                novaBanda.genero = Console.ReadLine();
                Console.Write("Nova qtde de integrantes:");
                novaBanda.integrantes = Convert.ToInt32(Console.ReadLine());
                Console.Write("Novo ranking:");
                novaBanda.ranking = Convert.ToInt32(Console.ReadLine());
                lista[i] = novaBanda;

            }// fim 
        }// fim for
        if (!flag) // flag==false
            Console.WriteLine("Banda não encontrada");

    }// fim funcao

    static int somaIntegrantes(List<TipoBanda> lista)
    {
        int soma = 0;
        for(int i = 0; i < lista.Count(); i++)
        {
            soma += lista[i].integrantes;
        }
         return soma;
    }

    static void salvarDados(List<TipoBanda> bandas, string nomeArquivo)
    {

        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            foreach (TipoBanda banda in bandas)
            {
                writer.WriteLine($"{banda.nome},{banda.genero},{banda.integrantes},{banda.ranking}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!"); 
        
     
    }

    static void carregarDados(List<TipoBanda> bandas, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhas = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhas)
            {
                string[] campos = linha.Split(',');
                TipoBanda banda = new TipoBanda
                {
                    nome = campos[0],
                    genero = campos[1],
                    integrantes = int.Parse(campos[2]),
                    ranking = int.Parse(campos[3])
                };
                bandas.Add(banda);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }

    static int menu()
    {
        Console.WriteLine("*** Sistema de Cadastros Rock4U ***");
        Console.WriteLine("1-Adicionar banda");
        Console.WriteLine("2-Listar");
        Console.WriteLine("3-Filtrar por ranking");
        Console.WriteLine("4-Buscar por nome");
        Console.WriteLine("5-Buscar pelo genero");
        Console.WriteLine("6-Alterar");
        Console.WriteLine("7-Remover");
        Console.WriteLine("8-Total Integrantes");
        Console.WriteLine("0-Sair");
        Console.Write("Entre com uma opção:");
        int op = Convert.ToInt32(Console.ReadLine());
        return op;
    }
    static void Main()
    {
        List<TipoBanda> listadeBandas = new List<TipoBanda>();
        int op;
        carregarDados(listadeBandas, "dados.txt");
        do
        {
            op = menu();
            switch (op)
            {
                case 1:
                    addBanda(listadeBandas);
                    break;
                case 2:
                    listaBandas(listadeBandas);
                    break;
                case 3: Console.Write("Ranking para filtro:");
                        int idRanking = Convert.ToInt32(Console.ReadLine());
                        listarRanking(listadeBandas, idRanking);
                    break;
                case 4:Console.Write("Nome para busca:");
                       string nomeBusca = Console.ReadLine();
                       buscarNome(listadeBandas, nomeBusca);
                    break;
                case 6:
                    Console.Write("Nome da banda para alterar:");
                    nomeBusca = Console.ReadLine();
                    alterarBanda(listadeBandas, nomeBusca);
                    break;
                case 7:
                    Console.Write("Nome da banda para remover:");
                    nomeBusca = Console.ReadLine();
                    removerBanda(listadeBandas, nomeBusca);
                    break;
                case 8: Console.WriteLine("Total de integrantes:" + somaIntegrantes);
                        break;
                case 0: Console.WriteLine("Saindo");
                        salvarDados(listadeBandas, "dados.txt");
                    break;
            }// fim switch
            Console.ReadKey();// pausa
            Console.Clear(); // limpa
        } while (op != 0);// fim while


        Console.ReadLine();//pause antes de fechar
    }

}// fim do programa