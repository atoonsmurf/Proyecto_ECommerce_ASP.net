using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;
using ProyectoMundoTronic.Models;

namespace ProyectoMundoTronic.DAO
{
    public class distritoDAO
    {
        conexionDAO cn = new conexionDAO();
        public IEnumerable<Distrito> listado()
        {
            List<Distrito> temporal = new List<Distrito>();

            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("select  cod_dist,nom_dist  from DISTRITO", cn.getcn);

                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    temporal.Add(new Distrito()
                    {
                        cod_dist = dr.GetInt32(0),
                        nom_dist = dr.GetString(1)

                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }

        public IEnumerable<Distrito> listado(string sp, SqlParameter[] pars = null)
        {
            List<Distrito> temporal = new List<Distrito>();

            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand(sp, cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (pars != null) cmd.Parameters.AddRange(pars.ToArray());
                cn.getcn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    temporal.Add(new Distrito()
                    {
                        cod_dist = dr.GetInt32(0),
                        nom_dist = dr.GetString(1)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }

            return temporal;
        }
    }

}