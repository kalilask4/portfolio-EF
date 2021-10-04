using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio_EF.Models
{
    class EntityContext : DbContext
    {
        public EntityContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new DataBaseInitializer());
        }

        public DbSet<Coin> Coins { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
