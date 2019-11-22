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
    public class EstrategiaController : ControllerBase
    {
        // GET: api/Estrategia
        [HttpGet]
        public List<EstrategiaDTO> Get()
        {
            ControllerVulkano.EstrategiaController estrategiaController = new ControllerVulkano.EstrategiaController();
            return estrategiaController.SelectEstrategia(0);

        }

        // GET: api/Estrategia/5
        [HttpGet("{id}", Name = "GetEstrategia")]
        public List<EstrategiaDTO> Get(int id)
        {
            ControllerVulkano.EstrategiaController estrategiaController = new ControllerVulkano.EstrategiaController();
            return estrategiaController.SelectEstrategia(id);
        }

        // POST: api/Estrategia
        [HttpPost]
        public ReturnMessage Post([FromBody] EstrategiaDTO estrategia)
        {
            ControllerVulkano.EstrategiaController estrategiaController = new ControllerVulkano.EstrategiaController();
            return estrategiaController.CreateEstrategia(estrategia);
        }

        [HttpPost("{id}", Name = "PostPrueba")]
        public ReturnMessage PostPrueba([FromBody]EstrategiaDTO estrategia)
        {
            ControllerVulkano.EstrategiaController estrategiaController = new ControllerVulkano.EstrategiaController();
            return estrategiaController.AddTipoPrueba(estrategia);
        }

        // PUT: api/Estrategia/5
        [HttpPut("{id}")]
        public ReturnMessage Put([FromBody] EstrategiaDTO estrategia)
        {
            ControllerVulkano.EstrategiaController estrategiaController = new ControllerVulkano.EstrategiaController();
            return estrategiaController.UpdateEstrategia(estrategia);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ReturnMessage Delete(int estrategiaID)
        {
            ControllerVulkano.EstrategiaController estrategiaController = new ControllerVulkano.EstrategiaController();
            return estrategiaController.DeleteEstrategia(estrategiaID);
        }
    }
}
