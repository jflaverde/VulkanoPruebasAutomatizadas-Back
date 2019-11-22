using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.DTO
{
    public class WorkerStatus
    {
        public int WorkerID { get; set; }
        public string TipoPrueba { get; set; }
        public EstadoDTO Estado { get; set; }

        public WorkerStatus() {
            Estado = new EstadoDTO();
        }
    }
}
