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
    public class HerramientaController : ControllerBase
    {
        // GET: api/Herramienta
        [HttpGet]
        public List<HerramientaDTO> GetHerramienta()
        {
            ControllerVulkano.HerramientaController herramientaController = new ControllerVulkano.HerramientaController();
            return herramientaController.SelectHerramienta(0);
        }

        // GET: api/Herramienta/5
        [HttpGet("{id}", Name = "GetHerramienta")]
        public List<HerramientaDTO> GetHerramienta(int id)
        {
            ControllerVulkano.HerramientaController herramientaController = new ControllerVulkano.HerramientaController();
            return herramientaController.SelectHerramienta(id);
        }

        // POST: api/Herramienta
        [HttpPost]
        public ReturnMessage Post([FromBody] HerramientaDTO herramienta)
        {
            ControllerVulkano.HerramientaController herramientaController = new ControllerVulkano.HerramientaController();
            return herramientaController.CreateHerramienta(herramienta);
        }

        // PUT: api/Herramienta/5
        [HttpPut("{id}")]
        public ReturnMessage Put([FromBody] HerramientaDTO herramienta)
        {
            ControllerVulkano.HerramientaController herramientaController = new ControllerVulkano.HerramientaController();
            return herramientaController.UpdateHerramienta(herramienta);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ReturnMessage Delete(int herramienta_id)
        {
            ControllerVulkano.HerramientaController herramientaController = new ControllerVulkano.HerramientaController();
            return herramientaController.DeleteHerramienta(herramienta_id);
        }
    }
}
