using System;
using QC = System.Data.SqlClient;


namespace DataFramework
{
    public class ConexionDB
    {
        private QC.SqlConnection Connection;

        /// <summary>
        /// Realiza la conexión de la base de datos
        /// </summary>
        /// <returns>Objeto de tipo SQLConnection</returns>
        public QC.SqlConnection ConectarDB()
        {
            string Db = "vulkanodb";
            string userID = "vulkanoadmin";
            string password = "Vulkano01";

            string conexion = @"Server=tcp:vulkanoserver.database.windows.net,1433;Initial Catalog={0};Persist Security Info=False;User ID={1};Password={2};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            conexion = string.Format(conexion, Db, userID, password);
            Connection = new QC.SqlConnection(conexion);
            return Connection;
        }

        /// <summary>
        /// Abre la conexión
        /// </summary>
        public void OpenConnection()
        {
            Connection.Open();
        }

        /// <summary>
        /// Cierra la conexión
        /// </summary>
        public void CloseConnection()
        {
            Connection.Close();
        }

    }
}
