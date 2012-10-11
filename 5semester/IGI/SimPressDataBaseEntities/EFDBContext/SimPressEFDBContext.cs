using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace SimPressDataBaseEntities.SimPress.EFDBContext
{
    public class SimPressEFDBContext:DbContext
    {
        public SimPressEFDBContext(string connectionString)
            : base(connectionString)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
