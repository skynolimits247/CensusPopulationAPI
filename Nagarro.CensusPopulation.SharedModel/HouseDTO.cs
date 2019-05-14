using Nagarro.CensusPopulation.SharedModel;
using System;
using System.Collections.Generic;


namespace Nagarro.CensusPopulation.SharedModel
{
    public enum Ownership
    {
        Owner = 1, Rented
    }
    public class HouseDTO
    {

        /// <summary>
        /// Id of the House
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// House Id
        /// </summary>

        public long CensusHouseNumber { get; set; } = DateTime.Now.ToFileTime();

        /// <summary>
        /// Building Appartmemnt Number
        /// </summary>
        public string BuildingAptNumber { get; set; }

        /// <summary>
        /// Address Line1
        /// </summary>
        public string Line1 { get; set; }

        /// <summary>
        /// Street Name
        /// </summary>
        public string StreetName { get; set; }

        /// <summary>
        /// Name of the City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Name of the State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Name of Head Of Family
        /// </summary>
        public string HeadOfFamily { get; set; }

        /// <summary>
        /// Ownership
        /// </summary>
        public Ownership OwnershipStatus { get; set; }

        /// <summary>
        /// Number Of Floor
        /// </summary>
        public int NumberOfFloor { get; set; }

        /// <summary>
        /// Number Of Rooms
        /// </summary>
        public int NumberOfRooms { get; set; }

        /// <summary>
        /// Date for the Record Creation
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Id of the Person who created the record
        /// </summary>
        ///
        public int CreatedBy { get; set; }

        /// <summary>
        /// Date for the Record Updation
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Id of the Person who updated the record
        /// </summary>
        public int? UpdatedBy { get; set; } = null;

        /// <summary>
        /// List of Book reading Event created by user
        /// </summary>
        public virtual ICollection<PersonDTO> Person { get; set; }

        ///// <summary>
        ///// List of Book reading Event created by user
        ///// </summary>
        //public virtual UserEntity User { get; set; }


    }
}
