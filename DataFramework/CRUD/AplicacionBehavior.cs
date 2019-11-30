using System;
using System.Collections.Generic;
using System.Text;
using DataFramework.DTO;
using System.Data.SqlClient;
using DataFramework.Messages;
using System.Linq;

namespace DataFramework.CRUD
{
    public class AplicacionBehavior:ConexionDB
    {
        /// <summary>
        /// Crea una estrategia
        /// </summary>
        /// <param name="aplicacion"></param>
        public ReturnMessage CreateAplicacion(AplicacionDTO aplicacion)
        {
            ReturnMessage mensaje = new ReturnMessage();
            string query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                            INSERT INTO APLICACION (NOMBRE,APLICACION_VERSION,RUTA_APLICACION,ES_WEB,DESCRIPCION) VALUES (@NOMBRE,@APLICACION_VERSION,@RUTA_APLICACION,@ES_WEB,@DESCRIPCION)

                            SELECT @@IDENTITY AS 'Identity';";

            using (var con = ConectarDB())
            {
                con.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.Add(new SqlParameter("@NOMBRE", !string.IsNullOrEmpty(aplicacion.Nombre)?aplicacion.Nombre:string.Empty));
                        command.Parameters.Add(new SqlParameter("@APLICACION_VERSION", !string.IsNullOrEmpty(aplicacion.Version)?aplicacion.Version:string.Empty));
                        command.Parameters.Add(new SqlParameter("@RUTA_APLICACION", !string.IsNullOrEmpty(aplicacion.Ruta_Aplicacion)?aplicacion.Ruta_Aplicacion:string.Empty));
                        command.Parameters.Add(new SqlParameter("@ES_WEB", aplicacion.Es_Web));
                        command.Parameters.Add(new SqlParameter("@DESCRIPCION", !string.IsNullOrEmpty(aplicacion.Descripcion)?aplicacion.Descripcion:string.Empty));

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                aplicacion.Aplicacion_ID = Convert.ToInt32(reader[0]);
                            }
                        }
                    }

                    mensaje.Mensaje = "La aplicación se creó correctamente";
                    mensaje.TipoMensaje = TipoMensaje.Correcto;
                    mensaje.obj = aplicacion;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("No se pudo insertar");
                    mensaje.Mensaje = ex.Message;
                    mensaje.TipoMensaje = TipoMensaje.Error;
                    mensaje.obj = aplicacion;
                }
                finally
                {
                    con.Close();
                }
                return mensaje;
            }

        }

        public ReturnMessage UpdateAplicacion(AplicacionDTO aplicacion)
        {
            ReturnMessage mensaje = new ReturnMessage();
            StringBuilder query = new StringBuilder();


            int flagUpdate = 0;
            query.Append(@"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                            UPDATE APLICACION SET");

            //valida que los campos tengan un valor para actualizar.
            if (!string.IsNullOrEmpty(aplicacion.Nombre))
            {
                query.Append(" NOMBRE=@NOMBRE");
                flagUpdate = 1;
            }
            if (!string.IsNullOrEmpty(aplicacion.Version))
            {
                if (flagUpdate == 1)
                    query.Append(",");
                query.Append(" APLICACION_VERSION=@APLICACION_VERSION");
                flagUpdate = 1;
            }
            if (!string.IsNullOrEmpty(aplicacion.Ruta_Aplicacion))
            {
                if (flagUpdate == 1)
                    query.Append(",");
                query.Append(" RUTA_APLICACION=@RUTA_APLICACION");
                flagUpdate = 1;
            }
            if (aplicacion.Es_Web != true)
            {
                if (flagUpdate == 1)
                    query.Append(",");
                query.Append(" ES_WEB=@ES_WEB");
                flagUpdate = 1;
            }
            if (!string.IsNullOrEmpty(aplicacion.Descripcion))
            {
                if (flagUpdate == 1)
                    query.Append(",");
                query.Append(" DESCRIPCION=@DESCRIPCION");
                flagUpdate = 1;
            }
            if (flagUpdate == 1)
            {
                query.Append(" WHERE APLICACION_ID=@APLICACION_ID;");
            }
            if (flagUpdate == 1)
            {
                using (var con = ConectarDB())
                {
                    con.Open();

                    try
                    {
                        using (SqlCommand command = new SqlCommand(query.ToString(), con))
                        {
                            command.Parameters.Add(new SqlParameter("@APLICACION_ID", aplicacion.Aplicacion_ID));

                            if (!string.IsNullOrEmpty(aplicacion.Nombre))
                            {
                                command.Parameters.Add(new SqlParameter("@NOMBRE", aplicacion.Nombre));
                            }
                            if (!string.IsNullOrEmpty(aplicacion.Version))
                            {
                                command.Parameters.Add(new SqlParameter("@APLICACION_VERSION", aplicacion.Version));
                            }
                            if (!string.IsNullOrEmpty(aplicacion.Ruta_Aplicacion))
                            {
                                command.Parameters.Add(new SqlParameter("@RUTA_APLICACION", aplicacion.Ruta_Aplicacion));
                            }
                            if (aplicacion.Es_Web != true)
                            {
                                command.Parameters.Add(new SqlParameter("@ES_WEB", aplicacion.Es_Web));
                            }
                            if (!string.IsNullOrEmpty(aplicacion.Descripcion))
                            {
                                command.Parameters.Add(new SqlParameter("@DESCRIPCION", aplicacion.Descripcion));
                            }
                            command.ExecuteNonQuery();
                        }
                        mensaje.Mensaje = "La aplicacion se actualizó correctamente";
                        mensaje.TipoMensaje = TipoMensaje.Correcto;
                        mensaje.obj = aplicacion;


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("No se pudo actualizar.");
                        mensaje.Mensaje = ex.Message;
                        mensaje.TipoMensaje = TipoMensaje.Error;
                        mensaje.obj = aplicacion;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return mensaje;
        }

        public ReturnMessage DeleteAplicacion(int aplicacion_id)
        {
            ReturnMessage mensaje = new ReturnMessage();
            string query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                                DELETE APLICACION WHERE APLICACION_ID=@APLICACION_ID";

            using (var con = ConectarDB())
            {
                con.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.Add(new SqlParameter("@APLICACION_ID", aplicacion_id));
                        command.ExecuteNonQuery();
                    }

                    mensaje.Mensaje = "La aplicacion se borró correctamente";
                    mensaje.TipoMensaje = TipoMensaje.Correcto;


                }
                catch (Exception ex)
                {
                    mensaje.Mensaje = ex.Message;
                    mensaje.TipoMensaje = TipoMensaje.Error;
                }
                finally
                {
                    con.Close();
                }
                return mensaje;
            }
        }
        

        public List<AplicacionDTO> SelectAplicacion(int aplicacion_id)
        {
            List<AplicacionDTO> listaAplicaciones = new List<AplicacionDTO>();
            StringBuilder query = new StringBuilder().Append(@"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
							SELECT 
							APLICACION_ID,
							NOMBRE,
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
                                    Es_Web = Convert.ToInt32(reader[2]) == 1 ? true : false,
                                    Descripcion = reader[3].ToString()
                                };
                                listaAplicaciones.Add(aplicacion);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("No se encontró el registro.");
                }
                finally
                {
                    con.Close();
                }
            }
            return listaAplicaciones;
        }
    }
}
