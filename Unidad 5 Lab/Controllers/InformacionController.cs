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
        // GET: Informacion
        public ActionResult Index()
        {
            return View(Data.Instancia.Equipos);
        }


        public ActionResult Carga()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Carga(HttpPostedFileBase file, FormCollection collection)
        {
            Upload(file);
            return RedirectToAction("Upload");
        }
        public ActionResult Upload(HttpPostedFileBase file)
        {

            var model = Server.MapPath("~/Upload/") + file.FileName;
            if (file.ContentLength > 0)
            {
                file.SaveAs(model);
                Data.Instancia.LecturaCSV(model);
                ViewBag.Msg = "";
            }
            else
            {
                ViewBag.Msg = "ERROR";
            }
            return RedirectToAction("Index");
        }


        //MODIFICAR
        public ActionResult Details(Dictionary<string, Informacion> diccionario)
        {
            return View(diccionario);
        }

        // GET: Informacion/Create
        static string llave = "";
        public ActionResult MostrarListas(string Llave)
        {
            llave = Llave;
            return View();
        }

        //[HttpPost]
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
