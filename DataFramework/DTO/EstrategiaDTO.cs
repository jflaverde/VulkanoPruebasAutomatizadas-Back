using System;
using System.Collections.Generic;
using System.Text;

namespace DataFramework.DTO
{
    public class EstrategiaDTO
    {
        public int Estrategia_ID { get; set; }
        public string Nombre { get; set; }
        public EstadoDTO Estado { get; set; }
        public AplicacionDTO Aplicacion { get; set; }
        public List<TipoPruebaDTO> TipoPruebas { get; set; }

        public AppVersionDTO Version { get; set; }

        public EstrategiaDTO()
        {
            Version = new AppVersionDTO();
            TipoPruebas = new List<TipoPruebaDTO>();
        }
    }
}
