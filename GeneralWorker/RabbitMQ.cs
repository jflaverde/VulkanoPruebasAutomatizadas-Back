using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Linq;

namespace WorkerCypress
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
            bool noAck = false;
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

                Controller.ScriptController scriptController = new Controller.ScriptController();

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
            ConnectionFactory factory = new ConnectionFactory();
            // "guest"/"guest" by default, limited to localhost connections
            //factory.UserName = "guest";
            //factory.Password = "guest";
            //factory.VirtualHost = "/";
            factory.HostName = "localhost";
            IConnection conn = factory.CreateConnection();



            return conn;
        }

    }
}
