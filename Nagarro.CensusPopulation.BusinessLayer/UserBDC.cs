using Nagarro.CensusPopulation.DAL.DAL;
using Nagarro.CensusPopulation.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.CensusPopulation.BusinessLayer
{
    public class UserBDC : IUserBDC
    {

        private IUserDAL userDAL;
        public UserBDC(IUserDAL _userDAL)
        {
            userDAL = _userDAL;
        }

        /// <summary>
        /// Create a new User
        /// </summary>
        /// <param name="userToCreate">User Instance of the user to be created</param>
        /// <returns></returns>
        public bool CreateUser(UserDTO userToCreate)
        {
            try
            {
                if (userDAL.FindUserByEmail(userToCreate, null) && userDAL.FindUserByUIDAI(userToCreate, null))
                {
                    return false;
                }
                return (userDAL.CreateUser(userToCreate));
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
        /// Find uer by Email
        /// </summary>
        /// <param name="email">Email of the user to find</param>
        /// <returns></returns>
        public bool FindUserByEmail(string email)
        {
            if (userDAL.FindUserByEmail(null, email))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Find user by Aadhar number
        /// </summary>
        /// <param name="uidai">Aadhar number of the user to be found</param>
        /// <returns></returns>
        public bool FindUserByUIDAI(string uidai)
        {
            if (userDAL.FindUserByUIDAI(null, uidai))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Authenticates logged in user
        /// </summary>
        /// <param name="userToLogin">Object instance of the user to be authenticated(Email and Password)</param>
        /// <returns></returns>
        public UserDTO AuthenticateUser(string email, string password)
        {
            UserDTO user = null;
            try
            {
                user = userDAL.AuthenticateUser(email, password);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (DALException dalEx)
            {
                throw new BusinessException("something Went wron at DAL layer...!");
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        /// <summary>
        /// functin to get list of all users
        /// </summary>
        /// <returns>list of users</returns>
        public List<UserDTO> GetAllUser()
        {
            try
            {
                return userDAL.GetAllUser();
            }
            catch (DALException dalEx)
            {
                throw new BusinessException("something Went wrong at DAL layer...!");
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        /// <summary>
        /// function to get user by ID
        /// </summary>
        /// <param name="id">Id of the user to be searched for</param>
        /// <returns>User object</returns>
        public UserDTO GetUserById(int id)
        {
            try
            {
                return userDAL.GetUserById(id);
            }
            catch (DALException dalEx)
            {
                throw new BusinessException("something Went wrong at DAL layer...!");
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        /// <summary>
        /// function to update user
        /// </summary>
        /// <param name="userToUpdate">Object of the user to be updated</param>
        /// <returns>boolean</returns>
        public bool UpdateUser(UserDTO userToUpdate)
        {
            try
            {
                return userDAL.UpdateUser(userToUpdate);
            }
            catch (DALException dalEx)
            {
                throw new BusinessException("something Went wrong at DAL layer...!");
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

    }
}
