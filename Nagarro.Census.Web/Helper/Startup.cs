using System;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using Nagarro.CensusPopulation.Web;
using Microsoft.Practices.Unity.Configuration;
using Unity;
using System.Configuration;

[assembly: OwinStartup(typeof(Nagarro.CensusPopulation.Web.Helper.Startup))]
namespace Nagarro.CensusPopulation.Web.Helper
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = new UnityContainer();
            var unitySection = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            unitySection.Configure(container);
            app.UseCors(CorsOptions.AllowAll);
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(2880),
                Provider = container.Resolve<IOAuthAuthorizationServerProvider>()
            };
            app.UseOAuthBearerTokens(OAuthOptions);
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            //HttpConfiguration config = new HttpConfiguration();
            //WebApiConfig.Register(config);   
        }
        //private void ConfigureAuth(IAppBuilder app)
        //{
        //    ConfigureAuth(app);
        //    GlobalConfiguration.Configure(WebApiConfig.Register);
        //}
    }
}
