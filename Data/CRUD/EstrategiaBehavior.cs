using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using System.Data.SqlClient;
using Data.Messages;

namespace Data.CRUD
{
    public class EstrategiaBehavior:ConexionDB
    {



        /// <summary>
        /// Crea una estrategia
        /// </summary>
        /// <param name="estrategia"></param>
        public ReturnMessage CreateEstrategia(EstrategiaDTO estrategia)
        {
            ReturnMessage mensaje = new ReturnMessage();
            string query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                            INSERT INTO ESTRATEGIA (NOMBRE,ESTADO_ID,APLICACION_ID) VALUES (@NOMBRE,@ESTADO,@APLICACION)";

            using (var con = ConectarDB())
            {
                con.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.Add(new SqlParameter("@NOMBRE", estrategia.Nombre));
                        command.Parameters.Add(new SqlParameter("@ESTADO", estrategia.Estado.ID));
                        command.Parameters.Add(new SqlParameter("@APLICACION", estrategia.Aplicacion.Aplicacion_ID));
                        command.ExecuteNonQuery();
                    }

                    mensaje.Mensaje="La estrategia se creó correctamente";
                    mensaje.TipoMensaje = TipoMensaje.Correcto;
                    mensaje.obj = estrategia;


                }
                catch (Exception ex)
                {
                    Console.WriteLine("Count not insert.");
                    mensaje.Mensaje = ex.Message;
                    mensaje.TipoMensaje = TipoMensaje.Error;
                    mensaje.obj = estrategia;
                }
                finally
                {
                    con.Close();
                }
                return mensaje;
            }
        }

        /// <summary>
        /// Actualiza una estrategia
        /// </summary>
        /// <param name="estrategia"></param>
        public ReturnMessage UpdateEstrategia(EstrategiaDTO estrategia)
        {
            ReturnMessage mensaje = new ReturnMessage();
            StringBuilder query = new StringBuilder();


            int flagUpdate = 0;
            query.Append(@"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                            UPDATE ESTRATEGIA SET");

            //valida que los campos tengan un valor para actualizar.
            if(!string.IsNullOrEmpty(estrategia.Nombre))
            {
                query.Append(" NOMBRE=@NOMBRE");
                flagUpdate = 1;
            }
            if (estrategia.Estado.ID != 0)
            {
                if (flagUpdate == 1)
                    query.Append(",");
                query.Append(" ESTADO_ID=@ESTADO");
                flagUpdate = 1;
            }
            if(estrategia.Aplicacion.Aplicacion_ID!=0)
            {
                if (flagUpdate == 1)
                    query.Append(",");
                query.Append(" APLICACION_ID=@APLICACION_ID");
                flagUpdate = 1;
            }

            if(flagUpdate==1)
            {
                query.Append(" , WHERE ESTRATEGIA_ID=@ESTRATEGIA_ID;");
            }

            if(flagUpdate==1)
            {
                using (var con = ConectarDB())
                {
                    con.Open();

                    try
                    {
                        using (SqlCommand command = new SqlCommand(query.ToString(), con))
                        {
                            if (!string.IsNullOrEmpty(estrategia.Nombre))
                            {
                                command.Parameters.Add(new SqlParameter("@NOMBRE", estrategia.Nombre));
                            }
                            if (estrategia.Estado.ID != 0)
                            {
                                command.Parameters.Add(new SqlParameter("@ESTADO", estrategia.Estado.ID));
                            }
                            if (estrategia.Aplicacion.Aplicacion_ID != 0)
                            {
                                command.Parameters.Add(new SqlParameter("@APLICACION", estrategia.Aplicacion.Aplicacion_ID));
                            }
                            command.ExecuteNonQuery();
                        }
                        mensaje.Mensaje = "La estrategia se actualizó correctamente";
                        mensaje.TipoMensaje = TipoMensaje.Correcto;
                        mensaje.obj = estrategia;


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Count not insert.");
                        mensaje.Mensaje = ex.Message;
                        mensaje.TipoMensaje = TipoMensaje.Error;
                        mensaje.obj = estrategia;
                    }
                    finally
                    {
                        con.Close();
                    }
                }

                
            }
            return mensaje;
        }

        /// <summary>
        /// Consulta una estrategia
        /// </summary>
        /// <param name="estrategiaID"></param>
        /// <returns></returns>
        public List<EstrategiaDTO> SelectEstrategia(int estrategiaID)
        {
            List<EstrategiaDTO> listEstrategias = new List<EstrategiaDTO>();
            StringBuilder query = new StringBuilder().Append(@"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                            SELECT 
                            T0.ESTRATEGIA_ID,
                            T0.NOMBRE,
                            T2.ESTADO_ID,
                            T2.NOMBRE NOMBREESTADO,
                            T1.APLICACION_ID,
                            T1.APLICACION_VERSION,
                            T1.DESCRIPCION,
                            T1.ES_WEB,
                            T1.NOMBRE NOMBREAPLICACION,
                            T1.RUTA_APLICACION
                            FROM ESTRATEGIA T0 
                            INNER JOIN APLICACION T1 ON T0.APLICACION_ID=T1.APLICACION_ID
                            INNER JOIN ESTADO T2 ON T0.ESTADO_ID=T2.ESTADO_ID");

            if(estrategiaID!=0)
            {
                query.Append(" WHERE T0.ESTRATEGIA_ID=@ESTRATEGIA_ID");
            }

            using (var con = ConectarDB())
            {
                con.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(query.ToString(), con))
                    {
                        if(estrategiaID!=0)
                            command.Parameters.Add(new SqlParameter("@ESTRATEGIA_ID", estrategiaID));
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EstrategiaDTO estrategia = new EstrategiaDTO();
                                estrategia.Estrategia_ID = Convert.ToInt32(reader[0]);
                                estrategia.Nombre = reader[1].ToString();
                                EstadoDTO estado = new EstadoDTO();
                                estado.ID = Convert.ToInt32(reader[2]);
                                estado.Nombre = reader[3].ToString();
                                estrategia.Estado = estado;
                                AplicacionDTO aplicacion = new AplicacionDTO();
                                aplicacion.Aplicacion_ID = Convert.ToInt32(reader[4]);
                                aplicacion.Version = reader[5].ToString();
                                aplicacion.Descripcion = reader[6].ToString();
                                aplicacion.Es_Web = Convert.ToInt32(reader[7]) == 1 ? true : false;
                                aplicacion.Nombre = reader[8].ToString();
                                aplicacion.Ruta_Aplicacion = reader[9].ToString();
                                estrategia.Aplicacion = aplicacion;
                                listEstrategias.Add(estrategia);
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
            return listEstrategias;
        }

        /// <summary>
        /// Borra una estrategia
        /// </summary>
        /// <param name="estrategiaID"></param>
        public ReturnMessage DeleteEstrategia(int estrategiaID)
        {
            ReturnMessage mensaje = new ReturnMessage();
            string query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                                DELETE ESTRATEGIA WHERE ESTRATEGIA_ID=@ESTRATEGIAID";

            using (var con = ConectarDB())
            {
                con.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.Add(new SqlParameter("@ESTRATEGIAID", estrategiaID));
                        command.ExecuteNonQuery();
                    }

                    mensaje.Mensaje = "La estrategia se borró correctamente";
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
    }
}
