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
    public class ClienteController : Controller
    {
        // GET: Cliente
        clienteDAO clientes = new clienteDAO();
        distritoDAO distrito = new distritoDAO();
        public ActionResult MantenimientoCli(String cod ="")
        {
            Cliente reg = (cod == "" ? new Cliente() : clientes.Buscar(cod));
            //las categorias

            ViewBag.distritos = new SelectList(distrito.listado(),
                "cod_dist",
                "nom_dist",
                reg.cod_dist);

            //los productos

            ViewBag.clientes = clientes.listado();


            return View(reg);
        }
        [HttpPost]
        public ActionResult MantenimientoCli(String btncrud, Cliente reg)
        {
            switch (btncrud)
            {
                case "Create": return Agregar(reg);
                case "Edit": return Actualizar(reg);
                case "Delete": return Eliminar(reg);
                default: return RedirectToAction("MantenimientoCli", new { cod = "" });

            }
        }
        public ActionResult Agregar(Cliente reg)
        {
            SqlParameter[] pars =
             {

               new SqlParameter(){ParameterName="@cod_cli",Value=reg.cod_cli},
                 new SqlParameter(){ParameterName="@DNI_cli",Value=reg.dni},
                  new SqlParameter(){ParameterName="@nom_cli",Value=reg.nombre},
                   new SqlParameter(){ParameterName="@ape_cli",Value=reg.apellido},
                new SqlParameter(){ParameterName="@direc_cli",Value=reg.direccion},
                new SqlParameter(){ParameterName="@cod_dist",Value=reg.cod_dist},
                new SqlParameter(){ParameterName="@cel_cli",Value=reg.celular},
                new SqlParameter(){ParameterName="@fijo_cli",Value=reg.fijo},
                new SqlParameter(){ParameterName="@email_cli",Value=reg.correo},
                new SqlParameter(){ParameterName="@clave_cli",Value=reg.clave}
              };

            //ejecutar
            ViewBag.mensaje = clientes.CRUD("usp_cliente_add", pars, 1);



            ViewBag.distritos = new SelectList(distrito.listado(),
                "cod_dist",
                "nom_dist",
                reg.cod_dist);

            //los productos

            ViewBag.clientes = clientes.listado();

            return View(reg);
        }

        public ActionResult Actualizar(Cliente reg)
        {
            SqlParameter[] pars =
             {

                new SqlParameter(){ParameterName="@cod_cli",Value=reg.cod_cli},
                 new SqlParameter(){ParameterName="@DNI_cli",Value=reg.dni},
                  new SqlParameter(){ParameterName="@nom_cli",Value=reg.nombre},
                   new SqlParameter(){ParameterName="@ape_cli",Value=reg.apellido},
                new SqlParameter(){ParameterName="@direc_cli",Value=reg.direccion},
                new SqlParameter(){ParameterName="@cod_dist",Value=reg.cod_dist},
                new SqlParameter(){ParameterName="@cel_cli",Value=reg.celular},
                new SqlParameter(){ParameterName="@fijo_cli",Value=reg.fijo},
                new SqlParameter(){ParameterName="@email_cli",Value=reg.correo},
                new SqlParameter(){ParameterName="@clave_cli",Value=reg.clave}

              };

            //ejecutar
            ViewBag.mensaje = clientes.CRUD("usp_cliente_update", pars, 2);


            ViewBag.distritos = new SelectList(distrito.listado(),
                "cod_dist",
                "nom_dist",
                reg.cod_dist);

            //los productos

            ViewBag.clientes = clientes.listado();

            return View(reg);
        }

        public ActionResult Eliminar(Cliente reg)
        {
            SqlParameter[] pars =
             {

                new SqlParameter(){ParameterName="@cod_cli",Value=reg.cod_cli}

              };

            //ejecutar
            ViewBag.mensaje = clientes.CRUD("usp_cliente_delete", pars, 3);

            //las categorias


            ViewBag.distritos = new SelectList(distrito.listado(),
                "cod_dist",
                "nom_dist",
                reg.cod_dist);

            //los productos

            ViewBag.clientes = clientes.listado();


            return View(reg);
        }

    }
}