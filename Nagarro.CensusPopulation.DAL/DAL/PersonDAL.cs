using Nagarro.CensusPopulation.DAL.Database;
using Nagarro.CensusPopulation.DAL.DataEntities;
using Nagarro.CensusPopulation.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity.Migrations;

namespace Nagarro.CensusPopulation.DAL.DAL
{
    public class PersonDAL : IPersonDAL
    {
        private IDbContext db;
        private IMapper mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        public PersonDAL(IDbContext _db)
        {
            db = _db;
            MapperConfiguration cofig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, UserEntity>().ReverseMap();
                cfg.CreateMap<PersonDTO, PersonEntity>().ReverseMap();
            });

            mapper = cofig.CreateMapper();
        }

        /// <summary>
        /// Create a House Listing
        /// </summary>
        /// <param name="houseToCreate">objetc of the hosue to be created</param>
        /// <returns></returns>
        public bool CreatePerson(PersonDTO personEntity)
        {
                try
                {
                    db.Persons.AddOrUpdate(mapper.Map<PersonEntity>(personEntity));
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DALException("Unable to Create House Listing" + ex.Message);
                }
        }

        /// <summary>
        ///To return gender count of person 
        /// </summary>
        /// <returns>Integer Array</returns>
        public List<PersonDTO> GetAllPersons()
        {
            try
            {
                //int[] genderCount = new int[2];
                List<PersonEntity> persons = db.Persons.ToList();
                //foreach (PersonEntity person in persons)
                //{
                //    if (person.Gender == DataEntities.Gender.Female)
                //    {
                //        genderCount[0]++;
                //    }
                //    if (person.Gender == DataEntities.Gender.Male)
                //    {
                //        genderCount[1]++;
                //    }
                //}
                return mapper.Map<List<PersonDTO>>(persons);
                //System.Diagnostics.Debug.WriteLine(obj);
                //return genderCount;
 
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw new DALException("Unable to Create House Listing" + ex.Message);
            }
        }

    }
}
