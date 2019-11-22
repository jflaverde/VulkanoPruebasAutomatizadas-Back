using System;
using System.Collections.Generic;
using System.Text;

namespace DataFramework.Messages
{
    public class ReturnMessage
    {
        public TipoMensaje TipoMensaje { get; set; }

        public string Mensaje { get; set; }

        public object obj { get; set; }
    }
}

public enum TipoMensaje
{
    Correcto=1,
    Error=2
}