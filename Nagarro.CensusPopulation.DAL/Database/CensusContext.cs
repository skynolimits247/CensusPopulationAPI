using Nagarro.CensusPopulation.DAL.DataEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.CensusPopulation.DAL.Database
{
    public class CensusContext : DbContext, IDbContext
    {
        public CensusContext() : base("PopulationDB")
        {
            new Migrations.Configuration();
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<HouseEntity> Houses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<HouseEntity>().ToTable("House");
            modelBuilder.Entity<UserEntity>().ToTable("User");
            modelBuilder.Entity<PersonEntity>().ToTable("Person");
        }
    }
}
