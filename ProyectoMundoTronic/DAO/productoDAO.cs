using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;

using ProyectoMundoTronic.Models;

namespace ProyectoMundoTronic.DAO
{
    public class productoDAO
    {
        conexionDAO cn = new conexionDAO();
        public IEnumerable<Producto> listado()

        {


            List<Producto> temporal = new List<Producto>();

            cn = new conexionDAO();

            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("usp_listado_productos", cn.getcn);

                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    temporal.Add(new Producto()
                    {

                        codpro = (string)dr[0],

                        nombre= (string)dr[1],

                        descripcion = (string)dr[2],
                        
                        categoria=(int)dr[3],

                        stock = (int)dr[4],


                        precio = (decimal)dr[5]  

                    });
                }

                dr.Close();
                cn.getcn.Close();

            }
            return temporal;
        }
        public IEnumerable<Producto> listado(String sp, SqlParameter[] pars = null)
        {
            {
                List<Producto> temporal = new List<Producto>();

                using (cn.getcn)
                {
                    SqlCommand cmd = new SqlCommand(sp, cn.getcn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (pars != null) cmd.Parameters.AddRange(pars.ToArray());

                    cn.getcn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        temporal.Add(new Producto()
                        {

                            codpro = (string)dr[0],

                            nombre = (string)dr[1],

                            descripcion = (string)dr[2],

                            categoria = (int)dr[3],

                            stock = (int)dr[4],


                            precio = (decimal)dr[5]

                        });
                    }
                    dr.Close();
                    cn.getcn.Close();
                }

                return temporal;
            }
        }

        public IEnumerable<Producto> filtro(string nombre)

        {

            return listado().Where(p => p.descripcion.StartsWith(nombre, StringComparison.CurrentCultureIgnoreCase));

        }

        public Producto Buscar(String cod)

        {
            
            Producto reg = listado().Where(c => c.codpro == cod).FirstOrDefault();

            return reg;

        }


        public String CRUD(string sp, SqlParameter[] pars = null, int op = 0)

        {

            string mensaje = "";

            try

            {

                SqlCommand cmd = new SqlCommand(sp, cn.getcn);

                cmd.CommandType = CommandType.StoredProcedure;



                if (pars != null) cmd.Parameters.AddRange(pars.ToArray());

                cn.getcn.Open();

               cmd.ExecuteNonQuery(); //ejecutamos el CRUD

                if (op == 1) mensaje ="Registro exitoso";
                else if (op == 2) mensaje = "Actualización exitosa";
                else if (op == 3) mensaje ="Eliminación exitosa";

            }

            catch (SqlException e)
            {

                mensaje = e.Message;

            }

            finally

            {

                cn.getcn.Close();

            }



            return mensaje;

        }

        public IEnumerable<Producto> filtroDescripcion(string descripcion)
        {
            return listado().Where(p => p.descripcion.StartsWith(descripcion, StringComparison.CurrentCultureIgnoreCase));
        }

        public Producto BuscarDescripcion(string cod)
        {
            Producto reg = listado().Where(c => c.codpro == cod).FirstOrDefault();
            return reg;
        }
    }
}