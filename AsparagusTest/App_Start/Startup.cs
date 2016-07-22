using AsparagusTest.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Duke.Owin.VkontakteMiddleware;

[assembly: OwinStartup(typeof(AsparagusTest.App_Start.Startup))]

namespace AsparagusTest.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseFacebookAuthentication(
               appId: "1608407786148520",
               appSecret: "f8d60154449d352b62f0717d1b905b02");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "577248928614-m537h7m57nmms0f8o0m0mfj1u20fudgv.apps.googleusercontent.com",
                ClientSecret = "KkS9nkXjRkXfpR0MizPpgs3I"
            });

            app.UseVkontakteAuthentication(new VkAuthenticationOptions()
            {
                AppId = "5424275",
                AppSecret = "4J1u5L9JcCkNmG8fPCz3"
            });
        }
    }
}
