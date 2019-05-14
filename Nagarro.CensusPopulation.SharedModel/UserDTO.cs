using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.CensusPopulation.SharedModel
{
    public enum status
    {
        Pending = 1, Approved, Declined
    }
    public class UserDTO
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// User First and Mid name
        /// </summary>
        public string FirstMidName { get; set; }

        /// <summary>
        /// User Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User Account Login Password
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// User Role Details
        /// </summary>
        /// 
        public bool IsApprover { get; set; } = false;

        /// <summary>
        /// User ProfileImage
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// User Adhar Number
        /// </summary>
        public long AdhaarNumber { get; set; }

        /// <summary>
        /// User account status
        /// </summary>
        public status CurrentStatus { get; set; }

        /// <summary>
        /// User Email Id
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Id of the person who approved the User
        /// </summary>
        public int ApprovedBy { get; set; }
    }
}
