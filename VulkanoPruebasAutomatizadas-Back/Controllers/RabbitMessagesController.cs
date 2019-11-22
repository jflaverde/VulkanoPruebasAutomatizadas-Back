using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataFramework.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitorNetFramework;

namespace VulkanoPruebasAutomatizadas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMessagesController : ControllerBase
    {
        // GET: api/RabbitMessages
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/RabbitMessages/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RabbitMessages
        [HttpPost]
        public string Post([FromBody] EstrategiaDTO estrategia)
        {
            EstrategiaController estrategiaController = new EstrategiaController();
            var newEstrategia = estrategiaController.Get(estrategia.Estrategia_ID).First();
            newEstrategia.TipoPruebas = estrategia.TipoPruebas;
            Dispatcher rabbit = new Dispatcher();
            rabbit.EnviaMensajes(newEstrategia);
            return "Mensaje enviado a la Cola";
        }

        // PUT: api/RabbitMessages/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] EstrategiaDTO estrategia)
        { 
            
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
