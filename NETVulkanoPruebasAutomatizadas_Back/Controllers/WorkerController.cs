using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DataFramework.DTO;
using ControllerVulkano;
using DataFramework.Messages;

namespace NETVulkanoPruebasAutomatizadas_Back.Controllers
{
    public class WorkerController : ApiController
    {
        // GET: Worker
        public List<WorkerStatus> Get()
        {
            ControllerVulkano.WorkerController workerController = new ControllerVulkano.WorkerController();
            return workerController.GetWorkerStatus(0);
        }

        // GET: Worker/Create
        [HttpPost]
        public ReturnMessage Create([FromBody]WorkerStatus worker)
        {
            return new ReturnMessage();
        }
       

        // GET: Worker/Edit/5
        public ReturnMessage Edit(int id)
        {
            return new ReturnMessage();
        }

       
    }
}
