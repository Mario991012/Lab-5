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


        //MODIFICAR
        public ActionResult Details(Dictionary<string, Informacion> diccionario)
        {
            return View(diccionario);
        }


        static string llave = "";
        public ActionResult MostrarListas(string Llave)
        {
            llave = Llave;
            ViewBag.Nombre = Llave;
            
            return View(Data.Instancia.Diccionario1[Llave].Todo); //MODIFICAR
        }

        static string llave2 = "";
        public ActionResult Actualizacion(string Llave)
        {
            if(Data.Instancia.Diccionario2.ContainsKey(Llave) == true)
            {
                llave2 = Llave;
                ViewBag.Msg = "";
                ViewBag.Nombre = Llave;
                return View(Data.Instancia.Diccionario2[Llave]);
            }
            else
            {
                ViewBag.Msg = "No se encontró el equipo buscado";
                return View("Index");
            }
        }
        public ActionResult Redirigir()
        {
            return RedirectToAction("Busqueda");           
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

        // GET: Informacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Informacion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Informacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Informacion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
