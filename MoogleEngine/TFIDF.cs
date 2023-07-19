using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using Busqueda;
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
        public TFIDF(string[] archivostxt, Dictionary<string, string[]> palabrasunicas, Dictionary<string, string[]> nombresvspalabras)
        {
            ArchivosTxt = archivostxt;
            PalabrasUnicas = palabrasunicas;
            NombresvsPalabras = nombresvspalabras;

        }


        //Metodo TF Optimizado//

        public void TF()
        {
            Dictionary<string, double> wordCounts = new Dictionary<string, double>();
            int documentoLength = 0;

            //Calcular la frecuencia de cada palabra en el documento//
            foreach (var item in NombresvsPalabras.Values)
            {
                foreach (var item2 in item)
                {
                    if (wordCounts.ContainsKey(item2))
                    {
                        wordCounts[item2]++;
                    }
                    else
                    {
                        wordCounts.Add(item2, 1);
                    }
                    documentoLength++;
                }
            }

            //Calcular el TF de cada palabra en el documento//
            
            foreach (var item in ArchivosTxt)
            {
                Dictionary<string, double> TF = new Dictionary<string, double>();
                foreach (var item2 in PalabrasUnicas[item])
                {
                    double contador = wordCounts[item2];
                    double tf = contador / documentoLength;
                    TF.Add(item2, tf);
                }
                Textos_Palabras_TF.Add(item, TF);
            }
            
        }
        // Metodo IDF Optimizado//

        public void IDF()
        {
            Dictionary<string, double> wordCounts = new Dictionary<string, double>();
            int documentoLength = 0;

            //Calcular la frecuencia de cada palabra en el documento//
            foreach (var item in NombresvsPalabras.Values)
            {
                foreach (var item2 in item)
                {
                    if (wordCounts.ContainsKey(item2))
                    {
                        wordCounts[item2]++;
                    }
                    else
                    {
                        wordCounts.Add(item2, 1);
                    }
                    documentoLength++;
                }
            }

            //Calcular el IDF de cada palabra en el documento//
            foreach (var item in ArchivosTxt)
            {
                Dictionary<string, double> IDF = new Dictionary<string, double>();
                foreach (var item2 in PalabrasUnicas[item])
                {
                    IDF.Add(item2, Math.Log10(documentoLength / wordCounts[item2]));
                }
                Textos_Palabras_IDF.Add(item, IDF);
            }

        }

        // Metodo TF_IDF Optimizado//

        public void TF_IDF()
        {
            foreach (var item in ArchivosTxt)
            {
                Dictionary<string, double> TF_IDF = new Dictionary<string, double>();
                foreach (var item2 in PalabrasUnicas[item])
                {
                    TF_IDF.Add(item2, Textos_Palabras_TF[item][item2] * Textos_Palabras_IDF[item][item2]);
                }
                DiccionarioTF_IDF.Add(item, TF_IDF);
            }
        }
          

        //Motor//

        public void Motor_TF_IDF()
        {
            TF();
            Console.WriteLine("TF Cargado");
            IDF();
            Console.WriteLine("IDF Cargado");
            TF_IDF();
            Console.WriteLine("TF-IDF Cargado");
            Console.WriteLine("Datos TF_IDF cargados");
        }





    }
}