using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unidad_5_Lab.Models;
using System.Web;

namespace Unidad_5_Lab.Singleton
{
    public class Data
    {
            private static Data instancia = null;
            public static Data Instancia
            {
                get
                {
                    if (instancia == null)
                    {
                        instancia = new Data();
                    }
                    return instancia;
                }
            }

        public Dictionary<string, Informacion> Diccionario1 = new Dictionary<string, Informacion>();
        public Dictionary<string, bool> Diccionario2 = new Dictionary<string, bool>();
        public List<string> Equipos = new List<string>();
        public List<Diccionario2Info> Faltantes = new List<Diccionario2Info>();

       

        public void LecturaCSVAlbum(string path)
        {
            string[] lineas = File.ReadAllLines(path);
            int contador = 0;

            foreach (var linea in lineas)
            {
                try
                {
                    Informacion Info = new Informacion();
                    if (contador > 0)
                    {
                        
                        string[] infolinea = linea.Split(';');
                        for (int i = 12; i < 23; i++)
                        {
                            Diccionario2Info dato = new Diccionario2Info();

                            
                            Estampilla estapa = new Estampilla();
                            Estampilla estapa2 = new Estampilla(); //Se crea para las estampas repetitivas
                           
                            estapa.cantidad = int.Parse(infolinea[i]); //Cantidad de una estampa
                            estapa2.cantidad = int.Parse(infolinea[i]);

                            estapa.numero = int.Parse(infolinea[i - 11]); 
                            estapa2.numero = int.Parse(infolinea[i - 11]); //Numero de estampa

                            string llaveD2 = $"{infolinea[0]}{"|"}{infolinea[i - 11]}"; //Llave para 2o diccionario
                            string llaveD1 = $"{infolinea[0]}"; //Llave para 1er diccionario
                           
                            if (estapa.cantidad == 0)
                            {
                                estapa.obtenida = false;
                                Info.Todo.Add(estapa);
                                Info.Faltantes.Add(estapa);
                                Diccionario2.Add(llaveD2, estapa.obtenida);
                                dato.Equipo = infolinea[0];
                                dato.NumeroEstampa = infolinea[i-11];
                                Faltantes.Add(dato); //Para lista de faltantes
                            }
                            else if (estapa.cantidad == 1)
                            {
                                estapa.obtenida = true;
                                Info.Todo.Add(estapa);
                                Info.Coleccionadas.Add(estapa);
                                Diccionario2.Add(llaveD2, estapa.obtenida);
                            }
                            else if (int.Parse(infolinea[i]) > 1)
                            {
                                estapa.obtenida = true;
                                Info.Todo.Add(estapa);
                                estapa2.obtenida = true;

                                estapa2.cantidad--;
                                estapa.cantidad = 1;
                                Info.Coleccionadas.Add(estapa);                                
                                Info.Disponibles.Add(estapa2);
                                Diccionario2.Add(llaveD2, estapa.obtenida);
                            }
                            
                        }
                        Equipos.Add(infolinea[0]);
                        Diccionario1.Add(infolinea[0], Info);
                    }
                    else { contador++; }
                }
                catch
                {
                    
                }
                

                
            }
        }

    }
}