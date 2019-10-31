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
    public class AplicacionController : ControllerBase
    {
        // GET: api/Aplicacion
        [HttpGet]
        public List<AplicacionDTO> GetAplicacion()
        {
            Controller.AplicacionController aplicacionController = new Controller.AplicacionController();
            return aplicacionController.SelectAplicacion(0);
        }

        // GET: api/Aplicacion/5
        [HttpGet("{id}", Name = "GetAplicacion")]
        public List<AplicacionDTO> Get(int id)
        {
            Controller.AplicacionController aplicacionController = new Controller.AplicacionController();
            return aplicacionController.SelectAplicacion(id);
        }

        // POST: api/Aplicacion
        [HttpPost]
        public ReturnMessage Post([FromBody] AplicacionDTO aplicacion)
        {
            Controller.AplicacionController aplicacionController = new Controller.AplicacionController();
            return aplicacionController.CreateAplicacion(aplicacion);
        }

        // PUT: api/Aplicacion/5
       [HttpPut("{id}")]
        public ReturnMessage Put([FromBody] AplicacionDTO aplicacion)
        {
            Controller.AplicacionController aplicacionController = new Controller.AplicacionController();
            return aplicacionController.UpdateAplicacion(aplicacion);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ReturnMessage Delete(int aplicacion_id)
        {
            Controller.AplicacionController aplicacionController = new Controller.AplicacionController();
            return aplicacionController.DeleteAplicacion(aplicacion_id);
        }
    }
}
