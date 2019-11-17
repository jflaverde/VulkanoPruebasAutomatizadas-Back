using System;
using Data;

namespace Controller
{
    public class PruebaConexion
    {
        public string TestConnection()
        {
            Data.ConexionDB con = new ConexionDB();
            var conexion = con.ConectarDB();
            con.OpenConnection();

            if (conexion.State == System.Data.ConnectionState.Open)
            {
                string strConn= "ConnectionWorks";
                con.CloseConnection();
                return strConn;
            }

            return "Connection Not Work";
        }
    }
}
