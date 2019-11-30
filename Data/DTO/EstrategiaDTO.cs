﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTO
{
    public class EstrategiaDTO
    {
        public int Estrategia_ID { get; set; }
        public string Nombre { get; set; }
        public EstadoDTO Estado { get; set; }
        public AplicacionDTO Aplicacion { get; set; }
        public List<TipoPruebaDTO> TipoPruebas { get; set; }
        public string Version { get; set; }
        public bool EsWeb { get; set; }
        public string URLAplicacion { get; set; }
        public string RutaAPK { get; set; }

        public EstrategiaDTO()
        {
            TipoPruebas = new List<TipoPruebaDTO>();
        }
    }
}