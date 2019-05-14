using AutoMapper;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Nagarro.CensusPopulation.Web.APIModels;
using Nagarro.CensusPopulation.BusinessLayer;
using Nagarro.CensusPopulation.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Nagarro.CensusPopulation.Web.Helper
{
    public class DotNetTechyAuthServerProvider : OAuthAuthorizationServerProvider
    {
        private IMapper mapper;
        public DotNetTechyAuthServerProvider()
        {
            MapperConfiguration cofig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, UserAPIModel>().ReverseMap();
                cfg.CreateMap<LoginDTO, LoginAPIModel>().ReverseMap();
            });


            mapper = cofig.CreateMapper();
        }
        private UserBDC userBDC = new UserBDC();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var authUser = userBDC.AuthenticateUser(context.UserName, context.Password);
            if (authUser != null)
            {
                UserAPIModel currentUser = mapper.Map<UserAPIModel>(authUser);
                //var currentUser = user.Where(u => u.Email == context.UserName && u.Password == context.Password).FirstOrDefault();
                    identity.AddClaim(new Claim("Role", Convert.ToString(currentUser.IsApprover)));
                    identity.AddClaim(new Claim("Status", Convert.ToString(currentUser.CurrentStatus)));
                    identity.AddClaim(new Claim("Id", Convert.ToString(currentUser.ID)));
                    identity.AddClaim(new Claim("Email", Convert.ToString(currentUser.Email)));
                    identity.AddClaim(new Claim("Fname", Convert.ToString(currentUser.FirstMidName)));
                    identity.AddClaim(new Claim(ClaimTypes.Role, Convert.ToString(currentUser.IsApprover)));
                    var props = new AuthenticationProperties(new Dictionary<string, string>
                                             {
                                                    {
                                                    "Email", context.UserName
                                                    },
                                                    {
                                                    "Role", Convert.ToString(currentUser.IsApprover)
                                                    },
                                                    {
                                                    "Status", Convert.ToString(currentUser.CurrentStatus)
                                                    },
                                                    {
                                                    "FName", currentUser.FirstMidName
                                                    }
                                                    });
                    var ticket = new AuthenticationTicket(identity, props);
                    context.Validated(ticket);
                //else
                //{
                //    //context.SetError("invalid_grant", "Provided username and password is not matching, Please retry!");
                //    //context.Rejected();
                //}
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is not matching, Please retry!");
                //context.Rejected();
            }
            return;

        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}