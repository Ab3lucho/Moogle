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
        //Metodo para calcular TF//
        public void CalcularTF()
        {
            //Calculo de TF
            foreach (KeyValuePair<string, string[]> item in PalabrasUnicas)
            {
                Dictionary<string, double> TF = new Dictionary<string, double>();
                foreach (string palabra in item.Value)
                {
                    double tf = 0;
                    foreach (string palabra2 in NombresvsPalabras[item.Key])
                    {
                        if (palabra == palabra2)
                        {
                            tf++;
                        }
                    }
                    tf = tf / NombresvsPalabras[item.Key].Length;
                    TF.Add(palabra, tf);
                }
                Textos_Palabras_TF.Add(item.Key, TF);
            }
        }
        //Metodo para calcular IDF//
        public void CalcularIDF()
        {
            //Calculo de IDF
            foreach (KeyValuePair<string, string[]> item in PalabrasUnicas)
            {
                Dictionary<string, double> IDF = new Dictionary<string, double>();
                foreach (string palabra in item.Value)
                {
                    double idf = 0;
                    foreach (KeyValuePair<string, string[]> item2 in PalabrasUnicas)
                    {
                        foreach (string palabra2 in item2.Value)
                        {
                            if (palabra == palabra2)
                            {
                                idf++;
                                break;
                            }
                        }
                    }
                    idf = Math.Log10(ArchivosTxt.Length / idf);
                    IDF.Add(palabra, idf);
                }
                Textos_Palabras_IDF.Add(item.Key, IDF);
            }
        }
        //Metodo para calcular TF-IDF//
        public void CalcularTF_IDF()
        {
            //Calculo de TF-IDF
            foreach (KeyValuePair<string, Dictionary<string, double>> item in Textos_Palabras_TF)
            {
                Dictionary<string, double> TF_IDF = new Dictionary<string, double>();
                foreach (KeyValuePair<string, double> item2 in item.Value)
                {
                    TF_IDF.Add(item2.Key, item2.Value * Textos_Palabras_IDF[item.Key][item2.Key]);
                }
                DiccionarioTF_IDF.Add(item.Key, TF_IDF);
            }
        }
        //Motor//
        public void Motor_TF_IDF()
        {
            CalcularTF();
            CalcularIDF();
            CalcularTF_IDF();
        }

    }
}