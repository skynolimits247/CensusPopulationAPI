using System.Collections.Generic;
using Nagarro.CensusPopulation.SharedModel;

namespace Nagarro.CensusPopulation.DAL.DAL
{
    public interface IUserDAL
    {

        UserDTO AuthenticateUser(string email, string password);

        /// <summary>
        /// Creates a new user in Database
        /// </summary>
        /// <param name="userToCreate">new user DTO Model</param>
        /// <returns>bool</returns>
        bool CreateUser(UserDTO userToCreate);

        /// <summary>
        /// Searches for a particular user in DB using email
        /// </summary>
        /// <param name="email">Email for searching a user</param>
        /// <returns>bool</returns>
        bool FindUserByEmail(UserDTO userToCreate, string email);

        /// <summary>
        /// Searches for a particular user in DB using AdhaarNumber
        /// </summary>
        /// <param name="email">Email for searching a user</param>
        /// <returns>bool</returns>
        bool FindUserByUIDAI(UserDTO userToCreate, string uidai);

        /// <summary>
        /// returns list of all users
        /// </summary>
        /// <returns>List of users</returns>
        List<UserDTO> GetAllUser();

        /// <summary>
        /// Function to return the user object according to its ID
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>User object</returns>
        UserDTO GetUserById(int id);

        /// <summary>
        /// function to update user role
        /// </summary>
        /// <param name="userToUpdate">object of the user to be update</param>
        bool UpdateUser(UserDTO userToUpdate);
    }
}