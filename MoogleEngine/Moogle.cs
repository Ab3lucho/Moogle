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
        string tempath = Path.Combine(Directory.GetCurrentDirectory());
        string mypath = tempath.Replace("MoogleServer", "");
        mypath += @"Content";
        ManejoDeArchivos.archivos objeto1 = new ManejoDeArchivos.archivos(mypath);
        objeto1.Motor_Manejo();
        
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

        // Devolver un objeto SearchResult que contiene los resultados de la búsqueda.
        return new SearchResult(items2);

    }

}
 


