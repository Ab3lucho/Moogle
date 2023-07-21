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
            double documentoLength = 0;

            //Calcular la frecuencia de cada palabra en el documento//
            foreach (var item in NombresvsPalabras.Values)
            {
                  documentoLength += item.Length;

                foreach (var item2 in item)
                {
                    if (wordCounts.ContainsKey(item2))
                    {
                        wordCounts[item2]++;
                    }
                    else
                    {
                        wordCounts[item2] = 1;
                    }
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
            //wordIDFs
            Dictionary<string, double> IDFTemp = new Dictionary<string, double>();
            double documentosTotales = this.ArchivosTxt.Length;

            //Calcular la frecuencia de cada palabra en el documento//
            foreach (var item in NombresvsPalabras.Values)
            {
                HashSet<string> palabras = new HashSet<string>(item);
                foreach (var item2 in palabras)
                {
                    if (wordCounts.ContainsKey(item2))
                    {
                        wordCounts[item2]++;
                    }
                    else
                    {
                        wordCounts[item2] = 1;
                    }
                }
            }

            //Calcular el IDF de cada palabra en el documento//
            foreach (var item in wordCounts.Keys)
            {
                    double idf = Math.Log(documentosTotales / wordCounts[item]);
                    IDFTemp[item] = idf;
            }
            foreach (string item in ArchivosTxt)
            {
                string[] palabras = this.NombresvsPalabras[item];
                Dictionary<string, double>NombrevsIDFTemp = new Dictionary<string, double>();

                foreach (string item2 in this.PalabrasUnicas[item])
                {
                    double idf = IDFTemp[item2];
                    NombrevsIDFTemp[item2] = idf;
                } 
                this.Textos_Palabras_IDF.Add(item, NombrevsIDFTemp);
            }
        }

//Cleaned

        // Metodo TF_IDF Optimizado//
        public void TF_IDF()
        {
            
            for (int i = 0; i < ArchivosTxt.Length; i++)
            {
                string[] PalabrasTemporales = this.NombresvsPalabras[this.ArchivosTxt[i]];
                string[] PalabrasNoRepetidasTemporales = this.PalabrasUnicas[ArchivosTxt[i]];

                Dictionary<string, double> PalabravsTF = this.Textos_Palabras_TF[ArchivosTxt[i]];
                Dictionary<string, double> PalabravsIDF = this.Textos_Palabras_IDF[ArchivosTxt[i]];

                Dictionary<string, double> PalabrasvsTFIDF = new Dictionary<string, double>();

                for (int x = 0; x < PalabrasNoRepetidasTemporales.Length; x++)
                {
                    double tf = PalabravsTF[PalabrasNoRepetidasTemporales[x]];
                    double idf = PalabravsIDF[PalabrasNoRepetidasTemporales[x]];

                    double tfidf = tf*idf;

                    PalabrasvsTFIDF.Add(PalabrasNoRepetidasTemporales[x], tfidf);
                }
                this.DiccionarioTF_IDF.Add(this.ArchivosTxt[i], PalabrasvsTFIDF);
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