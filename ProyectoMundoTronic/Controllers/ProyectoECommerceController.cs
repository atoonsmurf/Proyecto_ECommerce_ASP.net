using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.SqlClient;
using ProyectoMundoTronic.Models;
using ProyectoMundoTronic.DAO;
namespace ProyectoMundoTronic.Controllers
{
    public class ProyectoECommerceController : Controller
    {
        // GET: ProyectoECommerce
        //DAOS
        productoDAO productos = new productoDAO();
        ordenDAO ordenes = new ordenDAO();
        public ActionResult TiendaVirtual(string nombre ="")
        {
            if (Session["carroCompra"] == null)
                Session["carroCompra"] = new List<Item>();
 

            return View(productos.filtro(nombre));
        }
        public ActionResult Seleccionar(String codpro = "P001")
        {
            Producto reg = productos.Buscar(codpro);
            if (reg == null)
                return RedirectToAction("TiendaVirtual", new { nombre = "" });
            else
                ViewBag.d = (Session["carroCompra"] as List<Item>).Exists(p => p.codigo == codpro) ? true : false;
                

            return View(reg);
        }

     [HttpPost]
     public ActionResult Seleccionar(String codigo, int cantidad)
        {
            Producto reg = productos.Buscar(codigo);

            if (reg.stock< cantidad) {
                ViewBag.mensaje = "Debe agregar cantidad menor al stock";
                return View(reg);
            }
            else { 
            Item it = new Item()
            {
                codigo = codigo,
                nombre=reg.nombre,
                descripcion = reg.descripcion,
                categoria=reg.categoria,
                precio = reg.precio,
                cantidad = cantidad
            };

            (Session["carroCompra"] as List<Item>).Add(it);
            ViewBag.d = true; //deshabilitar la opcion Agregar

            ViewBag.mensaje = "Producto Agregado";

            return View(reg);
            }
        }

        public ActionResult Carrito()
        {

            if (Session["carroCompra"] == null)
                return RedirectToAction("TiendaVirtual", new { nombre = "" });
            else
                return View(Session["carroCompra"] as List<Item>);
        }

        [HttpPost]
        public ActionResult Actualizar(String codigo, int q)

        {
            Item reg =
                (Session["carroCompra"] as List<Item>).Where(p => p.codigo == codigo).FirstOrDefault();

            reg.cantidad = q;

            return RedirectToAction("Carrito");

        }
        
        public ActionResult Delete(String codigo)

        {

            Item reg = (Session["carroCompra"] as List<Item>).Find(p => p.codigo == codigo);

            (Session["carroCompra"] as List<Item>).Remove(reg);

            return RedirectToAction("Carrito");

        }

        public ActionResult Comprar()
        {
        
            if (Session["carroCompra"] == null)

                return RedirectToAction("TiendaVirtual", new { nombre = "" });
            else

                return View(new OrdenPedido());
        }
        [HttpPost]
        public ActionResult Comprar(OrdenPedido reg)

        {

            if ((Session["carroCompra"] as List<Item>).Count == 0)

            {

                ViewBag.mensaje = "Debe agregar productos a su canasta";

                return View(reg);

            }

            else

            {

                ViewBag.mensaje = ordenes.Transaccion(reg, Session["carroCompra"] as List<Item>);

                Session.Abandon(); //cerrar la sesion

                return View(reg);

            }

        }

        public ActionResult BuscarProductoPorDescripcion(string descripcion = "")
        {
            if (Session["carroCompra"] == null)
                Session["carroCompra"] = new List<Producto>();

            return View(productos.filtroDescripcion(descripcion));
        }
    }
}