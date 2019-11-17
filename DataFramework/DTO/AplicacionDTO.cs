using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
    public class AplicacionDTO
    {
        public int Aplicacion_ID { get; set; }
        public string Nombre { get; set; }
        public string Version { get; set; }
        public string Ruta_Aplicacion { get; set; }
        public bool Es_Web { get; set; }
        public string Descripcion { get; set; }
    }
}
