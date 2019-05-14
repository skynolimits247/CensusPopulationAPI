using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nagarro.CensusPopulation.Web.APIModels
{
    public class LoginAPIModel
    {
        /// <summary>
        /// User Email Id
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string UserName { get; set; }

        /// <summary>
        /// User Account Login Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}