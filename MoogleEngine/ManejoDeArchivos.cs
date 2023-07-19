using System.Collections.Generic;
using System;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace ManejoDeArchivos
{
    class archivos
    {
        public archivos(string RutaCarpeta)
    {
        rutaCarpeta = RutaCarpeta;
        ArchivosTxt = Directory.GetFiles(this.rutaCarpeta, "*.txt");
        ContenidoArchivos = new string[ArchivosTxt.Length];

    }
    public string rutaCarpeta { get; set; }
    public string[] ArchivosTxt { get; set; }
    public string[] ContenidoArchivos { get; set; }
    public Dictionary<string, string[]> PalabrasUnicas = new Dictionary<string, string[]>();
    public Dictionary<string, string[]> NombresvsPalabras = new Dictionary<string, string[]>();
   

    // Metodo para leer el contenido de cada array y almacenarlo en el array correspondinte
    public string[] ObtenerTextos()
    {
        for (int i = 0; i < this.ArchivosTxt.Length; i++)
        {
            if (this.ArchivosTxt[i] != null)
            {
                this.ContenidoArchivos[i] = File.ReadAllText(ArchivosTxt[i]);
                this.ContenidoArchivos[i] = this.ContenidoArchivos[i].ToLower();
            }
        }
        return this.ContenidoArchivos;
    }



    // Metodo para obtener palabras (Tokenizador)
    public void ObtenerPalabras()
    {
        // dividir el string en palabras 
        char[] delimitadores = { ' ', ',', '.', ':', '¿', '?', '!', '*', '/', '"', '#', ')', '(', };
        string StringTexto = "";
        for (int i = 0; i < this.ContenidoArchivos.Length; i++)
        {
            if (this.ContenidoArchivos[i] != null)
            {
                StringTexto += this.ContenidoArchivos[i];
                this.NombresvsPalabras.Add(this.ArchivosTxt[i], this.ContenidoArchivos[i].Split(delimitadores));
                this.PalabrasUnicas.Add(this.ArchivosTxt[i], NombresvsPalabras[this.ArchivosTxt[i]].Distinct().ToArray());

            }
        }


    }
    public string[] Motor_Manejo()
    {
        ObtenerTextos();
        ObtenerPalabras();

        System.Console.WriteLine("Datos cargados");
        return this.ContenidoArchivos;

    }
    }
}