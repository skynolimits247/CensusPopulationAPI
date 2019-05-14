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

namespace Nagarro.CensusPopulation.Web.Controllers
{
    
    public class HouseController : ApiController
    {
        private IHouseBDC houseBDC;
        private IUserBDC userBDC;
        private IMapper mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_houseBDC"></param>
        /// <param name="_userBDC"></param>
        public HouseController(IHouseBDC _houseBDC, IUserBDC _userBDC)
        {
            houseBDC = _houseBDC;
            userBDC = _userBDC;
            MapperConfiguration cofig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HouseDTO, HouseAPIModel>();
            });
            mapper = cofig.CreateMapper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="house">Object of the house mode to be created</param>
        /// <param name="approverId">Id of the user who is performing the action</param>
        /// <returns></returns>
        [Route("api/home")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize(Roles = "False")]
        public HttpResponseMessage CreateHouseListing([FromBody]HouseAPIModel house, [FromUri]int approverId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserAPIModel user = mapper.Map<UserAPIModel>(userBDC.GetUserById(approverId));
                    Boolean isHouseExists = (houseBDC.CheckCHN(house.CensusHouseNumber));
                    if (user.CurrentStatus == APIModels.status.Approved )
                    {
                        if (isHouseExists == false)
                        {
                            house.CreatedBy = user.ID;
                            if (houseBDC.CreateHouseListing(mapper.Map<HouseDTO>(house)))
                            {
                                System.Diagnostics.Debug.WriteLine(house);
                                string chn = house.CensusHouseNumber.ToString();
                                return Request.CreateResponse(HttpStatusCode.OK, chn);
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
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, Resources.Forbidden);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, false);
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        /// <summary>
        /// API end point to do bulk entry of house entities
        /// </summary>
        /// <param name="houses">objects of the houses to be created</param>
        /// <param name="approverId">Id of the user who is performing the action</param>
        /// <returns></returns>
        [Route("api/home/sync")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize(Roles = "False")]
        public HttpResponseMessage CreateBulkHouseListing([FromBody]List<HouseAPIModel> houses, [FromUri]int approverId)
        {
            try
            {
                List<HouseAPIModel> houseNotAdded = new List<HouseAPIModel>(); 
                Boolean isHouseExists;
                UserAPIModel user = mapper.Map<UserAPIModel>(userBDC.GetUserById(approverId));
                if (user.CurrentStatus == APIModels.status.Approved)
                {
                    foreach (HouseAPIModel house in houses)
                    {
                        isHouseExists = (houseBDC.CheckCHN(house.CensusHouseNumber));
                        if (isHouseExists == false)
                        {
                            house.CreatedBy = user.ID;
                            if (houseBDC.CreateHouseListing(mapper.Map<HouseDTO>(house)))
                            {
                                System.Diagnostics.Debug.WriteLine(house);
                            }
                            else
                            {
                                houseNotAdded.Add(house);
                            }
                        }
                        else
                        {
                            houseNotAdded.Add(house);
                        }
                    }
                    if(houseNotAdded.Count > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, houseNotAdded);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, true);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resources.Forbidden);
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        /// <summary>
        /// API end point to check for the existence of Census Housing Number
        /// </summary>
        /// <param name="chn">Census house number</param>
        /// <returns></returns>
        [Route("api/home/chncheck")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [AllowAnonymous]
        public HttpResponseMessage CheckCHN([FromUri]long? chn)
        {
            try
            {
                if (chn != null)
                {
                    if (houseBDC.CheckCHN(chn))
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
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        /// <summary>
        /// API end point to check for the connecvitivty
        /// </summary>
        /// <returns></returns>
        [Route("api/home/check")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [AllowAnonymous]
        public HttpResponseMessage CheckConnectivity()
        {
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }


    }
}
