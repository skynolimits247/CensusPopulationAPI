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
    public class UserDAL : IUserDAL
    {
        private IDbContext db;
        //private CensusContext db { get; set; }
        private IMapper mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserDAL(IDbContext _db)
        {
            db = _db;
            MapperConfiguration cofig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, UserEntity>().ReverseMap();
            });

            mapper = cofig.CreateMapper();
        }

        /// <summary>
        /// Creates a new user in Database
        /// </summary>
        /// <param name="userToCreate">new user DTO Model</param>
        /// <returns>bool</returns>
        public bool CreateUser(UserDTO userToCreate)
        {
                try
                {
                    db.Users.Add(mapper.Map<UserEntity>(userToCreate));
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new DALException("Unable to SignUp User" + ex.Message);
                }
        }


        /// <summary>
        /// Searches for a particular user in DB using email
        /// </summary>
        /// <param name="email">Email for searching a user</param>
        /// <returns>bool</returns>
        public bool FindUserByEmail(UserDTO userToCreate, string email)
        {
                try
                {
                    if (email == null)
                    {
                        UserEntity user = mapper.Map<UserEntity>(userToCreate);
                        email = user.Email;
                    }
                    var emailMatch = db.Users.FirstOrDefault(s => s.Email.Equals(email));
                    if (emailMatch == null)
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
                    throw new DALException("Unable to Find User" + ex.Message);
                }
        }

        /// <summary>
        /// Searches for a particular user in DB using AdhaarNumber
        /// </summary>
        /// <param name="email">Email for searching a user</param>
        /// <returns>bool</returns>
        public bool FindUserByUIDAI(UserDTO userToCreate, string uidai)
        {
                try
                {
                    if (uidai == null)
                    {
                        UserEntity user = mapper.Map<UserEntity>(userToCreate);
                        uidai = user.AdhaarNumber;
                    }
                    var aadharMatch = db.Users.FirstOrDefault(s => s.AdhaarNumber.Equals(uidai));
                    if (aadharMatch == null)
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
                    throw new DALException("Unable to Find User" + ex.Message);
                }
        }


        /// <summary>
        /// Authenticcate User
        /// </summary>
        /// <param name="email">email of the user to login</param>
        /// <param name="password"> password of the user to login</param>
        /// <returns></returns>
        public UserDTO AuthenticateUser(string email, string password)
        {
            using (CensusContext db = new CensusContext())
            {
                try
                {
                    var user = db.Users.FirstOrDefault(s => s.Email.Equals(email) && s.Password.Equals(password));
                    return mapper.Map<UserDTO>(user);
                }
                catch (Exception ex)
                {
                    throw new Exception("User does not exists..!" + ex.Message);
                }
            }
        }

        /// <summary>
        /// returns list of all users
        /// </summary>
        /// <returns>List of users</returns>
        public List<UserDTO> GetAllUser()
        {
                try
                {
                    List<UserEntity> user = db.Users.ToList();
                    return mapper.Map<List<UserDTO>>(user);
                }
                catch (Exception ex)
                {
                    throw new Exception("User does not exists..!" + ex.Message);
                }
        }

        /// <summary>
        /// Function to return the user object according to its ID
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>User object</returns>
        public UserDTO GetUserById(int id)
        {
                try
                {
                    UserEntity user = db.Users.FirstOrDefault(s => s.ID.Equals(id));
                    return mapper.Map<UserDTO>(user);
                }
                catch (Exception ex)
                {
                    throw new Exception("User does not exists..!" + ex.Message);
                }
        }

        /// <summary>
        /// function to update user role
        /// </summary>
        /// <param name="userToUpdate">object of the user to be update</param>
        /// <returns></returns>
        public bool UpdateUser(UserDTO userToUpdate)
        {
            using (CensusContext db = new CensusContext())
            {
                try
                {
                    UserEntity currentUser = mapper.Map<UserEntity>(userToUpdate);
                    if (currentUser != null)
                    {
                        db.Users.AddOrUpdate(currentUser);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw new DALException("Unable to SignUp User" + ex.Message);
                }
            }
        }

        ~UserDAL()
        {
            db.Dispose();
        }
    }
}
