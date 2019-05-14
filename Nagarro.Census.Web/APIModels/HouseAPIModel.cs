using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nagarro.CensusPopulation.Web.APIModels
{
    public enum Ownership
    {
        Owner = 1, Rented
    }
    public class HouseAPIModel
    {
        /// <summary>
        /// Id of the House
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// House Id
        /// </summary>
        public long CensusHouseNumber { get; set; } = DateTime.Now.ToFileTime();

        /// <summary>
        /// Building Appartmemnt Number
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string BuildingAptNumber { get; set; }

        /// <summary>
        /// Address Line1
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50, ErrorMessage = "Line1 address can not be more than 50 characters long!")]
        public string Line1 { get; set; }

        /// <summary>
        /// Street Name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50, ErrorMessage = "Street Name can not be more than 50 characters long!")]
        public string StreetName { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string City { get; set; }

        /// <summary>
        /// State
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string State { get; set; }

        /// <summary>
        /// Name of Head EnaOf Family
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string HeadOfFamily { get; set; }

        /// <summary>
        /// Ownership
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public Ownership OwnershipStatus { get; set; }

        /// <summary>
        /// Number Of Floor
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public int NumberOfFloor { get; set; }

        /// <summary>
        /// Number Of Rooms
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public int NumberOfRooms { get; set; }

        /// <summary>
        /// Date for the Record Creation
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Id of the Person who created the record
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Date for the Record Updation
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Id of the Person who updated the record
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// List of Book reading Event created by user
        /// </summary>
        public virtual ICollection<PersonAPIModel> Person { get; set; }

        ///// <summary>
        ///// List of Book reading Event created by user
        ///// </summary>

    }
}