using System;
using System.Collections.Generic;
using System.Text;

namespace DataFramework.DTO
{
    public class HistorialEjecucionPruebaDTO
    {
        public int ID { get; set; }
        public DateTime FechaEjecucion  { get; set; }

        public int HoraEjecucion { get; set; }

        public DateTime FechaFinalizacion { get; set; }

        public int HoraFinalizacion { get; set; }

        public string Estado { get; set; }

        public string RutaResultados { get; set; }
    }
}
