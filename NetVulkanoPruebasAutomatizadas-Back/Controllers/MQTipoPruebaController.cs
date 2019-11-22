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
    public class MQTipoPruebaController : ControllerBase
    {
        // GET: api/MQTipoPrueba
        [HttpGet]
        public IEnumerable<MQTipoPruebaDTO> Get()
        {
            Controller.MQTipoPruebaController mQTipoPruebaController = new Controller.MQTipoPruebaController();
            return mQTipoPruebaController.SelectMQTipoPrueba(0);
        }

        // GET: api/MQTipoPrueba/5
        [HttpGet("{id}", Name = "GetMQTipoPrueba")]
        public MQTipoPruebaDTO Get(int id)
        {
            Controller.MQTipoPruebaController mQTipoPruebaController = new Controller.MQTipoPruebaController();
            return mQTipoPruebaController.SelectMQTipoPrueba(id).First();
        }

        // POST: api/MQTipoPrueba
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/MQTipoPrueba/5
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
