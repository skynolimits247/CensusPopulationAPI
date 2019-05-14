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
    public class HouseDAL : IHouseDAL
    {

        private IMapper mapper;
        private IDbContext db;

        /// <summary>
        /// Constructor
        /// </summary>
        public HouseDAL(IDbContext _db)
        {
            db = _db;
            MapperConfiguration cofig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HouseDTO, HouseEntity>().ReverseMap();
            });

            mapper = cofig.CreateMapper();
        }

        /// <summary>
        /// Create a new house entry
        /// </summary>
        /// <param name="houseToCreate">Object of the house entity to be created</param>
        /// <returns>boolean</returns>
        public bool CreateHouseListing(HouseDTO houseToCreate)
        {

                try
                {
                    db.Houses.Add(mapper.Map<HouseEntity>(houseToCreate));
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DALException("Unable to Create House Listing" + ex.Message);
                }
        }

        /// <summary>
        /// check for the CHN in database
        /// </summary>
        /// <param name="chn">Census house number</param>
        /// <returns></returns>
        public bool CheckCHN(long? chn)
        {
            using (CensusContext db = new CensusContext())
            {
                try
                {
                    HouseEntity house =  db.Houses.FirstOrDefault(s => s.CensusHouseNumber == chn);
                    if (house == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw new DALException("Unable to Create House Listing" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Retreives the house entity using CHN number
        /// </summary>
        /// <param name="chn">Census house number</param>
        /// <returns></returns>
        public HouseDTO GetHouseByCHN(long chn)
        {
                try
                {
                    HouseEntity house = db.Houses.FirstOrDefault(s => s.CensusHouseNumber == chn);
                    if (house == null)
                    {
                        return null;
                    }
                    else
                    {
                        return mapper.Map<HouseDTO>(house);
                    }
                }
                catch (Exception ex)
                {
                    throw new DALException("Unable to Create House Listing" + ex.Message);
                }
        }

    }
}
