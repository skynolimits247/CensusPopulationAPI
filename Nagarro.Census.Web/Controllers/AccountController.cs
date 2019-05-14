using AutoMapper;
using Nagarro.CensusPopulation.Web.APIModels;
using Nagarro.CensusPopulation.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nagarro.CensusPopulation.BusinessLayer;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System.Security.Claims;
using System.Web;

namespace Nagarro.CensusPopulation.Web.Controllers
{
    public class AccountController : ApiController
    {
        private IUserBDC userBDC;
        private IMapper mapper;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_userBDC">Instance of the userBDC for Depedency Injection</param>
        public AccountController(IUserBDC _userBDC)
        {
            userBDC = _userBDC;
            MapperConfiguration cofig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, UserAPIModel>();
                cfg.CreateMap<LoginDTO, LoginAPIModel>();
            });
            mapper = cofig.CreateMapper();
        }

        /// <summary>
        /// Get the list of all users
        /// </summary>
        /// <returns></returns>
        [Route("api/user")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize(Roles = "True")]
        public HttpResponseMessage GetAllUser()
        {
            try
            {
                List<UserAPIModel> users = mapper.Map<List<UserAPIModel>>(userBDC.GetAllUser());
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, e);
            }
        }

        /// <summary>
        /// API end point to get all users
        /// </summary>
        /// <returns></returns>
        [Route("api/users")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [AllowAnonymous]
        public HttpResponseMessage GetAll()
        {
            try
            {
                List<UserAPIModel> users = mapper.Map<List<UserAPIModel>>(userBDC.GetAllUser());
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, e);
            }
        }

        /// <summary>
        /// API End point to signup users
        /// </summary>
        /// <param name="user">Object of the user to be created</param>
        /// <returns></returns>
        // POST: api/User
        [Route("api/user/signup")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [AllowAnonymous]
        public HttpResponseMessage Post([FromBody]UserAPIModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userBDC.CreateUser(mapper.Map<UserDTO>(user)))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, true);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Resources.AlreadyRegisteredUser);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }

        }

        /// <summary>
        /// API End point for checking the availability of email existence
        /// </summary>
        /// <param name="email">Email ID</param>
        /// <returns></returns>
        // POST: api/user/email
        [Route("api/user/email")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [AllowAnonymous]
        public HttpResponseMessage EmailCheck([FromUri]string email)
        {
            try
            {
                if (email != null)
                {
                    if (userBDC.FindUserByEmail(email))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, true);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, false);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        /// <summary>
        /// API to check for the existence of AADHA number in the databse
        /// </summary>
        /// <param name="uidai">UIDAI number</param>
        /// <returns></returns>
        // POST: api/user/uidai
        [Route("api/user/uidai")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [AllowAnonymous]
        public HttpResponseMessage UIDAICheck([FromUri]string uidai)
        {
            try
            {
                if (uidai != null)
                {
                    if (userBDC.FindUserByUIDAI(uidai))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, true);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, false);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        /// <summary>
        /// API end point for login
        /// </summary>
        /// <param name="user">login ID and password of the user trying to login </param>
        /// <returns></returns>
        // POST: api/login
        [Route("api/login")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize(Roles = "True")]
        public HttpResponseMessage Post([FromBody]LoginAPIModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserAPIModel Luser = mapper.Map<UserAPIModel>(userBDC.AuthenticateUser(user.UserName, user.Password));
                    if (Luser != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, Luser);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Resources.AlreadyRegisteredUser);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }

        }

        /// <summary>
        /// API end point to mark user as approved
        /// </summary>
        /// <param name="approverId">Id of the user who is approving the user</param>
        /// <param name="userId">userId of the user to be marked approved</param>
        /// <returns></returns>
        //GET: api/approve
        [Route("api/approve")]
        [HttpPut]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize(Roles = "True")]
        public HttpResponseMessage ApproveUser([FromBody]ApproverAPIID approverId, [FromUri]int userId)
        {
            try
            {
                UserAPIModel user =   mapper.Map<UserAPIModel>(userBDC.GetUserById(userId));
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, false);
                }
                else
                {
                    if (user.CurrentStatus == APIModels.status.Pending)
                    {
                        user.CurrentStatus = APIModels.status.Approved;
                        user.ApprovedBy = approverId.ApproverId;
                        UserDTO userToUpdate = mapper.Map<UserDTO>(user);
                        if(userBDC.UpdateUser(userToUpdate))
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, true);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, false);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, false);
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return Request.CreateResponse(HttpStatusCode.Forbidden, e);
            }
        }

        /// <summary>
        /// Api end point to mark user as rejected
        /// </summary>
        /// <param name="approverId">Id of the user who is rejecting the user</param>
        /// <param name="userId">userId of the user to be marked rejected</param>
        /// <returns></returns>
        //GET: api/reject
        [Route("api/reject")]
        [HttpPut]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize(Roles = "True")]
        public HttpResponseMessage RejectUser([FromBody]ApproverAPIID approverId, [FromUri]int userId)
        {
            try
            {
                UserAPIModel user = mapper.Map<UserAPIModel>(userBDC.GetUserById(userId));
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, false);
                }
                else
                {
                    if (user.CurrentStatus == APIModels.status.Pending)
                    {
                        user.CurrentStatus = APIModels.status.Declined;
                        user.ApprovedBy = approverId.ApproverId;
                        UserDTO userToUpdate = mapper.Map<UserDTO>(user);
                        if (userBDC.UpdateUser(userToUpdate))
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, true);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, false);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, false);
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return Request.CreateResponse(HttpStatusCode.Forbidden, e);
            }
        }

        /// <summary>
        /// Logout Function
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Logout()
        {
            var authentication = HttpContext.Current.GetOwinContext().Authentication;
            authentication.SignOut(DefaultAuthenticationTypes.ExternalBearer);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
