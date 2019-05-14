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
    public class UserAuthServerProvider : OAuthAuthorizationServerProvider
    {
        private IMapper mapper;
        private IUserBDC userBDC;
        public UserAuthServerProvider(IUserBDC _userBDC)
        {
            userBDC = _userBDC;
            MapperConfiguration cofig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, UserAPIModel>();
                cfg.CreateMap<LoginDTO, LoginAPIModel>();
            });


            mapper = cofig.CreateMapper();
        }
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
                    identity.AddClaim(new Claim(Resources.Id, Convert.ToString(currentUser.IsApprover)));
                    identity.AddClaim(new Claim(Resources.Status, Convert.ToString(currentUser.CurrentStatus)));
                    identity.AddClaim(new Claim(Resources.Id, Convert.ToString(currentUser.ID)));
                    identity.AddClaim(new Claim(Resources.Email, Convert.ToString(currentUser.Email)));
                    identity.AddClaim(new Claim(Resources.Fname, Convert.ToString(currentUser.FirstMidName)));
                    identity.AddClaim(new Claim(ClaimTypes.Role, Convert.ToString(currentUser.IsApprover)));
                    var props = new AuthenticationProperties(new Dictionary<string, string>
                                             {
                                                    {
                                                    Resources.Id, Convert.ToString(currentUser.ID)
                                                    },
                                                    {
                                                    Resources.Email, context.UserName
                                                    },
                                                    {
                                                    Resources.Role, Convert.ToString(currentUser.IsApprover)
                                                    },
                                                    {
                                                    Resources.Status, Convert.ToString(currentUser.CurrentStatus)
                                                    },
                                                    {
                                                    Resources.Fname, currentUser.FirstMidName
                                                    },
                                                                                                        {
                                                    Resources.ProfilePic, currentUser.ProfileImage
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
                context.SetError(Resources.InvalidGrant, Resources.InvalidCredentials);
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