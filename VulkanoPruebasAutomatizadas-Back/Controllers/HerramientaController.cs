using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Controller;
using Data.DTO;
using Data.Messages;

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
            Controller.HerramientaController herramientaController = new Controller.HerramientaController();
            return herramientaController.SelectHerramienta(0);
        }

        // GET: api/Herramienta/5
        [HttpGet("{id}", Name = "GetHerramienta")]
        public List<HerramientaDTO> GetHerramienta(int id)
        {
            Controller.HerramientaController herramientaController = new Controller.HerramientaController();
            return herramientaController.SelectHerramienta(id);
        }

        // POST: api/Herramienta
        [HttpPost]
        public ReturnMessage Post([FromBody] HerramientaDTO herramienta)
        {
            Controller.HerramientaController herramientaController = new Controller.HerramientaController();
            return herramientaController.CreateHerramienta(herramienta);
        }

        // PUT: api/Herramienta/5
        [HttpPut("{id}")]
        public ReturnMessage Put([FromBody] HerramientaDTO herramienta)
        {
            Controller.HerramientaController herramientaController = new Controller.HerramientaController();
            return herramientaController.UpdateHerramienta(herramienta);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ReturnMessage Delete(int herramienta_id)
        {
            Controller.HerramientaController herramientaController = new Controller.HerramientaController();
            return herramientaController.DeleteHerramienta(herramienta_id);
        }
    }
}
