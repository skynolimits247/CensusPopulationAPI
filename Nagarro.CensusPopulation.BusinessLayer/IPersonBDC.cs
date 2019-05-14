using Nagarro.CensusPopulation.SharedModel;
using System.Collections.Generic;

namespace Nagarro.CensusPopulation.BusinessLayer
{
    public interface IPersonBDC
    {
        /// <summary>
        /// function to create person entity
        /// </summary>
        /// <param name="personToCreate">Object of the person to be created</param>
        /// <returns>boolean</returns>
        bool CreatePerson(PersonDTO personToCreate);

        /// <summary>
        /// function to return list of all persons
        /// </summary>
        /// <returns>Persons List</returns>
        List<PersonDTO> GetAllPersons();
    }
}