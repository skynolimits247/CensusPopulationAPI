using Nagarro.CensusPopulation.SharedModel;
using System.Collections.Generic;

namespace Nagarro.CensusPopulation.DAL.DAL
{
    public interface IPersonDAL
    {
        /// <summary>
        /// Create a House Listing
        /// </summary>
        /// <param name="houseToCreate">objetc of the hosue to be created</param>
        /// <returns></returns>
        bool CreatePerson(PersonDTO personEntity);

        /// <summary>
        ///To return list of all persons
        /// </summary>
        /// <returns>List of all persons</returns>
        List<PersonDTO> GetAllPersons();
    }
}