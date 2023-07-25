using ManejoDeArchivos;
using TF_IDF;
using Busqueda;

namespace MoogleEngine;


public static class Moogle
{

    static Dictionary<string, string> Documento_Snippet = new Dictionary<string, string>();
    
    static ManejoDeArchivos.archivos objeto1;

    public static Dictionary<double, string > Cargar(string Query)
    {
           
        Documento_Snippet.Clear();

        if (objeto1 == null)
        {
        string tempath = Path.Combine(Directory.GetCurrentDirectory());
        string mypath = tempath.Replace("MoogleServer", "");
        mypath += @"Content";
        objeto1 = new ManejoDeArchivos.archivos(mypath);
        objeto1.Motor_Manejo(); 
        
        
        }

       
        
        TF_IDF.TFIDF objeto2 = new TF_IDF.TFIDF( objeto1.ArchivosTxt , objeto1.PalabrasUnicas , objeto1.NombresvsPalabras);
        objeto2.Motor_TF_IDF();

        Busqueda.busqueda objeto3 = new Busqueda.busqueda(objeto1.ArchivosTxt , objeto1.PalabrasUnicas , objeto1.NombresvsPalabras, objeto2.DiccionarioTF_IDF , Query);
        objeto3.Motor_Busqueda();
        objeto3.Motor_Resultados();
        objeto3.Motor_Snippet();
         

             foreach (var snipet in objeto3.Snippet)
        {
            if (Documento_Snippet.ContainsKey(snipet.Key))
            {
                break;
            }
            else
            {
                Documento_Snippet.Add(snipet.Key, snipet.Value);            
            }
        }
        // Devolver un diccionario que contiene los resultados de la búsqueda.
        return objeto3.Results;
    }

     // Sugerencia de busqueda (distancia de Levenshtein) //

     static int LevenshteinDistance(string s1, string s2)
{
    int m = s1.Length;
    int n = s2.Length;
    int[] prev = new int[n + 1];
    int[] curr = new int[n + 1];

    for (int j = 0; j <= n; j++)
        prev[j] = j;

    for (int i = 1; i <= m; i++)
    {
        curr[0] = i;
        for (int j = 1; j <= n; j++)
        {
            if (s1[i - 1] == s2[j - 1])
                curr[j] = prev[j - 1];
            else
                curr[j] = 1 + Math.Min(prev[j], Math.Min(curr[j - 1], prev[j - 1]));
        }
        int[] temp = prev;
        prev = curr;
        curr = temp;
    }
    return prev[n];
}

     

    public static SearchResult Query(string query) 
    {

        // Cargar los resultados de la búsqueda en un diccionario.
        Dictionary<double, string> Results = Cargar(query);
        // Ordenar los resultados por score.
        var ResultadosOrdenados = Results.OrderByDescending(x => x.Value);      

        // Crear una lista de objetos SearchItem para almacenar los resultados.
        List<SearchItem> items = new List<SearchItem>();

        // Iterar a través de los resultados ordenados y agregarlos a la lista de objetos SearchItem.
        foreach (KeyValuePair<double, string> item in ResultadosOrdenados)
        {
            items.Add(new SearchItem(item.Value,Documento_Snippet [item.Value], (float)item.Key));
        }
        // Convertir la lista de objetos SearchItem en un arreglo de objetos SearchItem.
        SearchItem[] items2 = items.ToArray();

        Console.WriteLine(" Busqueda finalizada ");

          static string Suggestion(string query){
            string suggestion = "";

            int[] Distancias = new int [objeto1.Palabras.Length];
            for (int i = 0; i < objeto1.Palabras.Length; i++)
            {
                Distancias[i] = LevenshteinDistance(query, objeto1.Palabras[i]);
            }
            int min = Distancias.Min();
            int index = Array.IndexOf(Distancias, min);
            suggestion = objeto1.Palabras[index];
            
            return suggestion;
        }
        string suggestion = Suggestion(query);

        if (query == suggestion)
        {
            SearchItem[] items4 = items2;
            return new SearchResult(items2);
        }
        else
        {
        // Devolver un objeto SearchResult que contiene los resultados de la búsqueda.
        return new SearchResult(items2, Suggestion(query));            
        }
    }
}
 


