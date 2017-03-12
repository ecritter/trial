using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Xm.Trial
{
    public partial class Startup
    {
        private void ConfigureAuth(IAppBuilder app)
        {
            var cookieOptions = new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Account/Login"),
                AuthenticationType = "ExternalCookie",
                ExpireTimeSpan = TimeSpan.FromMinutes(60),
            };

            app.UseCookieAuthentication(cookieOptions);

            app.SetDefaultSignInAsAuthenticationType(cookieOptions.AuthenticationType);

            var googleOAuth2AuthenticationOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "633929508797-o00j4ncrrtuc4e1ghlmq56ns0h1ls2va.apps.googleusercontent.com",
                ClientSecret = "ASR0zjlk4xedIzVxtRkVuoys",
                Provider = new GoogleOAuth2AuthenticationProvider() { }
            };
            
            googleOAuth2AuthenticationOptions.Scope.Add("email");

            app.UseGoogleAuthentication(googleOAuth2AuthenticationOptions);
        }
    }
}