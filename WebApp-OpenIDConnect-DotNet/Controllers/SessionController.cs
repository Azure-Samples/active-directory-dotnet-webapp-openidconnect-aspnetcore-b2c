using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApp_OpenIDConnect_DotNet.Controllers
{
    public class SessionController : Controller
    {
        public SessionController(IOptions<AzureAdB2COptions> b2cOptions)
        {
            AzureAdB2COptions = b2cOptions.Value;
        }

        public AzureAdB2COptions AzureAdB2COptions { get; set; }

        [HttpGet]
        public async Task SignIn()
        {
            await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
        }

        [HttpGet]
        public async Task ResetPassword()
        {
            var properties = new AuthenticationProperties() { RedirectUri = "/" };
            properties.Items[AzureAdB2COptions.PolicyAuthenticationProperty] = AzureAdB2COptions.ResetPasswordPolicyId;
            await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, properties);
        }

        [HttpGet]
        public async Task EditProfile()
        {
            var properties = new AuthenticationProperties() { RedirectUri = "/" };
            properties.Items[AzureAdB2COptions.PolicyAuthenticationProperty] = AzureAdB2COptions.EditProfilePolicyId;
            await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, properties);
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            return SignOut(
                new AuthenticationProperties { RedirectUri = Url.Action("SignedOut") },
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public IActionResult SignedOut()
        {
            return View();
        }
    }
}