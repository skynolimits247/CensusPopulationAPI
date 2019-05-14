using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nagarro.CensusPopulation.Web.APIModels
{
    public class ApproverAPIID
    {
        [Required]
        public int ApproverId { get; set; }
    }
}