using System;
using System.Collections.Generic;
using System.Text;
using DataFramework.DTO;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Linq;
using System.Configuration;

namespace GeneralWorkerNetFramework
{
    public class RabbitMQ
    {
        /// <summary>
        /// Obtiene los mensajes
        /// </summary>
        /// <returns></returns>
        public EstrategiaDTO GetMessages(string queueName)
        {
            var conn = ConnectionRabbitMQ();

            var channel = conn.CreateModel();
            bool noAck = true;
            BasicGetResult result = channel.BasicGet(queueName, noAck);
            if (result == null)
            {
                // No message available at this time.
            }
            else
            {
                IBasicProperties props = result.BasicProperties;
                byte[] body = result.Body;
                var base64Decoded = Encoding.UTF8.GetString(body);
                EstrategiaDTO estrategia = JsonConvert.DeserializeObject<EstrategiaDTO>(base64Decoded);

                ControllerVulkano.ScriptController scriptController = new ControllerVulkano.ScriptController();

                if (estrategia != null)
                {
                    var script = scriptController.SelectScript(estrategia.TipoPruebas.First().ID);
                    estrategia.TipoPruebas.First().Script = script;
                    //channel.BasicAck(result.DeliveryTag, false);
                }
                return estrategia;
            }
            return null;
        }

        /// <summary>
        /// Conexión con RabbitMQ
        /// </summary>
        /// <returns></returns>
        static IConnection ConnectionRabbitMQ()
        {
            Dispatcher dispatcher = new Dispatcher();

            var conn = dispatcher.ConnectionRabbitMQ();

            dispatcher.CrearColas();

            return conn;
        }

    }
}
