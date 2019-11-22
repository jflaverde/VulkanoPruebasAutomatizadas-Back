using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ControllerVulkano;
using DataFramework.DTO;
using DataFramework.Messages;

namespace VulkanoPruebasAutomatizadas_Back.Controllers
{

    public class EstrategiaController : ApiController
    {
        // GET: api/Estrategia
        public List<EstrategiaDTO> Get()
        {
            ControllerVulkano.EstrategiaController estrategiaController = new ControllerVulkano.EstrategiaController();
            return estrategiaController.SelectEstrategia(0);

        }

        // GET: api/Estrategia/5
        //[HttpGet("{id}", Name = "GetEstrategia")]
        public List<EstrategiaDTO> GetEstrategia(int id)
        {
            ControllerVulkano.EstrategiaController estrategiaController = new ControllerVulkano.EstrategiaController();
            return estrategiaController.SelectEstrategia(id);
        }

        // POST: api/Estrategia
        [HttpPost]
        public ReturnMessage Post([FromBody] EstrategiaDTO estrategia)
        {
            ControllerVulkano.EstrategiaController estrategiaController = new ControllerVulkano.EstrategiaController();
            var returnMessage= estrategiaController.CreateEstrategia(estrategia);
            return returnMessage;
           

        }
        /// <summary>
        /// Post Prueba
        /// </summary>
        /// <param name="estrategia"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Estrategia/PostPrueba")]
        public ReturnMessage PostPrueba([FromBody]EstrategiaDTO estrategia)
        {
            ControllerVulkano.EstrategiaController estrategiaController = new ControllerVulkano.EstrategiaController();
            var returnMessage = estrategiaController.AddTipoPrueba(estrategia);
            //enviar mensaje a la cola
            RabbitMessagesController mensajeRabbit = new RabbitMessagesController();
            mensajeRabbit.Post(estrategia);
            return returnMessage;
        }

        // PUT: api/Estrategia/5
        [HttpPut]
        public ReturnMessage Put([FromBody] EstrategiaDTO estrategia)
        {
            ControllerVulkano.EstrategiaController estrategiaController = new ControllerVulkano.EstrategiaController();
            return estrategiaController.UpdateEstrategia(estrategia);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public ReturnMessage Delete(int estrategiaID)
        {
            ControllerVulkano.EstrategiaController estrategiaController = new ControllerVulkano.EstrategiaController();
            return estrategiaController.DeleteEstrategia(estrategiaID);
        }
    }
}
