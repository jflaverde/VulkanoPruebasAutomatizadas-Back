using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace Monitor
{
    class Program
    {
        static void Main(string[] args)
        {

            var cola = CrearColaRabbitMQ();

            var mensajeObtenido = CrearMensaje();

            //cola.Excha

            cola.BasicPublish(exchange: exchangeName,
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: mensajeObtenido);


        }

        /// <summary>
        /// Consulta la información de RabbitMQ
        /// </summary>
        static void ConsultarRabbitMQ()
        {
            
            

        }


        static byte[] CrearMensaje()
        {
            string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            return body;
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

        /// <summary>
        /// Envia la información a RabbitMQ
        /// </summary>
        /// <returns></returns>
        static IModel CrearColaRabbitMQ()
        {
            using (var conn = ConnectionRabbitMQ())
            {
                IModel testModel = conn.CreateModel();

                Dictionary<string, object> dictionary = new Dictionary<string, object>();

                string objeto = "Esta es la segunda prueba de cola";
                string queueName = "TestQueue2";
                string exchangeName = "exhange de prueba";

                dictionary.Add("Mensaje de prueba", objeto);

                testModel.ExchangeDeclare(exchangeName, ExchangeType.);

                testModel.QueueDeclare(queueName, true, false, false, dictionary);

                testModel.QueueBind(queueName, exchangeName, "hello", null);

                

                return testModel;
            }
        }


    }
}
