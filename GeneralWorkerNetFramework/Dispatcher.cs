using System;
using System.Collections.Generic;
using System.Text;
using ControllerVulkano;
using DataFramework.DTO;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Linq;
using System.Configuration;

namespace GeneralWorkerNetFramework
{
    public class Dispatcher
    {


        public IConnection ConnectionRabbitMQ()
        {
            ConnectionFactory factory = new ConnectionFactory();
            // "guest"/"guest" by default, limited to localhost connections
            factory.UserName = ConfigurationManager.AppSettings["UserID"];
            factory.Password = ConfigurationManager.AppSettings["Pwd"];
            factory.Port = 5672;
            factory.VirtualHost = "/";
            factory.HostName = ConfigurationManager.AppSettings["RutaRabbit"];
            IConnection conn = factory.CreateConnection();

            return conn;
        }


        /// <summary>
        /// Crea las colas en caso que no existan.
        /// </summary>
        public void CrearColas()
        {
            MQTipoPruebaController mQTipoPruebaController = new MQTipoPruebaController();

            var listQueues= mQTipoPruebaController.SelectMQTipoPrueba(0);

            string exchange = "Dispather";

            foreach (var queue in listQueues)
            {
                using (var conn = ConnectionRabbitMQ())
                {
                    IModel modelRabbit = conn.CreateModel();
                    //Declaración del exchange, que juega el papel del dispatcher
                    modelRabbit.ExchangeDeclare(exchange, ExchangeType.Direct);
                    //Declaración de la cola
                    modelRabbit.QueueDeclare(queue.QueueName, true, false, false, null);
                    //Union entre el dispatcher y la cola
                    modelRabbit.QueueBind(queue.QueueName, exchange, queue.RouteKey, null);
                }
            }

        }


        /// <summary>
        /// 
        /// </summary>
        public void ObtenerEstrategias()
        {
            EstrategiaController estrategiaController = new EstrategiaController();
            var estrategias=estrategiaController.SelectEstrategia(0);

            foreach (var estrategia in estrategias)
            {
                EnviaMensajes(estrategia);
            }
        }

        public void EnviaMensajes(EstrategiaDTO estrategia)
        {

            foreach (var tipoPrueba in estrategia.TipoPruebas)
            {
                MQTipoPruebaController mqTipoPruebaController = new MQTipoPruebaController();

                var mqTipoPrueba = mqTipoPruebaController.SelectMQTipoPrueba(tipoPrueba.MQTipoPrueba.ID).First();

                EstrategiaDTO estrategiaNew = new EstrategiaDTO()
                {
                    Estrategia_ID = estrategia.Estrategia_ID,
                    Nombre = estrategia.Nombre,
                    Aplicacion = estrategia.Aplicacion,
                    Estado = estrategia.Estado
                };
                estrategiaNew.TipoPruebas.Add(tipoPrueba);

                String jsonified = JsonConvert.SerializeObject(estrategiaNew);
                byte[] estrategiaBuffer = Encoding.UTF8.GetBytes(jsonified);


                using (var conn = ConnectionRabbitMQ())
                {
                    IModel model = conn.CreateModel();

                    model.BasicPublish(exchange: "Dispather",
                           routingKey: mqTipoPrueba.RouteKey,
                           basicProperties: null,
                           body: estrategiaBuffer
                       );
                }  
            }
        }
    }
}
