using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.DTO
{
    public enum EstadoEnum
    {
        EnCola=1,
        EnEjecucion=2,
        Finalizado=3,
        FinalizadoConErrores=4,
        WorkerLibre=5,
        WorkerOcupado=6,
    }
}
