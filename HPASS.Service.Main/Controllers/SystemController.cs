using Autofac;
using HPASS.Business.Main.Abstraction;
using HPASS.Response.Common;
using HPASS.Service.Base.Controller;
using Microsoft.AspNetCore.Mvc;

namespace HPASS.Service.Main.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemController : BaseController
    {
        public SystemController(ILifetimeScope container, IConfiguration config) : base(container, config)
        {
        }

        [HttpGet]
        [Route("GenerateInitialDatabase")]
        public IActionResult GenerateInitialDatabase()
        {
            var mainBusiness = this.container.Resolve<IMainBusiness>();
            var databaseGenerationResult = mainBusiness.GenerateDatabaseAndInitialData();
            return Ok(databaseGenerationResult);
        }


        [HttpGet]
        [Route("HeartBeat")]
        public IActionResult HeartBeat()
        {
            return Ok(new ServiceResult<string>("HPASS Service Up and Running...", ""));
        }

    }
}