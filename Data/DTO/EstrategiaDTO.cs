using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
    public class EstrategiaDTO
    {
        public int Estrategia_ID { get; set; }
        public EstadoDTO Estado { get; set; }
        public AplicacionDTO Aplicacion { get; set; }
        public List<TipoPruebaDTO> TipoPruebas { get; set; }
    }
}
