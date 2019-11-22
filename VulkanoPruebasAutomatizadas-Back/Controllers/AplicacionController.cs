using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ControllerVulkano;
using DataFramework.DTO;
using DataFramework.Messages;

namespace VulkanoPruebasAutomatizadas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacionController : ControllerBase
    {
        // GET: api/Aplicacion
        [HttpGet]
        public ReturnMessage GetAplicacion()
        {
            ControllerVulkano.AplicacionController aplicacionController = new ControllerVulkano.AplicacionController();
            return aplicacionController.SelectAplicacion(0);
        }

        // GET: api/Aplicacion/5

        [HttpGet("{id}", Name = "GetAplicacion")]
        public ReturnMessage Get(int id)
        {
            ControllerVulkano.AplicacionController aplicacionController = new ControllerVulkano.AplicacionController();
            return aplicacionController.SelectAplicacion(id);
        }

        // POST: api/Aplicacion
        [HttpPost]
        public ReturnMessage Post([FromBody] AplicacionDTO aplicacion)
        {
            ControllerVulkano.AplicacionController aplicacionController = new ControllerVulkano.AplicacionController();
            return aplicacionController.CreateAplicacion(aplicacion);
        }

        // PUT: api/Aplicacion/5
       [HttpPut("{id}")]
        public ReturnMessage Put([FromBody] AplicacionDTO aplicacion)
        {
            ControllerVulkano.AplicacionController aplicacionController = new ControllerVulkano.AplicacionController();
            return aplicacionController.UpdateAplicacion(aplicacion);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]

        public ReturnMessage Delete(int aplicacion_id)
        {
            ControllerVulkano.AplicacionController aplicacionController = new ControllerVulkano.AplicacionController();
            return aplicacionController.DeleteAplicacion(aplicacion_id);

        }
    }
}
