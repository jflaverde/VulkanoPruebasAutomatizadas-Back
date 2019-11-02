using Data.DTO;
using Data.Messages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data.CRUD
{
    public class TipoPruebaBehavior:ConexionDB
    {
        /// <summary>
        /// lista las pruebas asociadas a una estrategia junto con el historial
        /// </summary>
        /// <param name="estrategia_id"></param>
        /// <param name="tipoprueba_id"></param>
        /// <returns></returns>
        public ReturnMessage ListPruebas(int estrategia_id,int tipoprueba_id)
        {
            ReturnMessage message = new ReturnMessage();
            List<TipoPruebaDTO> listaPruebas = new List<TipoPruebaDTO>();
            StringBuilder query = new StringBuilder().Append(@"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                            SELECT 
                                T3.TIPOPRUEBA_ID,
                                T3.NOMBRE,
                                T3.PARAMETROS,
                                T3.MQTIPOPRUEBA_ID,
                                T2.ESTRATEGIA_ID,
                                T2.NOMBRE,
                                T0.HISTORIAL_ID,
                                T0.FECHA_EJECUCION,
                                T0.FECHA_FINALIZACION,
                                T0.HORA_EJECUCION,
                                T0.HORA_FINALIZACION,
                                T0.ESTADO,
                                T0.RUTA_RESULTADOS
                                FROM HISTORIAL_EJECUCION_PRUEBA T0 
                                RIGHT JOIN ESTRATEGIA_TIPOPRUEBA T1 ON T0.ESTRATEGIA_TIPOPRUEBA=T1.ESTRATEGIA_TIPOPRUEBA_ID
                                INNER JOIN ESTRATEGIA T2 ON T2.ESTRATEGIA_ID=T1.ESTRATEGIA_ID
                                INNER JOIN TIPOPRUEBA T3 ON T3.TIPOPRUEBA_ID=T1.TIPOPRUEBA_ID
                                WHERE 1=1");

            if (estrategia_id != 0)
            {
                query.Append(" AND T2.ESTRATEGIA_ID=@ESTRATEGIA_ID");
            }

            if (tipoprueba_id != 0)
            {
                query.Append(" AND T3.TIPOPRUEBA_ID = @TIPOPRUEBA_ID");
            }

            using (var con = ConectarDB())
            {
                con.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(query.ToString(), con))
                    {
                        if (estrategia_id != 0)
                            command.Parameters.Add(new SqlParameter("@ESTRATEGIA_ID", estrategia_id));

                        if(tipoprueba_id!=0)
                            command.Parameters.Add(new SqlParameter("@TIPOPRUEBA_ID", tipoprueba_id));

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!listaPruebas.Exists(e => e.ID == Convert.ToInt32(reader[0])))
                                {
                                    TipoPruebaDTO tipoPrueba = new TipoPruebaDTO
                                    {
                                        ID = Convert.ToInt32(reader[0]),
                                        Nombre = reader[1].ToString(),
                                        Parametros = reader[2].ToString()
                                    };

                                    listaPruebas.Add(tipoPrueba);
                                }

                                if (!string.IsNullOrEmpty(reader[6].ToString()))
                                {
                                    HistorialEjecucionPruebaDTO historial = new HistorialEjecucionPruebaDTO();

                                    historial.FechaEjecucion = Convert.ToDateTime(reader[7]);
                                    historial.HoraEjecucion = !DBNull.Value.Equals(reader[9]) ? Convert.ToInt32(reader[9]) : 0;
                                    historial.HoraFinalizacion = !DBNull.Value.Equals(reader[10])? Convert.ToInt32(reader[10]):0;
                                    historial.Estado = Convert.ToInt32(reader[11]);
                                    historial.RutaResultados = !DBNull.Value.Equals(reader[12].ToString()) ? reader[12].ToString() : string.Empty;
                                   
                                    if (!DBNull.Value.Equals(reader[8]))
                                        historial.FechaFinalizacion = Convert.ToDateTime(reader[8]);


                                listaPruebas.Find(e => e.ID == Convert.ToInt32(reader[0])).HistorialEjecuciones.Add(historial);
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Count not insert.");
                    message.TipoMensaje = TipoMensaje.Error;
                    message.Mensaje = ex.Message;
                    message.obj = listaPruebas;
                    return message;
                }
                finally
                {
                    con.Close();
                }
            }

            message.TipoMensaje = TipoMensaje.Correcto;
            message.obj = listaPruebas;
            return message;
            
        }

        /// <summary>
        /// inserta el resultado de la prueba
        /// </summary>
        /// <param name="estrategia_id"></param>
        /// <param name="prueba_id"></param>
        /// <param name="ejecucion_id"></param>
        /// <param name="ruta_resultados"></param>
        /// <returns></returns>
        public int InsertEjecucionTipoPrueba(int estrategia_id,int prueba_id,int ejecucion_id,string ruta_resultados)
        {
            string query = @"EXEC [SPINSERTAR_EJECUCION] @ESTRATEGIA_ID,@PRUEBA_ID,@EJECUCION_ID,@RUTA_RESULTADOS";

            using (var con = ConectarDB())
            {
                con.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(query.ToString(), con))
                    {
                        
                        command.Parameters.Add(new SqlParameter("@ESTRATEGIA_ID", estrategia_id));
                        command.Parameters.Add(new SqlParameter("@PRUEBA_ID", prueba_id));
                        command.Parameters.Add(new SqlParameter("@EJECUCION_ID", ejecucion_id));
                        command.Parameters.Add(new SqlParameter("@RUTA_RESULTADOS", ruta_resultados));

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int respuesta = Convert.ToInt32(reader[0]);
                                return respuesta;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    con.Close();
                }
            }

            return 0; 

        }

    }
}
