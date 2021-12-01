using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

using ProyectoMundoTronic.Models;

namespace ProyectoMundoTronic.DAO
{
    public class conexionDAO

    {

        SqlConnection cn = new SqlConnection(

        @"server=.\SQLEXPRESS; database=MUNDOTRONIC_PERU; integrated security=true");

        public SqlConnection getcn { get { return cn; } }

    }
}