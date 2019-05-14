using Nagarro.CensusPopulation.DAL.DataEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nagarro.CensusPopulation.DAL.Database
{
    public interface IDbContext : IDisposable
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<PersonEntity> Persons { get; set; }
        DbSet<HouseEntity> Houses { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
