using ManejoDeArchivos;
using TF_IDF;
using Busqueda;

namespace MoogleEngine;


public static class Moogle
{

    public static void Cargar()
    {
        string tempath = Path.Combine(Directory.GetCurrentDirectory());
        string mypath = tempath.Replace("MoogleServer", "");
        mypath += @"Content";
        ManejoDeArchivos.archivos objeto1 = new ManejoDeArchivos.archivos(mypath);
        objeto1.Motor_Manejo();
        
        TF_IDF.TFIDF objeto2 = new TF_IDF.TFIDF( objeto1.ArchivosTxt , objeto1.PalabrasUnicas , objeto1.NombresvsPalabras);
        objeto2.Motor_TF_IDF();
        Busqueda.busqueda objeto3 = new Busqueda.busqueda(objeto1.ArchivosTxt , objeto1.PalabrasUnicas , objeto1.NombresvsPalabras);
        objeto3.TokenizarQuery("hola");
        objeto3.Motor_Busqueda();

     
    }
    

    

    public static SearchResult Query(string query) 
    {

        Cargar();
        

        SearchItem[] items = new SearchItem[3] 
        {
            
            new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.9f),
            new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.5f),
            new SearchItem("Hello World", "Lorem ipsum dolor sit amet", 0.1f),
        };

        return new SearchResult(items, query);
    }
}
 


