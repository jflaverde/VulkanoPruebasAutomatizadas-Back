using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DataFramework.DTO;
using DataFramework.Messages;


namespace VulkanoPruebasAutomatizadas_Back.Controllers
{

    public class ScriptController : ApiController
    {

        // POST: api/Script
        [HttpPost]
        public ReturnMessage Post([FromBody] ScriptDTO script)
        {
            ControllerVulkano.ScriptController scriptController = new ControllerVulkano.ScriptController();
            return scriptController.AddScript(script);
        }

        // PUT: api/Script/5
        [HttpPut]
        public ReturnMessage Put([FromBody] ScriptDTO script)
        {
            ControllerVulkano.ScriptController scriptController = new ControllerVulkano.ScriptController();
            return scriptController.UpdateScript(script);
        }

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
