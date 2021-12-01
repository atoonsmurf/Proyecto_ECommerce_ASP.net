using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;

using ProyectoMundoTronic.Models;
namespace ProyectoMundoTronic.DAO
{
    public class clienteDAO
    {
        conexionDAO cn = new conexionDAO();
        public IEnumerable<Cliente> listado()

        {


            List<Cliente> temporal = new List<Cliente>();

            cn = new conexionDAO();

            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("usp_cliente_listado", cn.getcn);

                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    temporal.Add(new Cliente()
                    {

                        cod_cli = (string)dr[0],

                        dni = (string)dr[1],

                        nombre = (string)dr[2],

                        apellido = (string)dr[3],

                        direccion = (string)dr[4],


                        cod_dist = (int)dr[5],
                        celular = (string)dr[6],
                        fijo = (string)dr[7],
                        correo = (string)dr[8],
                        clave = (string)dr[9],

                    });
                }

                dr.Close();
                cn.getcn.Close();

            }
            return temporal;
        }
        public IEnumerable<Cliente> listado(String sp, SqlParameter[] pars = null)
        {
            {
                List<Cliente> temporal = new List<Cliente>();

                using (cn.getcn)
                {
                    SqlCommand cmd = new SqlCommand(sp, cn.getcn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (pars != null) cmd.Parameters.AddRange(pars.ToArray());

                    cn.getcn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        temporal.Add(new Cliente()
                        {

                            cod_cli = (string)dr[0],

                            dni = (string)dr[1],

                            nombre = (string)dr[2],

                            apellido = (string)dr[3],

                            direccion = (string)dr[4],


                            cod_dist = (int)dr[5],
                            celular = (string)dr[6],
                            fijo = (string)dr[7],
                            correo = (string)dr[8],
                            clave = (string)dr[9],

                        });
                    }
                    dr.Close();
                    cn.getcn.Close();
                }

                return temporal;
            }
        }

        public IEnumerable<Cliente> filtro(string nombre)

        {

            return listado().Where(p => p.nombre.StartsWith(nombre, StringComparison.CurrentCultureIgnoreCase));

        }

        public Cliente Buscar(String cod)

        {

            Cliente reg = listado().Where(c => c.cod_cli == cod).FirstOrDefault();

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

                if (op == 1) mensaje = "Registro exitoso";
                else if (op == 2) mensaje = "Actualización exitosa";
                else if (op == 3) mensaje = "Eliminación exitosa";

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
    }
}