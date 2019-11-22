using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataFramework.Messages;

using ControllerVulkano;
using System.Web.Http;
using DataFramework.DTO;

namespace VulkanoPruebasAutomatizadas_Back.Controllers
{
    [RoutePrefix("api/TipoPrueba")]
    public class TipoPruebaController : ApiController
    {
        // GET: api/TipoPrueba/5
        [Route("{estrategia_id:int}")]
        public ReturnMessage Get(int estrategia_id)
        {
            ControllerVulkano.TipoPruebaController tipoPruebaController = new ControllerVulkano.TipoPruebaController();
            return tipoPruebaController.ListPruebas(estrategia_id, 0);
        }

        //[HttpGet("{estrategia_id}/{tipoprueba_id}", Name = "GetPruebas")]
        public ReturnMessage GetPruebas(int estrategia_id, int tipoprueba_id)
        {
            ControllerVulkano.TipoPruebaController tipoPruebaController = new ControllerVulkano.TipoPruebaController();
            return tipoPruebaController.ListPruebas(estrategia_id, tipoprueba_id);
        }

        /// <summary>
        /// Post Prueba
        /// </summary>
        /// <param name="estrategia"></param>
        /// <returns></returns>
        [HttpPost]

        public ReturnMessage Post([FromBody]EstrategiaDTO estrategia)
        {
            ControllerVulkano.EstrategiaController estrategiaController = new ControllerVulkano.EstrategiaController();
            var returnMessage = estrategiaController.AddTipoPrueba(estrategia);
            //enviar mensaje a la cola
            RabbitMessagesController mensajeRabbit = new RabbitMessagesController();
            mensajeRabbit.Post(estrategia);
            return returnMessage;
        }

        // PUT: api/TipoPrueba/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        public void Delete(int id)
        {
        }
    }
}
