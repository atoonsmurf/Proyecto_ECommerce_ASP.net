using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using ProyectoMundoTronic.Models;

namespace ProyectoMundoTronic.DAO
{
    public class ordenDAO
    {
        public string Transaccion(OrdenPedido reg, List<Item> carroCompra)

        {

            string mensaje = "";

            conexionDAO cn = new conexionDAO();

            cn.getcn.Open();

            SqlTransaction tr = cn.getcn.BeginTransaction(IsolationLevel.Serializable);

            try

            {

                //1.ejecutar el procedure usp_agregar_orden con su transaccion

                SqlCommand cm = new SqlCommand("usp_agregar_orden", cn.getcn, tr);

                cm.CommandType = CommandType.StoredProcedure;

                cm.Parameters.Add("@cliente", SqlDbType.VarChar, 100).Value = reg.cliente;

                cm.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = reg.email;

                cm.Parameters.Add("@fono", SqlDbType.VarChar, 25).Value = reg.fono;

                cm.Parameters.Add("@n", SqlDbType.Int).Direction = ParameterDirection.Output;

                cm.ExecuteNonQuery();



                //2.recuperar el valor de @n OUTPUT y almacenar en una variable

                int n = (int)cm.Parameters["@n"].Value;


                //3.ejecutar el procedure usp_agregar_orden_detalle, leyendo cada

                //elemento de carrito con la transaccion

                foreach (Item it in carroCompra)

                {

                    cm = new SqlCommand("usp_agregar_orden_detalle", cn.getcn, tr);

                    cm.CommandType = CommandType.StoredProcedure;



                    cm.Parameters.Add("@norden", SqlDbType.Int).Value = n;

                    cm.Parameters.Add("@idproducto", SqlDbType.VarChar).Value = it.codigo;

                    cm.Parameters.Add("@precio", SqlDbType.Decimal).Value = it.precio;

                    cm.Parameters.Add("@cantidad", SqlDbType.Int).Value = it.cantidad;

                    cm.ExecuteNonQuery();

                }


                foreach (Item it in carroCompra)

                {

                    cm = new SqlCommand("usp_actualiza_unidades", cn.getcn, tr);

                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add("@idproducto", SqlDbType.VarChar).Value = it.codigo;

                    cm.Parameters.Add("@cantidad", SqlDbType.Int).Value = it.cantidad;

                    cm.ExecuteNonQuery();

                }



                //si todo esta OK

                tr.Commit();

                mensaje = string.Format("Se ha registrado la Orden con numero {0}", n);

            }

            catch (SqlException ex)

            {

                mensaje = ex.Message;

                tr.Rollback();

            }

            finally { cn.getcn.Close(); }



            return mensaje;

        }
    }
}