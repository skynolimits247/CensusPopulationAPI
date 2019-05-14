using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.CensusPopulation.DAL.DataEntities
{
    public enum Relationship
    {
        Self = 1, Spouse, Son, Daughter, Sibling, Grandson, Granddaughter
    }
    public enum Gender
    {
        Male = 1, Female
    }

    public enum MaritalStatus
    {
        Married = 1, Unmarried
    }

    public class PersonEntity
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        [Key]
        public long ID { get; set; }

        /// <summary>
        /// Person Full name
        /// </summary>
        [Required]
        [MinLength(5, ErrorMessage = "Full name can not be lesser than five characters.")]
        public string FullName { get; set; }

        //HouseListing Number of the house where person lives
        public long CensusHouseNumber { get; set; }

        /// <summary>
        /// RelationsShip status with the owner of the house
        /// </summary>
        public Relationship RelationshipWithOwner { get; set; }

        /// <summary>
        /// Gender of the person
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Date for the Birth of Person
        /// </summary>
        ///
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Marital status of the person
        /// </summary>
        public MaritalStatus MaritalStatus
        { get; set; }

        /// <summary>
        /// Age of person at marriage
        /// </summary>
        public int? AgeAtMarriage { get; set; } = 0;

        /// <summary>
        /// Occupation of person
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// Nature of work of person
        /// </summary>
        public string NatureOfWork { get; set; }

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
        public int? UpdatedBy { get; set; } = null;

        /// <summary>
        /// HouseListing Instance of the house where person lives
        /// </summary>
        public int  HouseId { get; set; }


        public virtual UserEntity User { get; set; }
    }
}
