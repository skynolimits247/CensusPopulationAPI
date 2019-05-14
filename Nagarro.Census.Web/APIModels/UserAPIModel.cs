using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nagarro.CensusPopulation.Web.APIModels
{
    public enum status
    {
        Pending = 1, Approved, Declined
    }
    public class UserAPIModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// User First and Mid name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(5, ErrorMessage = "First name can not be lesser than five characters.")]
        public string FirstMidName { get; set; }

        /// <summary>
        /// User Last name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(5, ErrorMessage = "Last name can not be lesser than five characters.")]
        public string LastName { get; set; }

        /// <summary>
        /// User Account Login Password
        /// </summary>
        [Required]
        [RegularExpression("^((?=.*?[A-Za-z0-9])(?=.*?[#?!@$%^&*-])).{5,}$", ErrorMessage = "Password must be atleast 5 char long and contains a special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// User Role Details
        /// </summary>
        /// 
        public bool IsApprover { get; set; } = false;

        /// <summary>
        /// User ProfileImage
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string ProfileImage { get; set; }

        /// <summary>
        /// User Adhar Number
        /// </summary>
        [Required]
        [MaxLength(12)]
        [MinLength(12)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "UDAI must be numeric")]
        public string AdhaarNumber { get; set; }

        /// <summary>
        /// User account status
        /// </summary>
        public status CurrentStatus { get; set; } = status.Pending;

        /// <summary>
        /// User Email Id
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Id of the person who approved the User
        /// </summary>
        public int ApprovedBy { get; set; }
    }
}