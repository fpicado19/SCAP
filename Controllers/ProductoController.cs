using Infrastructure.Models;
using AppCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Utils;
using System.Reflection;
using System.IO;

namespace SCAP.Controllers
{
    public class ProductoController : Controller
    {
        // Listado productos
        IServiceProducto _ServiceProducto = new ServiceProducto();
        public ActionResult Index()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProducto();
            }
            catch (Exception e)
            {
                Log.Error(e, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }


        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.idMarca = ListaMarca();
            ViewBag.idTipoProd = ListaTipoProd();
            ViewBag.idTipoUnidad = ListaTipoUnidad();
            return View();
        }


        private SelectList ListaMarca()
        {
            IServiceMarca rep = new ServiceMarca();
            IEnumerable<Marca> lista = rep.GetListaMarca();
            return new SelectList(lista, "id", "descripcion");
        }
        private SelectList ListaTipoProd()
        {
            IServiceTipoProd rep = new ServiceTipoProd();
            IEnumerable<TipoProducto> lista = rep.GetListaTipo();
            return new SelectList(lista,"id", "descripcionTipo");
        }
        private SelectList ListaTipoUnidad()
        {
            IServiceTipoUnidad rep = new ServiceTipoUnidad();
            IEnumerable<TipoUnidad> lista = rep.GetListaTipoUnidad();
            return new SelectList(lista, "id", "descripcionTipo");
        }
        //// POST: Producto/Create
        [HttpPost]
        public ActionResult Save(Producto prod, HttpPostedFileBase ImageFile)
        {
            try
            {
                MemoryStream target = new MemoryStream();

                if (prod.imagen == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        prod.imagen = target.ToArray();
                        ModelState.Remove("Imagen");

                    }

                }
                //prod.idMarca = int.Parse(marca[0]);
                //prod.idTipoProducto = int.Parse(producto[0]);
                //prod.idTipoUnidad = int.Parse(unidad[0]);

                _ServiceProducto.Save(prod);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //// GET: Producto/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Producto/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Producto/Delete/5
        //public ActionResult Disable(int id)
        //{
        //    return View();
        //}

        //// POST: Producto/Delete/5
        //[HttpPost]
        //public ActionResult Disable(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
