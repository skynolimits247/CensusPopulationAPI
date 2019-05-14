using Nagarro.CensusPopulation.DAL.Database;
using Nagarro.CensusPopulation.DAL.DataEntities;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
namespace Nagarro.CensusPopulation.DAL.Migrations
{
    public class Configuration : DbMigrationsConfiguration<Nagarro.CensusPopulation.DAL.Database.CensusContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CensusContext context)
        {
            var approver = new UserEntity { FirstMidName = "Admin", LastName = "DashBoard", Email = "myadmin@census.com", Password = "Admin@1234", IsApprover = true, ProfileImage = "https://firebasestorage.googleapis.com/v0/b/demoproject-1287a.appspot.com/o/admin.png?alt=media&token=6290a56e-84bb-4909-93f1-131bf9ef9f66", AdhaarNumber = "232323232323", CurrentStatus = 0 , ApprovedBy=0 };
            context.Users.AddOrUpdate(approver);
            context.SaveChanges();

            var house = new HouseEntity
            {
                ID = 1,
                BuildingAptNumber = "13AB",
                Line1 = "House No : 55",
                StreetName = "King's Street",
                City = "Delhi",
                State = "Delhi",
                HeadOfFamily = "Mr. AAA",
                OwnershipStatus = Ownership.Owner,
                NumberOfFloor = 3,
                NumberOfRooms = 12,
                CreatedBy = 1,
                CensusHouseNumber = 132011783695550347
            };
            context.Houses.AddOrUpdate(house);
            house = new HouseEntity
            {
                ID = 1,
                BuildingAptNumber = "15AB",
                Line1 = "Flat No : 55",
                StreetName = "King's Street",
                City = "Goa",
                State = "Goa",
                HeadOfFamily = "Mr. BBB",
                OwnershipStatus = Ownership.Owner,
                NumberOfFloor = 3,
                NumberOfRooms = 12,
                CreatedBy = 1,
                CensusHouseNumber = 0000000000000004
            };
            context.Houses.AddOrUpdate(house);
            house = new HouseEntity
            {
                ID = 1,
                BuildingAptNumber = "122AB",
                Line1 = "House No : 523",
                StreetName = "King's Street",
                City = "Ahemdabad",
                State = "Gujrat",
                HeadOfFamily = "Mr. XXX",
                OwnershipStatus = Ownership.Owner,
                NumberOfFloor = 3,
                NumberOfRooms = 12,
                CreatedBy = 1,
                CensusHouseNumber = 0000000000000002
            };
            context.Houses.AddOrUpdate(house);
            house = new HouseEntity
            {
                ID = 1,
                BuildingAptNumber = "122AB",
                Line1 = "House No : 120",
                StreetName = "King's Street",
                City = "Kanpur",
                State = "Uttar Pradesh",
                HeadOfFamily = "Mr. XYZ",
                OwnershipStatus = Ownership.Owner,
                NumberOfFloor = 3,
                NumberOfRooms = 12,
                CreatedBy = 1,
                CensusHouseNumber = 0000000000000003
            };
            context.Houses.AddOrUpdate(house);
            context.SaveChanges();


            //var person = new PersonEntity
            //{

            //    FullName = "Demo1",
            //    CensusHouseNumber = 0000000000000003,
            //    RelationshipWithOwner = Relationship.Self,
            //    Gender = Gender.Male,
            //    DateOfBirth = DateTime.Parse("30-04-2019 12:00:00 AM"),
            //    MaritalStatus = MaritalStatus.Unmarried,
            //    AgeAtMarriage = 0,
            //    Occupation = "Student",
            //    NatureOfWork = "studying",
            //    CreatedBy = 0
            //};
            //context.Persons.AddOrUpdate(person);
            //person = new PersonEntity
            //{

            //    FullName = "Demo1",
            //    CensusHouseNumber = 0000000000000001,
            //    RelationshipWithOwner = Relationship.Self,
            //    Gender = Gender.Male,
            //    DateOfBirth = DateTime.Parse("30-04-2019 12:00:00 AM"),
            //    MaritalStatus = MaritalStatus.Unmarried,
            //    AgeAtMarriage = 0,
            //    Occupation = "IT",
            //    NatureOfWork = "studying",
            //    CreatedBy = 0
            //};
            //context.Persons.AddOrUpdate(person);
            //person = new PersonEntity
            //{

            //    FullName = "Demo1",
            //    CensusHouseNumber = 132011783695550347,
            //    RelationshipWithOwner = Relationship.Self,
            //    Gender = Gender.Female,
            //    DateOfBirth = DateTime.Parse("30-04-2019 12:00:00 AM"),
            //    MaritalStatus = MaritalStatus.Unmarried,
            //    AgeAtMarriage = 0,
            //    Occupation = "Teacher",
            //    NatureOfWork = "Government Job",
            //    CreatedBy = 0
            //};

            //context.Persons.AddOrUpdate(person);
            //person = new PersonEntity
            //{

            //    FullName = "Demo1",
            //    CensusHouseNumber = 132011783695550347,
            //    RelationshipWithOwner = Relationship.Self,
            //    Gender = Gender.Female,
            //    DateOfBirth = DateTime.Parse("30-04-2019 12:00:00 AM"),
            //    MaritalStatus = MaritalStatus.Unmarried,
            //    AgeAtMarriage = 0,
            //    Occupation = "IT",
            //    NatureOfWork = "private job",
            //    CreatedBy = 0
            //};
            //context.Persons.AddOrUpdate(person);
            //person = new PersonEntity
            //{

            //    FullName = "Demo1",
            //    CensusHouseNumber = 0000000000000002,
            //    RelationshipWithOwner = Relationship.Self,
            //    Gender = Gender.Male,
            //    DateOfBirth = DateTime.Parse("30-04-2019 12:00:00 AM"),
            //    MaritalStatus = MaritalStatus.Unmarried,
            //    AgeAtMarriage = 0,
            //    Occupation = "IT",
            //    NatureOfWork = "private job",
            //    CreatedBy = 0
            //};
            //context.Persons.AddOrUpdate(person);
        }
    }
}
