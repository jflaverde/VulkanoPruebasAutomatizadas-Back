using Data.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data.CRUD
{
    public class TipoPruebaBehavior:ConexionDB
    {
       // public List<TipoPruebaDTO> ListPruebas(int estrategia)
       // {
       //     List<EstrategiaDTO> listEstrategias = new List<EstrategiaDTO>();
       //     StringBuilder query = new StringBuilder().Append(@"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
       //                     SELECT 
       //                     T0.ESTRATEGIA_ID,
       //                     T0.NOMBRE,
       //                     T2.ESTADO_ID,
       //                     T2.NOMBRE NOMBREESTADO,
       //                     T1.APLICACION_ID,
       //                     T1.APLICACION_VERSION,
       //                     T1.DESCRIPCION,
       //                     T1.ES_WEB,
       //                     T1.NOMBRE NOMBREAPLICACION,
       //                     T1.RUTA_APLICACION,
							//T3.TIPOPRUEBA_ID,
							//T4.NOMBRE,
							//T4.MQTIPOPRUEBA_ID
       //                     FROM ESTRATEGIA T0 
       //                     INNER JOIN APLICACION T1 ON T0.APLICACION_ID=T1.APLICACION_ID
       //                     INNER JOIN ESTADO T2 ON T0.ESTADO_ID=T2.ESTADO_ID
							//LEFT JOIN ESTRATEGIA_TIPOPRUEBA T3 ON T0.ESTRATEGIA_ID=T3.ESTRATEGIA_ID
							//LEFT JOIN TIPOPRUEBA T4 ON T3.TIPOPRUEBA_ID=T4.TIPOPRUEBA_ID");

       //     if (estrategiaID != 0)
       //     {
       //         query.Append(" WHERE T0.ESTRATEGIA_ID=@ESTRATEGIA_ID");
       //     }

       //     using (var con = ConectarDB())
       //     {
       //         con.Open();

       //         try
       //         {
       //             using (SqlCommand command = new SqlCommand(query.ToString(), con))
       //             {
       //                 if (estrategiaID != 0)
       //                     command.Parameters.Add(new SqlParameter("@ESTRATEGIA_ID", estrategiaID));

       //                 using (var reader = command.ExecuteReader())
       //                 {
       //                     while (reader.Read())
       //                     {


       //                         if (!listEstrategias.Exists(e => e.Estrategia_ID == Convert.ToInt32(reader[0])))
       //                         {
       //                             EstrategiaDTO estrategia = new EstrategiaDTO();

       //                             estrategia.Estrategia_ID = Convert.ToInt32(reader[0]);
       //                             estrategia.Nombre = reader[1].ToString();
       //                             EstadoDTO estado = new EstadoDTO();
       //                             estado.ID = Convert.ToInt32(reader[2]);
       //                             estado.Nombre = reader[3].ToString();
       //                             estrategia.Estado = estado;
       //                             AplicacionDTO aplicacion = new AplicacionDTO();
       //                             aplicacion.Aplicacion_ID = Convert.ToInt32(reader[4]);
       //                             aplicacion.Version = reader[5].ToString();
       //                             aplicacion.Descripcion = reader[6].ToString();
       //                             aplicacion.Es_Web = Convert.ToInt32(reader[7]) == 1 ? true : false;
       //                             aplicacion.Nombre = reader[8].ToString();
       //                             aplicacion.Ruta_Aplicacion = reader[9].ToString();
       //                             estrategia.Aplicacion = aplicacion;
       //                             listEstrategias.Add(estrategia);
       //                         }

       //                         if (!string.IsNullOrEmpty(reader[10].ToString()))
       //                         {
       //                             TipoPruebaDTO tipoPrueba = new TipoPruebaDTO()
       //                             {
       //                                 ID = Convert.ToInt32(reader[10]),
       //                                 Nombre = reader[11].ToString(),
       //                             };

       //                             MQTipoPruebaDTO mqTipo = new MQTipoPruebaDTO();
       //                             if (!string.IsNullOrEmpty(reader[12].ToString()))
       //                             {
       //                                 mqTipo.ID = Convert.ToInt32(reader[12]);
       //                                 tipoPrueba.MQTipoPrueba = mqTipo;
       //                             }
       //                             listEstrategias.Find(e => e.Estrategia_ID == Convert.ToInt32(reader[0])).TipoPruebas.Add(tipoPrueba);
       //                         }
       //                     }
       //                 }

       //             }
       //         }
       //         catch (Exception ex)
       //         {
       //             Console.WriteLine("Count not insert.");
       //         }
       //         finally
       //         {
       //             con.Close();
       //         }
       //     }
       //     return listEstrategias;
       // }
        
    }
}
