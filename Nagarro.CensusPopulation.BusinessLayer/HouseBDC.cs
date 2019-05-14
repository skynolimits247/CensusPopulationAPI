using Nagarro.CensusPopulation.DAL.DAL;
using Nagarro.CensusPopulation.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nagarro.CensusPopulation.BusinessLayer
{
    public class HouseBDC : IHouseBDC
    {
        private IHouseDAL houseDAL;

        /// <summary>
        /// Constructor
        /// </summary>
        public HouseBDC(IHouseDAL _houseDAL)
        {
            houseDAL = _houseDAL;
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
                return houseDAL.CreateHouseListing(houseToCreate);
            }
            catch (DALException dalEx)
            {
                System.Console.WriteLine("Unable to find User...!" + dalEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }

        }

        /// <summary>
        /// check for the CHN in database
        /// </summary>
        /// <param name="chn">Census house number</param>
        /// <returns></returns>
        public bool CheckCHN(long? chn)
        {
            try
            {
                return houseDAL.CheckCHN(chn);
            }
            catch (DALException dalEx)
            {
                System.Console.WriteLine("Unable to find User...!" + dalEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
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
                return houseDAL.GetHouseByCHN(chn);
            }
            catch (DALException)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }

        }


    }
}
