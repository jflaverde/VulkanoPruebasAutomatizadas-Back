using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VulkanoPruebasAutomatizadas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacionController : ControllerBase
    {
        // GET: api/Aplicacion
        [HttpGet]
        public List<AplicacionDTO> GetApplication()
        {
            Controller.AplicacionController aplicacionController = new Controller.AplicacionController();
            return aplicacionController.SelectAplicacion(0);
        }

        // GET: api/Aplicacion/5
        [HttpGet("{id}", Name = "GetApplication")]
        public List<AplicacionDTO> GetApplication(int id)
        {
            Controller.AplicacionController aplicacionController = new Controller.AplicacionController();
            return aplicacionController.SelectAplicacion(id);
        }

        // POST: api/Aplicacion
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT: api/Aplicacion/5
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
