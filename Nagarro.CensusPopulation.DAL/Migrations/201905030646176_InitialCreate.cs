namespace Nagarro.CensusPopulation.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.House",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CensusHouseNumber = c.Long(nullable: false),
                        BuildingAptNumber = c.String(nullable: false),
                        Line1 = c.String(nullable: false, maxLength: 50),
                        StreetName = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        HeadOfFamily = c.String(nullable: false),
                        OwnershipStatus = c.Int(nullable: false),
                        NumberOfFloor = c.Int(nullable: false),
                        NumberOfRooms = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        CensusHouseNumber = c.Long(nullable: false),
                        RelationshipWithOwner = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        MaritalStatus = c.Int(nullable: false),
                        AgeAtMarriage = c.Int(),
                        Occupation = c.String(),
                        NatureOfWork = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        HouseId = c.Int(nullable: false),
                        User_ID = c.Int(),
                        HouseEntity_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.User_ID)
                .ForeignKey("dbo.House", t => t.HouseEntity_ID)
                .Index(t => t.User_ID)
                .Index(t => t.HouseEntity_ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstMidName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsApprover = c.Boolean(nullable: false),
                        ProfileImage = c.String(nullable: false),
                        AdhaarNumber = c.String(nullable: false, maxLength: 12),
                        CurrentStatus = c.Int(nullable: false),
                        ApprovedBy = c.Int(),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Person", "HouseEntity_ID", "dbo.House");
            DropForeignKey("dbo.Person", "User_ID", "dbo.User");
            DropIndex("dbo.Person", new[] { "HouseEntity_ID" });
            DropIndex("dbo.Person", new[] { "User_ID" });
            DropTable("dbo.User");
            DropTable("dbo.Person");
            DropTable("dbo.House");
        }
    }
}
