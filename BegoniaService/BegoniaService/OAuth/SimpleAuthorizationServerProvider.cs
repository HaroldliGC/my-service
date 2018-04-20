using System;
using System.Collections.Generic;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using BegoniaService.Models;

namespace BegoniaService.OAuth
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            /*
            AccountService accService = new AccountService();
            string md5Pwd = LogHelper.MD5CryptoPasswd(context.Password);
            IList<object[]> ul = accService.Login(context.UserName, md5Pwd);
            if (ul.Count() == 0)
            {
                context.SetError("invalid_grant", "The username or password is incorrect");
                return;
            }
            */
            BegoniaServiceContext db = new BegoniaServiceContext();
            var temp = db.Users.Where(user => user.Account == context.UserName && user.Password == context.Password);
            if (!temp.Any())
            {
                context.SetError("invalid_grant", "The username or password is incorrect");
                return;
            }


            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));
            context.Validated(identity);
        }
    }
}