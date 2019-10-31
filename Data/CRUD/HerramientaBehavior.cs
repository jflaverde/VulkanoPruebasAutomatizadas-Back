using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using System.Data.SqlClient;
using Data.Messages;
using System.Linq;

namespace Data.CRUD
{
    public class HerramientaBehavior:ConexionDB
    {
        /// <summary>
        /// Crea una estrategia
        /// </summary>
        /// <param name="aplicacion"></param>
        public ReturnMessage CreateHerramienta(HerramientaDTO herramienta)
        {
            ReturnMessage mensaje = new ReturnMessage();
            string query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                            INSERT INTO HERRAMIENTA (NOMBRE) VALUES (@NOMBRE,@TIPO_PRUEBA,@ES_WEB)

                            SELECT @@IDENTITY AS 'Identity';";

            using (var con = ConectarDB())
            {
                con.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.Add(new SqlParameter("@NOMBRE", herramienta.Nombre));
                        command.Parameters.Add(new SqlParameter("@TIPO_PRUEBA", herramienta.Tipo_Prueba));
                        command.Parameters.Add(new SqlParameter("@ES_WEB", herramienta.Es_Web));

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                herramienta.Herramienta_ID = Convert.ToInt32(reader[0]);
                            }
                        }
                    }

                    mensaje.Mensaje = "La herramienta se creó correctamente";
                    mensaje.TipoMensaje = TipoMensaje.Correcto;
                    mensaje.obj = herramienta;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("No se pudo insertar");
                    mensaje.Mensaje = ex.Message;
                    mensaje.TipoMensaje = TipoMensaje.Error;
                    mensaje.obj = herramienta;
                }
                finally
                {
                    con.Close();
                }
                return mensaje;
            }

        }

        public ReturnMessage UpdateHerramienta(HerramientaDTO herramienta)
        {
            ReturnMessage mensaje = new ReturnMessage();
            StringBuilder query = new StringBuilder();


            int flagUpdate = 0;
            query.Append(@"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                            UPDATE HERRAMIENTA SET");

            //valida que los campos tengan un valor para actualizar.
            if (!string.IsNullOrEmpty(herramienta.Nombre))
            {
                query.Append(" NOMBRE=@NOMBRE");
                flagUpdate = 1;
            }
            if (!string.IsNullOrEmpty(herramienta.Nombre))
            {
                query.Append(" TIPO_PRUEBA=@TIPO_PRUEBA");
                flagUpdate = 1;
            }
            if (!string.IsNullOrEmpty(herramienta.Nombre))
            {
                query.Append(" ES_WEB=@ES_WEB");
                flagUpdate = 1;
            }
            if (flagUpdate == 1)
            {
                query.Append(" , WHERE HERRAMIENTA_ID=@HERRAMIENTA_ID;");
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
                            if (!string.IsNullOrEmpty(herramienta.Nombre))
                            {
                                command.Parameters.Add(new SqlParameter("@NOMBRE", herramienta.Nombre));
                            }
                            if (!string.IsNullOrEmpty(herramienta.Nombre))
                            {
                                command.Parameters.Add(new SqlParameter("@TIPO_PRUEBA", herramienta.Nombre));
                            }
                            if (herramienta.Es_Web != true)
                            {
                                command.Parameters.Add(new SqlParameter("@ES_WEB", herramienta.Es_Web));
                            }
                            command.ExecuteNonQuery();
                        }
                        mensaje.Mensaje = "La herramienta se actualizó correctamente";
                        mensaje.TipoMensaje = TipoMensaje.Correcto;
                        mensaje.obj = herramienta;


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("No se pudo actualizar.");
                        mensaje.Mensaje = ex.Message;
                        mensaje.TipoMensaje = TipoMensaje.Error;
                        mensaje.obj = herramienta;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return mensaje;
        }

        public ReturnMessage DeleteHerramienta(int herramienta_id)
        {
            ReturnMessage mensaje = new ReturnMessage();
            string query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                                DELETE HERRAMIENTA WHERE HERRAMIENTA_ID=@HERRAMIENTA_ID";

            using (var con = ConectarDB())
            {
                con.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.Add(new SqlParameter("@HERRAMIENTA_ID", herramienta_id));
                        command.ExecuteNonQuery();
                    }

                    mensaje.Mensaje = "La herramienta se borró correctamente";
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

        public List<HerramientaDTO> SelectHerramienta(int herramienta_id)
        {
            List<HerramientaDTO> listaHerramientas = new List<HerramientaDTO>();
            StringBuilder query = new StringBuilder().Append(@"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
							SELECT 
							HERRAMIENTA_ID,
							NOMBRE,
                            TIPO_PRUEBA,
                            ES_WEB
							FROM HERRAMIENTA");

            if (herramienta_id != 0)
            {
                query.Append(" WHERE HERRAMIENTA_ID=@HERRAMIENTA_ID");
            }

            using (var con = ConectarDB())
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(query.ToString(), con))
                    {
                        if (herramienta_id != 0)
                            command.Parameters.Add(new SqlParameter("@HERRAMIENTA_ID", herramienta_id));

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                HerramientaDTO herramienta = new HerramientaDTO()
                                {
                                    Herramienta_ID = Convert.ToInt32(reader[0]),
                                    Nombre = reader[1].ToString()
                                };
                                listaHerramientas.Add(herramienta);
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
            return listaHerramientas;
        }
    }
}
