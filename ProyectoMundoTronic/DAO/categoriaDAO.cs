using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data.SqlClient;
using System.Data;
using ProyectoMundoTronic.Models;

namespace ProyectoMundoTronic.DAO
{
    public class categoriaDAO
    {
        conexionDAO cn = new conexionDAO();
        public IEnumerable<Categoria> listado()
        {
            List<Categoria> temporal = new List<Categoria>();

            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("select cod_cat, desc_cat from CATEGORIA", cn.getcn);

                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    temporal.Add(new Categoria()
                    {
                        codcat = dr.GetInt32(0),
                        desc_cat = dr.GetString(1)

                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }

        public IEnumerable<Categoria> listado(string sp, SqlParameter[] pars = null)
        {
            List<Categoria> temporal = new List<Categoria>();

            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand(sp, cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (pars != null) cmd.Parameters.AddRange(pars.ToArray());
                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    temporal.Add(new Categoria()
                    {
                        codcat = dr.GetInt32(0),
                        desc_cat = dr.GetString(1)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }

            return temporal;
        }
    }

}