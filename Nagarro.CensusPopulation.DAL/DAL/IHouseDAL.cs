using Nagarro.CensusPopulation.SharedModel;

namespace Nagarro.CensusPopulation.DAL.DAL
{
    public interface IHouseDAL
    {
        /// <summary>
        /// check for the CHN in database
        /// </summary>
        /// <param name="chn">Census house number</param>
        /// <returns></returns>

        bool CheckCHN(long? chn);

        /// <summary>
        /// Create a new house entry
        /// </summary>
        /// <param name="houseToCreate">Object of the house entity to be created</param>
        /// <returns></returns>
        bool CreateHouseListing(HouseDTO houseToCreate);

        /// <summary>
        /// Retreives the house entity using CHN number
        /// </summary>
        /// <param name="chn">Census house number</param>
        /// <returns></returns>
        HouseDTO GetHouseByCHN(long chn);
    }
}