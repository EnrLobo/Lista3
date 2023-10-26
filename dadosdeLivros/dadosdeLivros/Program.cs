using System;
using System.IO.Pipes;

class Program
{

    struct Livro
    {
        public string titulo;
        public string autor;
        public int ano;
        public int prateleira;
    }//fim do struct

    static int menu()
    {
        int op;
        Console.Write("\t*** Sistema de cadastro de livros em C# ***\n\n");
        Console.WriteLine("1-Cadastrar");
        Console.WriteLine("2-Buscar livro");
        Console.WriteLine("3-Listar livros publicados antes da data inserida");
        Console.WriteLine("0-Sair");
        Console.Write("\nEscolha uma opção: ");
        op = Convert.ToInt32(Console.ReadLine());
        return op;
    }//fim da função menu

    static void cadastLivro(List<Livro> lista)
    {
        Livro novoLivro = new Livro();// declarando uma variavel Eletro
        Console.Write("\nTitulo do Livro:");
        novoLivro.titulo = Console.ReadLine();
        Console.Write("Autor do livro:");
        novoLivro.autor = Console.ReadLine();
        Console.Write("Ano de publicação do livro:");
        novoLivro.ano = Convert.ToInt32(Console.ReadLine());
        Console.Write("Número de prateleira do livro:");
        novoLivro.prateleira = Convert.ToInt32(Console.ReadLine());
        lista.Add(novoLivro);
    }// fim funcao 

    static void buscarLivro(List<Livro> lista, string nomeBusca)
    {
        int qtd = lista.Count();
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].titulo.ToUpper().Contains(nomeBusca.ToUpper()))
            {
                Console.WriteLine("\t*** Dados do Livro ***");
                Console.WriteLine("Titulo:" + lista[i].titulo);
                Console.WriteLine("Prateleira em que o livro se encntra:" + lista[i].prateleira);
                // break;
            }// fim 

        }// fim for
    }// fim funcao

    static void dadosLivros(List<Livro> lista)
    {
        int qtd = lista.Count();
        for (int i = 0; i < qtd; i++)
        {
            Console.WriteLine("\t*** Dados dos Livros ***");
            Console.WriteLine("Titulo:" + lista[i].titulo);
            Console.WriteLine("Autor:" + lista[i].autor);
            Console.WriteLine("Ano de publicação:" + lista[i].ano);
            Console.WriteLine("Prateleira na qual o livro se encontra:" + lista[i].prateleira);
        }// fim for
    }// fim lista

    static void listarAnoMenor(List<Livro> lista, int idAno)
    {
        int qtd = lista.Count();
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].ano < idAno)
            {
                Console.WriteLine("\t*** Dados do LIvro ***");
                Console.WriteLine("Titulo do livro:" + lista[i].titulo);
                Console.WriteLine("Autor do livro:" + lista[i].autor);
                Console.WriteLine("Ano de publicação do Livro:" + lista[i].ano);
                Console.WriteLine("Prateleira em que o livro se encontra:" + lista[i].prateleira);
            }// fim else
        }// fim for
    }// fim lista

    static void salvarDados(List<Livro> liv, string nomeArquivo)
    {

        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            foreach (Livro livro in liv)
            {
                writer.WriteLine($"{livro.titulo},{livro.autor},{livro.ano},{livro.prateleira}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!");


    }

    static void carregarDados(List<Livro> liv, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhas = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhas)
            {
                string[] campos = linha.Split(',');
                Livro livro = new Livro
                {
                    titulo = campos[0],
                    autor = campos[1],
                    ano = int.Parse(campos[2]),
                    prateleira = int.Parse(campos[3])
                };
                liv.Add(livro);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }

    static void Main()
    {
        List<Livro> vetorLivros = new List<Livro>();//vetor que armazena um elemento do tipo livro em cada posição
        int op = 0;
        carregarDados(vetorLivros, "dados.txt");
        do
        {
            op = menu();
            switch (op)
            {
                case 1:
                    cadastLivro(vetorLivros);
                    break;
                case 2:
                    Console.Write("Titulo do livro para busca: ");
                    string nomeBusca = Console.ReadLine();
                    buscarLivro(vetorLivros, nomeBusca);
                    break;
                case 3:
                    Console.Write("Ano que você deseja usar como base para livros com data de publicação anterior: ");
                    int idAno = Convert.ToInt32(Console.ReadLine());
                    listarAnoMenor(vetorLivros, idAno);
                    break;
                case 0:
                    Console.WriteLine("Saindo");
                    salvarDados(vetorLivros, "dadosEletro.txt");
                    break;
            }// fim switch
            Console.ReadKey();//espera uma tecla ser clicada para que feche ou avance no codigo, é uma pausa
            Console.Clear();

            } while (op != 0);

        }

    }