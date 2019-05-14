using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.CensusPopulation.SharedModel
{
    public class LoginDTO
    {
        /// <summary>
        /// User Email Id
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User Account Login Password
        /// </summary>
        public string Password { get; set; }
    }
}
