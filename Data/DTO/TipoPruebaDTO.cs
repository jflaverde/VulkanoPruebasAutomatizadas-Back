using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
    public class TipoPruebaDTO
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Parametros { get; set; }
        public List<ScriptDTO> Scripts { get; set; }
        public string Descripcion { get; set; }
        public EstadoDTO Estado { get; set; }
        public MQTipoPruebaDTO MQTipoPrueba { get; set; }
    }
}
