using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MonitorNetFramework
{
    class Program
    {
        static void Main(string[] args)
        {

            //var cola = CrearColaRabbitMQ();

            Dispatcher dispatcher = new Dispatcher();

            dispatcher.CrearColas();

            dispatcher.ObtenerEstrategias();


            //ConsultarRabbitMQ();

        }

        /// <summary>
        /// Consulta la información de RabbitMQ
        /// </summary>
        static void ConsultarRabbitMQ()
        {

            //Definir las colas por cada tipo de prueba (check)
            //Definir el Exhange que hara las veces de Dispatcher (check)

            //Establecer los bindings para crear los routingKey (check)
            //Primero crear el registro en la base de datos. (check)
            //Enviar mensaje a la cola por cada tipo de prueba (check)


            //Crear workers capaces de leer mensajes encolados
            //Consultar información complementaria de la estrategia al worker
            //ejecutar el SCRIPT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!



            var conn = ConnectionRabbitMQ();

            //asumiendo que tenemos la cola, exhange y el routing creado capturamos un mensaje

            var channel = conn.CreateModel();
            string mensajes = string.Empty;
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                mensajes = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", mensajes);
            };



            channel.BasicConsume(queue: "QueueTest2",
                                 autoAck: true,
                                 consumer: consumer);



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

                IModel testModel2 = conn.CreateModel();

                string objeto = "Esta es la segunda prueba de cola";
                string queueName = "TestQueue2";
                string queueName2 = "TestQueue3";
                string exchangeName = "Exchange1";

                testModel.ExchangeDeclare(exchangeName, ExchangeType.Direct);

                testModel.QueueDeclare(queueName, true, false, false, null);

                testModel.QueueBind(queueName, exchangeName, "routing1", null);

                testModel2.QueueDeclare(queueName2, true, false, false, null);

                testModel2.QueueBind(queueName2, exchangeName, "routing1", null);

                string objeto2 = "Prueba de la cola 2";
                string exchangeName2 = "Exchange2";

                testModel2.ExchangeDeclare(exchangeName2, ExchangeType.Direct);

                testModel2.QueueDeclare(queueName2, true, false, false, null);

                testModel2.QueueBind(queueName2, exchangeName2, "routing2", null);

                var body = Encoding.UTF8.GetBytes(objeto);


                testModel.BasicPublish(exchange: exchangeName,
                        routingKey: "routing1",
                        basicProperties: null,
                        body: body
                    );

                body = Encoding.UTF8.GetBytes(objeto2);

                testModel.BasicPublish(exchange: exchangeName,
                       routingKey: "routing1",
                       basicProperties: null,
                       body: body
                   );


                return testModel;
            }
        }


    }
}
