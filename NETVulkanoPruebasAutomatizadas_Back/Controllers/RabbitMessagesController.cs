using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DataFramework.DTO;
using MonitorNetFramework;

namespace VulkanoPruebasAutomatizadas_Back.Controllers
{
    [RoutePrefix("api/RabbitMessages")]
    public class RabbitMessagesController : ApiController
    {
        // GET: api/RabbitMessages
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/RabbitMessages/5

        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RabbitMessages
        [Route("SendRabbitMessage")]
        [HttpPost]
        public string Post([FromBody] EstrategiaDTO estrategia)
        {
            EstrategiaController estrategiaController = new EstrategiaController();
            var newEstrategia = estrategiaController.GetEstrategia(estrategia.Estrategia_ID).First();
            newEstrategia.TipoPruebas = estrategia.TipoPruebas;
            Dispatcher rabbit = new Dispatcher();
            rabbit.EnviaMensajes(newEstrategia);
            return "Mensaje enviado a la Cola";
        }


        [Route("SendRabbitMessages")]
        [HttpPost]
        public string Post([FromBody]int estrategia_id)
        {
            EstrategiaController estrategiaController = new EstrategiaController();
            var newEstrategia = estrategiaController.GetEstrategia(estrategia_id).First();
            Dispatcher rabbit = new Dispatcher();
            rabbit.EnviaMensajes(newEstrategia);
            return "Mensajes enviados a la Cola";
        }

        // PUT: api/RabbitMessages/5
        public void Put(int id, [FromBody] EstrategiaDTO estrategia)
        { 
            
        }

        // DELETE: api/ApiWithActions/5
        public void Delete(int id)
        {
        }
    }
}
