using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DTO;
using Data.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VulkanoPruebasAutomatizadas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScriptController : ControllerBase
    {

        // POST: api/Script
        [HttpPost]
        public ReturnMessage Post([FromBody] ScriptDTO script)
        {
            Controller.ScriptController scriptController = new Controller.ScriptController();
            return scriptController.AddScript(script);
        }

        //// PUT: api/Script/5
        //[HttpPut(Name = "updateScript")]
        //public ReturnMessage Put([FromBody] ScriptDTO script)
        //{
        //    Controller.ScriptController scriptController = new Controller.ScriptController();
        //    return scriptController.UpdateScript(script);
        //}

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
