using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using DataFramework.DTO;
using DataFramework.Messages;
using ControllerVulkano;

namespace VulkanoPruebasAutomatizadas_Back.Controllers
{
    public class AplicacionController : ApiController
    {
        // GET: api/Aplicacion
        [System.Web.Http.HttpGet]
        public ReturnMessage GetAplicacion()
        {
            ControllerVulkano.AplicacionController aplicacionController = new ControllerVulkano.AplicacionController();
            return aplicacionController.SelectAplicacion(0);
        }

        // GET: api/Aplicacion/5
        public ReturnMessage Get(int id)
        {
            ControllerVulkano.AplicacionController aplicacionController = new ControllerVulkano.AplicacionController();
            return aplicacionController.SelectAplicacion(id);
        }

        // POST: api/Aplicacion
        [System.Web.Http.HttpPost]
        public ReturnMessage Post([FromBody] AplicacionDTO aplicacion)
        {
            ControllerVulkano.AplicacionController aplicacionController = new ControllerVulkano.AplicacionController();
            return aplicacionController.CreateAplicacion(aplicacion);
        }

        // PUT: api/Aplicacion/5
        public ReturnMessage Put([FromBody] AplicacionDTO aplicacion)
        {
            ControllerVulkano.AplicacionController aplicacionController = new ControllerVulkano.AplicacionController();
            return aplicacionController.UpdateAplicacion(aplicacion);
        }

        // DELETE: api/ApiWithActions/5
    
        public ReturnMessage Delete(int aplicacion_id)
        {
            ControllerVulkano.AplicacionController aplicacionController = new ControllerVulkano.AplicacionController();
            return aplicacionController.DeleteAplicacion(aplicacion_id);

        }
    }
}
