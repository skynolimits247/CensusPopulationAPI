using System.Collections.Generic;
using Nagarro.CensusPopulation.SharedModel;

namespace Nagarro.CensusPopulation.BusinessLayer
{
    public interface IUserBDC
    {
        /// <summary>
        /// Authenticates logged in user
        /// </summary>
        /// <param name="userToLogin">Object instance of the user to be authenticated(Email and Password)</param>
        /// <returns></returns>
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
        bool FindUserByEmail(string email);

        /// <summary>
        /// Find user by Aadhar number
        /// </summary>
        /// <param name="uidai">Aadhar number of the user to be found</param>
        /// <returns></returns>
        bool FindUserByUIDAI(string uidai);

        /// <summary>
        /// functin to get list of all users
        /// </summary>
        /// <returns>list of users</returns>
        List<UserDTO> GetAllUser();

        /// <summary>
        /// function to get user by ID
        /// </summary>
        /// <param name="id">Id of the user to be searched for</param>
        /// <returns>User object</returns>
        UserDTO GetUserById(int id);

        /// <summary>
        /// function to update user
        /// </summary>
        /// <param name="userToUpdate">Object of the user to be updated</param>
        /// <returns>boolean</returns>
        bool UpdateUser(UserDTO userToUpdate);
    }
}