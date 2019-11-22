using System;
using System.Collections.Generic;
using System.Text;

namespace DataFramework.DTO
{
    public class MQTipoPruebaDTO
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string RouteKey { get; set; }
        public string QueueName { get; set; }

        public int Es_Web { get; set; }
    }
}
