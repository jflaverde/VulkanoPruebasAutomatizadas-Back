using Data.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data.CRUD
{
    public class AplicacionBehavior:ConexionDB
    {
        public void CreateAplicacion(AplicacionDTO aplicacion)
        {

        }

        public void UpdateAplicacion(AplicacionDTO aplicacion)
        {

        }

        public List<AplicacionDTO> SelectAplicacion(int aplicacion_id)
        {
            List<AplicacionDTO> listaAplicaciones = new List<AplicacionDTO>();
            StringBuilder query = new StringBuilder().Append(@"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
							SELECT 
							APLICACION_ID,
							NOMBRE,
							APLICACION_VERSION,
							RUTA_APLICACION,
							ES_WEB,
							DESCRIPCION
							FROM APLICACION");

            if (aplicacion_id != 0)
            {
                query.Append(" WHERE APLICACION_ID=@AplicacionID");
            }

            using (var con = ConectarDB())
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(query.ToString(), con))
                    {
                        if (aplicacion_id != 0)
                            command.Parameters.Add(new SqlParameter("@AplicacionID", aplicacion_id));

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                AplicacionDTO aplicacion = new AplicacionDTO()
                                {
                                    Aplicacion_ID = Convert.ToInt32(reader[0]),
                                    Nombre = reader[1].ToString(),
                                    Version = reader[2].ToString(),
                                    Ruta_Aplicacion = reader[3].ToString(),
                                    Es_Web = Convert.ToInt32(reader[4]) == 1 ? true : false,
                                    Descripcion = reader[5].ToString()
                                };
                                listaAplicaciones.Add(aplicacion);
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Count not insert.");
                }
                finally
                {
                    con.Close();
                }
            }
            return listaAplicaciones;
            
        }

        public void DeleteAplicacion(AplicacionDTO aplicacion)
        {

        }
    }
}
