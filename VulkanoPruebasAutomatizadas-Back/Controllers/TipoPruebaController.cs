using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataFramework.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ControllerVulkano;

namespace VulkanoPruebasAutomatizadas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPruebaController : ControllerBase
    {
        // GET: api/TipoPrueba/5
        [HttpGet("{estrategia_id}", Name = "GetPruebasEstrategia")]
        public ReturnMessage Get(int estrategia_id)
        {
            ControllerVulkano.TipoPruebaController tipoPruebaController = new ControllerVulkano.TipoPruebaController();
            return tipoPruebaController.ListPruebas(estrategia_id, 0);
        }

        [HttpGet("{estrategia_id}/{tipoprueba_id}", Name = "GetPruebas")]
        public ReturnMessage Get(int estrategia_id, int tipoprueba_id)
        {
            ControllerVulkano.TipoPruebaController tipoPruebaController = new ControllerVulkano.TipoPruebaController();
            return tipoPruebaController.ListPruebas(estrategia_id, tipoprueba_id);
        }

        // POST: api/TipoPrueba
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/TipoPrueba/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
