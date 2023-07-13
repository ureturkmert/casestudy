using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HPASS.Service.Base.Controller
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILifetimeScope container;
        protected readonly IConfiguration config;

        public BaseController(ILifetimeScope container, IConfiguration config)
        {
            this.container = container;
            this.config = config;
        }
    }

}