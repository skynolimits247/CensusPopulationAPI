using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.CensusPopulation.DAL.Database
{
    public class CensusInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CensusContext>
    {
        protected override void Seed(CensusContext context)
        {
        }
    }
}
