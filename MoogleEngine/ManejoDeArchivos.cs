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
        ContenidoArchivos = new string[ArchivosTxt.Length + 1];
        

    }
    public string rutaCarpeta { get; set; }
    public string[] ArchivosTxt { get; set; }
    public string[] ContenidoArchivos { get; set; }
    public string[] Palabras {get;set;}
    public Dictionary<string, string[]> PalabrasUnicas = new Dictionary<string, string[]>();
    public Dictionary<string, string[]> NombresvsPalabras = new Dictionary<string, string[]>();
   

    
 private void FakeData(){
        string[] palabraFalsa = {"astrolopitecus"};
        NombresvsPalabras.Add("astrolopitecus", palabraFalsa);
        PalabrasUnicas.Add("astrolopitecus", palabraFalsa);
    }

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
        ContenidoArchivos[ContenidoArchivos.Length - 1] = "astrolopitecus";
    // Devolvemos la matriz de nombres actualizada
    
        return this.ContenidoArchivos;
    }

    public string[] LimpiarNombre(){

        for (int i = 0; i < this.ArchivosTxt.Length; i++)
        {
            if (ArchivosTxt != null)
            {
                string PathTemporal = Path.Combine(Directory.GetCurrentDirectory());
                string  path = PathTemporal.Replace("MoogleServer", "");
                path += @"Content";

                this.ArchivosTxt[i] = this.ArchivosTxt[i].Replace(path, "");
                this.ArchivosTxt[i] = this.ArchivosTxt[i].Replace(".txt", "");
                this.ArchivosTxt[i] = this.ArchivosTxt[i].Replace(@"\", "");
                
            }
        }    // Devolvemos la matriz de nombres actualizada
                List<string>Puente = new List<string>();
                for (int i = 0; i < this.ArchivosTxt.Length; i++)
                {
                    Puente.Add(this.ArchivosTxt[i]);   
                }
                Puente.Add("astrolopitecus");

        this.ArchivosTxt = Puente.ToArray();

        return this.ArchivosTxt;
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

    public string[] Obtenerpalabrasstring()
    {
        string text = "";
        char[] delimitadores = { ' ', ',', '.', ':', '¿', '?', '!', '*', '/', '"', '#', ')', '(', };

        for (int i = 0; i < this.ContenidoArchivos.Length; i++)
        {
            // Se concatenan los textos en una sola variable
            text += this.ContenidoArchivos[i];
            
        }  
        // Se separan las palabras de la variable TextVar utilizando los delimitadores definidos anteriormente
        this.Palabras =  text.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
        return this.Palabras;
    }
    public string[] Motor_Manejo()
    {
        ObtenerTextos();
        LimpiarNombre();
        ObtenerPalabras();
        Obtenerpalabrasstring();

        System.Console.WriteLine("Datos cargados");
        return this.ContenidoArchivos;
        

    }
    }
}