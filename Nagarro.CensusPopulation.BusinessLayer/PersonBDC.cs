using Nagarro.CensusPopulation.DAL.DAL;
using Nagarro.CensusPopulation.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.CensusPopulation.BusinessLayer
{
    public class PersonBDC : IPersonBDC
    {
        private IPersonDAL personDAL;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_personDAL"></param>
        public PersonBDC(IPersonDAL _personDAL)
        {
            personDAL = _personDAL;
        }

        /// <summary>
        /// function to create person entity
        /// </summary>
        /// <param name="personToCreate">Object of the person to be created</param>
        /// <returns>boolean</returns>
        public bool CreatePerson(PersonDTO personToCreate)
        {
            try
            {
                return personDAL.CreatePerson(personToCreate);
            }
            catch (DALException dalEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        /// <summary>
        /// function to return gender count of person
        /// </summary>
        /// <returns>integer array</returns>
        public List<PersonDTO> GetAllPersons()
        {
            try
            {
                return personDAL.GetAllPersons();
            }
            catch (DALException dalEx)
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
