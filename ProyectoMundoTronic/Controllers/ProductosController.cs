using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.SqlClient;
using ProyectoMundoTronic.Models;
using ProyectoMundoTronic.DAO;

namespace ProyectoMundoTronic.Controllers
{
    public class ProductosController : Controller
    {
        // GET: Productos

        productoDAO productos = new productoDAO();
        categoriaDAO categorias = new categoriaDAO();
        public ActionResult MantenimientoProd(String cod ="")
        {

            Producto reg = (cod == ""? new Producto() : productos.Buscar(cod));
            //las categorias

            ViewBag.categorias = new SelectList(categorias.listado(),
                "codcat",
                "desc_cat",
                reg.categoria);

            //los productos

            ViewBag.productos = productos.listado();


            return View(reg);
        }
        
        [HttpPost]
        public ActionResult MantenimientoProd(String btncrud, Producto reg)
        {
            switch (btncrud)
            {
                case "Create": return Agregar(reg);
                case "Edit": return Actualizar(reg);
                case "Delete": return Eliminar(reg);
                default: return RedirectToAction("MantenimientoProd", new { cod = "" });

            }
        }
        public ActionResult Agregar(Producto reg)
        {
            SqlParameter[] pars =
             {

                new SqlParameter(){ParameterName="@codpro",Value=reg.codpro},
                new SqlParameter(){ParameterName="@nombre",Value=reg.nombre},
                new SqlParameter(){ParameterName="@descripcion",Value=reg.descripcion},
                new SqlParameter(){ParameterName="@cod_cat",Value=reg.categoria},
                new SqlParameter(){ParameterName="@stock",Value=reg.stock},
                new SqlParameter(){ParameterName="@precio",Value=reg.precio}
              };
            
            //ejecutar
            ViewBag.mensaje = productos.CRUD("usp_agregar_productos", pars, 1);


            ViewBag.categorias = new SelectList(categorias.listado(),
                "codcat",
                "desc_cat",
                reg.categoria);


            //los clientes

            ViewBag.productos = productos.listado();

            return View(reg);
        }

        public ActionResult Actualizar(Producto reg)
        {
            SqlParameter[] pars =
             {

                new SqlParameter(){ParameterName="@codpro",Value=reg.codpro},
                new SqlParameter(){ParameterName="@nombre",Value=reg.nombre},
                new SqlParameter(){ParameterName="@descripcion",Value=reg.descripcion},
                new SqlParameter(){ParameterName="@cod_cat",Value=reg.categoria},
                new SqlParameter(){ParameterName="@stock",Value=reg.stock},
                new SqlParameter(){ParameterName="@precio",Value=reg.precio}

              };

            //ejecutar
            ViewBag.mensaje = productos.CRUD("usp_actualizar_productos", pars, 2);

            //refrescar la pagina: paises, lista de clientes y cliente agregado
            //los paises
            //las categorias

            ViewBag.categorias = new SelectList(categorias.listado(),
                "codcat",
                "desc_cat",
                reg.categoria);

            //los productos

            ViewBag.productos = productos.listado();

            return View(reg);
        }

        public ActionResult Eliminar(Producto reg)
        {
            SqlParameter[] pars =
             {

                new SqlParameter(){ParameterName="@codpro",Value=reg.codpro},

              };

            //ejecutar
            ViewBag.mensaje = productos.CRUD("usp_eliminar_productos", pars, 3);

            //las categorias

            ViewBag.categorias = new SelectList(categorias.listado(),
                "codcat",
                "desc_cat",
                reg.categoria);

            //los productos

            ViewBag.productos = productos.listado();

            return View(reg);
        }


       

    }
}