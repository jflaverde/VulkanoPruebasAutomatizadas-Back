using DataFramework.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DataFramework.CRUD
{
    public class MQTipoPruebaBehavior: ConexionDB
    {
        public List<MQTipoPruebaDTO> SelectMQTipoPrueba(int tipoPruebaID)
        {
            List<MQTipoPruebaDTO> listaMQTipoPruebas = new List<MQTipoPruebaDTO>();
            StringBuilder query = new StringBuilder().Append(@"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                        SELECT MQTIPOPRUEBA_ID,NOMBRE,ROUTEKEY,QUEUENAME,ES_WEB FROM MQTIPOPRUEBA");

            if(tipoPruebaID!=0)
            {
                query.Append(" WHERE MQTIPOPRUEBA_ID=@TipoPruebaID");
            }

            using (var con = ConectarDB())
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(query.ToString(), con))
                    {
                        if (tipoPruebaID != 0)
                            command.Parameters.Add(new SqlParameter("@TipoPruebaID", tipoPruebaID));

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MQTipoPruebaDTO mqTipoPrueba = new MQTipoPruebaDTO();
                                mqTipoPrueba.ID = Convert.ToInt32(reader[0]);
                                mqTipoPrueba.Nombre = reader[1].ToString();
                                mqTipoPrueba.RouteKey = reader[2].ToString();
                                mqTipoPrueba.QueueName = reader[3].ToString();
                                mqTipoPrueba.Es_Web = Convert.ToInt32(reader[4]);
                                listaMQTipoPruebas.Add(mqTipoPrueba);
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
            return listaMQTipoPruebas;
        }
    }
}
