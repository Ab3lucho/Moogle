using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace TF_IDF
{
    class TFIDF
    {
       public Dictionary<string, Dictionary<string, double>> Textos_Palabras_TF = new Dictionary<string, Dictionary<string, double>>();
       public Dictionary<string, Dictionary<string, double>> Textos_Palabras_IDF = new Dictionary<string, Dictionary<string, double>>();
       public Dictionary<string, Dictionary<string, double>> DiccionarioTF_IDF = new Dictionary<string, Dictionary<string, double>>();

       public string[] ArchivosTxt { get; set; }
       public Dictionary<string, string[]> PalabrasUnicas = new Dictionary<string, string[]>();
       public Dictionary<string, string[]> NombresvsPalabras = new Dictionary<string, string[]>();
     
       //Constructor//
         public TFIDF(string[] archivostxt  , Dictionary<string, string[]> palabrasunicas , Dictionary<string, string[]> nombresvspalabras )
         {
                ArchivosTxt = archivostxt;
                PalabrasUnicas = palabrasunicas;
                NombresvsPalabras = nombresvspalabras;
               
                // Dictionary<string, Dictionary<string, double>> Textos_Palabras_TF;
                // Dictionary<string, Dictionary<string, double>> Textos_Palabras_IDF;
                // Dictionary<string, Dictionary<string, double>> DiccionarioTF_IDF;
         }
       
         //Metodo TF//
         
         public void TF()
         {
             foreach (var item in PalabrasUnicas)
             {
                 Dictionary<string, double> TF = new Dictionary<string, double>();
                 foreach (var item2 in item.Value)
                 {
                     double contador = 0;
                     foreach (var item3 in NombresvsPalabras[item.Key])
                     {
                         if (item2 == item3)
                         {
                             contador++;
                         }
                     }
                     TF.Add(item2, contador / NombresvsPalabras[item.Key].Length);
                 }
                 Textos_Palabras_TF.Add(item.Key, TF);
             }
         }

         //Metodo IDF//

            public void IDF()
            {
                foreach (var item in PalabrasUnicas)
                {
                    Dictionary<string, double> IDF = new Dictionary<string, double>();
                    foreach (var item2 in item.Value)
                    {
                        double contador = 0;
                        foreach (var item3 in PalabrasUnicas)
                        {
                            foreach (var item4 in item3.Value)
                            {
                                if (item2 == item4)
                                {
                                    contador++;
                                    break;
                                }
                            }
                        }
                        IDF.Add(item2, Math.Log10(PalabrasUnicas.Count / contador));
                    }
                    Textos_Palabras_IDF.Add(item.Key, IDF);
                }
            }

            //Metodo TF-IDF//

            public Dictionary<string, Dictionary<string, double>> TF_IDF()
            {
                foreach (var item in PalabrasUnicas)
                {
                    Dictionary<string, double> TF_IDF = new Dictionary<string, double>();
                    foreach (var item2 in item.Value)
                    {
                        TF_IDF.Add(item2, Textos_Palabras_TF[item.Key][item2] * Textos_Palabras_IDF[item.Key][item2]);
                    }
                    DiccionarioTF_IDF.Add(item.Key, TF_IDF);
                }
                return DiccionarioTF_IDF;
            }
           

            //Motor//

            public void Motor_TF_IDF()
            {
                TF();
                IDF();
                TF_IDF();
                
            }



            

    }
}