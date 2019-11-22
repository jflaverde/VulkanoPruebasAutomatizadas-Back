using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFramework.DTO;
using DataFramework.Messages;

namespace DataFramework.CRUD
{
    public class WorkerBehavior:ConexionDB
    {
        /// <summary>
        /// Obtiene el listado de los estados del worker
        /// si es 0 devuelve todos
        /// un valor != 0 devuelve 1
        /// </summary>
        /// <param name="workerID"></param>
        /// <returns></returns>
        public List<WorkerStatus> GetWorkerStatus(int workerID)
        {
            List<WorkerStatus> workerStatusList = new List<WorkerStatus>();
            StringBuilder query = new StringBuilder().Append(@"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                            SELECT WORKER_ID,ESTADO_WORKER.TIPO_PRUEBA,ESTADO_WORKER.ESTADO_ID,ESTADO.NOMBRE from ESTADO_WORKER
                            INNER JOIN ESTADO ON ESTADO_WORKER.ESTADO_ID=ESTADO.ESTADO_ID");

            if(workerID!=0)
            {
                query.Append(" WHERE WORKER_ID=@WORKER_ID");
            }
            using (var con = ConectarDB())
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(query.ToString(), con))
                    {
                        if(workerID!=0)
                        {
                            command.Parameters.Add(new SqlParameter("@WORKER_ID", workerID));
                        }
                        

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                WorkerStatus workerStatus = new WorkerStatus()
                                {
                                    WorkerID = Convert.ToInt32(reader[0]),
                                    TipoPrueba = reader[1].ToString(),
                                };
                                
                                workerStatus.Estado.ID = Convert.ToInt32(reader[2]);
                                workerStatus.Estado.Nombre = reader[3].ToString();
                                workerStatusList.Add(workerStatus);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " Count not insert.");
                }
                finally
                {
                    con.Close();
                }
            }

            return workerStatusList;
        }


        /// <summary>
        /// Actualiza el estado del worker, si no existe se crea
        /// </summary>
        /// <param name="worker"></param>
        public ReturnMessage UpdateWorkerStatus(WorkerStatus worker)
        {
            ReturnMessage mensaje = new ReturnMessage();

            string query = @"[SPUpdateWorkerStatus] @WorkerID,@WorkerStatus,@Tipo_Prueba";
            int respuesta = 0;

            using (var con = ConectarDB())
            {
                con.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.Add(new SqlParameter("@WorkerID", worker.WorkerID));
                        command.Parameters.Add(new SqlParameter("@WorkerStatus",worker.Estado.ID));
                        command.Parameters.Add(new SqlParameter("@Tipo_Prueba", worker.TipoPrueba));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                respuesta = Convert.ToInt32(reader[0]);
                            }
                        }
                    }

                    if(respuesta==1)
                    {
                        mensaje.Mensaje = "Se actualizó el estado del worker";
                        mensaje.TipoMensaje = TipoMensaje.Correcto;
                        mensaje.obj = respuesta;
                    }
                    else
                    {
                        throw new Exception();
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Count not insert.");
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
