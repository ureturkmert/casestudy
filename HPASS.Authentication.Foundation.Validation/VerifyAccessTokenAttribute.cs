using HPASS.Authentication.Foundation.Option;
using HPASS.Crosscutting.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace HPASS.Authentication.Foundation.Validation
{
    [AttributeUsage(AttributeTargets.Method)]
    public class VerifyAccessTokenAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string accessRightsClaimKey = "AccessVault";

        private readonly string rightFunctionKey;

        private readonly JwtSecurityTokenHandler tokenHandler;

        private JwtSecurityToken securityToken;

        public VerifyAccessTokenAttribute(string rightFunctionKey = null)
        {
            this.rightFunctionKey = rightFunctionKey;
            this.tokenHandler = new JwtSecurityTokenHandler();
            this.securityToken = null;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authoriztionHeader = context.HttpContext.Request.Headers["Authorization"];
            string incomingJwtToken = string.Empty;

            if (string.IsNullOrWhiteSpace(authoriztionHeader))
            {
                context.HttpContext.Response.StatusCode = 401;
                JsonResult jsonResult = new JsonResult("MISSING AUTHORIZATION HEADER");
                context.Result = jsonResult;
                return;
            }
            else if (authoriztionHeader.StartsWith("Bearer"))
            {

                incomingJwtToken = authoriztionHeader.Substring("Bearer ".Length).Trim();
                this.securityToken = this.tokenHandler.ReadToken(incomingJwtToken) as JwtSecurityToken;
                if (this.securityToken is null)
                {
                    context.HttpContext.Response.StatusCode = 401;
                    JsonResult jsonResult = new JsonResult("ACCESS TOKEN CANNOT BE OBTAINED");
                    context.Result = jsonResult;
                    return;
                }

                bool isTokenValid = HpassTokenValidator.ValidateJwtToken(incomingJwtToken, new TokenValidationOptions());
                if (!isTokenValid)
                {
                    context.HttpContext.Response.StatusCode = 401;
                    JsonResult jsonResult = new JsonResult("INVALID ACCESS TOKEN");
                    context.Result = jsonResult;
                    return;
                }

                if (!this.CanTokenPassTroughRightFunctionKey())
                {
                    context.HttpContext.Response.StatusCode = 401;
                    JsonResult jsonResult = new JsonResult("INVALID RIGHT ACCESS");
                    context.Result = jsonResult;
                    return;
                }

                this.ProceedUserSessionContextGeneration(context);

            }
            else
            {
                context.HttpContext.Response.StatusCode = 401;
                JsonResult jsonResult = new JsonResult("AUTHORIZATION HEADER FORMAT INCORRECT");
                context.Result = jsonResult;
                return;
            }

        }

        private void ProceedUserSessionContextGeneration(AuthorizationFilterContext context)
        {

            var resolvedUserSessionContext = context.HttpContext.RequestServices.GetService(typeof(IUserSessionContext));
            if (resolvedUserSessionContext is null)
            {
                throw new Exception("'IUserSessionContext' cannot be resolved through context, while verifying token");
            }

            IUserSessionContext userSessionContext = resolvedUserSessionContext as IUserSessionContext;

            string userRightClaim = this.GetClaim(this.accessRightsClaimKey);
            if (!string.IsNullOrEmpty(userRightClaim) && !string.IsNullOrWhiteSpace(userRightClaim))
            {
                userSessionContext.Rights = userRightClaim.Split(",").ToList();
            }

            bool isUserIdConverted = Guid.TryParse(this.GetClaim("UserId"), out Guid userIdAsGuid);
            if (isUserIdConverted)
            {
                userSessionContext.UserId = userIdAsGuid;
            }
            else
            {
                userSessionContext.UserId = Guid.Empty;
            }

            bool isHPIdConverted = Guid.TryParse(this.GetClaim("HPID"), out Guid hpIdAsguid);
            if (isHPIdConverted)
            {
                userSessionContext.HPID = hpIdAsguid;
            }
            else
            {
                userSessionContext.HPID = Guid.Empty;
            }

            userSessionContext.HPName = this.GetClaim("HPName");
        }


        private bool CanTokenPassTroughRightFunctionKey()
        {
            if (string.IsNullOrWhiteSpace(this.rightFunctionKey))
            {
                return true;
            }
            else
            {

                return this.HasRequiredRightKey(this.rightFunctionKey);
            }

        }


        private string GetClaim(string claimKey)
        {
            var checkingClaim = this.securityToken.Claims.FirstOrDefault(claim => claim.Type == claimKey);
            var stringClaimValue = checkingClaim == null ? null : checkingClaim.Value;
            return stringClaimValue;
        }

        private bool HasRequiredRightKey(string checkingRightKey)
        {
            var rolesClaim = this.GetClaim(this.accessRightsClaimKey);
            if (string.IsNullOrEmpty(rolesClaim))
            {
                return false;
            }

            var rightList = rolesClaim.Split(',').ToList();

            return rightList.Contains(checkingRightKey);
        }
    }
}
