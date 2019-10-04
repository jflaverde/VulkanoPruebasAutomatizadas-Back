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
    public class EstrategiaController : ControllerBase
    {
        // GET: api/Estrategia
        [HttpGet]
        public List<EstrategiaDTO> Get()
        {
            Controller.EstrategiaController estrategiaController = new Controller.EstrategiaController();
            return estrategiaController.SelectEstrategia(0);

        }

        // GET: api/Estrategia/5
        [HttpGet("{id}", Name = "Get")]
        public List<EstrategiaDTO> Get(int id)
        {
            Controller.EstrategiaController estrategiaController = new Controller.EstrategiaController();
            return estrategiaController.SelectEstrategia(id);
        }

        // POST: api/Estrategia
        [HttpPost]
        public ReturnMessage Post([FromBody] EstrategiaDTO estrategia)
        {
            Controller.EstrategiaController estrategiaController = new Controller.EstrategiaController();
            return estrategiaController.CreateEstrategia(estrategia);
        }

        // PUT: api/Estrategia/5
        [HttpPut("{id}")]
        public ReturnMessage Put([FromBody] EstrategiaDTO estrategia)
        {
            Controller.EstrategiaController estrategiaController = new Controller.EstrategiaController();
            return estrategiaController.UpdateEstrategia(estrategia);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ReturnMessage Delete(int estrategiaID)
        {
            Controller.EstrategiaController estrategiaController = new Controller.EstrategiaController();
            return estrategiaController.DeleteEstrategia(estrategiaID);
        }
    }
}
