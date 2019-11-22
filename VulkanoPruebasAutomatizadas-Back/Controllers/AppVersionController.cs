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
    public class AppVersionController : ControllerBase
    {
        // GET: api/AppVersion
        [HttpGet]
        public ReturnMessage GetAppVersion()
        {
            Controller.AppVersionController appVersionController = new Controller.AppVersionController();
            return appVersionController.SelectAppVersion(0);
        }

        // POST: api/AppVersion
        [HttpPost]
        public ReturnMessage Post([FromBody] AppVersionDTO appVersion)
        {
            Controller.AppVersionController appVersionController = new Controller.AppVersionController();
            return appVersionController.CreateAppVersion(appVersion);
        }
    }
}
