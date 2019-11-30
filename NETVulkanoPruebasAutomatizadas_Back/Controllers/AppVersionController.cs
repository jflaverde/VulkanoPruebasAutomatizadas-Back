using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ControllerVulkano;
using DataFramework.DTO;
using DataFramework.Messages;


namespace NETVulkanoPruebasAutomatizadas_Back.Controllers
{
    [RoutePrefix("api/AppVersion")]
    public class AppVersionController : ApiController
    {
        // GET: AppVersion
        [HttpGet]
        [Route("GetAppVersion/{aplicacionId:int}")]
        public ReturnMessage Get(int aplicacionId)
        {
            ControllerVulkano.AppVersionController versionController = new ControllerVulkano.AppVersionController();
            return versionController.SelectAppVersion(aplicacionId);
        }

        [HttpGet]
        [Route("GetAppVersion")]
        public ReturnMessage GetAppVersion()
        {
            ControllerVulkano.AppVersionController versionController = new ControllerVulkano.AppVersionController();
            return versionController.SelectAppVersion(0);
        }


        // GET: AppVersion/Create
        public ReturnMessage Create([FromBody] AppVersionDTO appVersion)
        {
            ControllerVulkano.AppVersionController versionController = new ControllerVulkano.AppVersionController();
            return versionController.CreateAppVersion(appVersion);
            
        }
    }
}
