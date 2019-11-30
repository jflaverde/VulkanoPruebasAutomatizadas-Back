using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ControllerVulkano;
using DataFramework.DTO;
using DataFramework.Messages;

namespace NETVulkanoPruebasAutomatizadas_Back.Controllers
{
    public class AppVersionController : ApiController
    {
        // GET: AppVersion
        public ReturnMessage Get(int aplicacionId)
        {
            ControllerVulkano.AppVersionController versionController = new ControllerVulkano.AppVersionController();
            return versionController.SelectAppVersion(aplicacionId);
        }

 
        // GET: AppVersion/Create
        public ReturnMessage Create([FromBody] AppVersionDTO appVersion)
        {
            ControllerVulkano.AppVersionController versionController = new ControllerVulkano.AppVersionController();
            return versionController.CreateAppVersion(appVersion);
            
        }
    }
}
