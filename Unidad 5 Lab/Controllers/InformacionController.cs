using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unidad_5_Lab.Models;
using Unidad_5_Lab.Singleton;

namespace Unidad_5_Lab.Controllers
{
    public class InformacionController : Controller
    {
        public ActionResult Index()
        {
            return View(Data.Instancia.Equipos);
        }

        public ActionResult Faltantes()
        {
            return View(Data.Instancia.Faltantes);
        }

        static int contador = 0;
        public ActionResult Carga()
        {
            if(contador > 0)
            {
                ViewBag.Msg = "ERROR AL CARGAR EL ARCHIVO, INTENTE DE NUEVO";
            }
            contador++;
            return View();
        }

        [HttpPost]
        public ActionResult Carga(HttpPostedFileBase file)
        {
            if(file != null)
            {
                Upload(file);
                return RedirectToAction("Upload");
            }
            else
            {
                ViewBag.Msg = "ERROR AL CARGAR EL ARCHIVO, INTENTE DE NUEVO";
                return View();
            }
            
        }

        public ActionResult Upload(HttpPostedFileBase file)
        {
            string model = "";
            if (file != null && file.ContentLength > 0)
            {
                model = Server.MapPath("~/Upload/") + file.FileName;

                file.SaveAs(model);
                Data.Instancia.LecturaCSVAlbum(model);
                ViewBag.Msg = "Carga de archivo correcta";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Msg = "ERROR AL CARGAR EL ARCHIVO, INTENTE DE NUEVO";
                return RedirectToAction("Carga");
            }
        
        }


        static string llave = "";
        public ActionResult MostrarListas(string Llave)
        {
            llave = Llave;
            ViewBag.Nombre = Llave;
            
            return View(Data.Instancia.Diccionario1[Llave].Todo); 
        }

        public ActionResult Actualizacion(string id, string Equipo)
        {
            id = id.Trim();
            Equipo = Equipo.Trim();

            string Llave = Equipo + "|" + id;

            if(Data.Instancia.Diccionario2.ContainsKey(Llave) == true && Data.Instancia.Diccionario2[Llave] == false)
            {
                ViewBag.Mensaje = "La estampilla ha sido agregada, revise en las listas la actualización.";
                ViewBag.Msg = "";

                foreach (var elemento in Data.Instancia.Diccionario1[Equipo].Todo)
                {
                    if(elemento.numero == int.Parse(id))
                    {
                        elemento.cantidad = 1;
                        elemento.obtenida = true;
                        Data.Instancia.Diccionario2[Llave] = true;
                        Data.Instancia.Diccionario1[Equipo].Coleccionadas.Add(elemento);
                        break;
                    }
                }
                foreach (var elemento in Data.Instancia.Diccionario1[Equipo].Faltantes)
                {
                    if (elemento.numero == int.Parse(id))
                    {
                        Data.Instancia.Diccionario1[Equipo].Faltantes.Remove(elemento);
                        break;
                    }
                }
                foreach (var elemento in Data.Instancia.Faltantes)
                {
                    if (elemento.NumeroEstampa == id)
                    {
                        Data.Instancia.Faltantes.Remove(elemento);
                        break;
                    }
                }

                return View();
            }
            else if(Data.Instancia.Diccionario2.ContainsKey(Llave) == false)
            {
                ViewBag.Msg = "No se encuentra la estapilla";
                return View("Index");
            }
            else if(Data.Instancia.Diccionario2[Llave] == true)
            {
                ViewBag.Mensaje = "La estampilla ha sido agregada anteriormente, no es necesario agregarla de nuevo.";
                return View();
            }
            else
            {
                return View("Index");
            }   
        }


        public ActionResult MostrarListaEscogida(int lista)
        {
            try
            {
                if(lista == 1)
                {
                    ViewBag.Msg = "Lista de Faltantes";
                    return View(Data.Instancia.Diccionario1[llave].Faltantes);
                }
                else if(lista == 2)
                {
                    ViewBag.Msg = "Lista de Colecionadas";
                    return View(Data.Instancia.Diccionario1[llave].Coleccionadas);
                }
                else if(lista == 3)
                {
                    ViewBag.Msg = "Lista de Disponibles";
                    return View(Data.Instancia.Diccionario1[llave].Disponibles);
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
