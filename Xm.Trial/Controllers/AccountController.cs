using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;

namespace Xm.Trial.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            return new ChallengeResult("Google",
              Url.Action("ExternalLoginCallback", 
                          "Account", 
                          new { ReturnUrl = (Request.UrlReferrer == null) ? "" : Request.UrlReferrer.ToString()
        }));
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Blog");
        }
        
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Blog");
            return new RedirectResult(returnUrl);
        }
        
        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}