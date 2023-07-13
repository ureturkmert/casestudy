using Autofac;
using HPASS.Authentication.Foundation.Generation;
using HPASS.Authentication.Foundation.Option;
using HPASS.Authentication.Foundation.Token;
using HPASS.Business.Main.Abstraction;
using HPASS.Crosscutting.Helper;
using HPASS.Dto.Main;
using HPASS.Request.Common;
using HPASS.Response.Common;
using HPASS.Service.Base.Controller;
using Microsoft.AspNetCore.Mvc;

namespace HPASS.Service.Main.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(ILifetimeScope container, IConfiguration config) : base(container, config)
        {
        }



        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginRequest request)
        {

            var mainBusiness = this.container.Resolve<IMainBusiness>();

            var userSearchResult = mainBusiness.GetUserToAuthenticate(request);
            if (userSearchResult.HasError)
            {
                return Ok(new ServiceResult<HpassAuthenticationToken>(userSearchResult.ResponseCode));
            }

            UserAuthenticateDto workingDto = userSearchResult.Result;

            if (!PasswordHashUtility.ValidatePassword(request.Password, workingDto.Password))
            {
                return Unauthorized(new ServiceResult<HpassAuthenticationToken>("PASS_MISSMATCH"));
            }

            var tokenOption = new TokenGenerationOptions();
            tokenOption.ClaimsDictionary.Add("UserId", workingDto.Id.ToString());
            tokenOption.ClaimsDictionary.Add("HPID", workingDto.HealthcareProvider.Id.ToString());
            tokenOption.ClaimsDictionary.Add("HPName", workingDto.HealthcareProvider.Name.ToString());
            HpassAuthenticationToken tokenResponse = HpassTokenGenerator.GenerateToken(tokenOption);

            return Ok(new ServiceResult<HpassAuthenticationToken>(tokenResponse, ""));
        }
    }

}
