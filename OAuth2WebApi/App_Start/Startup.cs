using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using OAuth2WebApi.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2WebApi.App_Start
{
    public partial class Startup
    {    // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            //app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            //{
            //    AllowInsecureHttp = true,
            //    AuthorizeEndpointPath = new PathString("/oauth2/authorize"),
            //    TokenEndpointPath = new PathString("/oauth2/token"),
            //    Provider = new OAuthServerProvider(),
            //    AuthorizationCodeProvider = new AuthorizationCodeProvider(),
            //    RefreshTokenProvider = new RefreshTokenProvider()
            //});
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AuthorizeEndpointPath = new PathString("/oauth2/authorize"),
                TokenEndpointPath = new PathString("/oauth2"),
                Provider = new OAuthServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true,
                AuthorizationCodeProvider = new AuthorizationCodeProvider(),
            };
            app.UseOAuthBearerTokens(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }
    }
}