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
    public class PersonController : ApiController
    {
        private IPersonBDC personBDC;
        private IUserBDC userBDC;
        private IHouseBDC houseBDC;
        private IMapper mapper;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_personBDC"></param>
        /// <param name="_userBDC"></param>
        /// <param name="_houseBDC"></param>
        public PersonController(IPersonBDC _personBDC, IUserBDC _userBDC, IHouseBDC _houseBDC)
        {
            personBDC = _personBDC;
            userBDC = _userBDC;
            houseBDC = _houseBDC;
            MapperConfiguration cofig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PersonDTO, PersonAPIModel>();
            });
            mapper = cofig.CreateMapper();
        }

        /// <summary>
        /// POST: api/person API end point to create person
        /// </summary>
        /// <param name="person">Object of the person to be created</param>
        /// <param name="approverId">ApproverId of the user creating the person object</param>
        /// <returns>boolean</returns>
        [Route("api/person")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize(Roles = "False")]
        public HttpResponseMessage CreatePerson([FromBody]PersonAPIModel person, [FromUri]int approverId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserAPIModel user = mapper.Map<UserAPIModel>(userBDC.GetUserById(approverId));
                    HouseAPIModel house = mapper.Map<HouseAPIModel>(houseBDC.GetHouseByCHN(person.CensusHouseNumber));
                    if (user.CurrentStatus == APIModels.status.Approved)
                    {
                        if (house != null)
                        {
                            person.CreatedBy = approverId;
                            person.HouseId = house.ID;
                            if (personBDC.CreatePerson(mapper.Map<PersonDTO>(person)))
                            {
                                System.Diagnostics.Debug.WriteLine(person);
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
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, Resources.Forbidden);
                    }

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// API end point to create bullk entries of person
        /// </summary>
        /// <param name="person">Object of the person to be created</param>
        /// <param name="approverId">ApproverId of the user creating the person object</param>
        /// <returns></returns>
        [Route("api/person/sync")]
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize(Roles = "False")]
        public HttpResponseMessage CreateBulkPerson([FromBody]List<PersonAPIModel> persons, [FromUri]int approverId)
        {
            try
            {
                List<PersonAPIModel> personNotAdded = new List<PersonAPIModel>();
                UserAPIModel user = mapper.Map<UserAPIModel>(userBDC.GetUserById(approverId));
                HouseAPIModel house;
                if (user.CurrentStatus == APIModels.status.Approved)
                {
                    foreach (PersonAPIModel person in persons)
                    {
                        house = mapper.Map<HouseAPIModel>(houseBDC.GetHouseByCHN(person.CensusHouseNumber));
                        if (house != null)
                        {
                            person.CreatedBy = approverId;
                            person.HouseId = house.ID;
                            if (personBDC.CreatePerson(mapper.Map<PersonDTO>(person)))
                            {
                                System.Diagnostics.Debug.WriteLine(person);
                            }
                            else
                            {
                                personNotAdded.Remove(person);
                            }
                        }
                        else
                        {
                            personNotAdded.Add(person);
                        }
                    }
                    if (personNotAdded.Count > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, personNotAdded);
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
        /// API end point to return gender count
        /// </summary>
        /// <returns></returns>
        [Route("api/person/all")]
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize(Roles = "True")]
        public HttpResponseMessage GetAllPersons()
        {
            try
            {
                List<PersonAPIModel> personCount = mapper.Map<List<PersonAPIModel>>(personBDC.GetAllPersons());
                return Request.CreateResponse(HttpStatusCode.OK, personCount);
            }
            catch(Exception)
            {
                return Request.CreateResponse(HttpStatusCode.OK, false);
            }
        }
    }
}
