using System.Collections.Generic;
using System;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace Busqueda
{
    class busqueda
    {

       public string[] ArchivosTxt { get; set; }
       public string query { get; set; }

       public Dictionary<string, string[]> PalabrasUnicas = new Dictionary<string, string[]>();
       public Dictionary<string, string[]> NombresvsPalabras = new Dictionary<string, string[]>();

        public Dictionary<string, double> Query_TF = new Dictionary<string, double>();
        public Dictionary<string, double> Query_IDF = new Dictionary<string, double>();
        public Dictionary<string, double> Query_TF_IDF = new Dictionary<string, double>();

        public string[] QueryTokenizada { get; set; }
        
        //Constructor//
        
         public busqueda ( string[] archivostxt  , Dictionary<string, string[]> palabrasunicas , Dictionary<string, string[]> nombresvspalabras, string Query )
         {
                ArchivosTxt = archivostxt;
                PalabrasUnicas = palabrasunicas;
                NombresvsPalabras = nombresvspalabras;
                query = Query;
                
               
         }

         //Contador de documentos donde existe la palabra//

          public double ContadorPalabra(string Palabra, Dictionary<string, string[]> NombrevsPalabras, string[] ArchivosTxt)
        {
         double DocumentosDodeExiste = 0;
                
            for (int i = 0; i < ArchivosTxt.Length; i++)
            {
                string[] Documento = NombrevsPalabras[ArchivosTxt[i]];
                
                 for (int x = 0; x < Documento.Length; x++)
                 {
                   
                   if (Documento[x] == Palabra)
                   {
                   
                    DocumentosDodeExiste++;
                    break;

                   }

                 }

            }
         return DocumentosDodeExiste;
       }
        
        //Tokenizar la query y convertir en minusculas//

        public string[] TokenizarQuery()
        {
            char[] delimitadores = { ' ', ',', '.', ':', '¿', '?', '!', '*', '/', '"', '#', ')', '(', };
            string[] QueryTokenizada = this.query.Split(delimitadores);
            for (int i = 0; i < QueryTokenizada.Length; i++)
            {
                QueryTokenizada[i] = QueryTokenizada[i].ToLower();
            }
            return QueryTokenizada;
        }

        //Metodo TF de la query//

        public void TF_Query()
        {
            foreach (var item in QueryTokenizada)
            {
                double contador = 0;
                foreach (var item2 in QueryTokenizada)
                {
                    if (item == item2)
                    {
                        contador++;
                    }
                }
                Query_TF.Add(item, contador / QueryTokenizada.Length);
            }
        }

        //Metodo IDF de la query//  

        public void IDF_Query()
        {
            foreach (var item in QueryTokenizada)
            {
                double contador = 0;
                foreach (var item2 in QueryTokenizada)
                {
                    if (item == item2)
                    {
                        contador++;
                    }
                }
                Query_IDF.Add(item, Math.Log10(ArchivosTxt.Length / ContadorPalabra(item, NombresvsPalabras, ArchivosTxt)));
            }
        }

        //Metodo TF-IDF de la query//

        public void TF_IDF_Query()
        {
            foreach (var item in QueryTokenizada)
            {
                Query_TF_IDF.Add(item, Query_TF[item] * Query_IDF[item]);
            }
        }

        //Motor de busqueda//

        public void Motor_Busqueda()
        {
            TokenizarQuery();
            TF_Query();
            Console.WriteLine("TF de la query Cargado");
            IDF_Query();
            Console.WriteLine("IDF de la query Cargado");
            TF_IDF_Query(); 
            Console.WriteLine("TF-IDF de la query Cargado");
            Console.WriteLine("Buscando...");

        }

        //Similitud coseno//

        

       
        

    }
}