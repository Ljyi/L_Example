using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OAuth2WebApi.OAuth
{
    public class OAuthServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        ///  验证客户端 [Authorization Basic Base64(clientId:clientSecret)|Authorization: Basic 5zsd8ewF0MqapsWmDwFmQmeF0Mf2gJkW]
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }
            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }
            if (!string.IsNullOrEmpty(clientSecret))
            {
                context.OwinContext.Set("clientSecret", clientSecret);
            }
            var client = ClientRepository.Clients.Where(c => c.Id == clientId).FirstOrDefault();
            if (client != null)
            {
                context.Validated();
            }
            else
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }
            return Task.FromResult<object>(null);
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            var client = ClientRepository.Clients.Where(c => c.Id == context.ClientId).FirstOrDefault();
            if (client != null)
            {
                context.Validated(client.RedirectUrl);
            }
            return base.ValidateClientRedirectUri(context);
        }
        /// <summary>
        /// 客户端授权[生成access token]
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var secret = context.OwinContext.Get<string>("clientSecret");
            var client = ClientRepository.Clients.Where(c => c.Id == context.ClientId && c.Secret == secret).FirstOrDefault();
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, "iOS App"));
            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.Validated(ticket);
            return base.GrantClientCredentials(context);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //validate user credentials (验证用户名与密码)  should be stored securely (salted, hashed, iterated) 
            //  var userValid = await _accountService.ValidateUserNameAuthorizationPwdAsync(context.UserName, context.Password);
            var userValid = ClientRepository.Clients.Where(c => c.Id == context.ClientId && c.Secret == context.UserName).FirstOrDefault() != null;
            if (!userValid)
            {
                context.Rejected();
                context.SetError("123");
            }
            var claimsIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            var ticket = new AuthenticationTicket(claimsIdentity, new AuthenticationProperties());
            context.Validated(ticket);
            return base.GrantResourceOwnerCredentials(context);
        }
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            if (context.Ticket == null || context.Ticket.Identity == null)
            {
                context.Rejected();
                return base.GrantRefreshToken(context);
            }
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.OwinContext.Get<string>("as:client_id");
            // enforce client binding of refresh token
            if (originalClient != currentClient)
            {
                context.Rejected();
                return base.GrantRefreshToken(context);
            }
            // chance to change authentication ticket for refresh token requests
            var claimsIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "as:client_id", context.ClientId }
                });
            var newTicket = new AuthenticationTicket(claimsIdentity, props);
            context.Validated(newTicket);
            return base.GrantRefreshToken(context);
        }
    }
}